using AutoMapper;
using MeuLeeDiaPlayer.Common.Profiles;
using MeuLeeDiaPlayer.EntityFramework.Context;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using MeuLeeDiaPlayer.PlaylistHandler.SongLists;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.PlaylistRetrievers;
using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using MeuLeeDiaPlayer.WPF.Commands;
using MeuLeeDiaPlayer.WPF.State.ViewNavigator;
using MeuLeeDiaPlayer.WPF.ViewModels;
using MeuLeeDiaPlayer.WPF.ViewModels.Factories;
using MeuLeeDiaPlayer.WPF.ViewModels.SubViewModels;
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
                .AddSingleton<IPlaylistHolder, PlaylistHolder>()
                .AddSingleton<IPlaylistRetriever, PlaylistRetriever>()

                .AddSingleton<UpdateCurrentPlaylistCommand>()
                .AddSingleton<UpdateCurrentSongCommand>()
                .AddSingleton<NextSongCommand>()
                .AddSingleton<PauseResumeCurrentSongCommand>()
                .AddSingleton<SetLoopStyleCommand>()
                .AddSingleton<SetShuffleStyleCommand>()
                .AddSingleton<PreviousSongCommand>()
                .AddSingleton<PlayPlaylistCommand>()

                .AddScoped<MainViewModel>()
                .AddScoped<DownloadVideosViewModel>()
                .AddScoped<PlaylistsViewModel>()
                .AddScoped<SettingsViewModel>()
                .AddScoped<SinglePlaylistViewModel>()
                .AddScoped<CurrentSongBarViewModel>()
                .AddScoped(s => new MainWindow(s.GetRequiredService<MainViewModel>()))

                .AddScoped<IViewNavigator, ViewNavigator>()

                .AddSingleton<IViewModelFactory, ViewModelFactory>()

                .AddSingleton<IMapper, Mapper>(s => mapper);

            return services.BuildServiceProvider();
        }
    }
}
