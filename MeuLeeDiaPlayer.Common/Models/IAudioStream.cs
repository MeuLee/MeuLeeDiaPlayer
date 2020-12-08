using NAudio.Wave;

namespace MeuLeeDiaPlayer.Common.Models
{
    public interface IAudioStream
    {
        AudioFileReader Stream { get; }
    }
}
