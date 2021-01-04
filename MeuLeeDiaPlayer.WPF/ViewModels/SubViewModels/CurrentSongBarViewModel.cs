using MeuLeeDiaPlayer.Services.SoundPlayer;
using MeuLeeDiaPlayer.WPF.Commands;
using NAudio.Wave;
using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace MeuLeeDiaPlayer.WPF.ViewModels.SubViewModels
{
    public class CurrentSongBarViewModel : BaseViewModel
    {
        public string CurrentTimeFormat => $"{Reader?.CurrentTime ?? TimeSpan.FromSeconds(0):mm\\:ss}";
        public ISoundPlayerManager SoundPlayerManager { get; }
        public ICommand NextSongCommand { get; }
        public ICommand PauseResumeCurrentSongCommand { get; }
        public ICommand PreviousSongCommand { get; }
        public ICommand SetLoopStyleCommand { get; }
        public ICommand SetShuffleStyleCommand { get; }

        public double SliderPosition
        {
            get => _sliderPosition;
            set
            {
                _sliderPosition = value;
                if (Reader != null)
                {
                    Reader.CurrentTime = TimeSpan.FromSeconds(SliderPosition);
                }                
                OnPropertyChanged(nameof(SliderPosition));
                OnPropertyChanged(nameof(CurrentTimeFormat));
            }
        }

        private AudioFileReader Reader => SoundPlayerManager.CurrentSong?.FileReader.Stream;

        private double _sliderPosition;
        private DispatcherTimer _timer;

        public CurrentSongBarViewModel(
            ISoundPlayerManager soundPlayerManager,
            NextSongCommand nextSongCommand, 
            PauseResumeCurrentSongCommand pauseResumeCurrentSongCommand, 
            PreviousSongCommand previousSongCommand,
            SetLoopStyleCommand setLoopStyleCommand, 
            SetShuffleStyleCommand setShuffleStyleCommand)
        {
            SoundPlayerManager = soundPlayerManager;
            PreviousSongCommand = previousSongCommand;
            NextSongCommand = nextSongCommand;
            PauseResumeCurrentSongCommand = pauseResumeCurrentSongCommand;
            SetShuffleStyleCommand = setShuffleStyleCommand;
            SetLoopStyleCommand = setLoopStyleCommand;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Reader == null) return;

            SliderPosition = Reader.CurrentTime.TotalSeconds;
            OnPropertyChanged(nameof(SliderPosition));
            OnPropertyChanged(nameof(CurrentTimeFormat));
        }
    }
}
