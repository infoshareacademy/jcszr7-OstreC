using AutoMapper;
using OstreCWeb.DomainModels.ApiContracts;
using OstreCWEB.ViewModel.Api;
using static NuGet.Packaging.PackagingConstants;

namespace OstreCWEB.Mapping
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<SpellView, SpellResponseItem>();
            CreateMap<SpellResponseItem, SpellView>();
            CreateMap<FithEditionApiResponseView, FithEditionApiResponse>()
                .ForMember(x => x.Next, opt => opt.Ignore())
                .ForMember(x => x.Previous, opt => opt.Ignore()); 
            CreateMap<FithEditionApiResponse, FithEditionApiResponseView>();
            CreateMap<Filter, FilterView>();
            CreateMap<FilterView, Filter>();
        }
    }
}
