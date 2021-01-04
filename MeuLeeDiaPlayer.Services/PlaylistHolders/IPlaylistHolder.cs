using MeuLeeDiaPlayer.Common.Models;
using Meziantou.Framework.WPF.Collections;

namespace MeuLeeDiaPlayer.Services.PlaylistHolders
{
    public interface IPlaylistHolder
    {
        ConcurrentObservableCollection<PlaylistDto> Playlists { get; set; }
        PlaylistDto SoundPlaylist { get; set; }
        PlaylistDto UIPlaylist { get; set; }
    }
}
