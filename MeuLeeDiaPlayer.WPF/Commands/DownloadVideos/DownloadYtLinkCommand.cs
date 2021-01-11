using MeuLeeDiaPlayer.EntityFramework;
using MeuLeeDiaPlayer.Services.UrlValidator;
using MeuLeeDiaPlayer.YoutubeExplodeWrapper;

namespace MeuLeeDiaPlayer.WPF.Commands.DownloadVideos
{
    public class DownloadYtLinkCommand : BaseCommand
    {
        private readonly IYoutubeUrlValidator _youtubeUrlValidator;
        private readonly IYoutubeVideoDownloader _youtubeVideoDownloader;

        public DownloadYtLinkCommand(IYoutubeUrlValidator youtubeUrlValidator, IYoutubeVideoDownloader youtubeVideoDownloader)
        {
            _youtubeUrlValidator = youtubeUrlValidator;
            _youtubeVideoDownloader = youtubeVideoDownloader;
        }

        public override async void Execute(object parameter)
        {
            if (parameter is not string input) return;
            switch (_youtubeUrlValidator.GetYoutubeUrlType(input))
            {
                case YoutubeUrlType.Playlist:
                    var paths = await _youtubeVideoDownloader.TryDownloadPlaylist(Constants.DefaultSongsLocation, input);
                    break;
                case YoutubeUrlType.Video:
                    string path = await _youtubeVideoDownloader.TryDownloadVideo(Constants.DefaultSongsLocation, input);
                    break;
            }
        }
    }
}
