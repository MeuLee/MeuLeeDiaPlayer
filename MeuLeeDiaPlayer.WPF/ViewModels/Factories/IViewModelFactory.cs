using MeuLeeDiaPlayer.WPF.State.ViewNavigator;

namespace MeuLeeDiaPlayer.WPF.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
