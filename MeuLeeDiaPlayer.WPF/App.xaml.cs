using MeuLeeDiaPlayer.Common.Services;
using MeuLeeDiaPlayer.EntityFramework.Context;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.WPF.State.PlaylistNavigator;
using MeuLeeDiaPlayer.WPF.State.ViewNavigators;
using MeuLeeDiaPlayer.WPF.ViewModels;
using MeuLeeDiaPlayer.WPF.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace MeuLeeDiaPlayer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceProvider = CreateServiceProvider();

            var window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection()
                .AddDbContext<MeuLeeDiaPlayerDbContext>()
                .AddSingleton<IRepository<Playlist>, GenericRepository<Playlist>>()
                .AddSingleton<IRepository<Song>, GenericRepository<Song>>()
                .AddSingleton<ISongLoader, SongLoader>()
                .AddScoped<MainViewModel>()
                .AddScoped<DownloadVideosViewModel>()
                .AddScoped<PlaylistsViewModel>()
                .AddScoped<SettingsViewModel>()
                .AddScoped<IViewNavigator, ViewNavigator>()
                .AddScoped<IPlaylistNavigator, PlaylistNavigator>()
                .AddSingleton<IViewModelFactory, ViewModelFactory>()
                .AddScoped(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
