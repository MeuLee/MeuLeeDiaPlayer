using MeuLeeDiaPlayer.WPF.Commands.CurrentSongBar;
using MeuLeeDiaPlayer.WPF.Commands.DownloadVideos;
using MeuLeeDiaPlayer.WPF.Commands.Playlists;
using MeuLeeDiaPlayer.WPF.Commands.SinglePlaylist;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeuLeeDiaPlayer.WPF.HostBuilders
{
    public static class AddCommandsIHostBuilderExtensions
    {
        public static IHostBuilder AddCommands(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(s =>
            {
                s.AddSingleton<UpdateCurrentPlaylistCommand>()
                 .AddSingleton<UpdateCurrentSongCommand>()
                 .AddSingleton<NextSongCommand>()
                 .AddSingleton<PauseResumeCurrentSongCommand>()
                 .AddSingleton<SetLoopStyleCommand>()
                 .AddSingleton<SetShuffleStyleCommand>()
                 .AddSingleton<PreviousSongCommand>()
                 .AddSingleton<PlayPlaylistCommand>()
                 .AddSingleton<ShowCreatePlaylistDialogCommand>()
                 .AddSingleton<ShowEditPlaylistDialogCommand>()
                 .AddSingleton<DeletePlaylistCommand>()
                 .AddSingleton<DownloadYtLinkCommand>();
            });
        }
    }
}
