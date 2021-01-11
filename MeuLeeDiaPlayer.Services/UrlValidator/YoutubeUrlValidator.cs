using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace MeuLeeDiaPlayer.Services.UrlValidator
{
    public class YoutubeUrlValidator : IYoutubeUrlValidator
    {
        public YoutubeUrlType GetYoutubeUrlType(string input)
        {
            if (PlaylistId.TryParse(input) is not null) return YoutubeUrlType.Playlist;
            if (VideoId.TryParse(input) is not null) return YoutubeUrlType.Video;
            return YoutubeUrlType.Neither;
        }
    }
}
