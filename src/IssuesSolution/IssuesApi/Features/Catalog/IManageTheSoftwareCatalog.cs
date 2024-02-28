
namespace IssuesApi.Features.Catalog;

public interface IManageTheSoftwareCatalog
{
    Task<SoftwareCatalogSummaryResponseItem> AddSoftwareItemAsync(SoftwareItemRequestModel request, CancellationToken token);
    Task<CollectionResponse<SoftwareCatalogSummaryResponseItem>> GetAllCurrentSoftwareAsync(CancellationToken token);
    Task<SoftwareCatalogSummaryResponseItem?> GetItemByIdAsync(Guid id, CancellationToken token);
    Task RetireSoftwareItemAsync(Guid id, CancellationToken token);
}