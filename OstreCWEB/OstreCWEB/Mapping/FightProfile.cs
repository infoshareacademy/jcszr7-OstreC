using AutoMapper;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.ViewModel.Fight;

namespace OstreCWEB.Mapping
{
    public class FightProfile : Profile
    {
        public FightProfile()
        {
            CreateMap<FightInstance, FightViewModel>();
        }
    }
}