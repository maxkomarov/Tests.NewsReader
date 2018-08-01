using log4net;
using log4net.Config;
using System;

namespace Tests.NewsReader.ServiceModel
{
    public static class LogWriter
    {
        private static ILog iLog { get; } = LogManager.GetLogger("LOGWRITER");

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
            
        }

        /// <summary>
        /// Wrap default L4N logging methods
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="reportToConsole"></param>
        /// <param name="e"></param>
        public static void Log(LogLevelEnum logLevel, object message, bool reportToConsole = false, Exception e = null)
        {
            if (logLevel == LogLevelEnum.Error)
                iLog.Error(message, e);
              
            else if (logLevel == LogLevelEnum.Debug)
                iLog.Debug(message, e);
            else
                iLog.Info(message, e);

            if (reportToConsole)
            {
                Console.WriteLine($"{message}");
                if (e != null)
                    Console.WriteLine($"{e.Message}");
            }
        }

        public enum LogLevelEnum
        {
            Info,
            Debug,
            Error
        }
    }
}
