using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.EntityFramework.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeuLeeDiaPlayer.WPF.HostBuilders
{
    public static class AddRepositoriesIHostBuilderExtensions
    {
        public static IHostBuilder AddRepositories(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(s =>
            {
                s.AddScoped<IModelRepository<Playlist>, PlaylistRepository>()
                 .AddScoped<IModelRepository<Song>, SongRepository>()
                 .AddScoped<IPlaylistSongRepository, PlaylistSongRepository>();
            });
        }

    }
}
