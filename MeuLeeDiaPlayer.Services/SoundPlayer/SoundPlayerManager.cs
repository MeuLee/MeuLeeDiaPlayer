using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.PlaylistHandler.PlayModes;
using MeuLeeDiaPlayer.PlaylistHandler.SongLists;
using NAudio.Wave;
using System;

namespace MeuLeeDiaPlayer.Services.SoundPlayer
{
    public class SoundPlayerManager : ObservableObject, ISoundPlayerManager
    {
        public float Volume
        {
            get => _volume;
            set
            {
                value = (float)Math.Round(value, 1);
                if (value is > 1f or < 0f) throw new ArgumentOutOfRangeException(nameof(value));
                _volume = value;
                if (_waveOut is not null)
                {
                    _waveOut.Volume = _volume;
                }
            }
        }

        public SongDto CurrentSong
        {
            get => _currentSong;
            private set
            {
                _currentSong = value;
                OnPropertyChanged(nameof(CurrentSong));
            }
        }

        public bool Stopped
        {
            get => _stopped;
            set
            {
                _stopped = value;
                OnPropertyChanged(nameof(Stopped));
            }
        }

        private readonly ISongList _songList;
        private WaveOutEvent _waveOut = null;
        private SongDto _currentSong = null;
        private float _volume = 0.5f; // TODO unhardcode this ^_^
        private bool _stopped = false;

        public SoundPlayerManager(ISongList songList)
        {
            _songList = songList ?? throw new ArgumentNullException(nameof(songList));
            _songList.PlayMode = PlayMode.GetPlayMode(ShuffleStyle.NoShuffle, LoopStyle.LoopPlaylist); // TODO unhardcode this ^_^
        }

        public void ChangePlayMode(ShuffleStyle shuffleStyle, LoopStyle loopStyle)
        {
            _songList.PlayMode = PlayMode.GetPlayMode(shuffleStyle, loopStyle);
        }

        public void ChangePlaylist(PlaylistDto playlist)
        {
            _songList.Playlist = playlist;
        }

        public void PlayCurrentPlaylist()
        {
            PlayCurrent();
        }

        public void PlaySong(SongDto song)
        {
            CurrentSong = song;
            _songList.Play(song);
            StartPlaying(song);
        }

        public void PauseOrResume()
        {
            if (CurrentSong is null) return;
            if (Stopped)
            {
                _waveOut?.Play();
            }
            else
            {
                _waveOut?.Stop();
            }
            Stopped = !Stopped;
        }

        public void PlayCurrent()
        {
            var song = _songList.CurrentSong;
            StartPlaying(song);
        }

        public void PlayPrevious()
        {
            if (CurrentSong?.FileReader.Stream.CurrentTime > TimeSpan.FromSeconds(5))
            {
                Stopped = true;
                _waveOut!.Stop();
                CurrentSong!.FileReader.Stream.Position = 0;
                StartPlaying(CurrentSong);
            }
            else
            {
                var song = _songList.MovePrevious().CurrentSong;
                StartPlaying(song);
            }
        }

        public void PlayNext()
        {
            Stopped = false;
            PlayNext(null, null);
        }

        /// <summary>
        /// This event can be raised in 3 ways: 
        /// There was an exception, the user manually stopped the song, the song ended. 
        /// Each case is treated by this method.
        /// The playlist only keeps playing when there is no exception and the user didn't stop the song himself.
        /// </summary>
        private void PlayNext(object sender, StoppedEventArgs e)
        {
            if (e?.Exception is not null)
            {
                throw e!.Exception;
            }

            if (Stopped) return;

            var song = _songList.MoveNext().CurrentSong;
            StartPlaying(song);
        }

        private void StartPlaying(SongDto song)
        {
            if (_waveOut is not null)
            {
                _waveOut.PlaybackStopped -= PlayNext;
                _waveOut.Dispose();
            }

            if (song is null) return;

            song.FileReader.Stream.Position = 0;
            _waveOut = new WaveOutEvent { Volume = _volume };
            _waveOut.PlaybackStopped += PlayNext;
            _waveOut.Init(song.FileReader.Stream);
            _waveOut.Play();
            CurrentSong = song;
            Stopped = false;
        }
    }
}
