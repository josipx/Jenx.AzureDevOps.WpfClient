using Jenx.AzureDevOps.WpfClient.ViewModels;
using System.Windows.Controls;

namespace Jenx.AzureDevOps.WpfClient.Views
{
    public abstract class BaseView : UserControl
    {
        protected BaseView()
        {
            Loaded += ViewLoaded;
        }

        public virtual void ViewLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var model = DataContext as BaseViewModel;
            model?.OnLoaded();
        }
    }
}