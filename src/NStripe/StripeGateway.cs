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

        public IResult<T> ExecuteInternal<T>(WebRequest request)
        {
            Result<T> result = new Result<T>();

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    result.RawResponse = reader.ReadToEnd();
                }
                result.Success = true;
                result.StatusCode = response.StatusCode;
                result.StatusDescription = ToErrorDescription((int)response.StatusCode);
                result.Data = result.RawResponse.FromJson<T>();
                result.RequestId = response.Headers[StripeHeaders.RequestId];
                result.StripeVersion = response.Headers[StripeHeaders.StripeVersion];
            }
            catch (WebException ex)
            {
                //result.Success = false;

                var httpRes = ex.Response as HttpWebResponse;
                using (var stream = httpRes.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    result.RawResponse = reader.ReadToEnd();
                }
                result.StatusCode = httpRes.StatusCode;
                result.StatusDescription = ToErrorDescription((int)result.StatusCode);

                if (result.StatusCode >= HttpStatusCode.BadRequest && result.StatusCode < HttpStatusCode.InternalServerError)
                {
                    result.Error = result.RawResponse.FromJson<StripeErrors>().Error;
                    //throw new StripeException(result.Error)
                    //{
                    //    StatusCode = result.StatusCode
                    //};
                }
                //throw;
            }
            return result;
        }

        public HttpWebRequest PrepareRequest(string url, string httpMethod, string body = null, string idempotencyKey = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseUrl + url);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            request.Headers.Add(StripeHeaders.StripeVersion, NStripeConfig.StripeVersion);
            request.Accept = MediaTypes.Json;
            request.Credentials = Credentials;
            request.Method = httpMethod;

            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put)
                request.ContentType = MediaTypes.FormUrlEncoded;

            if (!string.IsNullOrEmpty(idempotencyKey))
                request.Headers[StripeHeaders.IdempotencyKey] = idempotencyKey;

            if ((httpMethod.Equals(HttpMethod.Post) || httpMethod.Equals(HttpMethod.Put)) && body == null)
                throw new ArgumentNullException("body");

            if (body != null)
            {
                ASCIIEncoding encode = new ASCIIEncoding();
                byte[] requestData = encode.GetBytes(body);
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

        public IResult<T> Get<T>(IResponse<T> request)
        {
            return Execute(request, HttpMethod.Get, null);
        }

        public IResult<T> Post<T>(IResponse<T> request, string idempotencyKey = null)
        {
            return Execute(request, HttpMethod.Post, request.ToString(), idempotencyKey);
        }

        private IResult<T> Execute<T>(IResponse<T> request, string httpMethod, string body = null, string idempotencyKey = null)
        {
            var webRequest = PrepareRequest(request.ToUrl(), httpMethod, body, idempotencyKey);
            return ExecuteInternal<T>(webRequest);
        }

        public string ToErrorDescription(int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return "Everything worked as expected.";

                case 400:
                    return "Often missing a required parameter.";

                case 401:
                    return "No valid API key provided.";

                case 402:
                    return "Parameters were valid but request failed.";

                case 404:
                    return "The requested item doesn't exist.";

                case 500:
                case 502:
                case 503:
                case 504:
                    return "Something went wrong on Stripe's end.";

                default:
                    return "Unclassified error";
            }
        }
    }
}
