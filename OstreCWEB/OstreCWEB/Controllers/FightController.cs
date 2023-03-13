using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.ManyToMany;
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
        private IUserService _userService;
        private IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IUserParagraphRepository<UserParagraph> _userParagraphRepository;
        private readonly ILogger<FightController> _logger;

        public FightController(
            IUserService userService,
            IFightService fightService,
            IFightRepository fightRepository,
            IMapper mapper,
            IUserParagraphRepository<UserParagraph> userParagraphRepository,
            ILogger<FightController> logger,
            IGameService gameService
            )
        {
            _fightService = fightService;
            _mapper = mapper;
            _userService = userService;
            _userParagraphRepository = userParagraphRepository;
            _logger = logger;
            _gameService = gameService;
        }

        public async Task<ActionResult> FightView()
        {
            try
            {
                var fightInstance = await _fightService.GetFightInstanceAsync();
                if (fightInstance != null)
                {
                    var model = _mapper.Map<FightViewModel>(fightInstance);
                    return View(model);
                }
                return RedirectToAction("Game","Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Game","Index");
        }
        [HttpGet]
        public async Task<ActionResult> SetActiveAction(int id)
        {
            var activeGameInstance = await _userParagraphRepository.GetActiveByUserIdNoTrackingAsync(_userService.GetUserId(User));
            var activeFightInstance = _fightService.GetActiveFightInstance(_userService.GetUserId(User), activeGameInstance.ActiveCharacter.Id);
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
            var activeFightInstance = _fightService.GetActiveFightInstance(_userService.GetUserId(User), activeGameInstance.ActiveCharacter.Id);
            var target = _fightService.ChooseTarget(id);
            _fightService.UpdateActiveTarget(target);
            return RedirectToAction(nameof(FightView));
        }
        [HttpGet]
        public async Task<ActionResult> CommitPlayerAction()
        {
            var userId = _userService.GetUserId(User);
            var activeGameInstance = await _userParagraphRepository.GetActiveByUserIdAsync(_userService.GetUserId(User));
            var activeFightInstance = _fightService.GetActiveFightInstance(userId, activeGameInstance.ActiveCharacter.Id);
            await _fightService.CommitAction(userId);
     ;

            //if (fightState.CombatFinished)
            //{
            //    //We apply changes from player from static list to player in db. They get saved during game session update in gameservice.
            //    activeGameInstance.ActiveCharacter.CurrentHealthPoints = activeFightInstance.ActivePlayer.CurrentHealthPoints;
            //    activeGameInstance.ActiveCharacter.LinkedAbilities.ForEach(x => x.UsesLeftBeforeRest = activeFightInstance.ActivePlayer.LinkedAbilities.FirstOrDefault(y => y.CharacterActionId == x.CharacterActionId).UsesLeftBeforeRest);
            //    for (var i = activeGameInstance.ActiveCharacter.LinkedItems.Count - 1; i >= 0; i--)
            //    {
            //        var contains = false;
            //        var item = activeGameInstance.ActiveCharacter.LinkedItems[i];
            //        activeFightInstance.ActivePlayer.LinkedItems.ForEach(x => { if (x.Id == item.Id) { contains = true; } });
            //        if (!contains) { activeGameInstance.ActiveCharacter.LinkedItems.RemoveAt(i); }
            //    }
            //    if (fightState.PlayerWon)
            //    {

            //        await _fightService.DeleteFightInstanceAsync(userId);
            //        await _gameService.NextParagraphAfterFightAsync(activeGameInstance, 1);
            //    }
            //    else
            //    {
            //        await _fightService.DeleteFightInstanceAsync(userId);
            //        await _gameService.NextParagraphAfterFightAsync(activeGameInstance, 0);
            //    }
            //    return RedirectToAction("Index", "StoryReader");
            //}

            var model = _mapper.Map<FightViewModel>(activeFightInstance);
            return View(model);
        }
        public async Task<ActionResult> SetActiveActionFromItem(int id)
        {
            var activeGameInstance = await _userParagraphRepository.GetActiveByUserIdNoTrackingAsync(_userService.GetUserId(User));
            var activeFightInstance = _fightService.GetActiveFightInstance(_userService.GetUserId(User), activeGameInstance.ActiveCharacter.Id);
            var chosenItem = activeFightInstance.ActivePlayer.LinkedItems.First(i => i.Id == id);
            activeFightInstance.IsItemToDelete = false;
            activeFightInstance.ActionGrantedByItem = true;
            if (chosenItem.Item.DeleteOnUse)
            {
                _fightService.UpdateItemToRemove(id);
                activeFightInstance.IsItemToDelete = true;
            }
            _fightService.UpdateActiveAction(_fightService.ChooseAction(chosenItem.Item.Ability.Id));
            _fightService.ResetActiveTarget();
            return RedirectToAction("FightView");
        }
    }
}
