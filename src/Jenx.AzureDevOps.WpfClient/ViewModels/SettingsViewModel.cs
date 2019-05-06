using Jenx.AzureDevOps.Client;
using Jenx.AzureDevOps.WpfClient.Logger;
using Jenx.AzureDevOps.WpfClient.Services;
using Jenx.AzureDevOps.WpfClient.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Windows.Input;

namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public class SettingsViewModel : BaseViewModel, INavigationAware
    {
        private readonly ISettings _settings;
        private readonly IAzureDevOpsSettings _azureDevOpsSettings;

        public SettingsViewModel(
            IRegionManager regionManager, ISettings settings, IAzureDevOpsSettings azureDevOpsSettings,
            IDialogService dialogService, IEventAggregator eventAggregator, IMessageService messageService,
            IApplicationLogger logger)
            : base(regionManager, dialogService, eventAggregator, messageService)
        {
            _settings = settings;
            _azureDevOpsSettings = azureDevOpsSettings;
        }

        #region Commands

        public ICommand SaveSettingsCommand => new DelegateCommand(SaveSettings);

        #endregion Commands

        #region Properties

        private string _organizationName;

        public string OrganizationName
        {
            get => _organizationName;
            set
            {
                _organizationName = value;
                RaisePropertyChanged();
            }
        }

        private string _token;

        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                RaisePropertyChanged();
            }
        }

        #endregion Properties

        #region Privates

        private void SaveSettings()
        {
            try
            {
                IsBusy = true;
                _settings.OrganizationName = OrganizationName;
                _settings.PersonalAccessToken = Token;
                _settings.Store();

                _azureDevOpsSettings.OrganizationName = _settings.OrganizationName;
                _azureDevOpsSettings.PersonalAccessToken = _settings.PersonalAccessToken;
                ShowMessage("Settings saved");
            }
            catch
            {
                ShowMessage("Failed saving settings");
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion Privates

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                IsBusy = true;
                OrganizationName = _settings.OrganizationName;
                Token = _settings.PersonalAccessToken;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}