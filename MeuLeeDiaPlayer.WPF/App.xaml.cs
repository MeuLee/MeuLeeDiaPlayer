using AutoMapper;
using MeuLeeDiaPlayer.Common.Profiles;
using MeuLeeDiaPlayer.Common.Services;
using MeuLeeDiaPlayer.EntityFramework.Context;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.PlaylistHandler.SongLists;
using MeuLeeDiaPlayer.SoundPlayer;
using MeuLeeDiaPlayer.WPF.State.PlaylistNavigator;
using MeuLeeDiaPlayer.WPF.State.SongNavigator;
using MeuLeeDiaPlayer.WPF.State.ViewNavigator;
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
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PlaylistProfile>();
                cfg.AddProfile<SongProfile>();
            }));

            var services = new ServiceCollection()
                .AddDbContext<MeuLeeDiaPlayerDbContext>()

                .AddSingleton<IRepository<Playlist>, GenericRepository<Playlist>>()
                .AddSingleton<IRepository<Song>, GenericRepository<Song>>()

                .AddSingleton<ISongLoader, SongLoader>()
                .AddSingleton<ISongList, SongList>()
                .AddSingleton<ISoundPlayerManager, SoundPlayerManager>()

                .AddScoped<MainViewModel>()
                .AddScoped<DownloadVideosViewModel>()
                .AddScoped<PlaylistsViewModel>()
                .AddScoped<SettingsViewModel>()
                .AddScoped(s => new MainWindow(s.GetRequiredService<MainViewModel>()))

                .AddScoped<IViewNavigator, ViewNavigator>()
                .AddScoped<IPlaylistNavigator, PlaylistNavigator>()
                .AddScoped<ISongNavigator, SongNavigator>()

                .AddSingleton<IViewModelFactory, ViewModelFactory>()

                .AddSingleton<IMapper, Mapper>(s => mapper);

            return services.BuildServiceProvider();
        }
    }
}
