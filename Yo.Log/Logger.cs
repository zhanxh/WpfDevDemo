using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Yo.Log
{
    //1.交易日志与MT系统日志分离
    //2.配置开关: OFF、BASIC、FATAL、ERROR、WARN、INFO、DEBUG、ALL
    //3.系统日志:        FATAL、ERROR、WARN、INFO、DEBUG, 系统日志只落地不显示
    //4.业务日志: BASIC、       ERROR、WARN、INFO、DEBUG, 业务日志落地且显示

    public static class Logger
    {

        private static ILog _LogBusiness;// = LogManager.GetLogger("logbusiness");
        private static ILog _LogSystem;// = LogManager.GetLogger("logsystem");
        private static StringBuilder strBuffer;
        private static bool _hasUser;
        private static string _LastLogText { get; set; }
        /// <summary>
        /// 最新业务日志
        /// </summary>
        public static string LastLogText
        {
            get
            {
                return _LastLogText;
            }
            private set
            {
                strBuffer.Append(value);
                strBuffer.Append("\r\n");
                _LastLogText = value;
            }
        }
        /// <summary>
        /// 所有业务日志
        /// </summary>
        public static string LogBuffer
        {
            get
            {
                return strBuffer.ToString();
            }
        }
        //日志通知事件
        public static event Action<LogMsg> LogMsgNotify;
        private static XmlElement _element;
        static Logger()
        {
            strBuffer = new StringBuilder();
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("Config\\log4net.config"));
            _LogBusiness = LogManager.GetLogger("logbusiness");
            _LogSystem = LogManager.GetLogger("logsystem");
            LogManager.GetRepository().LevelMap.Add(CustomLevel.BasiLevel);
        }
        
        public static string GetCurTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff,");
        }
        public static string Encrypt(string sourceString)
        {
            byte[] btKey = Encoding.Default.GetBytes("@1d^20?Q");
            byte[] btIV = Encoding.Default.GetBytes("yo2019");
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = null;
                try
                {
                    inData = Encoding.Default.GetBytes(sourceString);
                }
                catch
                {
                    return string.Empty;
                }
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    return string.Empty;
                }
            }

        }

        #region 系统日志接口
        /// <summary>
        /// 系统致命错误错误
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysFatal(string msg, string location="")
        {
            if (location.Count() <= 0)
            {
                location = GetCallLocation();
            }

            var info = string.Format("{{{0}}}{{{1}}}", location, msg);
            _LogSystem.Fatal(info);
        }
        /// <summary>
        /// 系统致命错误日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="ex">异常</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysFatal(Exception ex, string location = "")
        {
            var info = string.Format("{{{0}}}{{}}", location);
            _LogSystem.Fatal(info, ex);
        }
        /// <summary>
        /// 系统错误日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysError(string msg, string location = "")
        {
            if (location.Count() <= 0)
            {
                location = GetCallLocation();
            }

            var info = string.Format("{{{0}}}{{{1}}}", location, msg);
            _LogSystem.Error(info);
        }
        /// <summary>
        /// 系统错误日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="ex">异常</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysError(Exception ex, string location = "")
        {
            var info = string.Format("{{{0}}}{{}}", location);
            _LogSystem.Error(info, ex);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysWarn(string msg, string location = "")
        {
            if (location.Count() <= 0)
            {
                location = GetCallLocation();
            }

            var info = string.Format("{{{0}}}{{{1}}}", location, msg);
            _LogSystem.Warn(info);
        }
        /// <summary>
        /// 系统警告日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="ex">异常</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysWarn(Exception ex, string location = "")
        {
            var info = string.Format("{{{0}}}{{}}", location);
            _LogSystem.Warn(info, ex);
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysInfo(string msg, string location="")
        {
            if( location.Count() <= 0)
            {
                location = GetCallLocation();
            }            

            var info = string.Format("{{{0}}}{{{1}}}", location, msg);
            _LogSystem.Info(info);
        }

        private static string GetCallLocation()
        {
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod();
            string className = method.ReflectedType.FullName;

            string classmethodName = string.Format("{0} {1}", className, method.Name);

            return classmethodName;
        }
        /// <summary>
        /// 系统信息日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="ex">异常</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysInfo(Exception ex, string location = "")
        {
            var info = string.Format("{{{0}}}{{}}", location);
            _LogSystem.Info(info, ex);
        }
        /// <summary>
        /// 系统调试日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysDebug(string msg, string location = "")
        {
            if (location.Count() <= 0)
            {
                location = GetCallLocation();
            }

            var info = string.Format("{{{0}}}{{{1}}}", location, msg);
            _LogSystem.Debug(info);
        }
        /// <summary>
        /// 系统调试日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="ex">异常</param>
        /// <param name="location">位置，可空</param>
        public static void LogSysDebug(Exception ex, string location = "")
        {
            var info = string.Format("{{{0}}}{{{1}}}", location);
            _LogSystem.Debug(info, ex);
        }

        #endregion

        #region 业务日志接口
        /// <summary>
        /// 业务基础日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogBusiBasic(string msg, string location = "")
        {
            LastLogText = string.Format("{0}{1}", GetCurTime(), msg);
            LogMsg lm = new LogMsg() { LogType = LogType.LOGBUSINESS, LogLevel = LogLevel.LOGBASIC, Msg = LastLogText };
            AddLogMsg(lm);
            var strInfo = string.Format("{{{0}}}{{{1}}}", location, Encrypt(LastLogText));
            _LogBusiness.LogBasic(strInfo);
        }
        /// <summary>
        /// 业务错误日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogBusiError(string msg, string location = "")
        {
            LastLogText = string.Format("{0}{1}", GetCurTime(), msg);
            LogMsg lm = new LogMsg() { LogType = LogType.LOGBUSINESS, LogLevel = LogLevel.LOGERROR, Msg = LastLogText };
            AddLogMsg(lm);
            var strInfo = string.Format("{{{0}}}{{{1}}}", location, Encrypt(LastLogText));
            _LogBusiness.Error(strInfo);
        }
        /// <summary>
        /// 业务警告日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogBusiWarn(string msg, string location = "")
        {
            LastLogText = string.Format("{0}{1}", GetCurTime(), msg);
            LogMsg lm = new LogMsg() { LogType = LogType.LOGBUSINESS, LogLevel = LogLevel.LOGWARN, Msg = LastLogText };
            AddLogMsg(lm);
            var strInfo = string.Format("{{{0}}}{{{1}}}", location, Encrypt(LastLogText));
            _LogBusiness.Warn(strInfo);
        }
        /// <summary>
        /// 业务信息日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogBusiInfo(string msg, string location = "")
        {
            LastLogText = string.Format("{0}{1}", GetCurTime(), msg);
            LogMsg lm = new LogMsg() { LogType = LogType.LOGBUSINESS, LogLevel = LogLevel.LOGINFO, Msg = LastLogText };
            AddLogMsg(lm);
            var strInfo = string.Format("{{{0}}}{{{1}}}", location, Encrypt(LastLogText));
            _LogBusiness.Info(strInfo);
        }
        /// <summary>
        /// 业务调试日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="location">位置，可空</param>
        public static void LogBusiDebug(string msg, string location = "")
        {
            LastLogText = string.Format("{0}{1}", GetCurTime(), msg);
            var strInfo = string.Format("{{{0}}}{{{1}}}", location, Encrypt(LastLogText));
            _LogBusiness.Debug(strInfo);
        }
        /// <summary>
        /// 业务信息集合，用于消息窗口提示
        /// </summary>
        private static List<LogMsg> LogMsgs = new List<LogMsg>();
        /// <summary>
        /// 记录业务日志并通知订阅者
        /// </summary>
        /// <param name="info"></param>
        private static void AddLogMsg(LogMsg info)
        {
            LogMsgs.Add(info);
            LogMsgNotify?.Invoke(info);
        }

        #endregion

    }
}
