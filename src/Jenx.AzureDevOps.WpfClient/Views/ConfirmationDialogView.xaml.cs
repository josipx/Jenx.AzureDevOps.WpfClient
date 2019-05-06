using Jenx.AzureDevOps.WpfClient.ViewModels;

namespace Jenx.AzureDevOps.WpfClient.Views
{
    public partial class ConfirmationDialogView : IConfirmationDialogView
    {
        public ConfirmationDialogView(IConfirmationDialogViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public new IConfirmationDialogViewModel DataContext
        {
            get => (IConfirmationDialogViewModel)base.DataContext;
            set => base.DataContext = value;
        }
    }
}