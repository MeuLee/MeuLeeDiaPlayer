using MeuLeeDiaPlayer.EntityFramework.Audio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Services.SongLoaders
{
    public interface ISongLoader
    {
        Dictionary<string, IAudioStream> Songs { get; }

        Task LoadSongs();
    }
}
