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
        public int Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                if (_waveOut is not null)
                {
                    _waveOut.Volume = Volume / 100f;
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

        public ShuffleStyle ShuffleStyle
        {
            get => _shuffleStyle;
            set
            {
                _shuffleStyle = value;
                _songList.PlayMode = PlayMode.GetPlayMode(ShuffleStyle, LoopStyle);
                OnPropertyChanged(nameof(ShuffleStyle));
            }
        }

        public LoopStyle LoopStyle
        {
            get => _loopStyle;
            set
            {
                _loopStyle = value;
                _songList.PlayMode = PlayMode.GetPlayMode(ShuffleStyle, LoopStyle);
                OnPropertyChanged(nameof(LoopStyle));
            }
        }

        public PlaylistDto CurrentPlaylist => _songList.Playlist;

        private ShuffleStyle _shuffleStyle = ShuffleStyle.Shuffle; // TODO unhardcode this ^_^
        private LoopStyle _loopStyle = LoopStyle.LoopPlaylist; // TODO unhardcode this ^_^
        private readonly ISongList _songList;
        private IWavePlayer _waveOut = null;
        private SongDto _currentSong = null;
        private int _volume = 30; // TODO unhardcode this ^_^
        private bool _stopped = false;

        public SoundPlayerManager(ISongList songList)
        {
            _songList = songList ?? throw new ArgumentNullException(nameof(songList));
            _songList.PlayMode = PlayMode.GetPlayMode(_shuffleStyle, _loopStyle); 
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
            if (CurrentSong is null)
            {
                Stopped = true;
                return;
            }
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
            var song = _songList.CurrentSong ?? _songList.MoveNext().CurrentSong;
            StartPlaying(song);
        }

        public void PlayPrevious()
        {
            var song = _songList.MovePrevious().CurrentSong;
            StartPlaying(song);
        }

        public void PlayNext()
        {
            if (LoopStyle == LoopStyle.LoopSong)
            {
                LoopStyle = LoopStyle.LoopPlaylist;
            }

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

            CurrentSong = song;

            if (song?.FileReader is null) return;

            song.FileReader.Stream.Position = 0;
            _waveOut = new WaveOutEvent
            {
                Volume = Volume / 100f,
                DesiredLatency = 175
            };
            _waveOut.PlaybackStopped += PlayNext;
            _waveOut.Init(song.FileReader.Stream);
            _waveOut.Play();
            Stopped = false;
        }
    }
}
