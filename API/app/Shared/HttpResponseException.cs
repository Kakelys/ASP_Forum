using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace app.Shared
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get;private set; }

        public HttpResponseException(HttpStatusCode statusCode, string message) 
            : base(message ?? "Something went wrong")
        {
            StatusCode = statusCode;
        }

        public HttpResponseException(
            HttpStatusCode statusCode, 
            string message, 
            Exception innerException
            ) : base(message ?? "Something went wrong", innerException) 
        {
            StatusCode = statusCode;
        }
    }
}