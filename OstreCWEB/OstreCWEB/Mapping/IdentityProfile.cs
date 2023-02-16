using AutoMapper;
using OstreCWEB.ViewModel.Identity;

namespace OstreCWEB.Mapping
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<User, UserView>();
        }
    }
}