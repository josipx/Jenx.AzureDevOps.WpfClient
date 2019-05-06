using Prism.Mvvm;
using System.Windows.Input;

namespace Jenx.AzureDevOps.WpfClient.Models
{
    public class MenuItem : BindableBase
    {
        private string _name;
        private ICommand _menuCommand;

        public MenuItem(string name, ICommand menuCommand)
        {
            _name = name;
            _menuCommand = menuCommand;
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICommand MenuCommand
        {
            get => _menuCommand;
            set => SetProperty(ref _menuCommand, value);
        }
    }
}