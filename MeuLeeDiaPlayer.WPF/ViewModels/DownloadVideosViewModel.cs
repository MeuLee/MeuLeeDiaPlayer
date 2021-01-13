using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.WPF.Commands.DownloadVideos;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class DownloadVideosViewModel : BaseViewModel
    {
        public DownloadYtLinkCommand DownloadYtLinkCommand { get; }
        public DeleteSongCommand DeleteSongCommand { get; }
        public ISongLoader SongLoader { get; }

        public DownloadVideosViewModel(
            DownloadYtLinkCommand downloadYtLinkCommand,
            DeleteSongCommand deleteSongCommand,
            ISongLoader songLoader)
        {
            DownloadYtLinkCommand = downloadYtLinkCommand;
            DeleteSongCommand = deleteSongCommand;
            SongLoader = songLoader;
        }
    }
}
