using MeuLeeDiaPlayer.EntityFramework.Audio;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Common.Services
{
    public class SongLoader : ISongLoader
    {
        public Task LoadSong(Song song)
        {
            return Task.FromResult(song.FileReader = new AudioStream(song.Path));
        }
    }
}
