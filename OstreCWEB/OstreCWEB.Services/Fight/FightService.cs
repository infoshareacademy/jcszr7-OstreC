using OstreCWEB.Data.DataBase;
using OstreCWEB.Data.Repository.Characters.CoreClasses;
using OstreCWEB.Data.Repository.Characters.Enums;
using OstreCWEB.Data.Repository.Fight;
using OstreCWEB.Services.Factories;

namespace OstreCWEB.Services.Fight
{
    public class FightService : IFightService
    {
        private IFightRepository _fightRepository;
        public StaticLists _db { get; } = new StaticLists();
        private FightInstance _activeFightInstance;
        private IFightFactory _fightFactory; 

        //The property below can be used to introduce a system where enemies will act one by one.
        //We could use combat ID or position in _activeEnemies list for this.
        /*public static int NextActiveEnemyCombatId { get; set; }*/

        public FightService(IFightRepository fightRepository, IFightFactory fightFactory)
        {
            //UserId is hardcoded to one for now. _fightRepository.GetById(currentUser) should be here instead.
            var userId = 1;
            _fightRepository = fightRepository;
            _db = new StaticLists();
            _activeFightInstance = _fightRepository.GetById(userId);
            _fightFactory = fightFactory;
        }
        public FightInstance GetActiveFightInstance() {
            
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
        public void InitializeFight()
        {
            var userId = 1;
            var fightInstance = _fightFactory.BuildNewFightInstance();
            fightInstance.FightHistory.Add("Fight initialized");
            _fightRepository.Add(userId, fightInstance, out string operationResult);
            
        }
        public void CommitAction()
        {
            _activeFightInstance.PlayerActionCounter--;
            ApplyAction(_activeFightInstance.ActiveTarget, _activeFightInstance.ActivePlayer, _activeFightInstance.ActiveAction);
            var combatEnded = IsFightFinished(_activeFightInstance.ActiveEnemies, GetActivePlayer());
            var fightWon = IsFightWon(_activeFightInstance.ActivePlayer);
            if (combatEnded) { FinishFight(fightWon); }
            else
            {
                _activeFightInstance.TurnNumber = UpdateTurnNumber(_activeFightInstance.TurnNumber);
                if(_activeFightInstance.PlayerActionCounter == 0) { _activeFightInstance.PlayerActionCounter = 2; StartAiTurn(); }
               
            }
            
        }
        public List<string> ReturnHistory() => _activeFightInstance.FightHistory;
        public void UpdateActiveTarget(Character character)
        {
            _activeFightInstance.ActiveTarget = character;
        }
        public Data.Repository.Characters.CoreClasses.CharacterAction ChooseAction(int id) => _activeFightInstance.ActivePlayer.AllAvailableActions.First(a => a.CharacterActionId == id);
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
        private void StartAiTurn()
        {
            //Ai needs to determine the actions it wants to use and apply them one by one.
            //The decision making can be random at first but later on we could go for a scripted behaviour depending
            //on current hp AND / OR the amount of dead enemies. For instance if the enemy is the last remainign enemy 
            //he will start to use his special actions. 
            //(update the view every 2 seconds maybe instead of showing it all at once? 

        } 
        public FightInstance GetFightState(int userId)
        {
            //We should return a fight state object instead :TODO 
            return _fightRepository.GetById(userId);
        } 
        public Character GetActiveTarget()
        {
            return _activeFightInstance.ActiveTarget;
        } 
        public Data.Repository.Characters.CoreClasses.CharacterAction GetActiveActions()
        {
            return _activeFightInstance.ActiveAction;
        } 
        public void UpdateActiveAction(Data.Repository.Characters.CoreClasses.CharacterAction action)
        {
            _activeFightInstance.ActiveAction = action;
        }  
        private PlayableCharacter GetActivePlayer()
        {
            return _activeFightInstance.ActivePlayer;
        } 
        private int UpdateTurnNumber(int turnNumber)
        {
            return turnNumber += 1;
        } 
        private List<string> UpdateFightHistory(List<string> FightHistory, string message)
        {
            FightHistory.Add(message);
            return FightHistory;
        } 
        private void ApplyAction(Character target, Character caster, Data.Repository.Characters.CoreClasses.CharacterAction action)
        {
            //TODO: APPLY STATUS 
            if (action.SavingThrowPossible)
            {
                var damage = 0;
                var savingThrow = SpellSavingThrow(target, caster, action);
                if (savingThrow == false)
                {
                    damage = ApplyDamage(target, action, savingThrow);

                    _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                        $" {target.CharacterName} lost {damage} healthpoints, current healthpoints {target.MaxHealthPoints}," +
                        $" due to {caster.CharacterName} using {action.ActionName}");
                }
                else
                {
                    damage = ApplyDamage(target, action, savingThrow);
                    ApplyStatus(target, action.Status);

                    _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                        $" {target.CharacterName} lost {damage} healthpoints, current healthpoints {target.MaxHealthPoints}, " +
                        $"due to {caster.CharacterName} using {action.ActionName}, ");
                }
            }
            else
            { 
                var savingThrow = false;
                if (action.Status != null) { ApplyStatus(target, action.Status); }

                if (action.AggressiveAction) {
                    var damage = ApplyDamage(target, action, savingThrow);

                    if(IsTargetAlive(target))
                    {
                        _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                       $" {target.CharacterName} lost {damage} healthpoints, current healthpoints {target.MaxHealthPoints}," +
                       $" due to {caster.CharacterName}  using {action.ActionName}");
                    }
                    else
                    {
                        _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                       $" {target.CharacterName} lost {damage} healthpoints, current healthpoints {target.MaxHealthPoints}," +
                       $" due to {caster.CharacterName}  using {action.ActionName} and died");
                    }
                }
                else
                {
                    var heal = ApplyHeal(target, action, savingThrow);
                    _activeFightInstance.FightHistory = UpdateFightHistory(_activeFightInstance.FightHistory,
                        $" {target.CharacterName} gained {heal} healthpoints, current healthpoints {target.MaxHealthPoints}," +
                        $" due to {caster.CharacterName}  using {action.ActionName}");
                } 
            }
        }
        private bool IsTargetAlive(Character target){
            if(target.MaxHealthPoints <= 0){ return false;}
            else { return true; }
        } 
        private void ApplyStatus(Character character,Status status)
        {
            if(status != null)
            {
                character.ActiveStatuses.Add(status);
            } 
        } 
        private int ApplyDamage(Character target, Data.Repository.Characters.CoreClasses.CharacterAction actions, bool savingThrow)
        { 
            var updateValue = 0;

            for (int i = 0; i < actions.Hit_Dice_Nr; i++)
            {
                updateValue += DiceThrow(actions.Max_Dmg) + actions.Flat_Dmg;
            }
            if (savingThrow)
            {
                target.CurrentHealthPoints = target.CurrentHealthPoints - (updateValue / 2);
            }
            else
            { 
                target.CurrentHealthPoints = target.CurrentHealthPoints - updateValue;
            }
                return updateValue;
        }
        private int ApplyHeal(Character target, Data.Repository.Characters.CoreClasses.CharacterAction actions, bool savingThrow)
        {
            var updateValue = 0;

            for (int i = 0; i < actions.Hit_Dice_Nr; i++)
            {
                updateValue += DiceThrow(actions.Max_Dmg) + actions.Flat_Dmg;
            }
            if (!savingThrow)
            {
                target.CurrentHealthPoints = target.CurrentHealthPoints + (updateValue / 2);
            }
            else
            {
                target.CurrentHealthPoints = target.CurrentHealthPoints + updateValue;
            }
            return updateValue;
        } 
        private bool SpellSavingThrow(Character target, Character caster, Data.Repository.Characters.CoreClasses.CharacterAction action)
        {
            var targetMod = 0;
            var targetRoll = 0;
            var casterModifier = 0;
            switch (action.StatForTest)
            {
                case Statistics.Strenght:
                    targetMod = CalculateModifier(target.Strength);
                    targetRoll = DiceThrow(20) + targetMod;
                    casterModifier = SpellCastingModifier(caster,action.StatForTest);
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
        private int SpellCastingModifier(Character caster,Statistics statsForTest)
        {
            switch (statsForTest)
            {
                case Statistics.Strenght:
                   return 8 + CalculateModifier(caster.Strength); // Dopisać premię z biegłości...gdy takowa powstanie.
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
        private int DiceThrow(int maxValue)
        {
            Random rand = new Random();
            var diceThrowResult = rand.Next(1, maxValue + 1);
            return diceThrowResult;
        }
        private void FinishFight(bool playerWon)
        {
            _activeFightInstance.CombatFinished = true;
            if (playerWon) { _activeFightInstance.PlayerWon = true; }
            _activeFightInstance.PlayerWon = false;
        } 
        private bool IsFightFinished(List<Enemy> activeEnemies, PlayableCharacter activePlayer)
        {
            if (activeEnemies.Count() == 0 || activePlayer.CurrentHealthPoints == 0) { return true; }
            return false;
        }
        private bool IsFightWon(PlayableCharacter activePlayer)
        {
            if (activePlayer.CurrentHealthPoints > 0) { return true; }
            else { return false; }
        }
    }
}