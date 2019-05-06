using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Jenx.AzureDevOps.WpfClient.Views
{
    public partial class ShellView : IShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void OnLeftDrawerMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // close left docker menu
            MenuToggleButton.IsChecked = false;

            // and execute left menu item command, small hack
            var command = ((Models.MenuItem)((ListBox)sender).SelectedItem).MenuCommand;
            command.Execute(null);
        }

        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            DrawerToggleButton.IsChecked = true;
        }

        private void DrawerToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            MenuToggleButton.IsChecked = false;
        }
    }
}