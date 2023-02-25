using Microsoft.AspNetCore.Http;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.Services.Factory;
using System.Security.Claims;

namespace OstreCWEB.Services.Fight
{
    internal class FightService : IFightService
    {
        private IFightRepository _fightRepository;
        private FightInstance _activeFightInstance;
        private IFightFactory _fightFactory;
        private IUserParagraphRepository _userParagraphRepository;
        private ICharacterFactory _characterFactory;
        private readonly IPlayableCharacterRepository _playableCharacterRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FightService(
            IFightRepository fightRepository,
            IFightFactory fightFactory,
            IUserParagraphRepository userParagraphRepository,
            ICharacterFactory characterFactory,
            IPlayableCharacterRepository playableCharacterRepository,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _fightRepository = fightRepository;
            _fightFactory = fightFactory;
            _userParagraphRepository = userParagraphRepository;
            _characterFactory = characterFactory;
            _playableCharacterRepository = playableCharacterRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public FightInstance GetActiveFightInstance(string userId, int characterId)
        {
            var httpUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _activeFightInstance = _fightRepository.GetById(userId, characterId);
            return _activeFightInstance;
        }

        public bool ValidateFightInstanceModel(FightInstance model)
        {
            if (model.ActivePlayer != null)
            {
                return true;
            }
            else
                return false;
        }


        public async Task InitializeFightAsync(string userId, UserParagraph gameInstance)
        {
            if (_activeFightInstance != null) { throw new Exception("Fight already initialized"); }

            if (gameInstance != null && gameInstance.Paragraph.FightProp != null)
            {
                var fightInstance = _fightFactory.BuildNewFightInstance(gameInstance, _characterFactory.CreateEnemiesInstances(gameInstance.Paragraph.FightProp.ParagraphEnemies).Result);
                fightInstance.FightHistory.Add("Fight initialized");

               var playerInitiative =  InitiativeCheck(fightInstance.ActivePlayer);
               var enemiesInitiative = InitiativeCheck(fightInstance.ActiveEnemies.FirstOrDefault());
               if (playerInitiative >= enemiesInitiative)
               {
                    fightInstance.isPlayerFirst= true;
               }
               else fightInstance.isPlayerFirst= false;

              

                _fightRepository.Add(userId, fightInstance, out string operationResult); 

            }
            else
            {
                throw new Exception("Fight initialization failed. Game instance doesn't exist or active paragraph is not a fight");
            }
        }
        public async Task CommitAction(string userId)
        {
            _activeFightInstance.PlayerActionCounter--;
            ApplyAction(_activeFightInstance.ActiveTarget, _activeFightInstance.ActivePlayer, _activeFightInstance.ActiveAction);

            if (_activeFightInstance.ActiveTarget.CombatId != 1)
            {
                if (_activeFightInstance.ActiveTarget.CurrentHealthPoints <= 0)
                {
                    _activeFightInstance.ActiveEnemies.Remove((Enemy)GetActiveTarget());
                    _activeFightInstance.ActiveTarget = null;
                }
            }
            if (_activeFightInstance.ActionGrantedByItem && _activeFightInstance.IsItemToDelete)
            {
                _fightRepository.DeleteLinkedItem(_activeFightInstance, _activeFightInstance.ItemToDeleteId);
            }
            if (!_activeFightInstance.ActionGrantedByItem && _activeFightInstance.ActiveAction.ActionType != AbilityType.Cantrip)
            {
                _activeFightInstance.ActivePlayer.LinkedAbilities.First(a => a.CharacterAction.AbilityId == _activeFightInstance.ActiveAction.AbilityId).UsesLeftBeforeRest--;
            }

            if (_activeFightInstance.PlayerActionCounter <= 0)
            {
                _activeFightInstance.PlayerActionCounter = 2;
                StartAiTurn();
                _activeFightInstance.ActivePlayer.ActiveStatuses.Clear();
                _activeFightInstance.ActiveEnemies.ForEach(e => e.ActiveStatuses.Clear());
                _activeFightInstance.TurnNumber = UpdateTurnNumber(_activeFightInstance.TurnNumber);
            }
            var combatEnded = IsFightFinished(_activeFightInstance.ActiveEnemies, GetActivePlayer());
            if (combatEnded)
            {
                var fightWon = IsFightWon(_activeFightInstance.ActivePlayer);
                FinishFight(fightWon);
            }
        }
        public List<string> ReturnHistory() => _activeFightInstance.FightHistory;
        public void UpdateActiveTarget(Character character)
        {
            _activeFightInstance.ActiveTarget = character;
        }
        public Ability ChooseAction(int id) => _activeFightInstance.ActivePlayer.AllAbilities.First(a => a.AbilityId == id);
        public Character ChooseTarget(int id)
        {
            if (id == _activeFightInstance.ActivePlayer.CombatId)
            {
                return _activeFightInstance.ActivePlayer;
            }
            return _activeFightInstance.ActiveEnemies.First(a => a.CombatId == id);
        }
        public Character ResetActiveTarget()
        {
            //We  create playable character instance and replace the active target with null values. Character class is abstract.   
            _activeFightInstance.ActiveTarget = new PlayableCharacter();
            return _activeFightInstance.ActiveTarget;
        }
        public Ability ResetActiveAction()
        {
            //We  create playable character instance and replace the active target with null values. Character class is abstract.   
            _activeFightInstance.ActiveAction = new Ability();
            return _activeFightInstance.ActiveAction;
        }
        private void StartAiTurn()
        {
            var random = new Random();

            foreach (var enemy in _activeFightInstance.ActiveEnemies)
            {
                var result = random.Next(0, enemy.AllAbilities.Count());
                var enemyAction = enemy.AllAbilities[result];
                ApplyAction(_activeFightInstance.ActivePlayer, enemy, enemyAction);
            }
        }
        public FightInstance GetFightState(string userId, int characterId) => _fightRepository.GetById(userId, characterId);
        public Character GetActiveTarget() => _activeFightInstance.ActiveTarget;
        public Ability GetActiveActions() => _activeFightInstance.ActiveAction;
        public void UpdateActiveAction(Ability action)
        {
            _activeFightInstance.ActiveAction = action;
        }
        private PlayableCharacter GetActivePlayer() => _activeFightInstance.ActivePlayer;
        private int UpdateTurnNumber(int turnNumber) => turnNumber += 1;

        private List<string> UpdateFightHistory(List<string> FightHistory, string message)
        {
            FightHistory.Add(message);
            return FightHistory;
        }

        private void ApplyAction(Character target, Character caster, Ability action)
        {
            var IsHit = IsTargetHit(target, caster, action);

            if (IsHit)
            {

                if (action.SavingThrowPossible)
                {
                    var damage = 0;
                    var savingThrow = SpellSavingThrow(target, caster, action);
                    damage = ApplyDamage(target, action, savingThrow);
                    if (!savingThrow)
                    {
                        ApplyStatus(target, action.Status);
                    }

                    _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                           $" {target.CharacterName} lost {damage} healthpoints, current healthpoints {target.CurrentHealthPoints}," +
                           $" due to {caster.CharacterName} using {action.AbilityName}" +
                           $" saving throw was {(savingThrow ? "successful" : "failed")}, " +
                           GetLogCharacterIsBlind(caster) +
                           GetLogCharacterIsBlessed(caster));
                }
                else
                {
                    var savingThrow = false;
                    if (action.Status != null) { ApplyStatus(target, action.Status); }

                    if (action.AggressiveAction)
                    {
                        //var tryToHit = TryToHit()
                        var damage = ApplyDamage(target, action, savingThrow);

                        if (IsTargetAlive(target))
                        {
                            _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                           $" {target.CharacterName} lost {damage} healthpoints, current healthpoints {target.CurrentHealthPoints}," +
                           $" due to {caster.CharacterName}  using {action.AbilityName}" +
                           GetLogCharacterIsBlind(caster) +
                           GetLogCharacterIsBlessed(caster));
                        }
                        else
                        {
                            _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                           $" {target.CharacterName} lost {damage} healthpoints, current healthpoints {target.CurrentHealthPoints}," +
                           $" due to {caster.CharacterName}  using {action.AbilityName} and died" +
                           GetLogCharacterIsBlind(caster) +
                           GetLogCharacterIsBlessed(caster));
                        }
                    }
                    else
                    {
                        if (action.AbilityName == "Bless")
                        {
                            _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                                                        $" {target.CharacterName} used bless on himself!");
                        }
                        else
                        {
                            var heal = ApplyHeal(target, action, savingThrow);
                            _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                                $" {target.CharacterName} gained {heal} healthpoints, current healthpoints {target.MaxHealthPoints}," +
                                $" due to {caster.CharacterName}  using {action.AbilityName}");
                        }

                    }
                }

            }
            else
            {
                _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                                            $" {caster.CharacterName} tried to use {action.AbilityName} on {target.CharacterName}, but have missed");
            }

        }

        private bool IsTargetHit(Character target, Character caster, Ability action)
        {
            if (action.AggressiveAction && action.ActionType != AbilityType.Spell && action.ActionType != AbilityType.Cantrip)
            {

                var casterMod = CheckStatForHit(caster, action);
                var targetArmor = CheckArmor(target);
                if (targetArmor > casterMod) return false;
                else return true;
            }
            return true;

        }

        private bool IsPlayerFirst()
        {
            var playerInitiative = InitiativeCheck(_activeFightInstance.ActivePlayer);
            var enemyInitiaive = InitiativeCheck(_activeFightInstance.ActiveEnemies.FirstOrDefault());
            if (playerInitiative >= enemyInitiaive)
            {
                return true;
            }
            return false;
        }

        private int InitiativeCheck(Character character)
        {
           var  result = DiceThrow20() + CalculateModifier(character.Dexterity);
           return result;
        }

        private int CheckStatForHit(Character caster, Ability action)
        {
            var roll = HitRollWithStatus(caster);
            var modifier = 0;
            switch (action.StatForTest)
            {
                case Statistics.Strenght:
                    modifier = CalculateModifier(caster.Strenght);
                    break;
                case Statistics.Intelligence:
                    modifier = CalculateModifier(caster.Intelligence);
                    break;
                case Statistics.Constitution:
                    modifier = CalculateModifier(caster.Constitution);
                    break;
                case Statistics.Wisdom:
                    modifier = CalculateModifier(caster.Wisdom);
                    break;
                case Statistics.Dexterity:
                    modifier = CalculateModifier(caster.Dexterity);
                    break;
                case Statistics.Charisma:
                    modifier = CalculateModifier(caster.Charisma);
                    break;
            }
            return roll + modifier;
        }

        private int HitRollWithStatus(Character caster)
        {
            var roll = 0;

            if (IsBlind(caster) && !IsBlessed(caster))
            {
                roll = Math.Min(DiceThrow20(), DiceThrow20());
            }
            else if (IsBlessed(caster) && !IsBlind(caster))
            {
                roll = Math.Max(DiceThrow20(), DiceThrow20());
            }
            else roll = DiceThrow20();
            return roll;
        }


        private int CheckArmor(Character target)
        {
            int? armor = 0;
            foreach (var item in target.LinkedItems)
            {
                if (item.IsEquipped)
                {
                    if (item.Item.ItemType == ItemType.Armor || item.Item.ItemType == ItemType.Shield)
                    {
                        armor = armor + item.Item.ArmorClass;
                    }
                }
            }
            return (int)armor;
        }
        private bool IsTargetAlive(Character target)
        {
            if (target.MaxHealthPoints <= 0) { return false; }
            else { return true; }
        }
        private void ApplyStatus(Character character, Status status)
        {
            if (status != null)
            {
                character.ActiveStatuses.Add(status);
            }
        }
        private int ApplyDamage(Character target, Ability actions, bool savingThrow)
        {
            var updateValue = 0;

            for (int i = 0; i < actions.Hit_Dice_Nr; i++)
            {
                updateValue += DiceThrow(actions.Max_Dmg);
            }
            updateValue += actions.Flat_Dmg;


            if (actions.SavingThrowPossible && savingThrow)
            {
                updateValue = updateValue / 2;
            }
            target.CurrentHealthPoints = target.CurrentHealthPoints - updateValue;

            return updateValue;
        }
        private int ApplyHeal(Character target, Ability actions, bool savingThrow)
        {
            var updateValue = 0;

            for (int i = 0; i < actions.Hit_Dice_Nr; i++)
            {
                updateValue += DiceThrow(actions.Max_Dmg);
            }
            updateValue += actions.Flat_Dmg;
            if (actions.SavingThrowPossible && !savingThrow)
            {
                updateValue = updateValue / 2;
            }

            if (target.CurrentHealthPoints < target.MaxHealthPoints)
            {
                target.CurrentHealthPoints = target.CurrentHealthPoints + updateValue;
                if (target.CurrentHealthPoints > target.MaxHealthPoints)
                {
                    target.CurrentHealthPoints = target.MaxHealthPoints;
                }
            }


            return updateValue;
        }
        private bool SpellSavingThrow(Character target, Character caster, Ability action)
        {
            var targetMod = 0;
            var targetRoll = 0;
            var casterModifier = 0;
            switch (action.StatForTest)
            {
                case Statistics.Strenght:
                    targetMod = CalculateModifier(target.Strenght);
                    targetRoll = DiceThrow(20) + targetMod;
                    casterModifier = SpellCastingModifier(caster, action.StatForTest);
                    if (casterModifier > targetRoll) return false;
                    return true;
                case Statistics.Intelligence:
                    targetMod = CalculateModifier(target.Intelligence);
                    targetRoll = DiceThrow(20) + targetMod;
                    casterModifier = SpellCastingModifier(caster, action.StatForTest);
                    if (casterModifier > targetRoll) return false;
                    return true;
                case Statistics.Constitution:
                    targetMod = CalculateModifier(target.Constitution);
                    targetRoll = DiceThrow(20) + targetMod;
                    casterModifier = SpellCastingModifier(caster, action.StatForTest);
                    if (casterModifier > targetRoll) return false;
                    return true;
                case Statistics.Wisdom:
                    targetMod = CalculateModifier(target.Wisdom);
                    targetRoll = DiceThrow(20) + targetMod;
                    casterModifier = SpellCastingModifier(caster, action.StatForTest);
                    if (casterModifier > targetRoll) return false;
                    return true;
                case Statistics.Dexterity:
                    targetMod = CalculateModifier(target.Dexterity);
                    targetRoll = DiceThrow(20) + targetMod;
                    casterModifier = SpellCastingModifier(caster, action.StatForTest);
                    if (casterModifier > targetRoll) return false;
                    return true;
                case Statistics.Charisma:
                    targetMod = CalculateModifier(target.Charisma);
                    targetRoll = DiceThrow(20) + targetMod;
                    casterModifier = SpellCastingModifier(caster, action.StatForTest);
                    if (casterModifier > targetRoll) return false;
                    return true;
                default:
                    return false;

            }
        }
        private int SpellCastingModifier(Character caster, Statistics statsForTest)
        {
            switch (statsForTest)
            {
                case Statistics.Strenght:
                    return 8 + CalculateModifier(caster.Strenght); // Dopisać premię z biegłości...gdy takowa powstanie.
                case Statistics.Intelligence:
                    return 8 + CalculateModifier(caster.Intelligence); // Dopisać premię z biegłości...gdy takowa powstanie.
                case Statistics.Constitution:
                    return 8 + CalculateModifier(caster.Constitution); // Dopisać premię z biegłości...gdy takowa powstanie.
                case Statistics.Wisdom:
                    return 8 + CalculateModifier(caster.Wisdom); // Dopisać premię z biegłości...gdy takowa powstanie.
                case Statistics.Dexterity:
                    return 8 + CalculateModifier(caster.Dexterity); // Dopisać premię z biegłości...gdy takowa powstanie.
                case Statistics.Charisma:
                    return 8 + CalculateModifier(caster.Charisma); // Dopisać premię z biegłości...gdy takowa powstanie.
                default:
                    return 0;
            }
        }


        private int CalculateModifier(int value)
        {
            List<int> numbers = new List<int>() {
                   -5,-4,-4,-3,-3,-2,-2,-1,-1, 0,
                    0, 1, 1, 2, 2, 3, 3, 4, 4, 5,
                    5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };

            return numbers.First(x => x == numbers[value - 1]);
        }

        private int DiceThrow20()
        {
            return DiceThrow(20);
        }
        private int DiceThrow(int maxValue)
        {
            Random rand = new Random();
            var diceThrowResult = rand.Next(1, maxValue + 1);
            return diceThrowResult;
        }
        private void FinishFight(bool playerWon)
        {
            _activeFightInstance.CombatFinished = true;
            if (playerWon) { _activeFightInstance.PlayerWon = true; return; }
            _activeFightInstance.PlayerWon = false;
        }
        private bool IsFightFinished(List<Enemy> activeEnemies, PlayableCharacter activePlayer)
        {
            if (activeEnemies.Count() == 0 || activePlayer.CurrentHealthPoints <= 0) { return true; }
            return false;
        }
        private bool IsFightWon(PlayableCharacter activePlayer)
        {
            if (activePlayer.CurrentHealthPoints > 0) { return true; }
            else { return false; }
        }

        public async Task RemoveItem()
        {
            var itemToDelete = _activeFightInstance.ItemToDeleteId;
            _fightRepository.DeleteLinkedItem(_activeFightInstance, itemToDelete);
        }

        public async Task UpdateItemToRemove(int id)
        {
            _activeFightInstance.ItemToDeleteId = id;
        }
        public async Task DeleteFightInstanceAsync(string userId)
        {
            _fightRepository.Delete(userId, _activeFightInstance.ActivePlayer.CharacterId, out string operationResult);
        }

        public bool IsBlind(Character character)
        {
            return character.ActiveStatuses.Where(s => s.StatusType == StatusType.Blind).Any();
        }

        public String GetLogCharacterIsBlind(Character character)
        {
            if (IsBlind(character))
            {
                return "attack was made with disadventage due to blind status";
            }
            return "";
        }

        public bool IsBlessed(Character character)
        {
            return character.ActiveStatuses.Where(s => s.StatusType == StatusType.Bless).Any();
        }

        public String GetLogCharacterIsBlessed(Character character)
        {
            if (IsBlessed(character))
            {
                return "attack was made with advantage due to bless status";
            }
            return "";
        }
    }
}