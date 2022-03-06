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
            if (FireEvent != null)
            {
                FireEvent(this, e, response);
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
