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

        public Dictionary<string, IAudioStream> Songs { get; private set; }

        public SongLoader(IRepository<Song> songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public Task LoadSongs()
        {
            return _songRepository.GetAllAsync(s => s.Playlists)
                .ContinueWith(LoadSongs);
        }

        private void LoadSongs(Task<List<Song>> task)
        {
            if (task.Exception != null) throw task.Exception;

            Songs = task.Result.Select(s => _mapper.Map<SongDto>(s)).ToDictionary(s => s.Path, s => s.FileReader);
            Task.WhenAll(Songs.Select(LoadSong));
        }

        private Task LoadSong(KeyValuePair<string, IAudioStream> song)
        {
            string filePath = song.Key;
            return Task.FromResult(Songs[filePath] = new AudioStream(filePath));
        }
    }
}
