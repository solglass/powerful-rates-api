namespace PowerfulRatesAPI
{
    public static class Constants
    {
        public const string GLOBAL_ERROR_MESSAGE_SUBJECT = "An error occured";
        public const string ERROR_503_MESSAGE_SUBJECT = "Error request failed with status code 500";
        public const string ERROR_503_MESSAGE_BODY = "Service unavailable.An error occurred while getting currency rates.Please check your source settings and try again";
        public const string PARSING_ERROR_MESSAGE_SUBJECT = "An error occured while parsing data";
        public const string PARSING_ERROR_MESSAGE_BODY = "Cannot parse data in currencyRatesGetterService, please check your data source!";
    }
}
