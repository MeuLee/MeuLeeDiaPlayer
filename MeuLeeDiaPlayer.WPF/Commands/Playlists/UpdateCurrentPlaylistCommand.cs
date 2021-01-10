using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Services.PlaylistHolders;

namespace MeuLeeDiaPlayer.WPF.Commands.Playlists
{
    public class UpdateCurrentPlaylistCommand : BaseCommand
    {
        private readonly IPlaylistHolder _playlistHolder;

        public UpdateCurrentPlaylistCommand(IPlaylistHolder playlistHolder)
        {
            _playlistHolder = playlistHolder;
        }

        public override void Execute(object parameter)
        {
            if (parameter is PlaylistDto playlist)
            {
                _playlistHolder.UIPlaylist = playlist;
            }
        }
    }
}
