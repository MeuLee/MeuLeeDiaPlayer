using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Services.SongLoaders;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MeuLeeDiaPlayer.WPF.Views.Forms
{
    /// <summary>
    /// Interaction logic for CreatePlaylistWindow.xaml
    /// </summary>
    public partial class CreatePlaylistWindow : Window
    {
        public string PlaylistName { get; private set; }
        public List<SongDto> CheckedSongs { get; }

        public ISongLoader SongLoader { get; }

        public CreatePlaylistWindow(string windowTitle, ISongLoader songLoader)
            : this(windowTitle, songLoader, null) { }

        public CreatePlaylistWindow(string windowTitle, ISongLoader songLoader, PlaylistDto playlist)
        {
            SongLoader = songLoader;
            InitializeComponent();
            Title = windowTitle;
            PlaylistName = playlist?.Name;
            TbPlaylistName.Text = playlist?.Name ?? string.Empty;
            CheckedSongs = playlist?.Songs.ToList() ?? new();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string playlistName = TbPlaylistName.Text;
            if (string.IsNullOrWhiteSpace(playlistName))
            {
                DialogResult = false;
            }
            else
            {
                DialogResult = true;
                PlaylistName = playlistName;
            }
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement elem 
                && elem.DataContext is SongDto song
                && !CheckedSongs.Contains(song))
            {
                CheckedSongs.Add(song);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement elem && elem.DataContext is SongDto song)
            {
                CheckedSongs.Remove(song);
            }
        }
    }
}
