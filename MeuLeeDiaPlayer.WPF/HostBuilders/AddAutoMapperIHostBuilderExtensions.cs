using AutoMapper;
using MeuLeeDiaPlayer.Common.Profiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeuLeeDiaPlayer.WPF.HostBuilders
{
    public static class AddAutoMapperIHostBuilderExtensions
    {
        public static IHostBuilder AddAutoMapper(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(s =>
            {
                s.AddSingleton<IMapper, Mapper>(s => new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<PlaylistProfile>();
                    cfg.AddProfile<SongProfile>();
                })));
            });
        }
    }
}
