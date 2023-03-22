using AutoMapper;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Properties;
using OstreCWEB.Services.StoryService.Models;
using OstreCWEB.Services.StoryService.ModelsDto;

namespace OstreCWEB.Services.Mapping
{
    public class StoryProfile : Profile
    {
        public StoryProfile()
        {
            CreateMap<Story, StoryView>()
                .ReverseMap()
                    .ForMember(dest => dest.Paragraphs, opt => opt.Ignore());

            CreateMap<Story, StoryParagraphsView>();

            CreateMap<Paragraph, ParagraphElementView>();

            CreateMap<ParagraphDetails, ParagraphDetailsView>();
            CreateMap<ParagraphWithCoice, ParagraphWithCoiceView>();

            CreateMap<CreatNewParagraphView, Paragraph>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Choices, opt => opt.Ignore())
                .ForMember(dest => dest.UserParagraphs, opt => opt.Ignore())
                .ForMember(dest => dest.ParagraphItems, opt => opt.Ignore())
                .ForMember(dest => dest.FightProp, opt => opt.Ignore())
                .ForMember(dest => dest.DialogProp, opt => opt.Ignore())
                .ForMember(dest => dest.TestProp, opt => opt.Ignore())
                .ForMember(dest => dest.ShopkeeperProp, opt => opt.Ignore())
                .ForMember(dest => dest.Story, opt => opt.Ignore());

            CreateMap<CreatParagraphFightView, Paragraph>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Choices, opt => opt.Ignore())
                .ForMember(dest => dest.UserParagraphs, opt => opt.Ignore())
                .ForMember(dest => dest.ParagraphItems, opt => opt.Ignore())
                .ForMember(dest => dest.FightProp, opt => opt.Ignore())
                .ForMember(dest => dest.DialogProp, opt => opt.Ignore())
                .ForMember(dest => dest.TestProp, opt => opt.Ignore())
                .ForMember(dest => dest.ShopkeeperProp, opt => opt.Ignore())
                .ForMember(dest => dest.Story, opt => opt.Ignore());

            CreateMap<CreatParagraphTestView, Paragraph>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Choices, opt => opt.Ignore())
                .ForMember(dest => dest.UserParagraphs, opt => opt.Ignore())
                .ForMember(dest => dest.ParagraphItems, opt => opt.Ignore())
                .ForMember(dest => dest.FightProp, opt => opt.Ignore())
                .ForMember(dest => dest.DialogProp, opt => opt.Ignore())
                .ForMember(dest => dest.TestProp, opt => opt.Ignore())
                .ForMember(dest => dest.ShopkeeperProp, opt => opt.Ignore())
                .ForMember(dest => dest.Story, opt => opt.Ignore());

            CreateMap<CreatNewParagraphView, CreatParagraphFightView>()
                .ForMember(dest => dest.FirstEnemyId, opt => opt.Ignore())
                .ForMember(dest => dest.FirstAmountOfEnemy, opt => opt.Ignore())
                .ForMember(dest => dest.SecondEnemyId, opt => opt.Ignore())
                .ForMember(dest => dest.SecondAmountOfEnemy, opt => opt.Ignore())
                .ForMember(dest => dest.ThirdEnemyId, opt => opt.Ignore())
                .ForMember(dest => dest.ThirdAmountOfEnemy, opt => opt.Ignore())
                .ForMember(dest => dest.Enemies, opt => opt.Ignore());

            CreateMap<CreatNewParagraphView, CreatParagraphTestView>()
                .ForMember(dest => dest.AbilityScores, opt => opt.Ignore())
                .ForMember(dest => dest.TestDifficulty, opt => opt.Ignore());

            CreateMap<Choice, CurrentChoiceView>();

            CreateMap<EditParagraph, EditParagraphView>()
                .ReverseMap();

            CreateMap<EnemyInParagraphService, OstreCWEB.Services.StoryService.ModelsDto.EnemyInParagraphView>()
                .ForMember(dest => dest.Enemies, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}