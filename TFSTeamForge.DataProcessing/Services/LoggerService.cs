using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing.Services
{
    public class LoggerService : ILoggerService
    {
        private static string LogName = "TFSServiceHookLogger";
        private ILog _logger;

        public LoggerService()
        {
            _logger = LogManager.GetLogger(LogName);
        }

        public void Log(LogType type, string message)
        {
            switch(type)
            {
                case LogType.Debug:
                    Debug(message);
                    break;
                case LogType.Info:
                    Info(message);
                    break;
                case LogType.Warn:
                    Warn(message);
                    break;
                case LogType.Error:
                    Error(message);
                    break;
                case LogType.Fatal:
                    Fatal(message);
                    break;
            }
        }

        public void Log(LogType type, string message, Exception exception)
        {
            switch (type)
            {
                case LogType.Debug:
                    Debug(message, exception);
                    break;
                case LogType.Info:
                    Info(message, exception);
                    break;
                case LogType.Warn:
                    Warn(message, exception);
                    break;
                case LogType.Error:
                    Error(message, exception);
                    break;
                case LogType.Fatal:
                    Fatal(message, exception);
                    break;
            }
        }

        private void Debug(string message)
        {
            _logger.Debug(message);
        }

        private void Debug(string message, Exception exception)
        {
            _logger.Debug(message, exception);
        }


        private void Info(string message)
        {
            _logger.Info(message);
        }

        private void Info(string message, Exception exception)
        {
            _logger.Info(message, exception);
        }


        private void Warn(string message)
        {
            _logger.Warn(message);
        }

        private void Warn(string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }

        private void Error(string message)
        {
            _logger.Error(message);
        }

        private void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        private void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        private void Fatal(string message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }
    }
}