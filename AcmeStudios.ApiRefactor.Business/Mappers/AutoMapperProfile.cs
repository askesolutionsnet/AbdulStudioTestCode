using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.Models;
using AutoMapper;

namespace AcmeStudios.ApiRefactor.Business;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<StudioItem, StudioItems>();
        CreateMap<StudioItems, StudioItem>();
        CreateMap<StudioItem, StudioItemHeaders>();
        CreateMap<StudioItemHeaders, StudioItem>();
        CreateMap<StudioItemHeaders, StudioItem>();
        CreateMap<StudioItemTypes, StudioItemType>();
        CreateMap<StudioItemType, StudioItemTypes>();
    }
}

