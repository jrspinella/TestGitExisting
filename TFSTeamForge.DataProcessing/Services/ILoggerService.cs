using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Services
{
    public enum LogType
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public interface ILoggerService
    {
        void Log(LogType type, string message);
        void Log(LogType type, string message, Exception exception);
    }
}
