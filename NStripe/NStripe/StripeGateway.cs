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

        public T PostRequest<T>(string url, string stringToPost)
        {
            WebRequest request = PrepareRequest(url, "POST", stringToPost);
            return ExecuteRequest<T>(request);
        }

        public T PostRequest<T>(string url, string stringToPost, string idempotencyKey)
        {
            WebRequest request = PrepareRequest(url, "POST", stringToPost, idempotencyKey);
            return ExecuteRequest<T>(request);
        }

        public T GetRequest<T>(string url)
        {
            WebRequest request = PrepareRequest(url, "GET");
            return ExecuteRequest<T>(request);
        }

        public T PutRequest<T>(string url, string stringToPut)
        {
            WebRequest request = PrepareRequest(url, "PUT", stringToPut);
            return ExecuteRequest<T>(request);
        }

        public T DeleteRequest<T>(string url, string stringToDelete)
        {
            WebRequest request = PrepareRequest(url, "DELETE", stringToDelete);
            return ExecuteRequest<T>(request);
        }

        public T ExecuteRequest<T>(WebRequest request)
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

        public HttpWebRequest PrepareRequest(string url, string httpMethod, string stringToSend = null, string idempotencyKey = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseUrl + url);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            request.Accept = MediaTypes.Json;
            request.Credentials = Credentials;
            if (httpMethod == "POST" || httpMethod == "PUT")
                request.ContentType = MediaTypes.FormUrlEncoded;

            if (!string.IsNullOrWhiteSpace(idempotencyKey))
                request.Headers["Idempotency-Key"] = idempotencyKey;

            request.Method = httpMethod;

            if (httpMethod.Equals("POST") && stringToSend == null)
            {
                throw new ArgumentNullException("body");
            }

            if (stringToSend != null)
            {
                ASCIIEncoding encode = new ASCIIEncoding();
                byte[] requestData = encode.GetBytes(stringToSend);
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
    }
}
