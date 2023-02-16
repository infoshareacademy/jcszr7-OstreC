using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Services.Factory
{
    public interface IFightFactory
    {
        public FightInstance BuildNewFightInstance(UserParagraph userParagraph, List<Enemy> enemies);
    }
}
