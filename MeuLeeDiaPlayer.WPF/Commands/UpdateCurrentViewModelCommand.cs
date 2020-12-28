using MeuLeeDiaPlayer.WPF.State.ViewNavigator;
using MeuLeeDiaPlayer.WPF.ViewModels.Factories;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : BaseCommand
    {
        private readonly IViewNavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(IViewNavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public override void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
