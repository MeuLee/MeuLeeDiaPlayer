using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using System.Windows;

namespace MeuLeeDiaPlayer.WPF.Commands.SinglePlaylist
{
    public class DeletePlaylistCommand : BaseCommand
    {
        private readonly IPlaylistHolder _playlistHolder;
        private readonly IModelRepository<Playlist> _playlistRepository;

        public DeletePlaylistCommand(
            IPlaylistHolder playlistHolder,
            IModelRepository<Playlist> playlistRepository)
        {
            _playlistHolder = playlistHolder;
            _playlistRepository = playlistRepository;
        }

        public override async void Execute(object parameter)
        {
            if (parameter is not PlaylistDto playlist) return;
            if (ShowConfirmDialog(playlist.Name) != MessageBoxResult.Yes) return;

            await _playlistRepository.DeleteAsync(playlist.Id);
            _playlistHolder.Playlists.Remove(playlist);
            if (_playlistHolder.SoundPlaylist == playlist)
            {
                _playlistHolder.SoundPlaylist = null;
            }
            if (_playlistHolder.UIPlaylist == playlist)
            {
                _playlistHolder.UIPlaylist = _playlistHolder.SoundPlaylist; // can be null. either there's no ui playlist or ui playlist = sound playlist
            }
        }

        private static MessageBoxResult ShowConfirmDialog(string playlistName)
        {
            return MessageBox.Show($"Are you sure you want to delete the playlist \"{playlistName}\"?", "Confirm action", MessageBoxButton.YesNo);
        }
    }
}
