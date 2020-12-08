using MeuLeeDiaPlayer.Common.Enums;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Common.PlaylistPlayMode;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuLeeDiaPlayer.Common.Tests
{
    public class SongListTest
    {
        #region Fields

        private Playlist _bigPlaylist;
        private Playlist _smallPlaylist;

        private PlayMode _noShuffleLoopPlaylist;
        private PlayMode _noShuffleLoopSong;
        private PlayMode _noShuffleNoLoop;


        #endregion

        #region Constants

        private const string _songPrefixBig = "Song";
        private const string _songPrefixSmall = "Chanson";
        private const int _nbSongs = 25;
        private const int _nbLessSongs = 5;
        private const int _queueSize = 10; // should match the constant defined in SongList.cs

        #endregion

        #region SetUp

        [OneTimeSetUp]
        public void SetupClass()
        {
            var songs1 = TestUtils.GenerateSongs(_songPrefixBig, _nbSongs).ToList();
            _bigPlaylist = new Playlist(songs1, "1");

            var songs2 = TestUtils.GenerateSongs(_songPrefixSmall, _nbLessSongs).ToList();
            _smallPlaylist = new Playlist(songs2, "2");

            _noShuffleLoopPlaylist = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopPlaylist);
            _noShuffleLoopSong = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopSong);
            _noShuffleNoLoop = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.NoLoop);
        }

        [SetUp]
        public void SetupTest()
        {
            _smallPlaylist.ResetSongsCounter();
            _bigPlaylist.ResetSongsCounter();
        }

        #endregion

        #region SetPlayMode

        [Test]
        public void When_PlayModeSet_DoesNotChangeCurrentSong()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _bigPlaylist);

            // Act
            var songBefore = songList.CurrentSong;
            songList.PlayMode = _noShuffleLoopSong;
            var songAfter = songList.CurrentSong;

            // Assert
            Assert.AreEqual(songBefore, songAfter);
        }

        [Test]
        public void When_PlayModeSetToNull_ThrowsArgumentNull()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _bigPlaylist);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => songList.PlayMode = null);
        }

        [Test]
        public void When_PlayModeSetToNoShuffleNoLoopAndSmallPlaylist_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _smallPlaylist);

            // Act
            songList.PlayMode = _noShuffleNoLoop;

            // Assert
            Assert.AreEqual(_nbLessSongs, songList.FollowingSongs.Count);
            for (int i = 0; i < _nbLessSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{i}", songList.FollowingSongs[i].SongName);
            }
        }

        [Test]
        public void When_PlayModeSetToNoShuffleNoLoop_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _bigPlaylist);

            // Act
            songList.PlayMode = _noShuffleNoLoop;

            // Assert
            Assert.AreEqual(_queueSize, songList.FollowingSongs.Count);
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i}", songList.FollowingSongs[i].SongName);
            }
        }

        [Test]
        public void When_PlayModeSetToNoShuffleLoopPlaylist_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleLoopPlaylist, _bigPlaylist);

            // Act
            songList.PlayMode = _noShuffleLoopPlaylist;

            // Assert
            Assert.AreEqual(_queueSize, songList.FollowingSongs.Count);
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i}", songList.FollowingSongs[i].SongName);
            }
        }

        #endregion

        #region SetPlaylist

        [Test]
        public void When_PlaylistSet_ChangesCurrentSong()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _bigPlaylist);

            // Act
            var songBefore = songList.CurrentSong;
            songList.Playlist = _smallPlaylist;
            var songAfter = songList.CurrentSong;

            // Assert
            Assert.AreNotEqual(songBefore, songAfter);
        }

        [Test]
        public void When_PlaylistSetToNull_ThrowsArgumentNull()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _bigPlaylist);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => songList.Playlist = null);
        }

        [Test]
        public void When_PlaylistSetToNoShuffleNoLoopAndSmallPlaylist_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _bigPlaylist);

            // Act
            songList.Playlist = _smallPlaylist;

            // Assert
            Assert.AreEqual(_nbLessSongs, songList.FollowingSongs.Count);
            for (int i = 0; i < _nbLessSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{i}", songList.FollowingSongs[i].SongName);
            }
        }

        [Test]
        public void When_PlaylistSetToNoShuffleNoLoop_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _smallPlaylist);

            // Act
            songList.Playlist = _bigPlaylist;

            // Assert
            Assert.AreEqual(_queueSize, songList.FollowingSongs.Count);
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i}", songList.FollowingSongs[i].SongName);
            }
        }

        [Test]
        public void When_PlaylistSetToNoShuffleLoopPlaylist_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleLoopPlaylist, _bigPlaylist);

            // Act
            songList.Playlist = _smallPlaylist;

            // Assert
            Assert.AreEqual(_queueSize, songList.FollowingSongs.Count);
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{i % _nbLessSongs}", songList.FollowingSongs[i].SongName);
            }
        }

        #endregion

        #region MoveNext

        [Test]
        public void When_PlayModeIsNoLoop_MoveNextSongAtLoopEndIsNull()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _smallPlaylist);
            for (int i = 0; i < _nbLessSongs; i++)
            {
                songList.MoveNext();
            }

            // Act
            var song = songList.MoveNext().CurrentSong;

            // Assert
            Assert.IsNull(song);
        }

        [Test]
        public void When_SongListHasFollowingSongs_MoveNextReturnsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleLoopPlaylist, _bigPlaylist);

            // Act
            var songsPlayed = new List<Song>();
            for (int i = 0; i < _nbSongs * 2; i++)
            {
                songsPlayed.Add(songList.CurrentSong);
                songList.MoveNext();
            }

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i % _nbSongs}", songsPlayed[i].SongName);
            }
        }

        [Test]
        public void When_SetNewPlayModeAndCallMoveNext_FollowingSongsContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _smallPlaylist);

            // Act
            songList.MoveNext();
            songList.PlayMode = _noShuffleLoopPlaylist;

            // Assert
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{(i + 1) % _nbLessSongs}", songList.FollowingSongs[i].SongName);
            }
        }

        [Test]
        public void When_SetPlaylistAndCallMoveNext_FollowingSongsContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleLoopPlaylist, _bigPlaylist);

            // Act
            songList.MoveNext();
            songList.Playlist = _smallPlaylist;

            // Assert
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{i % _nbLessSongs}", songList.FollowingSongs[i].SongName);
            }
        }

        #endregion

        #region MovePrevious

        [Test]
        public void When_SongListHasNoPreviousSongs_MovePreviousReturnsNull()
        {
            // Arrange
            var songList = new SongList(_noShuffleLoopPlaylist, _bigPlaylist);

            // Act
            var song = songList.MovePrevious().CurrentSong;

            // Assert
            Assert.IsNull(song);
        }

        [Test]
        public void When_SongListHasPreviousSongs_MovePreviousReturnsCorrectSongs()
        {
            // Arrange
            var songList = new SongList(_noShuffleNoLoop, _bigPlaylist);
            for (int i = 0; i < _nbSongs; i++)
            {
                songList.MoveNext();
            }

            // Act
            var previousSongs = new List<Song>();
            for (int i = 0; i < _nbSongs; i++)
            {
                var song = songList.MovePrevious().CurrentSong;
                previousSongs.Add(song);
            }

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{_nbSongs - 1 - i}", previousSongs[i].SongName);
            }
        }

        [Test]
        public void When_SetNewPlayMode_MovePreviousFollowingSongsContainsCorrectSongs()
        {
            // Arrange
            int goBack = 5;
            var songList = new SongList(_noShuffleLoopPlaylist, _bigPlaylist);
            for (int i = 0; i < _nbSongs; i++) // play some songs
            {
                songList.MoveNext();
            }
            for (int i = 0; i < goBack; i++) // go back 5 songs
            {
                songList.MovePrevious();
            }

            // Act
            songList.PlayMode = _noShuffleNoLoop;

            // Assert
            for (int i = _nbSongs - goBack; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i}", songList.FollowingSongs[i - (_nbSongs - goBack)].SongName);
            }
        }

        #endregion
    }
}
