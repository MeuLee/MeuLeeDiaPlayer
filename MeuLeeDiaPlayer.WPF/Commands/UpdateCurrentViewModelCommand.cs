using MeuLeeDiaPlayer.WPF.State.ViewNavigator;
using MeuLeeDiaPlayer.WPF.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IViewNavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(IViewNavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
