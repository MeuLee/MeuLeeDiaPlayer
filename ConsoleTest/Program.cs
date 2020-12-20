using MeuLeeDiaPlayer.EntityFramework.Context;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        private const string _folder = @"C:\Users\mathi\OneDrive\Documents\Anime\Songs\";
        private static List<Song> _songs;
        private static ServiceProvider _serviceProvider;

        static async Task Main()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<MeuLeeDiaPlayerDbContext>()
                .AddSingleton<IRepository<Playlist>, GenericRepository<Playlist>>()
                .AddSingleton<IRepository<Song>, GenericRepository<Song>>()
                .BuildServiceProvider();

            RefreshDbData();
            var playlistIds = await InsertData();
            var playlists = (await RetrieveData(playlistIds)).ToList();
            await UpdateData(playlists);
        }

        private static void RefreshDbData()
        {
            var dbContext = _serviceProvider.GetService<MeuLeeDiaPlayerDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        private static async Task<IEnumerable<int>> InsertData()
        {
            var playlistRepository = _serviceProvider.GetService<IRepository<Playlist>>();
            var songRepository = _serviceProvider.GetService<IRepository<Song>>();

            var songsAfterAdd = new List<Song>();
            _songs = new List<Song>
            {
                new Song { Path = $"{_folder}Angel Beats! OP.mp3", SongName = "Angel Beats! OP" },
                new Song { Path = $"{_folder}Blend S - OP.mp3", SongName = "Blend S - OP" },
                new Song { Path = $"{_folder}Bunny Girl OP.mp3", SongName = "Bunny Girl OP" },
                new Song { Path = $"{_folder}K-ON! OP.mp3", SongName = "K-ON! OP" },
                new Song { Path = $"{_folder}New Game! OP.mp3", SongName = "New Game! OP" }
            };

            foreach (var song in _songs)
            {
                var item = await songRepository.CreateAsync(song);
                songsAfterAdd.Add(item);
            }

            var playlistList = new List<Playlist>
            {
                new Playlist { PlaylistName = "Playlist1" },
                new Playlist { PlaylistName = "Playlist2" }
            };

            playlistList[0].Songs.AddRange(new List<Song> { songsAfterAdd[0], songsAfterAdd[1] });
            playlistList[1].Songs.AddRange(new List<Song> { songsAfterAdd[2], songsAfterAdd[3], songsAfterAdd[4] });

            var result = new List<int>();

            foreach (var playlist in playlistList)
            {
                await playlistRepository.CreateAsync(playlist);
                result.Add(playlist.Id);
            }

            return result;
        }

        private static async Task<IEnumerable<Playlist>> RetrieveData(IEnumerable<int> playlistIds)
        {
            var playlistRepository = _serviceProvider.GetService<IRepository<Playlist>>();
            var playlists = new List<Playlist>();
            foreach (int id in playlistIds)
            {
                var playlist = await playlistRepository.GetAsync(id);
                playlists.Add(playlist);
            }
            return playlists;
        }

        private static async Task UpdateData(List<Playlist> playlists)
        {
            var playlistRepository = _serviceProvider.GetService<IRepository<Playlist>>();
            var playlist = playlists[0];
            playlist.PlaylistName = "yo";
            var song = playlists[1].Songs[0];
            song.SongName = "patente";
            playlist.Songs.Add(song);
            await playlistRepository.UpdateAsync(playlist.Id, playlist);
        }
    }
}
