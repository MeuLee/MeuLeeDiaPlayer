using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class UpdateCurrentPlaylistCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly PlaylistsViewModel _playlistsViewModel;

        public UpdateCurrentPlaylistCommand(PlaylistsViewModel playlistsViewModel)
        {
            _playlistsViewModel = playlistsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is PlaylistDto playlist)
            {
                _playlistsViewModel.SongNavigator.CurrentPlaylist = playlist;
            }
        }
    }
}
