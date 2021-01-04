using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.Audio;
using MeuLeeDiaPlayer.PlaylistHandler.SongLists;
using Moq;
using System;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.PlaylistHandler.Tests
{
    public static class Utils
    {

        public static IEnumerable<SongDto> GenerateSongs(string songPrefix, int nbSongs)
        {
            for (int i = 0; i < nbSongs; i++)
            {
                var mockAudioFileReader = new Mock<IAudioStream>();
                yield return new SongDto { SongName = $"{songPrefix}{i}", ArtistName = "ArtistName", FileReader = mockAudioFileReader.Object };
            }
        }

        public static IEnumerable<TOut> CallFunctionRepeatedly<TIn, TOut>(Func<TIn, TOut> func, int count, TIn arg)
        {
            for (int i = 0; i < count; i++)
            {
                yield return func(arg);
            }
        }

        public static List<SongDto> GetFollowingSongs(this ISongList songList, int count)
        {
            var list = new List<SongDto>
            {
                songList.CurrentSong
            };

            for (int i = 0; i < count - 1; i++)
            {
                list.Add(songList.MoveNext().CurrentSong);
            }

            return list;
        }
    }
}
