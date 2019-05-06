using Jenx.AzureDevOps.Client;
using Jenx.AzureDevOps.WpfClient.Logger;
using Jenx.AzureDevOps.WpfClient.Services;
using Jenx.AzureDevOps.WpfClient.Settings;
using Jenx.AzureDevOps.WpfClient.ViewModels;
using Jenx.AzureDevOps.WpfClient.Views;
using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace Jenx.AzureDevOps.WpfClient
{
    public partial class App
    {
        private IApplicationLogger _logger;

        protected override Window CreateShell()
        {
            return (Window)Container.Resolve<IShellView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApplicationLogger, NlogLogger>();

            containerRegistry.Register<IExceptionDialogViewModel, ExceptionDialogViewModel>();
            containerRegistry.Register<IExceptionDialogView, ExceptionDialogView>();

            containerRegistry.Register<IConfirmationDialogViewModel, ConfirmationDialogViewModel>();
            containerRegistry.Register<IConfirmationDialogView, ConfirmationDialogView>();

            containerRegistry.Register<IAboutDialogView, AboutDialogView>();

            containerRegistry.RegisterInstance<ISnackbarMessageQueue>(
                new SnackbarMessageQueue(new TimeSpan(0, 0, 0, 5)));
            containerRegistry.Register<IDialogService, DialogService>();
            containerRegistry.Register<IMessageService, MessageService>();

            containerRegistry.RegisterInstance<ISettings>(
                new AppSettingsService(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jenx.Azure.DevOps.WpfClient", "appSettings.json")));

            containerRegistry.RegisterSingleton<IAzureDevOpsSettings, AzureDevOpsSettings>();

            containerRegistry.RegisterSingleton<IAzureDevOpsService, AzureDevOpsService>();

            containerRegistry.RegisterInstance<IAssemblyInfoService>(new AssemblyInfoService(System.Reflection.Assembly.GetExecutingAssembly()));
            containerRegistry.Register<ISystemInfoService, SystemInfoService>();

            containerRegistry.RegisterForNavigation<ProjectsView>();
            containerRegistry.RegisterForNavigation<BuildsView>();
            containerRegistry.RegisterForNavigation<ReposView>();
            containerRegistry.RegisterForNavigation<SettingsView>();

            containerRegistry.Register<IShellView, ShellView>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Current.DispatcherUnhandledException += AppDispatcherUnhandledException;
            _logger = Container.Resolve<IApplicationLogger>();
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Exception(e.Exception);

            //#if DEBUG   // In debug mode do not custom-handle the exception, let Visual Studio handle it
            //    e.Handled = false;
            //#else
            ShowUnhandledException(e);
            //#endif
        }

        private async void ShowUnhandledException(DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            _logger.Exception(e.Exception);

            var errorMessage =
                $"An application error occurred.{Environment.NewLine}" +
                $"Please check whether your data is correct and repeat the action. If this error occurs again there seems to be a more serious malfunction in the application, and you better close it." +
                $"{Environment.NewLine}{Environment.NewLine}Error: {e.Exception.Message + (e.Exception.InnerException != null ? $"{Environment.NewLine}" + e.Exception.InnerException.Message : null)}";

            try // try fancy message box first...
            {
                var dialogService = Container.Resolve<IDialogService>();
                if (!await dialogService.ShowExceptionDialog(errorMessage))
                {
                    if (await dialogService.ShowDialog(
                        $"WARNING: The application will close.{Environment.NewLine}Do you really want to close it?",
                        "Yes", "No"))
                    {
                        Current.Shutdown();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex);

                if (MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Error) == MessageBoxResult.No)
                {
                    if (MessageBox.Show(
                            "WARNING: The application will close. Do you really want to close it?",
                            "Close the application!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) ==
                        MessageBoxResult.Yes)
                    {
                        Current.Shutdown();
                    }
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            LoadSettings();
        }

        private void LoadSettings()
        {
            // load settings first
            var settings = Container.Resolve<ISettings>();
            settings.Load();

            // apply settings also to azure data service
            var azureDevOpsSettings = Container.Resolve<IAzureDevOpsSettings>();
            azureDevOpsSettings.OrganizationName = settings.OrganizationName; // works because of singleton!
            azureDevOpsSettings.PersonalAccessToken = settings.PersonalAccessToken; // works because of singleton!
        }
    }
}