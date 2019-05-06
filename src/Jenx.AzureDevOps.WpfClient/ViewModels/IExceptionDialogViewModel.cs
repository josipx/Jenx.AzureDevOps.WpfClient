namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public interface IExceptionDialogViewModel
    {
        string ExceptionAdditional { get; set; }

        string Exception { get; set; }

        string ConfirmButtonText { get; set; }

        string CancelButtonText { get; set; }
    }
}