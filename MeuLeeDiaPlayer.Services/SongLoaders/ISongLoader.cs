using MeuLeeDiaPlayer.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Services.SongLoaders
{
    public interface ISongLoader
    {
        Task LoadSongs(ICollection<PlaylistDto> playlists);
    }
}
