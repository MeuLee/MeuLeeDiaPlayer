using NAudio.Wave;

namespace MeuLeeDiaPlayer.EntityFramework.Audio
{
    public interface IAudioStream
    {
        AudioFileReader Stream { get; }
    }
}
