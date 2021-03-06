﻿using MeuLeeDiaPlayer.WPF.ViewModels;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.State.ViewNavigator
{
    public enum ViewType
    {
        Playlists,
        DownloadVideos,
        Settings
    }

    public interface IViewNavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
