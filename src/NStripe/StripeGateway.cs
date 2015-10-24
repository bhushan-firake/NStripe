using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace NStripe
{
    public class StripeGateway
    {
        private const string BaseUrl = "https://api.stripe.com/v1";

        private string apiKey;
        private string publishableKey;

        private ICredentials Credentials { get; set; }
        private string UserAgent { get; set; }

        public StripeGateway(string apiKey = null, string publishableKey = null)
        {
            this.apiKey = apiKey;
            this.publishableKey = publishableKey;

            if (string.IsNullOrEmpty(apiKey))
            {
                string configuredApiKey = NStripeConfig.ApiKey;
                if (string.IsNullOrEmpty(configuredApiKey))
                    throw new ConfigurationErrorsException("Stripe ApiKey not configured.");
                this.apiKey = configuredApiKey;
            }
            else
            {
                NStripeConfig.ApiKey = this.apiKey;
            }

            Credentials = new NetworkCredential(this.apiKey, "");
            UserAgent = "NStripe";
        }

        public IResult<T> Get<T>(IResponse<T> request)
        {
            return Execute(request, HttpMethod.Get, null);
        }

        public IResult<T> Delete<T>(IResponse<T> request)
        {
            return Execute(request, HttpMethod.Delete, null);
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

        private IResult<T> ExecuteInternal<T>(WebRequest request)
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
                result.StatusDescription = ((int)response.StatusCode).ToStripeDescription();
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
                result.StatusDescription = ((int)result.StatusCode).ToStripeDescription();

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

        private HttpWebRequest PrepareRequest(string url, string httpMethod, string body = null, string idempotencyKey = null)
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
    }
}
