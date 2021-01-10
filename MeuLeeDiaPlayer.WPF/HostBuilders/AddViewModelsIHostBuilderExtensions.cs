using MeuLeeDiaPlayer.WPF.ViewModels;
using MeuLeeDiaPlayer.WPF.ViewModels.Factories;
using MeuLeeDiaPlayer.WPF.ViewModels.SubViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeuLeeDiaPlayer.WPF.HostBuilders
{
    public static class AddViewModelsIHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(s =>
            {
                s.AddScoped<MainViewModel>()
                 .AddScoped<DownloadVideosViewModel>()
                 .AddScoped<PlaylistsViewModel>()
                 .AddScoped<SettingsViewModel>()
                 .AddScoped<SinglePlaylistViewModel>()
                 .AddScoped<CurrentSongBarViewModel>()
                 .AddSingleton<IViewModelFactory, ViewModelFactory>();
            });
        }
    }
}
