using MeuLeeDiaPlayer.WPF.Commands;
using MeuLeeDiaPlayer.WPF.Models;
using MeuLeeDiaPlayer.WPF.ViewModels;
using MeuLeeDiaPlayer.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.State.ViewNavigators
{
    public class ViewNavigator : ObservableObject, IViewNavigator
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateCurrentViewModelCommand { get; }

        public ViewNavigator(IViewModelFactory viewModelFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelFactory);
        }
    }
}
