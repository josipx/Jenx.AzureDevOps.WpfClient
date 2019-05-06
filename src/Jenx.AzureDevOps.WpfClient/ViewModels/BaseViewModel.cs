using Jenx.AzureDevOps.WpfClient.EventAggregator;
using Jenx.AzureDevOps.WpfClient.Services;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        public event EventHandler Loaded;

        private readonly IMessageService _messageService;

        public BaseViewModel(IRegionManager regionManager, IDialogService dialogService, IEventAggregator eventAggregator, IMessageService messageService)
        {
            RegionManager = regionManager;
            DialogService = dialogService;
            _messageService = messageService;
            EventAggregator = eventAggregator;
            ShowStatusBar = true;
        }

        public IRegionManager RegionManager { get; }

        public IEventAggregator EventAggregator { get; }

        public IDialogService DialogService { get; }

        public void OnLoaded()
        {
            var handler = Loaded;
            handler?.Invoke(this, new EventArgs());
        }

        public void ShowMessage(string message)
        {
            _messageService.SendMessage(message);
        }

        public bool IsBusy
        {
            set => EventAggregator.GetEvent<IsBusyEvent>().Publish(value);
        }

        public bool ShowStatusBar { get; set; }
    }
}