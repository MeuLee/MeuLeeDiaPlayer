using MeuLeeDiaPlayer.WPF.State.ViewNavigator;
using MeuLeeDiaPlayer.WPF.ViewModels.SubViewModels;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public IViewNavigator ViewNavigator { get; }
        public CurrentSongBarViewModel CurrentSongBarViewModel { get; }

        public MainViewModel(IViewNavigator viewNavigator, CurrentSongBarViewModel currentSongBarViewModel)
        {
            ViewNavigator = viewNavigator;
            CurrentSongBarViewModel = currentSongBarViewModel;

            ViewNavigator.UpdateCurrentViewModelCommand.Execute(ViewType.Playlists);
        }
    }
}
