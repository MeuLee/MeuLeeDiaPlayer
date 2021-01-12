using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.WPF.Commands.DownloadVideos;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class DownloadVideosViewModel : BaseViewModel
    {
        public DownloadYtLinkCommand DownloadYtLinkCommand { get; }
        public ISongLoader SongLoader { get; }

        public DownloadVideosViewModel(DownloadYtLinkCommand downloadYtLinkCommand, ISongLoader songLoader)
        {
            DownloadYtLinkCommand = downloadYtLinkCommand;
            SongLoader = songLoader;
        }
    }
}
