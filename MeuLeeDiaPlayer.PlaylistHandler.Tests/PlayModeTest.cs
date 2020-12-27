using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.PlaylistHandler.Models;
using MeuLeeDiaPlayer.PlaylistHandler.PlaylistPlayMode;
using NUnit.Framework;
using System.Linq;

namespace MeuLeeDiaPlayer.PlaylistHandler.Tests
{
    public class PlayModeTest
    {
        private PlaylistLoopInfo _playlist;
        private PlayMode _playMode;

        private const string _songPrefix = "Song";
        private const int _nbSongs = 10;

        [OneTimeSetUp]
        public void SetupClass()
        {
            var songs = TestUtils.GenerateSongs(_songPrefix, _nbSongs).ToList();
            _playlist = new PlaylistLoopInfo(new PlaylistDto { Songs = songs });
        }

        [SetUp]
        public void SetupTest()
        {
            _playlist.ResetSongsCounter();
            _playMode = null;
        }

        #region NoShuffle

        [Test]
        public void When_PlayModeIsNoShuffleLoopPlaylist_ReceiveSongsInOrder()
        {
            // Arrange
            _playMode = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopPlaylist);

            // Act
            var songs = TestUtils.CallFunctionRepeatedly(_playMode.GetNextSong, _nbSongs, _playlist).ToList();

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefix}{i}", songs[i].Song.SongName);
            }
        }

        [Test]
        public void When_PlayModeIsNoShuffleLoopPlaylist_PlaylistRestartsFromBeginWhenReachEnd()
        {
            // Arrange
            _playMode = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopPlaylist);

            // Act
            TestUtils.CallFunctionRepeatedly(_playMode.GetNextSong, _nbSongs, _playlist);
            var songs = TestUtils.CallFunctionRepeatedly(_playMode.GetNextSong, _nbSongs, _playlist).ToList();

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefix}{i}", songs[i].Song.SongName);
            }
        }

        [Test]
        public void When_PlayModeIsNoShuffleLoopSong_AlwaysReceiveFirstSongOnly()
        {
            // Arrange
            _playMode = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopSong);

            // Act
            var songs = TestUtils.CallFunctionRepeatedly(_playMode.GetNextSong, _nbSongs, _playlist).ToList();

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefix}0", songs[i].Song.SongName);
            }
        }

        [Test]
        public void When_PlayModeIsNoShuffleNoLoop_AfterLastSongIsNull()
        {
            // Arrange
            _playMode = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.NoLoop);

            // Act
            _ = TestUtils.CallFunctionRepeatedly(_playMode.GetNextSong, _nbSongs, _playlist).ToList();
            var nullSong = _playMode.GetNextSong(_playlist).Song;

            // Assert
            Assert.IsNull(nullSong);
        }

        #endregion

        #region Shuffle

        [Test]
        public void When_PlayModeIsShuffleLoopPlaylist_EachSongIsPlayedExactlyOnceWithinALoop()
        {
            // Arrange
            _playMode = PlayMode.GetPlayMode(ShuffleStyle.Shuffle, LoopStyle.LoopPlaylist);

            // Act
            var songs = TestUtils.CallFunctionRepeatedly(_playMode.GetNextSong, _nbSongs * 2, _playlist);
            var songsFirstLoop = songs.Take(_nbSongs);
            var songsSecondLoop = songs.Skip(_nbSongs).Take(_nbSongs);

            // Assert
            CollectionAssert.AllItemsAreUnique(songsFirstLoop);
            CollectionAssert.AllItemsAreUnique(songsSecondLoop);
        }

        [Test]
        public void When_PlayModeIsShuffleLoopSong_AlwaysReceiveTheSameSongOnly()
        {
            // Arrange
            _playMode = PlayMode.GetPlayMode(ShuffleStyle.Shuffle, LoopStyle.LoopSong);

            // Act
            var songs = TestUtils.CallFunctionRepeatedly(_playMode.GetNextSong, _nbSongs, _playlist);
            var songsGrouped = songs.GroupBy(s => s.Song.SongName).ToList();

            // Assert
            Assert.AreEqual(1, songsGrouped.Count);
        }

        [Test]
        public void When_PlayModeIsShuffleNoLoop_EachSongIsPlayedExactlyOnce()
        {
            // Arrange
            _playMode = PlayMode.GetPlayMode(ShuffleStyle.Shuffle, LoopStyle.NoLoop);

            // Act
            var songs = TestUtils.CallFunctionRepeatedly(_playMode.GetNextSong, _nbSongs, _playlist);

            // Assert
            CollectionAssert.AllItemsAreUnique(songs);
        }

        [Test]
        public void When_PlayModeIsShuffleNoLoop_AfterLastSongIsNull()
        {
            // Arrange
            _playMode = PlayMode.GetPlayMode(ShuffleStyle.Shuffle, LoopStyle.NoLoop);

            // Act
            _ = TestUtils.CallFunctionRepeatedly(_playMode.GetNextSong, _nbSongs, _playlist).ToList();
            var nullSong = _playMode.GetNextSong(_playlist).Song;

            // Assert
            Assert.IsNull(nullSong);
        }

        #endregion

        [Test]
        public void When_PlayModeIsLoopSongAndEverySongPlayedOnce_GetNextSongDoesntReturnNull()
        {
            // Arrange
            _playMode = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopSong);
            for (int i = 0; i < _nbSongs; i++)
            {
                _playMode.GetNextSong(_playlist);
            }

            // Act
            var song = _playMode.GetNextSong(_playlist);

            // Assert
            Assert.IsNotNull(song);
        }
    }
}