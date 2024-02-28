using IssuesApi.Services;

namespace IssuesApi;

public static class DevelopmentSeedData
{
    public static async Task InitializeSoftwareCatalogAsync(IssuesDataContext context)
    {
        context.Database.EnsureCreated();
        // Magic!
        var sampleSoftware = new List<SoftwareItem> {
            new SoftwareItem {
                Id = Guid.Parse("e42171ef-6faa-4071-9dd8-85370e8e20ed"),
                Title = "Visual Studio",
                Version = "2019",
                DateAdded = DateTime.Now.AddDays(-120).ToUniversalTime(),
            },
             new SoftwareItem {
                Id = Guid.Parse("e3c22006-50ec-4009-8ca0-46940d481a3c"),
                Title = "Visual Studio",
                Version = "2022",
                DateAdded =  DateTime.Now.AddDays(-120).ToUniversalTime(),
            },
              new SoftwareItem {
                Id = Guid.Parse("79f0bab7-32c1-4e2d-8e9f-32a07536a4c1"),
                Title = "Docker Desktop",
                Version = "3.14",
                DateAdded =DateTime.Now.AddDays(-120).ToUniversalTime(),
            },
               new SoftwareItem {
                Id = Guid.Parse("d08b3c4a-9d29-4271-bf2d-9e6b8f4a531c"),
                Title = "Visio",
                Version = "12",
                DateAdded =DateTime.Now.AddDays(-120).ToUniversalTime(),
                DateRetired = DateTime.Now.AddDays(-120).ToUniversalTime(),
            }
            };
        foreach (var item in sampleSoftware)
        {
            var existing = await context.SoftwareCatalog.FindAsync(item.Id);
            if (existing is null)
            {
                context.SoftwareCatalog.Add(item);
            }
            await context.SaveChangesAsync();
        }

    }
}
