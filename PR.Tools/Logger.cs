using log4net;
using System;

namespace PR.Tools
{
    public static class Logger
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Logger));

        public static void ErrorFatal(string message, Exception ex)
        {
            _log.Error(message, ex);
        }

        public static void Info(string message, Exception ex = null)
        {
            if (ex != null)
                _log.Info(message, ex);
            else
                _log.Info(message);
        }
    }
}