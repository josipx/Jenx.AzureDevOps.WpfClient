using Jenx.AzureDevOps.Client;
using Jenx.AzureDevOps.Client.Models;
using Jenx.AzureDevOps.WpfClient.Logger;
using Jenx.AzureDevOps.WpfClient.Services;
using Jenx.AzureDevOps.WpfClient.Settings;
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
    public class ProjectsViewModel : BaseViewModel
    {
        private readonly IAzureDevOpsService _azureDevOpsService;
        private readonly IApplicationLogger _logger;

        private readonly ISettings _settings;

        public ProjectsViewModel(IRegionManager regionManager, IAzureDevOpsService azureDevOpsService,
            IDialogService dialogService, IEventAggregator eventAggregator, IMessageService messageService,
            ISettings settings, IApplicationLogger logger)
            : base(regionManager, dialogService, eventAggregator, messageService)
        {
            _azureDevOpsService = azureDevOpsService;
            _settings = settings;
            _logger = logger;

            Loaded += async (sender, args) =>
            {
                try
                {
                    IsBusy = true;
                    await BindInitialDataAsync();
                }
                finally
                {
                    IsBusy = false;
                }
            };
        }

        private async Task BindInitialDataAsync()
        {
            Projects = await GetProjectsAsync();
            ShowMessage($"Project list loaded for organization {_settings.OrganizationName}");
        }

        private async Task<ObservableCollection<Project>> GetProjectsAsync()
        {
            try
            {
                var serviceResponse = await _azureDevOpsService.GetProjectsAsync();
                return new ObservableCollection<Project>(serviceResponse.Projects.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.Exception(ex);
            }

            return new ObservableCollection<Project>();
        }

        #region Commands

        public ICommand NavigateToProjectBuildsCommand => new DelegateCommand<Project>(NavigateToProjectBuildsView);

        public ICommand NavigateToProjectReposCommand => new DelegateCommand<Project>(NavigateToProjectReposView);

        #endregion Commands

        #region Properties

        private ObservableCollection<Project> _projects;

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                RaisePropertyChanged();
            }
        }

        #endregion Properties

        #region Privates

        private void NavigateToProjectReposView(Project project)
        {
            if (project != null)
                RegionManager.RequestNavigate("MainRegion", new Uri($"ReposView?projectName={project.Name}", UriKind.Relative));
        }

        private void NavigateToProjectBuildsView(Project project)
        {
            if (project != null)
                RegionManager.RequestNavigate("MainRegion", new Uri($"BuildsView?projectName={project.Name}", UriKind.Relative));
        }

        #endregion Privates
    }
}