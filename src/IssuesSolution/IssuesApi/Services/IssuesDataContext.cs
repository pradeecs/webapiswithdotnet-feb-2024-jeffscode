using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Services;

public class IssuesDataContext(DbContextOptions<IssuesDataContext> options) : DbContext(options)
{
    public DbSet<SoftwareItem> SoftwareCatalog { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SoftwareItem>().HasData([
            new SoftwareItem { Id = Guid.NewGuid(), Title="Microsoft Word", Version = "97", DateAdded =DateTimeOffset.Now}
            ]);
        base.OnModelCreating(modelBuilder);
    }

    public IQueryable<SoftwareItem> ActiveSoftwareItems()
    {
        return SoftwareCatalog.Where(i => i.DateRetired == null);
    }
}

// "Entity" 
public class SoftwareItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty; // Excel
    public string Version { get; set; } = string.Empty;

    public DateTimeOffset DateAdded { get; set; }
    public DateTimeOffset? DateRetired { get; set; } = null;

    public bool RetirementNotificationsSent { get; set; } = false;



}