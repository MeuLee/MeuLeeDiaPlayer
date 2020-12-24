using MeuLeeDiaPlayer.WPF.State.ViewNavigators;
using System;

namespace MeuLeeDiaPlayer.WPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly PlaylistsViewModel _playlistViewModel;
        private readonly DownloadVideosViewModel _downloadVideosViewModel;
        private readonly SettingsViewModel _settingsViewModel;

        public ViewModelFactory(
            PlaylistsViewModel playlistViewModel, 
            DownloadVideosViewModel downloadVideosViewModel, 
            SettingsViewModel settingsViewModel)
        {
            _playlistViewModel = playlistViewModel;
            _downloadVideosViewModel = downloadVideosViewModel;
            _settingsViewModel = settingsViewModel;
        }

        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Playlists => _playlistViewModel,
                ViewType.DownloadVideos => _downloadVideosViewModel,
                ViewType.Settings => _settingsViewModel,
                _ => throw new ArgumentException("The ViewType does not have a ViewModel", nameof(viewType)),
            };
        }
    }
}
