using System;

namespace SuiteAccount.Logging.Abstracts
{
    public interface ILogService
    {
        void LoggerTrace(string message);
        void ErrorTrace(string procedureName, Exception ex);
        void WarnTrace(string message);
    }
}
