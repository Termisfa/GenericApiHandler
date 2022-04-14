using CryptoAlertsBot.ApiHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApiHandler
{
    public class LogEvent
    {
        public event LogEventHandler FireEvent;
        public delegate void LogEventHandler(LogEvent m, EventArgs e, Response response = default, Exception exc = default);
        
        public LogEvent()
        {
        }

        public void Log(Response response = default, Exception exc = default)
        {
            try
            {
                FireEvent?.Invoke(this, new(), response, exc);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
