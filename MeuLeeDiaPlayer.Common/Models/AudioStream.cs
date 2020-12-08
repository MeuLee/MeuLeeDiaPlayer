using NAudio.Wave;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class AudioStream : IAudioStream
    {
        public AudioFileReader Stream { get; private set; }

        public AudioStream(string filePath, bool normalizeVolume = true)
        {
            InitializeStream(filePath, normalizeVolume);
        }

        private void InitializeStream(string filePath, bool normalizeVolume)
        {
            Stream = new AudioFileReader(filePath);
            if (!normalizeVolume) return;

            float maxVolume = Stream.GetMaxVolume();
            if (maxVolume is 0 or >= 1.0f) return; // normalization is not possible outside these bounds
            Stream.Volume = 1 / maxVolume;
        }
    }
}
