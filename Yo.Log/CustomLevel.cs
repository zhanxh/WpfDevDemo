using log4net;
using System;

namespace Yo.Log
{
    public static class CustomLevel
    {
        public static readonly log4net.Core.Level BasiLevel = new log4net.Core.Level(9999999, "BASIC");

        public static void LogBasic(this ILog log, string message)
        {
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                BasiLevel, message, null);
        }
        public static void LogBasic(this ILog log, string message, Exception ex)
        {
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                BasiLevel, message, ex);
        }

        public static void LogBasicFormat(this ILog log, string message, params object[] args)
        {
            string formattedMessage = string.Format(message, args);
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                BasiLevel, formattedMessage, null);
        }
    }
}
