 using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels.Properties;

namespace OstreCWEB.Repository.Factory
{
    public interface ICharacterFactory
    {
        public Task<PlayableCharacter> CreatePlayableCharacterInstance(PlayableCharacter playableCharacter);
        public Task <List<Enemy>> CreateEnemiesInstances(List<EnemyInParagraph> enemiesInParagraphs);
    }
}
