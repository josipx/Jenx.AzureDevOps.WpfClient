using Jenx.AzureDevOps.Client;
using Jenx.AzureDevOps.Client.Models;
using Jenx.AzureDevOps.WpfClient.Logger;
using Jenx.AzureDevOps.WpfClient.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public class BuildsViewModel : BaseViewModel, INavigationAware
    {
        private readonly IAzureDevOpsService _azureDevOpsService;
        private readonly IApplicationLogger _logger;

        private string _projectName;

        public BuildsViewModel(
            IRegionManager regionManager, IAzureDevOpsService azureDevOpsService,
            IDialogService dialogService, IEventAggregator eventAggregator, IMessageService messageService,
            IApplicationLogger logger)
            : base(regionManager, dialogService, eventAggregator, messageService)
        {
            _azureDevOpsService = azureDevOpsService;
            _logger = logger;
        }

        #region Commands

        public ICommand TriggerNewBuildCommand => new DelegateCommand<Value>(TriggerNewBuild);

        #endregion Commands

        #region Properties

        private ObservableCollection<Build> _builds;

        public ObservableCollection<Build> Builds
        {
            get { return _builds; }
            set
            {
                _builds = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Value> _buildDefinitions;

        public ObservableCollection<Value> BuildDefinitions
        {
            get { return _buildDefinitions; }
            set
            {
                _buildDefinitions = value;
                RaisePropertyChanged();
            }
        }

        #endregion Properties

        #region Privates

        private async Task BindInitialDataAsync()
        {
            Builds = await GetBuildsAsync();
            BuildDefinitions = await GetBuildDefinitionsAsync();
        }

        private async Task<ObservableCollection<Build>> GetBuildsAsync()
        {
            try
            {
                var serviceResponse = await _azureDevOpsService.GetProjectBuildsAsync(_projectName);
                return new ObservableCollection<Build>(serviceResponse.Builds.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.Exception(ex);
            }

            return new ObservableCollection<Build>();
        }

        private async Task TriggerNewBuildAsync(int definitionId)
        {
            try
            {
                await _azureDevOpsService.TriggerProjectBuildAsync(_projectName, definitionId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex);
            }
        }

        private async Task<ObservableCollection<Value>> GetBuildDefinitionsAsync()
        {
            try
            {
                var serviceResponse = await _azureDevOpsService.GetProjectBuildDefinitionsAsync(_projectName);
                return new ObservableCollection<Value>(serviceResponse.BuildDefinitions.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.Exception(ex);
            }

            return new ObservableCollection<Value>();
        }

        private async void TriggerNewBuild(Value buildDefinition)
        {
            try
            {
                IsBusy = true;
                await TriggerNewBuildAsync(buildDefinition.Id);
                //await Task.Delay(5000);
                await GetBuildsAsync(); // refresh
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion Privates

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                IsBusy = true;
                var projectName = navigationContext.Parameters.FirstOrDefault(x => x.Key.ToLower() == "projectname").Value;

                if (projectName != null)
                {
                    _projectName = (string)projectName;
                    await BindInitialDataAsync();
                }
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