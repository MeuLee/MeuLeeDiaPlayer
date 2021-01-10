using MeuLeeDiaPlayer.PlaylistHandler.SongLists;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.PlaylistRetrievers;
using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using MeuLeeDiaPlayer.WPF.State.ViewNavigator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeuLeeDiaPlayer.WPF.HostBuilders
{
    public static class AddServicesIHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(s =>
            {
                s.AddSingleton<ISongLoader, SongLoader>()
                 .AddSingleton<ISongList, SongList>()
                 .AddSingleton<ISoundPlayerManager, SoundPlayerManager>()
                 .AddSingleton<IPlaylistHolder, PlaylistHolder>()
                 .AddSingleton<IPlaylistRetriever, PlaylistRetriever>()
                 .AddScoped<IViewNavigator, ViewNavigator>();
            });
        }
    }
}
