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
        public List<SongDto> Songs { get; } = new();
        public List<Song> DbSongs { get; private set; }

        private readonly IModelRepository<Song> _songRepository;
        private readonly IMapper _mapper;
        private ICollection<PlaylistDto> _playlists;

        public SongLoader(IModelRepository<Song> songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task LoadSongsAsync(ICollection<PlaylistDto> playlists)
        {
            _playlists = playlists;

            DbSongs = (await _songRepository.GetAsync()).ToList();
            await LoadSongs(DbSongs);
        }

        public void MapSongs(PlaylistDto playlist)
        {
            foreach (var song in playlist.Songs)
            {
                if (song.FileReader is not null) continue;
                var songMatch = Songs.FirstOrDefault(s => s.Id == song.Id);
                if (songMatch?.FileReader is null) continue;
                song.FileReader = songMatch.FileReader;
            }
        }

        public async Task AddSongAsync(Song song)
        {
            song = await _songRepository.CreateAsync(song);
            DbSongs.Add(song);
            var songDto = _mapper.Map<SongDto>(song);
            await LoadSong(songDto);
        }

        private async Task LoadSongs(List<Song> dbSongs)
        {
            var mappedSongs = dbSongs.Select(s => _mapper.Map<SongDto>(s));
            await Task.WhenAll(mappedSongs.Select(LoadSong));
        }

        private Task LoadSong(SongDto song)
        {
            return Task.Run(() =>
            {
                var fileReader = new AudioStream(song.Path);

                foreach (var playlist in _playlists)
                {
                    var songMatch = playlist.Songs.FirstOrDefault(s => s.Id == song.Id);
                    if (songMatch == null) continue;
                    songMatch.FileReader = fileReader;
                }

                song.FileReader = fileReader;
                Songs.Add(song);
            });            
        }
    }
}
