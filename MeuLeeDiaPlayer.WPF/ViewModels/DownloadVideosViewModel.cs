using MeuLeeDiaPlayer.WPF.Commands.DownloadVideos;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class DownloadVideosViewModel : BaseViewModel
    {
        public DownloadYtLinkCommand DownloadYtLinkCommand { get; }

        public DownloadVideosViewModel(DownloadYtLinkCommand downloadYtLinkCommand)
        {
            DownloadYtLinkCommand = downloadYtLinkCommand;
        }
    }
}
