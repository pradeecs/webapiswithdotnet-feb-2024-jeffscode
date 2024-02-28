
using System.Threading.Channels;

namespace IssuesApi.Features.Catalog;

public class ChannelListenerBackgroundSoftwareMonitor(Channel<Guid> channel, ILogger<ChannelListenerBackgroundSoftwareMonitor> logger) : BackgroundService
{

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await channel.Reader.WaitToReadAsync(stoppingToken))
        {
            if (channel.Reader.TryRead(out var guid))
            {
                logger.LogInformation("We got something from the channel, yo {guid}", guid);
            }
        }
    }
}
