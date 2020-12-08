using MeuLeeDiaPlayer.Common.Models;
using System.Collections.Generic;
using Moq;
using System;

namespace MeuLeeDiaPlayer.Common.Tests
{
    public static class TestUtils
    {

        public static IEnumerable<Song> GenerateSongs(string songPrefix, int nbSongs)
        {
            for (int i = 0; i < nbSongs; i++)
            {
                var mockAudioFileReader = new Mock<IAudioStream>();
                yield return new Song(mockAudioFileReader.Object, $"{songPrefix}{i}", "ArtistName");
            }
        }

        public static IEnumerable<TOut> CallFunctionRepeatedly<TIn, TOut>(Func<TIn, TOut> func, int count, TIn arg)
        {
            for (int i = 0; i < count; i++)
            {
                yield return func(arg);
            }
        }
    }
}
