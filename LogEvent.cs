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
        public EventArgs e = null;
        public delegate void LogEventHandler(LogEvent m, EventArgs e, Response response);

        public LogEvent()
        {
        }

        public void Log(Response response)
        {
            try
            {
                if (FireEvent != null)
                {
                    FireEvent(this, e, response);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
