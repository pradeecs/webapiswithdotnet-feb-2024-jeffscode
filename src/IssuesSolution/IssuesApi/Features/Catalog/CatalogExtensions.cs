using System.Threading.Channels;

namespace IssuesApi.Features.Catalog;

public static class CatalogExtensions
{
    public static IServiceCollection AddCatalogFeature(this IServiceCollection services)
    {
        //services.AddScoped<IManageTheSoftwareCatalog, EntityFrameworkSoftwareCatalogManager>();

        var boundedChannel = Channel.CreateBounded<Guid>(new BoundedChannelOptions(10)
        {

            SingleReader = true,
            SingleWriter = true,
        });
        services.AddSingleton(Channel.CreateUnbounded<Guid>());
        services.AddHostedService<ChannelListenerBackgroundSoftwareMonitor>();
        // anything else - more later here.
        // services.AddHostedService<BackgroundSoftwareMonitor>();
        return services;
    }
}
