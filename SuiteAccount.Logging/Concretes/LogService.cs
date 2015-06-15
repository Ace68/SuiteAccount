using System;
using Common.Logging;

using SuiteAccount.Logging.Abstracts;

namespace SuiteAccount.Logging.Concretes
{
    public class LogService : ILogService
    {
        private readonly ILog _log = LogManager.GetLogger("SuiteAccount");

        public void LoggerTrace(string message)
        {
            _log.Trace(message);
        }

        public void ErrorTrace(string procedureName, Exception ex)
        {
            if (ex.InnerException != null)
                this.ErrorTrace(procedureName, ex.InnerException);

            _log.Error(ex.Message);
        }

        public void WarnTrace(string message)
        {
            _log.Warn(message);
        }
    }
}
