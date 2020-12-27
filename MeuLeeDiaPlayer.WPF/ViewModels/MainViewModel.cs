using MeuLeeDiaPlayer.WPF.State.ViewNavigator;

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
