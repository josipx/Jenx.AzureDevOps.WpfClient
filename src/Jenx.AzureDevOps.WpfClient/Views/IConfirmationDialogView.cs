using Jenx.AzureDevOps.WpfClient.ViewModels;

namespace Jenx.AzureDevOps.WpfClient.Views
{
    public interface IConfirmationDialogView
    {
        IConfirmationDialogViewModel DataContext { get; set; }
    }
}