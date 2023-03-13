using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Services.Factory;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;
using System.Security.Claims;

namespace OstreCWEB.Services.Fight
{
    internal class FightService : IFightService
    {
        private IFightRepository _fightRepository;
        //private FightInstance _activeFightInstance;
        private IFightFactory _fightFactory;
        private ICharacterFactory _characterFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUserParagraphRepository<UserParagraph> _userParagraphRepository;
        private IUserService _userService;
        private readonly ILogger<FightService> _logger;
        private IGameService _gameService;

        public FightService
            (
            IFightRepository fightRepository,
            IFightFactory fightFactory,
            ICharacterFactory characterFactory,
            IHttpContextAccessor httpContextAccessor,
            IUserParagraphRepository<UserParagraph> userParagraphRepository,
            IUserService userService,
            ILogger<FightService> logger,
            IGameService gameService

            )
        {
            _fightRepository = fightRepository;
            _fightFactory = fightFactory;
            _characterFactory = characterFactory;
            _httpContextAccessor = httpContextAccessor;
            _userParagraphRepository = userParagraphRepository;
            _userService = userService;
            _logger = logger;
            _gameService = gameService;
        }

        public async Task<FightInstance> GetFightInstanceAsync()
        {
            var httpUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;


            var result = int.TryParse(httpUserId, out var httpUserIdInt);

            if (!result)
            {
                throw new Exception("Failed to parse id");
            }

            var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(httpUserIdInt);

            var fightInstance =  _fightRepository.GetById(httpUserIdInt, (int)userParagraph.ActiveCharacterId);
            if (fightInstance != null && fightInstance.ActivePlayer.Id == userParagraph.ActiveCharacter.Id)
            {
                return fightInstance;
            }
            else
            {
                try
                {
                    if (userParagraph != null && userParagraph.Paragraph.FightProp != null)
                    {
                        await InitializeFightAsync(httpUserIdInt, userParagraph);
                        return _fightRepository.GetById(httpUserIdInt, (int)userParagraph.ActiveCharacterId);
                    }
                    else
                    {
                        throw new Exception("failed to initialize fight, userParagraph doesnt exist");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogDebug($"{ex.Message} // Initialize fight was called but a fight instance already exists for this user.");
                }
                return null;
            }
        }

        //public FightInstance GetActiveFightInstance(int userId, int characterId)
        //{
        //    var httpUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    _activeFightInstance = _fightRepository.GetById(userId, characterId);
        //    return _activeFightInstance;
        //}

        public bool ValidateFightInstanceModel(FightInstance model)
        {
            if (model.ActivePlayer != null)
            {
                return true;
            }
            else
                return false;
        }


        public async Task InitializeFightAsync(int userId, UserParagraph gameInstance)
        {

                var fightInstance = _fightFactory.BuildNewFightInstance(gameInstance, _characterFactory.CreateEnemiesInstances(gameInstance.Paragraph.FightProp.ParagraphEnemies).Result);
                fightInstance.FightHistory.Add("Fight initialized");

                var playerInitiative = InitiativeCheck(fightInstance.ActivePlayer);
                var enemiesInitiative = InitiativeCheck(fightInstance.ActiveEnemies.FirstOrDefault());
                if (playerInitiative >= enemiesInitiative)
                {
                    fightInstance.isPlayerFirst = true;
                }
                else fightInstance.isPlayerFirst = false;

                _fightRepository.Add(userId, fightInstance, out string operationResult);
        }



        public async Task CommitAction(int userId)
        {
           var activeFightInstance =await GetFightInstanceAsync();
           var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(userId);

            var isplayerFirst = activeFightInstance.isPlayerFirst;

            if (!isplayerFirst && activeFightInstance.AiFirstTurnCounter == 1)
            {
                activeFightInstance.FightHistory = UpdateFightHistory(activeFightInstance.FightHistory,
                "Player has lost an initiaive roll, The monsters will attack now!");
                StartAiTurn();
                activeFightInstance.AiFirstTurnCounter--;
            }

            activeFightInstance.PlayerActionCounter--;
            ApplyAction(activeFightInstance.ActiveTarget, activeFightInstance.ActivePlayer, activeFightInstance.ActiveAction);

            if (activeFightInstance.ActiveTarget.CombatId != 1)
            {
                if (activeFightInstance.ActiveTarget.CurrentHealthPoints <= 0)
                {
                    activeFightInstance.ActiveEnemies.Remove((Enemy)GetActiveTarget());
                    activeFightInstance.ActiveTarget = null;
                }
            }
            if (activeFightInstance.ActionGrantedByItem && activeFightInstance.IsItemToDelete)
            {
                _fightRepository.DeleteLinkedItem(activeFightInstance, activeFightInstance.ItemToDeleteId);
            }
            if (!activeFightInstance.ActionGrantedByItem && activeFightInstance.ActiveAction.ActionType != AbilityType.Cantrip)
            {
                activeFightInstance.ActivePlayer.LinkedAbilities.First(a => a.CharacterAction.Id == activeFightInstance.ActiveAction.Id).UsesLeftBeforeRest--;
            }

            if (activeFightInstance.PlayerActionCounter <= 0)
            {
                activeFightInstance.PlayerActionCounter = 2;
                StartAiTurn();
                activeFightInstance.ActivePlayer.ActiveStatuses.Clear();
                activeFightInstance.ActiveEnemies.ForEach(e => e.ActiveStatuses.Clear());
                activeFightInstance.TurnNumber = UpdateTurnNumber(activeFightInstance.TurnNumber);
            }
            var combatEnded = IsFightFinished(activeFightInstance.ActiveEnemies, GetActivePlayer());
            if (combatEnded)
            {
                var fightWon = IsFightWon(activeFightInstance.ActivePlayer);
                FinishFight(fightWon, activeFightInstance);
            }

           await EndTurnAsync(activeFightInstance, userParagraph, userId);

        }
        public List<string> ReturnHistory(FightInstance fightInstance) => fightInstance.FightHistory;
        public void UpdateActiveTarget(Character character, FightInstance fightInstance)
        {
            fightInstance.ActiveTarget = character;
        }
        public Ability ChooseAction(int id, FightInstance fightInstance) => fightInstance.ActivePlayer.AllAbilities.First(a => a.Id == id);
        public Character ChooseTarget(int id, FightInstance fightInstance)
        {
            if (id == fightInstance.ActivePlayer.CombatId)
            {
                return fightInstance.ActivePlayer;
            }
            return fightInstance.ActiveEnemies.First(a => a.CombatId == id);
        }
        public Character ResetActiveTarget(FightInstance fightInstance)
        {
            fightInstance.ActiveTarget = new PlayableCharacter();
            return fightInstance.ActiveTarget;
        }
        public Ability ResetActiveAction(FightInstance fightInstance)
        {
            fightInstance.ActiveAction = new Ability();
            return fightInstance.ActiveAction;
        }
        private void StartAiTurn(FightInstance fightInstance)
        {
            var random = new Random();

            foreach (var enemy in fightInstance.ActiveEnemies)
            {
                var result = random.Next(0, enemy.AllAbilities.Count());
                var enemyAction = enemy.AllAbilities[result];
                ApplyAction(fightInstance.ActivePlayer, enemy, enemyAction);
            }
        }
        public Character GetActiveTarget(FightInstance fightInstance) => fightInstance.ActiveTarget;
        public Ability GetActiveActions(FightInstance fightInstance) => fightInstance.ActiveAction;
        public void UpdateActiveAction(Ability action, FightInstance fightInstance)
        {
            fightInstance.ActiveAction = action;
        }
        private PlayableCharacter GetActivePlayer(FightInstance fightInstance) => fightInstance.ActivePlayer;
        private int UpdateTurnNumber(int turnNumber) => turnNumber += 1;

        private List<string> UpdateFightHistory(List<string> FightHistory, string message)
        {
            FightHistory.Add(message);
            return FightHistory;
        }

        private void ApplyAction(Character target, Character caster, Ability action, FightInstance fightInstance)
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

                    fightInstance.FightHistory = UpdateFightHistory(fightInstance.FightHistory,
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
                            fightInstance.FightHistory = UpdateFightHistory(fightInstance.FightHistory,
                           $" {target.CharacterName} lost {damage} healthpoints, current healthpoints {target.CurrentHealthPoints}," +
                           $" due to {caster.CharacterName}  using {action.AbilityName}" +
                           GetLogCharacterIsBlind(caster) +
                           GetLogCharacterIsBlessed(caster));
                        }
                        else
                        {
                            fightInstance.FightHistory = UpdateFightHistory(fightInstance.FightHistory,
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
                            fightInstance.FightHistory = UpdateFightHistory(fightInstance.FightHistory,
                                                        $" {target.CharacterName} used bless on himself!");
                        }
                        else
                        {
                            var heal = ApplyHeal(target, action, savingThrow);
                            fightInstance.FightHistory = UpdateFightHistory(fightInstance.FightHistory,
                                $" {target.CharacterName} gained {heal} healthpoints, current healthpoints {target.MaxHealthPoints}," +
                                $" due to {caster.CharacterName}  using {action.AbilityName}");
                        }

                    }
                }

            }
            else
            {
                fightInstance.FightHistory = UpdateFightHistory(fightInstance.FightHistory,
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

        private bool IsPlayerFirst(FightInstance fightInstance)
        {
            var playerInitiative = InitiativeCheck(fightInstance.ActivePlayer);
            var enemyInitiaive = InitiativeCheck(fightInstance.ActiveEnemies.FirstOrDefault());
            if (playerInitiative >= enemyInitiaive)
            {
                return true;
            }
            return false;
        }

        private int InitiativeCheck(Character character)
        {
            var result = DiceThrow20() + CalculateModifier(character.Dexterity);
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
        private void FinishFight(bool playerWon,FightInstance fightInstance)
        {
            fightInstance.CombatFinished = true;
            if (playerWon) { fightInstance.PlayerWon = true; return; }
            fightInstance.PlayerWon = false;
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

        public async Task RemoveItem(FightInstance fightInstance)
        {
            var itemToDelete = fightInstance.ItemToDeleteId;
            _fightRepository.DeleteLinkedItem(fightInstance, itemToDelete);
        }

        public async Task UpdateItemToRemove(int id, FightInstance fightInstance)
        {
            fightInstance.ItemToDeleteId = id;
        }
        public async Task DeleteFightInstanceAsync(int userId, FightInstance fightInstance)
        {
            _fightRepository.Delete(userId, fightInstance.ActivePlayer.Id, out string operationResult);
        }

        public bool IsBlind(Character character)
        {
            return character.ActiveStatuses.Where(s => s.StatusType == StatusType.Blind).Any();
        }

        public String GetLogCharacterIsBlind(Character character)
        {
            if (IsBlind(character))
            {
                return "attack was made with disadvantage due to blind status";
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

        public async Task EndTurnAsync(FightInstance fightInstance, UserParagraph userParagraph, int userId)
        {
            fightInstance.ActionGrantedByItem = false;
            ResetActiveTarget(fightInstance);
            ResetActiveAction(fightInstance);

            if (fightInstance.CombatFinished)
            {
                //We apply changes from player from static list to player in db. They get saved during game session update in gameservice.
                userParagraph.ActiveCharacter.CurrentHealthPoints = fightInstance.ActivePlayer.CurrentHealthPoints;
                userParagraph.ActiveCharacter.LinkedAbilities.ForEach(x => x.UsesLeftBeforeRest = fightInstance.ActivePlayer.LinkedAbilities.FirstOrDefault(y => y.CharacterActionId == x.CharacterActionId).UsesLeftBeforeRest);
                for (var i = userParagraph.ActiveCharacter.LinkedItems.Count - 1; i >= 0; i--)
                {
                    var contains = false;
                    var item = userParagraph.ActiveCharacter.LinkedItems[i];
                    fightInstance.ActivePlayer.LinkedItems.ForEach(x => { if (x.Id == item.Id) { contains = true; } });
                    if (!contains) { userParagraph.ActiveCharacter.LinkedItems.RemoveAt(i); }
                }
                if (fightInstance.PlayerWon)
                {

                    await DeleteFightInstanceAsync(userId);
                    await _gameService.NextParagraphAfterFightAsync(userParagraph, 1);
                }
                else
                {
                    await DeleteFightInstanceAsync(userId);
                    await _gameService.NextParagraphAfterFightAsync(userParagraph, 0);
                }
                return RedirectToAction("Index", "StoryReader");
            }
        }

    }
}