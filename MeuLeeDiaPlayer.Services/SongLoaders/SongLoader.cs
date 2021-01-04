using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.Audio;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Services.SongLoaders
{
    /// <summary>
    /// This class initializes the SongDto.FileReader value for each song received in constructor.
    /// </summary>
    public class SongLoader : ISongLoader
    {
        private readonly IRepository<Song> _songRepository;
        private readonly IMapper _mapper;
        private ICollection<PlaylistDto> _playlists;

        public SongLoader(IRepository<Song> songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task LoadSongs(ICollection<PlaylistDto> playlists)
        {
            _playlists = playlists;

            var songs = await _songRepository.GetAllAsync(s => s.Playlists);
            await LoadSongs(songs);
        }

        private async Task LoadSongs(List<Song> songs)
        {
            var mappedSongs = songs.Select(s => _mapper.Map<SongDto>(s));
            await Task.WhenAll(mappedSongs.Select(LoadSong));
        }

        private Task LoadSong(SongDto song)
        {
            var fileReader = new AudioStream(song.Path);

            foreach (var playlist in _playlists)
            {
                var songMatchesPath = playlist.Songs.FirstOrDefault(s => s.Path == song.Path);
                if (songMatchesPath == null) continue;
                songMatchesPath.FileReader = fileReader;
            }

            return Task.CompletedTask;
        }
    }
}
