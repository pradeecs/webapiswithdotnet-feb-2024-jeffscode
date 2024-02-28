using AutoMapper;
using AutoMapper.QueryableExtensions;
using IssuesApi.Services;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Features.Catalog;

public class EntityFrameworkSoftwareCatalogManager(IssuesDataContext context, IMapper mapper, MapperConfiguration mapperConfig) : IManageTheSoftwareCatalog
{

    public async Task<CollectionResponse<SoftwareCatalogSummaryResponseItem>> GetAllCurrentSoftwareAsync(CancellationToken token)
    {
        var results = await context.SoftwareCatalog.Where(s => s.DateRetired == null)
            .ProjectTo<SoftwareCatalogSummaryResponseItem>(mapperConfig)
            .ToListAsync(token);


        return new CollectionResponse<SoftwareCatalogSummaryResponseItem>(results);
    }

    public async Task<SoftwareCatalogSummaryResponseItem> AddSoftwareItemAsync(SoftwareItemRequestModel request, CancellationToken token)
    {
        var newItem = mapper.Map<SoftwareItem>(request);
        context.SoftwareCatalog.Add(newItem);
        await context.SaveChangesAsync(); // some weird entity framework object reference magic here.


        return mapper.Map<SoftwareCatalogSummaryResponseItem>(newItem);

    }

    public async Task<SoftwareCatalogSummaryResponseItem?> GetItemByIdAsync(Guid id, CancellationToken token)
    {
        var response = await context
            .ActiveSoftwareItems()
            .Where(item => item.Id == id)
            .ProjectTo<SoftwareCatalogSummaryResponseItem>(mapperConfig)
            .SingleOrDefaultAsync(token); // if this return 0, make response null, if it returns 1 make that the response, if it returns MORE than one item, throw an exception.

        return response;
    }

    public async Task RetireSoftwareItemAsync(Guid id, CancellationToken token)
    {
        var savedItem = await context.ActiveSoftwareItems().SingleOrDefaultAsync(item => item.Id == id, token);
        if (savedItem is not null)
        {
            savedItem.DateRetired = DateTimeOffset.UtcNow;
            await context.SaveChangesAsync();
        }
    }
}
