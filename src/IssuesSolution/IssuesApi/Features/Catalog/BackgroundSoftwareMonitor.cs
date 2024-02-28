
using IssuesApi.Services;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Features.Catalog;

public class BackgroundSoftwareMonitor(ILogger<BackgroundSoftwareMonitor> logger, IServiceProvider sp) : BackgroundService
{
    private PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromHours(1)); // .NET 6

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync())
        {
            await DoWork();
        }
    }

    private async Task DoWork()
    {
        using (var scope = sp.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IssuesDataContext>();

            var retired = await context.SoftwareCatalog
                .Where(c => c.DateRetired != null && c.RetirementNotificationsSent == false)
                .ToListAsync();

            foreach (var r in retired)
            {
                using (logger.BeginScope("Retired Software"))
                {
                    // remove it from the database, send an email, do whatever.
                    logger.LogInformation("Item {id} is Retired", r.Id);
                    // Do all that other work, notifying the user their issue has been cancelled, etc.
                    r.RetirementNotificationsSent = true;
                }
            }
            await context.SaveChangesAsync();

        } // Dispose
    }
}


