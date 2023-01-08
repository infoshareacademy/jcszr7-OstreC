using AutoMapper;
using OstreCWEB.ViewModel.PlayableCharacter;
using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.CoreClasses;

namespace OstreCWEB.Mapping
{
    public class CharacterProfile:Profile
    {
        public CharacterProfile()
        {
            CreateMap<PlayableCharacter, PlayableCharacterAttrView>();
            CreateMap<PlayableCharacter, PlayableCharacterRace>();
        }
    }
}
