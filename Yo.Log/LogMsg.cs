using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yo.Log
{
    public class LogMsg
    {
        public LogType LogType { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Msg { get; set; }
    }
}
