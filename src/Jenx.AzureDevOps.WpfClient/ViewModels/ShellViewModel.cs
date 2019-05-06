using Jenx.AzureDevOps.WpfClient.EventAggregator;
using Jenx.AzureDevOps.WpfClient.Models;
using Jenx.AzureDevOps.WpfClient.Services;
using Jenx.AzureDevOps.WpfClient.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private readonly SystemInfoService _systemInfoService;
        private readonly IAboutDialogView _aboutDialogView;

        public ShellViewModel(
            IRegionManager regionManager, ISnackbarMessageQueue snackbarMessageQueue,
            IDialogService dialogService, SystemInfoService systemInfoService,
            IEventAggregator eventAggregator, IMessageService messageService, IAboutDialogView aboutDialogView)
            : base(regionManager, dialogService, eventAggregator, messageService)
        {
            _systemInfoService = systemInfoService;
            _aboutDialogView = aboutDialogView;
            MessageQueue = snackbarMessageQueue;

            EventAggregator.GetEvent<IsBusyEvent>().Subscribe(OnBusyEvent);
            EventAggregator.GetEvent<IsBusyMessageEvent>().Subscribe(OnBusyMessageEvent);
            EventAggregator.GetEvent<SentGlobalMessageEvent>().Subscribe(OnSentGlobalMessagEvent);

            BindData();
        }

        #region Props

        private string _title = string.Empty;

        public string Title
        {
            get => _title;
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;

        public new bool IsBusy
        {
            get => _isBusy;
            set { SetProperty(ref _isBusy, value); }
        }

        private string _isBusyMessage;

        public string IsBusyMessage
        {
            get => _isBusyMessage;
            set { SetProperty(ref _isBusyMessage, value); }
        }

        public ISnackbarMessageQueue MessageQueue
        {
            get; set;
        }

        public ObservableCollection<MenuItem> MenuItems { get; private set; }

        #endregion Props

        #region Events

        internal void OnBusyEvent(bool flag)
        {
            IsBusy = flag;

            // clear messages on false....
            if (!flag)
                IsBusyMessage = null;
        }

        internal void OnBusyMessageEvent(string message)
        {
            IsBusyMessage += message + Environment.NewLine;
        }

        internal void OnSentGlobalMessagEvent(string message)
        {
            MessageQueue.Enqueue(message, true);
        }

        #endregion Events

        #region Commands

        public ICommand ExitApplicationCommand => new DelegateCommand(async () =>
        {
            if (await DialogService.ShowDialog("Do you really want to exit app?", "Yes", "No"))
                Application.Current.Shutdown();
        });

        public ICommand ShowAboutViewCommand => new DelegateCommand(async () =>
        {
            await DialogHost.Show(_aboutDialogView, "RootDialog");
        });

        public ICommand ShowSettingsViewCommand => new DelegateCommand(() =>
        {
            RegionManager.RequestNavigate("MainRegion", new Uri("SettingsView", UriKind.Relative));
        });

        #endregion Commands

        #region Privates

        private void BindData()
        {
            Title = $"{_systemInfoService.ProductName } ({_systemInfoService.ProductVersion})";

            MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem(
                    "Projects",
                    new DelegateCommand(() =>
                    {
                        RegionManager.RequestNavigate("MainRegion", new Uri("ProjectsView", UriKind.Relative));
                    })),
                new MenuItem(
                    "Settings",
                    new DelegateCommand(() =>
                    {
                        RegionManager.RequestNavigate("MainRegion", new Uri("SettingsView", UriKind.Relative));
                    })
                ),
#if DEBUG
// test menus only for testing
                new MenuItem(
                    "Fake exception",
                    new DelegateCommand(() =>
                    {
                        int.Parse("=");
                    })),
#endif
                new MenuItem(
                    "Exit",
                    new DelegateCommand(CloseApplication, () => true)),
            };
        }

        private async void CloseApplication()
        {
            if (await DialogService.ShowDialog("Do you really want to exit app?", "Yes", "No"))
                Application.Current.Shutdown();
        }

        #endregion Privates
    }
}