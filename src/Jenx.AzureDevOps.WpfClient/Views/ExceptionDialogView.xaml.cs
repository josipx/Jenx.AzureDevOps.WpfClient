using Jenx.AzureDevOps.WpfClient.ViewModels;

namespace Jenx.AzureDevOps.WpfClient.Views
{
    public partial class ExceptionDialogView : IExceptionDialogView
    {
        public ExceptionDialogView(IExceptionDialogViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public new IExceptionDialogViewModel DataContext
        {
            get => (IExceptionDialogViewModel)base.DataContext;
            set => base.DataContext = value;
        }
    }
}