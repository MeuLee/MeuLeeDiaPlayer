using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Services.PlaylistRetrievers
{
    public interface IPlaylistRetriever
    {
        Task LoadPlaylists();
    }
}
