using System;
using System.Net;

namespace PowerfulRatesAPI.CustomExceptions
{
    public class ParsingException : Exception
    {
        public int StatusCode { get; private set; } 
        public string ErrorMessage { get; private set; }

        public ParsingException()
        {
            StatusCode = (int)HttpStatusCode.Conflict;
            ErrorMessage = "Cannot parse data, please check data source!"; // get errors from modelState and combine them into ErrorMessage
        }
    }
}
