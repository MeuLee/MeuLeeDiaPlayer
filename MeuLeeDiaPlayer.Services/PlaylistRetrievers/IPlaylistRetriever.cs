using MeuLeeDiaPlayer.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Services.PlaylistRetrievers
{
    public interface IPlaylistRetriever
    {
        Task LoadPlaylists();
    }
}
