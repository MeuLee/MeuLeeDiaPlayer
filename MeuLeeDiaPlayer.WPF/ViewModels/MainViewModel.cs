using MeuLeeDiaPlayer.WPF.State.ViewNavigators;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public IViewNavigator ViewNavigator { get; set; }

        public MainViewModel(IViewNavigator viewNavigator)
        {
            ViewNavigator = viewNavigator;

            ViewNavigator.UpdateCurrentViewModelCommand.Execute(ViewType.Playlists);
        }
    }
}
