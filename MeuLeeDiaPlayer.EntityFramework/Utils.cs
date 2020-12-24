using NAudio.Wave;
using System;
using System.Collections.Generic;

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

    public static class TimeExtensions
    {
        public static TimeSpan Sum<T>(this IEnumerable<T> source, Func<T, TimeSpan> selector)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            _ = selector ?? throw new ArgumentNullException(nameof(selector));

            var totalTime = new TimeSpan();
            foreach (T item in source)
            {
                totalTime += selector(item);
            }
            return totalTime;
        }
    }
}
