using MeuLeeDiaPlayer.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeuLeeDiaPlayer.WPF.HostBuilders
{
    public static class AddViewsIHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(s =>
            {
                s.AddScoped(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
            });
        }
    }
}
