using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.SongLists;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.PlaylistHandler.PlayModes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using MeuLeeDiaPlayer.PlaylistHandler.Utils;

namespace MeuLeeDiaPlayer.PlaylistHandler.Tests.SongLists
{
    public class SongListTest
    {
        #region Fields

        private PlaylistDto _bigPlaylist;
        private PlaylistDto _smallPlaylist;

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
            var songs1 = Utils.GenerateSongs(_songPrefixBig, _nbSongs).ToConcurrentObservableCollection();
            _bigPlaylist = new PlaylistDto { Songs = songs1 };

            var songs2 = Utils.GenerateSongs(_songPrefixSmall, _nbLessSongs).ToConcurrentObservableCollection();
            _smallPlaylist = new PlaylistDto { Songs = songs2 };

            _noShuffleLoopPlaylist = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopPlaylist);
            _noShuffleLoopSong = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopSong);
            _noShuffleNoLoop = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.NoLoop);
        }

        #endregion

        #region SetPlayMode

        [Test]
        public void When_PlayModeSet_DoesNotChangeCurrentSong()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _bigPlaylist
            };

            // Act
            var songBefore = songList.CurrentSong;
            songList.PlayMode = _noShuffleLoopSong;
            var songAfter = songList.CurrentSong;

            // Assert
            Assert.AreEqual(songBefore, songAfter);
        }

        [Test]
        public void When_PlayModeSet_ToNull_ThrowsArgumentNull()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _bigPlaylist
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => songList.PlayMode = null);
        }

        [Test]
        public void When_PlayModeSet_ToNoShuffleNoLoopAndSmallPlaylist_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _smallPlaylist
            };

            // Act
            songList.PlayMode = _noShuffleNoLoop;
            var songs = songList.GetFollowingSongs(_nbLessSongs);

            // Assert
            Assert.AreEqual(_nbLessSongs, songs.Count);
            for (int i = 0; i < _nbLessSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{i}", songs[i].Title);
            }
        }

        [Test]
        public void When_PlayModeSet_ToNoShuffleNoLoop_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _bigPlaylist
            };

            // Act
            songList.PlayMode = _noShuffleNoLoop;
            var songs = songList.GetFollowingSongs(_queueSize);

            // Assert
            Assert.AreEqual(_queueSize, songs.Count);
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i}", songs[i].Title);
            }
        }

        [Test]
        public void When_PlayModeSet_ToNoShuffleLoopPlaylist_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _bigPlaylist
            };

            // Act
            songList.PlayMode = _noShuffleLoopPlaylist;
            var songs = songList.GetFollowingSongs(_queueSize);

            // Assert
            Assert.AreEqual(_queueSize, songs.Count);
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i}", songs[i].Title);
            }
        }

        [Test]
        public void When_PlayModeSet_ChangesToLoopSong_CurrentSongIsLooped()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _smallPlaylist
            };

            // Act
            var expectedSong = songList.CurrentSong;
            songList.PlayMode = _noShuffleLoopSong;
            var songs = songList.GetFollowingSongs(_queueSize);

            // Assert
            foreach (var actualSong in songs)
            {
                Assert.AreEqual(expectedSong.Title, actualSong.Title);
            }
        }

        [Test]
        public void When_PlayModeSet_MovePreviousFollowingSongsContainsCorrectSongs()
        {
            // Arrange
            int goBack = 5;
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _bigPlaylist
            };

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
            var songs = songList.GetFollowingSongs(_queueSize);

            // Assert
            for (int i = _nbSongs - goBack; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i}", songs[i - (_nbSongs - goBack)].Title);
            }
        }

        #endregion

        #region SetPlaylist

        [Test]
        public void When_PlaylistSet_ChangesCurrentSong()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _bigPlaylist
            };

            // Act
            var songBefore = songList.CurrentSong;
            songList.Playlist = _smallPlaylist;
            var songAfter = songList.CurrentSong;

            // Assert
            Assert.AreNotEqual(songBefore, songAfter);
        }

        [Test]
        public void When_PlaylistSet_ToNoShuffleNoLoopAndSmallPlaylist_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _bigPlaylist
            };

            // Act
            songList.Playlist = _smallPlaylist;
            var songs = songList.GetFollowingSongs(_nbLessSongs);

            // Assert
            Assert.AreEqual(_nbLessSongs, songs.Count);
            for (int i = 0; i < _nbLessSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{i}", songs[i].Title);
            }
        }

        [Test]
        public void When_PlaylistSet_ToNoShuffleNoLoop_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _smallPlaylist
            };

            // Act
            songList.Playlist = _bigPlaylist;
            var songs = songList.GetFollowingSongs(_queueSize);

            // Assert
            Assert.AreEqual(_queueSize, songs.Count);
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i}", songs[i].Title);
            }
        }

        [Test]
        public void When_PlaylistSet_ToNoShuffleLoopPlaylist_ContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _bigPlaylist
            };

            // Act
            songList.Playlist = _smallPlaylist;
            var songs = songList.GetFollowingSongs(_queueSize);

            // Assert
            Assert.AreEqual(_queueSize, songs.Count);
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{i % _nbLessSongs}", songs[i].Title);
            }
        }

        #endregion

        #region MoveNext

        [Test]
        public void When_MoveNext_PlayModeIsNoLoop_SongAtLoopEndIsNull()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _smallPlaylist
            };

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
        public void When_MoveNext_SongListHasFollowingSongs_ReturnsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _bigPlaylist
            };

            // Act
            var songsPlayed = new List<SongDto>();
            for (int i = 0; i < _nbSongs * 2; i++)
            {
                songsPlayed.Add(songList.CurrentSong);
                songList.MoveNext();
            }

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{i % _nbSongs}", songsPlayed[i].Title);
            }
        }

        [Test]
        public void When_MoveNextAndPlayModeSet_FollowingSongsContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _smallPlaylist
            };

            // Act
            songList.MoveNext();
            songList.PlayMode = _noShuffleLoopPlaylist;
            var songs = songList.GetFollowingSongs(_queueSize);

            // Assert
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{(i + 1) % _nbLessSongs}", songs[i].Title);
            }
        }

        [Test]
        public void When_MoveNextAndPlaylistSet_FollowingSongsContainsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _bigPlaylist
            };

            // Act
            songList.MoveNext();
            songList.Playlist = _smallPlaylist;
            var songs = songList.GetFollowingSongs(_queueSize);

            // Assert
            for (int i = 0; i < _queueSize; i++)
            {
                Assert.AreEqual($"{_songPrefixSmall}{i % _nbLessSongs}", songs[i].Title);
            }
        }

        #endregion

        #region MovePrevious

        [Test]
        public void When_MovePrevious_SongListHasNoPreviousSongs_ReturnsNull()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _bigPlaylist
            };

            // Act
            var song = songList.MovePrevious().CurrentSong;

            // Assert
            Assert.IsNull(song);
        }

        [Test]
        public void When_MovePrevious_SongListHasPreviousSongs_ReturnsCorrectSongs()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _bigPlaylist
            };

            for (int i = 0; i < _nbSongs; i++)
            {
                songList.MoveNext();
            }

            // Act
            var previousSongs = new List<SongDto>();
            for (int i = 0; i < _nbSongs; i++)
            {
                var song = songList.MovePrevious().CurrentSong;
                previousSongs.Add(song);
            }

            // Assert
            for (int i = 0; i < _nbSongs; i++)
            {
                Assert.AreEqual($"{_songPrefixBig}{_nbSongs - 1 - i}", previousSongs[i].Title);
            }
        }

        [Test]
        public void When_MovePrevious_CalledTwice_SecondCallDoesNothing()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _bigPlaylist
            };
            var expected = songList.CurrentSong;

            // Act
            var actual = songList.MovePrevious().MovePrevious().MoveNext().CurrentSong;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region PlaySong

        [Test]
        public void When_PlaySong_SongArgIsNull_ThrowsArgumentNull()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _smallPlaylist
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => songList.Play(null));
        }

        [Test]
        public void When_PlaySong_SongArgIsInPlaylist_CurrentSongSetToArg()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _smallPlaylist
            };
            var expectedSong = _smallPlaylist.Songs[1];

            // Act
            songList.Play(expectedSong);

            // Assert
            Assert.AreEqual(expectedSong, songList.CurrentSong);
        }

        [Test]
        public void When_PlaySong_SongArgIsNotInPlaylist_CurrentSongDoesNotChange()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _smallPlaylist
            };
            var expectedSong = songList.CurrentSong;

            // Act
            songList.Play(_bigPlaylist.Songs[1]);

            // Assert
            Assert.AreEqual(expectedSong, songList.CurrentSong);
        }

        [Test]
        public void When_PlaySong_SongArgFileReaderIsNull_CurrentSongDoesNotChange()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _smallPlaylist
            };
            var expectedSong = songList.CurrentSong;

            // Act
            songList.Play(new SongDto());

            // Assert
            Assert.AreEqual(expectedSong, songList.CurrentSong);
        }

        #endregion

        #region CurrentSong

        [Test]
        public void When_CurrentSongGet_PlayModeIsNull_ReturnsNull()
        {
            // Arrange
            var songList = new SongList
            {
                Playlist = _smallPlaylist
            };

            // Act & Assert
            Assert.IsNull(songList.CurrentSong);
        }

        [Test]
        public void When_CurrentSongGet_PlayListIsNull_ReturnsNull()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist
            };

            // Act & Assert
            Assert.IsNull(songList.CurrentSong);
        }

        [Test]
        public void When_CurrentSongGet_HasFollowingSongs_MovePreviousReturnsCorrectSong()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _smallPlaylist
            };

            // Act
            var expected = songList.MoveNext().CurrentSong;
            songList.MoveNext();
            var actual = songList.MovePrevious().CurrentSong;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void When_CurrentSong_HasNoPreviousSongs_MovePreviousReturnsNull()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _smallPlaylist
            };

            // Act
            var actual = songList.MovePrevious().CurrentSong;

            // Assert
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void When_CurrentSong_PlayedLastSong_MoveNextReturnsNull()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleNoLoop,
                Playlist = _smallPlaylist
            };

            // Act
            for (int i = 0; i < _nbLessSongs; i++)
            {
                songList.MoveNext();
            }
            var actual = songList.MoveNext().CurrentSong;

            // Assert
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void When_CurrentSong_HasNoFollowingSongs_MoveNextReturnsCorrectSong()
        {
            // Arrange
            var songList = new SongList
            {
                PlayMode = _noShuffleLoopPlaylist,
                Playlist = _smallPlaylist
            };
            var expected = _smallPlaylist.Songs[1];

            // Act
            for (int i = 0; i < _nbLessSongs; i++)
            {
                songList.MoveNext();
            }
            var actual = songList.MoveNext().CurrentSong;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
