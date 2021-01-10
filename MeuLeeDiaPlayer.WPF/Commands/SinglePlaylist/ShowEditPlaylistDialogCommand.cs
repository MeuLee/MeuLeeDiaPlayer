using AutoMapper;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.WPF.Views.Forms;
using System.Linq;

namespace MeuLeeDiaPlayer.WPF.Commands.SinglePlaylist
{
    public class ShowEditPlaylistDialogCommand : BasePlaylistDialogCommand
    {
        private readonly IPlaylistHolder _playlistHolder;
        private readonly IModelRepository<Playlist> _playlistRepository;
        private const string _editPlaylistTitle = "Edit playlist";

        public ShowEditPlaylistDialogCommand(
            IMapper mapper,
            ISongLoader songLoader,
            IPlaylistHolder playlistHolder,
            IModelRepository<Playlist> playlistRepository)
            : base(mapper, songLoader)
        {
            _playlistHolder = playlistHolder;
            _playlistRepository = playlistRepository;
        }

        public override async void Execute(object parameter)
        {
            if (parameter is not PlaylistDto playlistDto) return;
            var window = new CreatePlaylistWindow(_editPlaylistTitle, _songLoader, playlistDto);

            bool? result = window.ShowDialog();

            if (!(result ?? false)) return;
            int playlistDtoIndex = _playlistHolder.Playlists.IndexOf(playlistDto);
            var playlist = await _playlistRepository.GetAsync(playlistDto.Id);
            playlist.Name = window.PlaylistName;
            playlistDto = await ModifyPlaylistInDb(playlist, window.CheckedSongs.Select(s => s.Id), _playlistRepository.UpdateAsync);

            if (playlistDto is null) return;
            _playlistHolder.Playlists[playlistDtoIndex] = playlistDto;
            _playlistHolder.UIPlaylist = playlistDto;
            _playlistHolder.OnModifiedPlaylistList();
        }
    }
}
