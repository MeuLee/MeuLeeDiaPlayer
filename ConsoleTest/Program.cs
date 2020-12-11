using MeuLeeDiaPlayer.Common.Enums;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Common.PlaylistPlayMode;
using MeuLeeDiaPlayer.SoundPlayer;
using System;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        private const string _folder = @"C:\Users\mathi\OneDrive\Documents\Anime\Songs";
        static void Main(string[] args)
        {
            var playlist = new Playlist(_folder, "playlist1");
            var songList = new SongList(PlayMode.GetPlayMode(ShuffleStyle.Shuffle, LoopStyle.LoopPlaylist), playlist);
            var soundPlayerManager = new SoundPlayerManager(songList)
            {
                Volume = 0.2f
            };

            Console.WriteLine($"Songs:\n{string.Join('\n', songList.FollowingSongs)}");
            Task.Run(() => soundPlayerManager.PlayCurrent());
            Menu(soundPlayerManager);
        }

        private static void Menu(SoundPlayerManager soundPlayerManager)
        {
            string input;
            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "loud":
                        soundPlayerManager.Volume += 0.1f;
                        Console.WriteLine($"New volume: {soundPlayerManager.Volume}");
                        break;
                    case "quiet":
                        soundPlayerManager.Volume -= 0.1f;
                        Console.WriteLine($"New volume: {soundPlayerManager.Volume}");
                        break;
                    case "next":
                        soundPlayerManager.PlayNext();
                        break;
                    case "prev":
                        soundPlayerManager.PlayPrevious();
                        break;
                    case "pause":
                        soundPlayerManager.Pause();
                        break;
                    case "resume":
                        soundPlayerManager.Resume();
                        break;
                }
            }
            while (input != "a");
        }
    }
}
