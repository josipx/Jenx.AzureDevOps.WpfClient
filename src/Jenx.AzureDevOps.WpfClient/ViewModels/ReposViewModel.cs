using Jenx.AzureDevOps.Client;
using Jenx.AzureDevOps.Client.Models;
using Jenx.AzureDevOps.WpfClient.Logger;
using Jenx.AzureDevOps.WpfClient.Services;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public class ReposViewModel : BaseViewModel, INavigationAware
    {
        private readonly IAzureDevOpsService _azureDevOpsService;
        private readonly IApplicationLogger _logger;

        public ReposViewModel(
            IRegionManager regionManager, IAzureDevOpsService azureDevOpsService,
            IDialogService dialogService, IEventAggregator eventAggregator,
            IMessageService messageService, IApplicationLogger logger)
            : base(regionManager, dialogService, eventAggregator, messageService)
        {
            _azureDevOpsService = azureDevOpsService;
            _logger = logger;
        }

        private async Task BindInitialDataAsync(string projectName)
        {
            Repos = await GetReposAsync(projectName);
        }

        private async Task<ObservableCollection<Repos>> GetReposAsync(string projectName)
        {
            try
            {
                var serviceResponse = await _azureDevOpsService.GetReposAsync(projectName);
                return new ObservableCollection<Repos>(serviceResponse.Repos.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.Exception(ex);
            }

            return new ObservableCollection<Repos>();
        }

        #region Commands

        //public ICommand NavigateToSoftwareInstallationCommand => new DelegateCommand<object>(NavigateToSoftwareInstallation, o => SelectedChannel != null);

        #endregion Commands

        #region Properties

        private ObservableCollection<Repos> _repos;

        public ObservableCollection<Repos> Repos
        {
            get => _repos;
            set
            {
                _repos = value;
                RaisePropertyChanged();
            }
        }

        #endregion Properties

        #region Privates

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                IsBusy = true;
                var projectName = navigationContext.Parameters.FirstOrDefault(x => x.Key.ToLower() == "projectname").Value;

                if (projectName != null)
                    await BindInitialDataAsync((string)projectName);
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

        #endregion Privates
    }
}