using MeuLeeDiaPlayer.Common.Enums;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.PlaylistHandler;
using MeuLeeDiaPlayer.PlaylistHandler.PlaylistPlayMode;
using NAudio.Wave;
using System;

namespace MeuLeeDiaPlayer.SoundPlayer
{
    // Meant to be used as a singleton with DI framework.
    public class SoundPlayerManager
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

        private readonly SongList _songList;
        private WaveOutEvent _waveOut = null;
        private Song _currentSong = null;
        private float _volume = 0.5f;
        private bool _stopped = false;

        public SoundPlayerManager(SongList songList)
        {
            _songList = songList ?? throw new ArgumentNullException(nameof(songList));
        }

        public void ChangePlayMode(ShuffleStyle shuffleStyle, LoopStyle loopStyle)
        {
            _songList.PlayMode = PlayMode.GetPlayMode(shuffleStyle, loopStyle);
        }

        public void ChangePlaylist(Playlist playlist)
        {
            _songList.Playlist = playlist;
            PlayCurrent();
        }

        public void Pause()
        {
            if (_currentSong is null) return;
            _stopped = true;
            _waveOut?.Stop();
        }

        public void Resume()
        {
            if (_currentSong is null) return;
            _stopped = false;
            _waveOut?.Play();
        }

        public void PlayCurrent()
        {
            var song = _songList.CurrentSong;
            StartPlaying(song);
        }

        public void PlayPrevious()
        {
            if (_currentSong?.FileReader.Stream.CurrentTime > TimeSpan.FromSeconds(5))
            {
                _stopped = true;
                _waveOut!.Stop();
                _currentSong!.FileReader.Stream.Position = 0;
                StartPlaying(_currentSong);
            }
            else
            {
                var song = _songList.MovePrevious().CurrentSong;
                StartPlaying(song);
            }            
        }

        public void PlayNext()
        {
            _stopped = false;
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

            if (_stopped) return;

            var song = _songList.MoveNext().CurrentSong;
            StartPlaying(song);
        }

        private void StartPlaying(Song song)
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
            _currentSong = song;
            _stopped = false;
        }
    }
}
