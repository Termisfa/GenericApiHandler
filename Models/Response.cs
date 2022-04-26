
namespace CryptoAlertsBot.ApiHandler.Models
{
    public class Response
    {
        public bool Success { get; set; }

        public ErrorInfo ErrorInfo { get; set; }

        public string Result { get; set; }

        public string QueryUsed { get; set; }

        public string UriUsed { get; set; }

        public static Response SuccesfulResponse(string result, string query = default)
        {
            return new Response()
            {
                Success = true,
                Result = result,
                QueryUsed = query
            };
        }

        public static Response UnsuccesfulResponse(ErrorInfo errorInfo, string query = default)
        {
            return new Response()
            {
                Success = false,
                ErrorInfo = errorInfo,
                QueryUsed = query
            };
        }

        public static Response UnsuccesfulResponseFromException(Exception exc, string query = default)
        {
            ErrorInfo errorInfo = ErrorInfo.ErrorFromException(exc);
            return new Response()
            {
                Success = false,
                ErrorInfo = errorInfo,
                QueryUsed = query
            };
        }
    }
}
