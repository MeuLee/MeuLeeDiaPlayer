using MeuLeeDiaPlayer.WPF.Commands.DownloadVideos;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class DownloadVideosViewModel : BaseViewModel
    {
        public ICommand DownloadYtLinkCommand { get; }

        public DownloadVideosViewModel(DownloadYtLinkCommand downloadYtLinkCommand)
        {
            DownloadYtLinkCommand = downloadYtLinkCommand;
        }
    }
}
