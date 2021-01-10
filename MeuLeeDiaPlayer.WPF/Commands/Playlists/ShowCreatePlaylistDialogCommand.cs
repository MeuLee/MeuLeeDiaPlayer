using AutoMapper;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.WPF.Views.Forms;
using System.Linq;

namespace MeuLeeDiaPlayer.WPF.Commands.Playlists
{
    public class ShowCreatePlaylistDialogCommand : BasePlaylistDialogCommand
    {
        private readonly IPlaylistHolder _playlistHolder;
        private readonly IModelRepository<Playlist> _playlistRepository;
        private const string _createPlaylistTitle = "Create a new playlist";

        public ShowCreatePlaylistDialogCommand(
            IPlaylistHolder playlistHolder,
            IModelRepository<Playlist> playlistRepository,
            IMapper mapper,
            ISongLoader songLoader)
            : base(mapper, songLoader)
        {
            _playlistHolder = playlistHolder;
            _playlistRepository = playlistRepository;
        }

        public override async void Execute(object parameter)
        {
            var window = new CreatePlaylistWindow(_createPlaylistTitle, _songLoader);
            bool? result = window.ShowDialog();

            if (!(result ?? false)) return;

            var playlist = new Playlist { Name = window.PlaylistName };
            var playlistDto = await ModifyPlaylistInDb(playlist, window.CheckedSongs.Select(s => s.Id), _playlistRepository.CreateAsync);
            
            if (playlistDto is null) return;
            _playlistHolder.Playlists.Add(playlistDto);
        }
    }
}
