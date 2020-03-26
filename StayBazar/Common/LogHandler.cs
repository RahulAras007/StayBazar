using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
namespace StayBazar.Common
{
    public class LogHandler
    {

        private static readonly ILog log;
        static LogHandler()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger("Staybazar_Logs");

        }
        public static ILog Logger
        {
            get { return log; }
        }
        public static void LogError(Exception ex)
        {
            log.Error(ex.Message + " Stack trace: " + ex.StackTrace);
        }
        public static void HandleError(Exception ex)
        {
            LogError(ex);
        }
        public static void AddLog(string message)
        {
            log.Error(message);
        }
    }
}