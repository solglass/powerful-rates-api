using System;
using System.Net;

namespace PowerfulRatesAPI.CustomExceptions
{
    public class ServiceUnavailableException : Exception
    {

        public int StatusCode { get; private set; } 
        public string ErrorMessage { get; private set; }

        public ServiceUnavailableException()
        {
            StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            ErrorMessage = "Service unavailable";
        }
    }
}
