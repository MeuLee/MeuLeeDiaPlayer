using MeuLeeDiaPlayer.EntityFramework.Audio;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using Moq;
using System;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.PlaylistHandler.Tests
{
    public static class TestUtils
    {

        public static IEnumerable<Song> GenerateSongs(string songPrefix, int nbSongs)
        {
            for (int i = 0; i < nbSongs; i++)
            {
                var mockAudioFileReader = new Mock<IAudioStream>();
                yield return new Song { SongName = $"{songPrefix}{i}", ArtistName = "ArtistName", FileReader = mockAudioFileReader.Object };
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
