using NAudio.Wave;
using System;

namespace MeuLeeDiaPlayer.EntityFramework
{
    public static class AudioExtensions
    {
        public static float GetMaxVolume(this AudioFileReader reader)
        {
            float maxVolume = 0;
            var buffer = new float[reader.WaveFormat.SampleRate];
            int read;
            do
            {
                read = reader.Read(buffer, 0, buffer.Length);
                for (int i = 0; i < read; i++)
                {
                    var abs = Math.Abs(buffer[i]);
                    if (abs > maxVolume)
                    {
                        maxVolume = abs;
                    }
                }
            }
            while (read > 0);
            reader.Position = 0;

            return maxVolume;
        }
    }
}
