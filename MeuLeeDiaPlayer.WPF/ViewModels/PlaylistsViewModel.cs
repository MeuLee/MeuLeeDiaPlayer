using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Common.Services;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.SoundPlayer;
using MeuLeeDiaPlayer.WPF.State.PlaylistNavigator;
using MeuLeeDiaPlayer.WPF.State.SongNavigator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class PlaylistsViewModel : BaseViewModel
    {
        public List<PlaylistDto> Playlists { get; set; }

        public IPlaylistNavigator PlaylistNavigator { get; }
        public ISongNavigator SongNavigator { get; }

        private readonly IRepository<Playlist> _playlistRepository;
        private readonly ISongLoader _songLoader;
        private readonly IMapper _mapper;

        public PlaylistsViewModel(IRepository<Playlist> playlistRepository, ISongLoader songLoader, IMapper mapper, ISongNavigator songNavigator)
        {
            _playlistRepository = playlistRepository;
            _songLoader = songLoader;
            _mapper = mapper;
            SongNavigator = songNavigator;
            PlaylistNavigator = new PlaylistNavigator(this);

            LoadPlaylists();
        }

        private void LoadPlaylists()
        {
            var fetchPlaylistsTask = _playlistRepository.GetAllAsync(p => p.Songs)
                .ContinueWith(SetPlaylists);
            var loadSongsTask = _songLoader.LoadSongs();
            Task.WhenAll(fetchPlaylistsTask, loadSongsTask).ContinueWith(task => InitializePlaylists());
        }

        private void SetPlaylists(Task<List<Playlist>> task)
        {
            if (task.Exception != null) throw task.Exception;

            Playlists = task.Result.Select(p => _mapper.Map<PlaylistDto>(p)).ToList();
        }

        private void InitializePlaylists()
        {
            foreach (var playlist in Playlists)
            {
                foreach (var song in playlist.Songs)
                {
                    song.FileReader = _songLoader.Songs[song.Path];
                }
            }
        }
    }
}
