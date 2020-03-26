using System;
using System.Diagnostics;


namespace Mailer.Common
{
    public class SystemLogger
    {
        private string _sSource;
        private string _sLog;
       // private string _sEvent;

        public SystemLogger()
        {
            _sSource = "Staybazar Mailer";
            _sLog = "Application";
        }
        public SystemLogger(string source)
        {
            _sSource = source;
            _sLog = "Application";
            if (!EventLog.SourceExists(_sSource))
                EventLog.CreateEventSource(_sSource, _sLog);
        }

        public void LogError(string message)
        {
            EventLog.WriteEntry(_sSource,message,EventLogEntryType.Error);
        }

        public void LogInfo(string message)
        {
            EventLog.WriteEntry(_sSource, message, EventLogEntryType.Information);
        }

    }
}
