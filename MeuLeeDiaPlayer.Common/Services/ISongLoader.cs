using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Common.Services
{
    public interface ISongLoader
    {
        Task LoadSong(Song song);
    }
}
