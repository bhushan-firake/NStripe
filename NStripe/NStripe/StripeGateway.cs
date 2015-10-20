using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeGateway
    {
        private const string BaseUrl = "https://api.stripe.com/v1";

        private string apiKey;
        private string publishableKey;
        public ICredentials Credentials { get; set; }
        private string UserAgent { get; set; }

        public StripeGateway(string apiKey, string publishableKey = null)
        {
            this.apiKey = apiKey;
            this.publishableKey = publishableKey;
            Credentials = new NetworkCredential(apiKey, "");
            UserAgent = "NStripe";
        }

        public T PostRequest<T>(string url, string stringToPost, string idempotencyKey = null)
        {
            WebRequest request = PrepareRequest(url, HttpMethod.Post, stringToPost, idempotencyKey);
            return ExecuteInternal<T>(request);
        }

        public T GetRequest<T>(string url)
        {
            WebRequest request = PrepareRequest(url, HttpMethod.Get);
            return ExecuteInternal<T>(request);
        }

        public T PutRequest<T>(string url, string stringToPut)
        {
            WebRequest request = PrepareRequest(url, HttpMethod.Put, stringToPut);
            return ExecuteInternal<T>(request);
        }

        public T DeleteRequest<T>(string url, string stringToDelete)
        {
            WebRequest request = PrepareRequest(url, HttpMethod.Delete, stringToDelete);
            return ExecuteInternal<T>(request);
        }

        public T ExecuteInternal<T>(WebRequest request)
        {
            string responseAsString = string.Empty;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseAsString = HandleResponse(response);
            }
            catch (WebException ex)
            {
                string errorBody = string.Empty;
                var httpRes = ex.Response as HttpWebResponse;
                using (var stream = httpRes.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    errorBody = reader.ReadToEnd();
                }
                var errorStatus = httpRes.StatusCode;

                if (errorStatus >= HttpStatusCode.BadRequest && errorStatus < HttpStatusCode.InternalServerError)
                {
                    var result = errorBody.FromJson<StripeErrors>();
                    throw new StripeException(result.Error)
                    {
                        StatusCode = errorStatus
                    };
                }
                throw;
            }
            return responseAsString.FromJson<T>();
        }

        public HttpWebRequest PrepareRequest(string url, string httpMethod, string body = null, string idempotencyKey = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseUrl + url);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            request.Accept = MediaTypes.Json;
            request.Credentials = Credentials;
            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put)
                request.ContentType = MediaTypes.FormUrlEncoded;

            if (!string.IsNullOrWhiteSpace(idempotencyKey))
                request.Headers["Idempotency-Key"] = idempotencyKey;

            request.Method = httpMethod;

            if ((httpMethod.Equals(HttpMethod.Post) || httpMethod.Equals(HttpMethod.Put)) && body == null)
            {
                throw new ArgumentNullException("body");
            }

            if (body != null)
            {
                ASCIIEncoding encode = new ASCIIEncoding();
                byte[] requestData = encode.GetBytes(body);
                request.ContentLength = requestData.Length;
                try
                {
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(requestData, 0, requestData.Length);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("body" + e);
                }
            }

            return request;
        }

        public string HandleResponse(HttpWebResponse response)
        {
            try
            {
                Stream responseStream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(responseStream))
                    return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Error reading response stream" + e);
            }
        }

        public T Get<T>(IResponse<T> request)
        {
            return Execute(request, HttpMethod.Get);
        }

        private T Execute<T>(IResponse<T> request, string httpMethod, string idempotencyKey = null)
        {
            var webRequest = PrepareRequest(request.ToUrl(), httpMethod, request.ToString(), idempotencyKey);
            return ExecuteInternal<T>(webRequest);
        }
    }
}
