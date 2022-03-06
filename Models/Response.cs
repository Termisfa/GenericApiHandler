
namespace CryptoAlertsBot.ApiHandler.Models
{
    public class Response
    {
        public bool Success { get; set; }

        public ErrorInfo ErrorInfo { get; set; }

        public string Result { get; set; }

        public static Response SuccesfulResponse(string result)
        {
            return new Response()
            {
                Success = true,
                Result = result
            };
        }

        public static Response UnsuccesfulResponse(ErrorInfo errorInfo)
        {
            return new Response()
            {
                Success = false,
                ErrorInfo = errorInfo
            };
        }

        public static Response UnsuccesfulResponseFromException(Exception exc)
        {
            ErrorInfo errorInfo = ErrorInfo.ErrorFromException(exc);
            return new Response()
            {
                Success = false,
                ErrorInfo = errorInfo
            };
        }
    }
}
