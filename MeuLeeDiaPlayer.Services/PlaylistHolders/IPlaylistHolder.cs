using MeuLeeDiaPlayer.Common.Models;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.Services.PlaylistHolders
{
    public interface IPlaylistHolder
    {
        public List<PlaylistDto> Playlists { get; set; }
        public PlaylistDto CurrentPlaylist { get; set; }
    }
}
