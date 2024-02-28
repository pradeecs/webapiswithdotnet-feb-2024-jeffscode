using System.ComponentModel.DataAnnotations;

namespace IssuesApi.Features.Catalog;

public record SoftwareCatalogSummaryResponseItem
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string Version { get; set; } = string.Empty;
};


public record CollectionResponse<T>(IList<T> Items);


public record SoftwareItemRequestModel
{
    public string Title { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}
