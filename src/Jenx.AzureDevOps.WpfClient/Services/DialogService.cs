using Jenx.AzureDevOps.WpfClient.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;

namespace Jenx.AzureDevOps.WpfClient.Services
{
    public class DialogService : IDialogService
    {
        private readonly IExceptionDialogView _exceptionDialogView;
        private readonly IConfirmationDialogView _confirmationDialogView;

        public DialogService(IExceptionDialogView exceptionDialogView, IConfirmationDialogView confirmationDialogView)
        {
            _exceptionDialogView = exceptionDialogView;
            _confirmationDialogView = confirmationDialogView;
        }

        public async Task<bool> ShowDialog(string message)
        {
            return await ShowDialog(message, "Yes", null, false);
        }

        public async Task<bool> ShowExceptionDialog(string errorMessage)
        {
            _exceptionDialogView.DataContext.Exception = "Application error";
            _exceptionDialogView.DataContext.ExceptionAdditional = errorMessage;
            return (bool)await DialogHost.Show(_exceptionDialogView, "ExceptionDialogHost");
        }

        public async Task<bool> ShowDialog(string message, string confirmationButtonText, string cancelationButtonText, bool isCancelButtonVisible)
        {
            CloseAllDialogs();
            try
            {
                _confirmationDialogView.DataContext.Message = message;
                _confirmationDialogView.DataContext.ConfirmButtonText = confirmationButtonText;
                _confirmationDialogView.DataContext.CancelButtonText = cancelationButtonText;
                _confirmationDialogView.DataContext.IsCancelButtonVisible = isCancelButtonVisible;
                return (bool)await DialogHost.Show(_confirmationDialogView, "RootDialog");
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void CloseAllDialogs()
        {
            DialogHost.CloseDialogCommand.Execute(false, null);
        }
    }
}