using MeuLeeDiaPlayer.Common.Services;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.WPF.State.PlaylistNavigator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class PlaylistsViewModel : BaseViewModel
    {
        public List<Playlist> Playlists { get; set; }

        public IPlaylistNavigator PlaylistNavigator { get; set; }

        private IRepository<Playlist> _playlistRepository;
        private ISongLoader _songLoader;

        public PlaylistsViewModel(IRepository<Playlist> playlistRepository, ISongLoader songLoader)
        {
            _playlistRepository = playlistRepository;
            _songLoader = songLoader;

            PlaylistNavigator = new PlaylistNavigator(this);

            _playlistRepository.GetAllAsync(p => p.Songs)
                .ContinueWith(task =>
                {
                    if (task.Exception == null)
                    {
                        Playlists = task.Result;
                    }
                })
                .ContinueWith(_ =>
                {
                    Task.WhenAll(Playlists.SelectMany(p => p.Songs.Select(s => _songLoader.LoadSong(s)))).Wait();
                });
        }
    }
}
