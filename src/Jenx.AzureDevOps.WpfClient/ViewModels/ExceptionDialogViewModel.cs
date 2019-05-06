using Prism.Mvvm;

namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public class ExceptionDialogViewModel : BindableBase, IExceptionDialogViewModel
    {
        public ExceptionDialogViewModel()
        {
            ConfirmButtonText = "Yes";
            ConfirmButtonText = "No";
        }

        private string _exceptionAdditional;

        public string ExceptionAdditional
        {
            get => _exceptionAdditional;
            set
            {
                _exceptionAdditional = value;
                RaisePropertyChanged();
            }
        }

        private string _exception;

        public string Exception
        {
            get => _exception;
            set
            {
                _exception = value;
                RaisePropertyChanged();
            }
        }

        private string _confirmButtonText;

        public string ConfirmButtonText
        {
            get => _confirmButtonText;
            set
            {
                _confirmButtonText = value;
                RaisePropertyChanged();
            }
        }

        private string _cancelButtonText;

        public string CancelButtonText
        {
            get => _cancelButtonText;
            set
            {
                _cancelButtonText = value;
                RaisePropertyChanged();
            }
        }
    }
}