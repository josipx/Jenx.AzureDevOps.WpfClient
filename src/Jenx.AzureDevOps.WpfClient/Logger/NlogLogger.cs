using Prism.Logging;
using System;

namespace Jenx.AzureDevOps.WpfClient.Logger
{
    public class NlogLogger : ILoggerFacade, IApplicationLogger
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    Logger.Debug(message);
                    break;

                case Category.Warn:
                    Logger.Warn(message);
                    break;

                case Category.Exception:
                    Logger.Debug(message);
                    break;

                case Category.Info:
                    Logger.Info(message);
                    break;

                default:
                    Logger.Info(message);
                    break;
            }
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);
        }

        public void Exception(Exception exception)
        {
            Logger.Error(exception);
        }
    }
}