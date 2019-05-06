using System.Threading.Tasks;

namespace Jenx.AzureDevOps.WpfClient.Services
{
    public interface IDialogService
    {
        Task<bool> ShowDialog(string message);

        Task<bool> ShowExceptionDialog(string message);

        Task<bool> ShowDialog(string message, string confirmationButtonText, string cancelationButtonText, bool isCancelButtonVisible = true);

        void CloseAllDialogs();
    }
}