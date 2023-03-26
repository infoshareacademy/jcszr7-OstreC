using AutoMapper;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Properties;
using OstreCWEB.ViewModel.Identity;
using OstreCWEB.ViewModel.StoryReader;

namespace OstreCWEB.Mapping
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<UserParagraph, UserParagraphView>();
            CreateMap<Paragraph, CurrentParagraphView>();
            CreateMap<Choice, CurrentChoicesView>();
            CreateMap<PlayableCharacter, CurrentCharacterView>().
                ForMember(dest => dest.ItemCharacterWithAction, options => options.Ignore());
            CreateMap<ParagraphItem, ParagraphItemView>();
        }
    }
}