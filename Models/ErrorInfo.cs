using System.Diagnostics;
using System.Reflection;

namespace CryptoAlertsBot.ApiHandler.Models
{
    public class ErrorInfo
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public static ErrorInfo ErrorFromException(Exception exc)
        {
            MethodBase? method = new StackTrace().GetFrame(3)?.GetMethod();
            return new ErrorInfo()
            {
                Message = exc.Message,
                StackTrace = exc.StackTrace
            };
        }
    }
}
