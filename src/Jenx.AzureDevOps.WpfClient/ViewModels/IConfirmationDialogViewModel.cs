namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public interface IConfirmationDialogViewModel
    {
        string Message { get; set; }

        string ConfirmButtonText { get; set; }

        string CancelButtonText { get; set; }

        bool IsCancelButtonVisible { get; set; }
    }
}