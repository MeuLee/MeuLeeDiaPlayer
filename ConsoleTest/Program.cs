using MeuLeeDiaPlayer.YoutubeExplodeWrapper;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        private const string _videoId = "LB7kQN6uNok"; // hyadain no joujou yuujou
        private const string _playlistId = "PLavloY7qV15IKdrZMAxyEdFuv_TFREEnA"; // Arc North
        private const string _folder = @"C:\Users\mathi\OneDrive\Documents\Dev\downloaded videos folder";
        private static readonly IVideoDownloader _downloader = new YoutubeVideoDownloader();

        public static async Task Main()
        {
            var progress = new Progress<double>(OnProgressUpdate);
            await _downloader.DownloadPlaylist(_folder, _playlistId, progress);
        }

        private static void OnProgressUpdate(double progress)
        {
            int nbStars = (int)(progress * 20);
            Console.CursorLeft = 0;
            var stringBuilder = new StringBuilder(nbStars);
            for (int i = 0; i < nbStars; i++)
            {
                stringBuilder.Append('*');
            }
            Console.WriteLine(stringBuilder);
        }
    }
}
