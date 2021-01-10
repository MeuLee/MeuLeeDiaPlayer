using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.PlaylistHandler.Models;
using MeuLeeDiaPlayer.PlaylistHandler.PlayModes;
using MeuLeeDiaPlayer.PlaylistHandler.Utils;
using NUnit.Framework;
using System.Linq;

namespace MeuLeeDiaPlayer.PlaylistHandler.Tests.PlayModes
{
    public class PlayModeTest
    {
        private PlaylistLoopInfo _playlist;

        private PlayMode _noShuffleLoopPlaylist;
        private PlayMode _noShuffleLoopSong;
        private PlayMode _noShuffleNoLoop;
        private PlayMode _shuffleLoopPlaylist;
        private PlayMode _shuffleLoopSong;
        private PlayMode _shuffleNoLoop;

        private const string _songPrefix = "Song";
        private const int _nbSongs = 10;

        [OneTimeSetUp]
        public void SetupClass()
        {
            var songs = Utils.GenerateSongs(_songPrefix, _nbSongs).ToConcurrentObservableCollection();
            _playlist = new PlaylistLoopInfo(new PlaylistDto { Songs = songs });

            _noShuffleLoopPlaylist = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopPlaylist);
            _noShuffleLoopSong = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopSong);
            _noShuffleNoLoop = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.NoLoop);
            _shuffleLoopPlaylist = PlayMode.GetPlayMode(ShuffleStyle.Shuffle, LoopStyle.LoopPlaylist);
            _shuffleLoopSong = PlayMode.GetPlayMode(ShuffleStyle.Shuffle, LoopStyle.LoopSong);
            _shuffleNoLoop = PlayMode.GetPlayMode(ShuffleStyle.Shuffle, LoopStyle.NoLoop);
        }

        [SetUp]
        public void SetupTest()
        {
            _playlist.ResetSongsCounter();
            _playlist.LastSongPlayed = null;
        }

        #region NoShuffle

        [Test]
        public void When_PlayModeIsNoShuffleLoopPlaylist_ReceiveSongsInOrder()
        {
            // Arrange
            var playMode = _noShuffleLoopPlaylist;

            // Act
            var songs = Utils.CallFunctionRepeatedly(playMode.GetNextSong, _nbSongs, _playlist).ToList();

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefix}{i}", songs[i].Title);
            }
        }

        [Test]
        public void When_PlayModeIsNoShuffleLoopPlaylist_PlaylistRestartsFromBeginWhenReachEnd()
        {
            // Arrange
            var playMode = _noShuffleLoopPlaylist;

            // Act
            Utils.CallFunctionRepeatedly(playMode.GetNextSong, _nbSongs, _playlist);
            var songs = Utils.CallFunctionRepeatedly(playMode.GetNextSong, _nbSongs, _playlist).ToList();

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefix}{i}", songs[i].Title);
            }
        }

        [Test]
        public void When_PlayModeIsNoShuffleLoopSong_AlwaysReceiveFirstSongOnly()
        {
            // Arrange
            var playMode = _noShuffleLoopSong;

            // Act
            var songs = Utils.CallFunctionRepeatedly(playMode.GetNextSong, _nbSongs, _playlist).ToList();

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefix}0", songs[i].Title);
            }
        }

        [Test]
        public void When_PlayModeIsLoopSongAndEverySongPlayedOnce_GetNextSongDoesntReturnNull()
        {
            // Arrange
            var playMode = _noShuffleLoopSong;
            for (int i = 0; i < _nbSongs; i++)
            {
                playMode.GetNextSong(_playlist);
            }

            // Act
            var song = playMode.GetNextSong(_playlist);

            // Assert
            Assert.IsNotNull(song);
        }

        [Test]
        public void When_PlayModeIsNoShuffleNoLoop_AfterLastSongIsNull()
        {
            // Arrange
            var playMode = _noShuffleNoLoop;

            // Act
            _ = Utils.CallFunctionRepeatedly(playMode.GetNextSong, _nbSongs, _playlist).ToList();
            var nullSong = playMode.GetNextSong(_playlist);

            // Assert
            Assert.IsNull(nullSong);
        }

        #endregion

        #region Shuffle

        [Test]
        public void When_PlayModeIsShuffleLoopPlaylist_EachSongIsPlayedExactlyOnceWithinALoop()
        {
            // Arrange
            var playMode = _shuffleLoopPlaylist;

            // Act
            var songs = Utils.CallFunctionRepeatedly(playMode.GetNextSong, _nbSongs * 2, _playlist);
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
            var playMode = _shuffleLoopSong;

            // Act
            var songs = Utils.CallFunctionRepeatedly(playMode.GetNextSong, _nbSongs, _playlist);
            var songsGrouped = songs.GroupBy(s => s.Title).ToList();

            // Assert
            Assert.AreEqual(1, songsGrouped.Count);
        }

        [Test]
        public void When_PlayModeIsShuffleNoLoop_EachSongIsPlayedExactlyOnce()
        {
            // Arrange
            var playMode = _shuffleNoLoop;

            // Act
            var songs = Utils.CallFunctionRepeatedly(playMode.GetNextSong, _nbSongs, _playlist);

            // Assert
            CollectionAssert.AllItemsAreUnique(songs);
        }

        [Test]
        public void When_PlayModeIsShuffleNoLoop_AfterLastSongIsNull()
        {
            // Arrange
            var playMode = _shuffleNoLoop;

            // Act
            _ = Utils.CallFunctionRepeatedly(playMode.GetNextSong, _nbSongs, _playlist).ToList();
            var nullSong = playMode.GetNextSong(_playlist);

            // Assert
            Assert.IsNull(nullSong);
        }

        #endregion
    }
}