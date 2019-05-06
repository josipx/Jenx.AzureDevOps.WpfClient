using Jenx.AzureDevOps.WpfClient.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Windows.Input;

namespace Jenx.AzureDevOps.WpfClient.ViewModels
{
    public class AboutDialogViewModel : BaseViewModel, IAboutDialogViewModel
    {
        private readonly ISystemInfoService _systemInfoService;

        public AboutDialogViewModel(ISystemInfoService systemInfoService, IRegionManager regionManager,
            IMessageService messageService, IDialogService dialogService, IEventAggregator eventAggregator) :
            base(regionManager, dialogService, eventAggregator, messageService)
        {
            _systemInfoService = systemInfoService;
            BindData();
        }

        private void BindData()
        {
            ProductVersion = _systemInfoService.ProductVersion;
            CopyrightInfo = _systemInfoService.ProductCopyright;
            ProductDetail = _systemInfoService.ProductDescription;
            ProductInfo = _systemInfoService.ProductName;
            ProductCompany = _systemInfoService.ProductCompany;
        }

        private string _productInfo;

        public string ProductInfo
        {
            get => _productInfo;
            set => SetProperty(ref _productInfo, value);
        }

        private string _productVersion;

        public string ProductVersion
        {
            get => _productVersion;
            set => SetProperty(ref _productVersion, value);
        }

        private string _copyrightInfo;

        public string CopyrightInfo
        {
            get => _copyrightInfo;
            set => SetProperty(ref _copyrightInfo, value);
        }

        private string _productDetail;

        public string ProductDetail
        {
            get => _productDetail;
            set => SetProperty(ref _productDetail, value);
        }

        private string _productCompany;

        public string ProductCompany
        {
            get => _productCompany;
            set => SetProperty(ref _productCompany, value);
        }

        public ICommand NavigateToUrlCommand => new DelegateCommand<string>(Tools.Browser.OpenBrowser);
    }
}