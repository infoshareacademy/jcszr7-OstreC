using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Services.Fight;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;
using OstreCWEB.ViewModel.Fight;

namespace OstreCWEB.Controllers
{
    [Authorize]
    public class FightController : Controller
    {
        private IFightService _fightService;
        private IFightRepository _fightRepository;
        private IUserService _userService;
        private IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IUserParagraphRepository _userParagraphRepository;
        private readonly ILogger<FightController> _logger;

        public FightController(
            IUserService userService,
            IFightService fightService,
            IFightRepository fightRepository,
            IMapper mapper,
            IUserParagraphRepository userParagraphRepository,
            ILogger<FightController> logger,
            IGameService gameService
            )
        {
            _fightRepository = fightRepository;
            _fightService = fightService;
            _mapper = mapper;
            _userService = userService;
            _userParagraphRepository = userParagraphRepository;
            _logger = logger;
            _gameService = gameService;
        }
        [HttpGet]
        public async Task<ActionResult> InitializeFight()
        {
            try
            {
                var gameInstance = await _userParagraphRepository.GetActiveByUserIdNoTrackingAsync(_userService.GetUserId(User));

                if (gameInstance != null && gameInstance.Paragraph.FightProp != null)
                {
                    await _fightService.InitializeFightAsync(_userService.GetUserId(User), gameInstance);
                }
                else
                {
                    return RedirectToAction("Index", "Game");
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"{ex.Message} // Initialize fight was called but a fight instance already exists for this user.");
            }
            return RedirectToAction(nameof(FightView));
        }
        public async Task<ActionResult> FightView()
        {
            var activeGameInstance = await _userParagraphRepository.GetActiveByUserIdNoTrackingAsync(_userService.GetUserId(User));
            var activeFightInstance = _fightService.GetActiveFightInstance(_userService.GetUserId(User), activeGameInstance.ActiveCharacter.CharacterId);
            var model = new FightViewModel();
            //Each game instance character id is unique. We make sure each figh instance is unique too.Players can have multiple saves per story.
            if (activeFightInstance != null && activeFightInstance.ActivePlayer.CharacterId == activeGameInstance.ActiveCharacter.CharacterId)
            {
                model = _mapper.Map<FightViewModel>(activeFightInstance);
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(InitializeFight));
            }
        }
        [HttpGet]
        public async Task<ActionResult> SetActiveAction(int id)
        {
            var activeGameInstance = await _userParagraphRepository.GetActiveByUserIdNoTrackingAsync(_userService.GetUserId(User));
            var activeFightInstance = _fightService.GetActiveFightInstance(_userService.GetUserId(User), activeGameInstance.ActiveCharacter.CharacterId);
            activeFightInstance.ActionGrantedByItem = false;
            _fightService.UpdateActiveAction(_fightService.ChooseAction(id));
            //We reset active target since each action can target different types of targets.
            _fightService.ResetActiveTarget();
            return RedirectToAction(nameof(FightView));
        }
        [HttpGet]
        public async Task<ActionResult> SetActiveTarget(int id)
        {
            var activeGameInstance = await _userParagraphRepository.GetActiveByUserIdNoTrackingAsync(_userService.GetUserId(User));
            var activeFightInstance = _fightService.GetActiveFightInstance(_userService.GetUserId(User), activeGameInstance.ActiveCharacter.CharacterId);
            var target = _fightService.ChooseTarget(id);
            _fightService.UpdateActiveTarget(target);
            return RedirectToAction(nameof(FightView));
        }
        [HttpGet]
        public async Task<ActionResult> CommitPlayerAction()
        {
            var userId = _userService.GetUserId(User);
            var activeGameInstance = await _userParagraphRepository.GetActiveByUserIdAsync(_userService.GetUserId(User));
            var activeFightInstance = _fightService.GetActiveFightInstance(userId, activeGameInstance.ActiveCharacter.CharacterId);
            await _fightService.CommitAction(userId);
            var fightState = _fightService.GetFightState(_userService.GetUserId(User), activeGameInstance.ActiveCharacter.CharacterId);
            activeFightInstance.ActionGrantedByItem = false;
            _fightService.ResetActiveTarget();
            _fightService.ResetActiveAction();
            if (fightState.CombatFinished)
            {
                //We apply changes from player from static list to player in db. They get saved during game session update in gameservice.  
                activeGameInstance.ActiveCharacter.CurrentHealthPoints = activeFightInstance.ActivePlayer.CurrentHealthPoints;
                activeGameInstance.ActiveCharacter.LinkedActions.ForEach(x => x.UsesLeftBeforeRest = activeFightInstance.ActivePlayer.LinkedActions.FirstOrDefault(y => y.CharacterActionId == x.CharacterActionId).UsesLeftBeforeRest);
                for (var i = activeGameInstance.ActiveCharacter.LinkedItems.Count - 1; i >= 0; i--)
                {
                    var contains = false;
                    var item = activeGameInstance.ActiveCharacter.LinkedItems[i];
                    activeFightInstance.ActivePlayer.LinkedItems.ForEach(x => { if (x.Id == item.Id) { contains = true; } });
                    if (!contains) { activeGameInstance.ActiveCharacter.LinkedItems.RemoveAt(i); }
                }
                if (fightState.PlayerWon)
                {

                    await _fightService.DeleteFightInstanceAsync(userId);
                    await _gameService.NextParagraphAfterFightAsync(activeGameInstance, 1);
                }
                else
                {
                    await _fightService.DeleteFightInstanceAsync(userId);
                    await _gameService.NextParagraphAfterFightAsync(activeGameInstance, 0);
                }
                return RedirectToAction("Index", "StoryReader");
            }

            return RedirectToAction(nameof(FightView));
        }
        public async Task<ActionResult> SetActiveActionFromItem(int id)
        {
            var activeGameInstance = await _userParagraphRepository.GetActiveByUserIdNoTrackingAsync(_userService.GetUserId(User));
            var activeFightInstance = _fightService.GetActiveFightInstance(_userService.GetUserId(User), activeGameInstance.ActiveCharacter.CharacterId);
            var chosenItem = activeFightInstance.ActivePlayer.LinkedItems.First(i => i.Id == id);
            activeFightInstance.IsItemToDelete = false;
            activeFightInstance.ActionGrantedByItem = true;
            if (chosenItem.Item.DeleteOnUse)
            {
                _fightService.UpdateItemToRemove(id);
                activeFightInstance.IsItemToDelete = true;
            }
            _fightService.UpdateActiveAction(_fightService.ChooseAction(chosenItem.Item.ActionToTrigger.CharacterActionId));
            _fightService.ResetActiveTarget();
            return RedirectToAction("FightView");
        }
    }
}
