﻿using AutoMapper;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels.Properties;
using OstreCWEB.Repository.DataBase;

namespace OstreCWEB.Repository.Factory
{
    internal class CharacterFactory : ICharacterFactory
    {
        private OstreCWebContext _ostreCWebContext;
        private readonly IMapper _mapper;

        public CharacterFactory(OstreCWebContext ostreCWebContext,IMapper mapper)
        {
            _ostreCWebContext = ostreCWebContext;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }

        public  Task<PlayableCharacter> CreatePlayableCharacterInstance(PlayableCharacter template)
        {
            var newInstance = new PlayableCharacter(); 
            ConfigureNewInstanceProperties(template, newInstance);
            //_ostreCWebContext.PlayableCharacters.Add(newInstance);
            //_ostreCWebContext.SaveChanges();

            ConfigureNewInstanceAbilities(template, newInstance);
            ConfigureNewInstanceItems(template, newInstance);
            //_ostreCWebContext.SaveChanges(); 
            return Task.FromResult(newInstance);
        } 
        public Task<List<Enemy>> CreateEnemiesInstances(List<EnemyInParagraph> enemiesInParagraphs)
        {
            var generatedEnemies = new List<Enemy>();
            foreach(var creationInstructions in enemiesInParagraphs)
            {
                for(var i =0; i < creationInstructions.AmountOfEnemy;i++)
                {//todo://Define newinstance configuration
                    var newInstance = new Enemy();
                    ConfigureNewInstanceProperties(creationInstructions.Enemy, newInstance);
                    ConfigureNewInstanceAbilities(creationInstructions.Enemy, newInstance);
                    ConfigureNewInstanceItems(creationInstructions.Enemy, newInstance);

                    generatedEnemies.Add(newInstance);
                }
            }
            return Task.FromResult(generatedEnemies);
        }
        
        private Task<Enemy> ConfigureNewInstanceProperties(Enemy template,Enemy newInstance)
        {
            newInstance.IsTemplate = false;
            newInstance.CharacterName = template.CharacterName;
            newInstance.MaxHealthPoints = template.MaxHealthPoints;
            newInstance.CurrentHealthPoints = template.CurrentHealthPoints;           
            newInstance.Strenght = template.Strenght;
            newInstance.Dexterity = template.Dexterity;
            newInstance.Constitution = template.Constitution;
            newInstance.Intelligence = template.Intelligence;
            newInstance.Wisdom = template.Wisdom;
            newInstance.Charisma = template.Charisma;
            newInstance.NonPlayableRace = template.NonPlayableRace;
            return Task.FromResult(newInstance);
        }
        private  Task<PlayableCharacter> ConfigureNewInstanceProperties(PlayableCharacter template, PlayableCharacter newInstance)
        { 
            newInstance.IsTemplate = false;
            newInstance.CharacterName = template.CharacterName;
            newInstance.MaxHealthPoints = template.MaxHealthPoints;
            newInstance.CurrentHealthPoints = template.CurrentHealthPoints;
            newInstance.Strenght = template.Strenght;
            newInstance.Dexterity = template.Dexterity;
            newInstance.Constitution = template.Constitution;
            newInstance.Intelligence = template.Intelligence;
            newInstance.Wisdom = template.Wisdom;
            newInstance.Charisma = template.Charisma; 
            newInstance.PlayableClassId = template.PlayableClassId;
            newInstance.RaceId = template.RaceId; 

            return Task.FromResult(newInstance);
        }  
        private PlayableCharacter ConfigureNewInstanceAbilities(PlayableCharacter template, PlayableCharacter newInstance)
        {
            if(template.CharacterClass.ActionsGrantedByClass != null && template.CharacterClass.ActionsGrantedByClass.Count() >0)
            {
                foreach (var linkedAction in template.CharacterClass.ActionsGrantedByClass)
                {
                    newInstance.LinkedAbilities.Add(
                         new AbilitiesCharacter()
                         {
                            Character = newInstance,
                             CharacterActionId = linkedAction.AbilityId, 
                             UsesLeftBeforeRest = linkedAction.UsesMaxBeforeRest
                         });
                }
            } 
            return newInstance;
        } 
        private PlayableCharacter ConfigureNewInstanceItems(PlayableCharacter template, PlayableCharacter newInstance)
        { 
           if(template.CharacterClass.ItemsGrantedByClass != null && template.CharacterClass.ItemsGrantedByClass.Count() > 0)
            {
                foreach (var linkedItem in template.CharacterClass.ItemsGrantedByClass)
                {
                    newInstance.LinkedItems.Add(
                        new ItemCharacter()
                        {
                            Character = newInstance,
                            ItemId = linkedItem.ItemId,
                            IsEquipped = false
                        });
                } 
            } 
            return newInstance; 
        }
        private Enemy ConfigureNewInstanceItems(Enemy template, Enemy newInstance)
        {
            if (template.LinkedAbilities != null)
            {
                foreach (var linkedItem in template.LinkedItems)
                {
                    newInstance.LinkedItems.Add(
                        new ItemCharacter()
                        {
                            CharacterId = newInstance.CharacterId,
                            Character = newInstance,
                            ItemId = linkedItem.ItemId,
                            Item = linkedItem.Item,
                            IsEquipped = linkedItem.IsEquipped
                        });
                }
            }
            return newInstance;
        }
        private Enemy ConfigureNewInstanceAbilities(Enemy template, Enemy newInstance)
        {
            if (template.LinkedAbilities != null)
            {
                foreach (var linkedAction in template.LinkedAbilities)
                {
                    newInstance.LinkedAbilities.Add(
                         new AbilitiesCharacter()
                         {
                             CharacterId = newInstance.CharacterId,
                             Character = newInstance,
                             CharacterActionId = linkedAction.CharacterActionId,
                             CharacterAction = linkedAction.CharacterAction,
                             UsesLeftBeforeRest = linkedAction.UsesLeftBeforeRest
                         });
                }
            }
            return newInstance;
        }
    }
}
