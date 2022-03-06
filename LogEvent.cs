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
        public delegate void LogEventHandler(LogEvent m, EventArgs e, ErrorInfo errorInfo);

        public LogEvent()
        {
        }

        public void Log(ErrorInfo errorInfo)
        {
            if (FireEvent != null)
            {
                FireEvent(this, e, errorInfo);
            }
        }

        //public void Start()
        //{
        //    while (true)
        //    {
        //        System.Threading.Thread.Sleep(3000);
        //        if (FireEvent != null)
        //        {
        //            FireEvent(this, e);
        //        }
        //    }
        //}
    }
}
