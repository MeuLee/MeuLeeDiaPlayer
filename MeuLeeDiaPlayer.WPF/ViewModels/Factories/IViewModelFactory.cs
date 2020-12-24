using MeuLeeDiaPlayer.WPF.State.ViewNavigators;

namespace MeuLeeDiaPlayer.WPF.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
