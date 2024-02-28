using AutoMapper;
using IssuesApi.Features.Catalog;
using IssuesApi.Services;

namespace IssuesApi.MapperProfiles;

public class Software : Profile
{
    public Software()
    {
        // CreateMap<Source,Destination>
        CreateMap<SoftwareItem, SoftwareCatalogSummaryResponseItem>()
           .ForMember(dest => dest.Title, config => config.MapFrom(src => src.Title + " " + src.Version));

        CreateMap<SoftwareItemRequestModel, SoftwareItem>();

    }
}
