using Jenx.AzureDevOps.WpfClient.ViewModels;

namespace Jenx.AzureDevOps.WpfClient.Views
{
    public interface IExceptionDialogView
    {
        IExceptionDialogViewModel DataContext { get; set; }
    }
}