using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public interface IResult
    {
    }

    public interface IResult<T> : IResult
    {
        bool Success { get; set; }
        T Data { get; set; }
        string RawResponse { get; set; }
        HttpStatusCode StatusCode { get; set; }
        string StatusDescription { get; set; }
        string RequestId { get; set; }
        string StripeVersion { get; set; }
        StripeError Error { get; set; }
    }

    public class Result<T> : IResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string RawResponse { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string RequestId { get; set; }
        public string StripeVersion { get; set; }
        public StripeError Error { get; set; }
    }
}
