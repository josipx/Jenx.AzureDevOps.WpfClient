using Prism.Mvvm;

namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public class ConfirmationDialogViewModel : BindableBase, IConfirmationDialogViewModel
    {
        public ConfirmationDialogViewModel()
        {
            ConfirmButtonText = "OK";
            CancelButtonText = "Cancel";
            IsCancelButtonVisible = true;
        }

        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
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

        private bool _isCancelButtonVisible;

        public bool IsCancelButtonVisible
        {
            get => _isCancelButtonVisible;
            set
            {
                _isCancelButtonVisible = value;
                RaisePropertyChanged();
            }
        }
    }
}