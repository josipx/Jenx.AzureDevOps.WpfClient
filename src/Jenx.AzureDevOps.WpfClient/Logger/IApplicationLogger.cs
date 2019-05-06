using System;

namespace Jenx.AzureDevOps.WpfClient.Logger
{
    public interface IApplicationLogger
    {
        void Info(string message);

        void Debug(string message);

        void Warn(string message);

        void Exception(Exception message);
    }
}