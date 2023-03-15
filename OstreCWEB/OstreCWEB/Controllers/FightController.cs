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
                return RedirectToAction("Game", "Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Game", "Index");
        }
        [HttpGet]
        public async Task<ActionResult> SetActiveAction(int id)
        {
            try
            {
                var fightInstance = await _fightService.GetFightInstanceAsync();
                await _fightService.UpdateActiveActionAsync(id, fightInstance);
                if (fightInstance != null)
                {
                    var model = _mapper.Map<FightViewModel>(fightInstance);
                    return View(model);
                }

                return RedirectToAction("Game", "Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Game", "Index");
        }
        [HttpGet]
        public async Task<ActionResult> SetActiveTarget(int id)
        {
            try
            {
                var fightInstance = await _fightService.GetFightInstanceAsync();
                await _fightService.UpdateActiveTargetAsync(id, fightInstance);
                if (fightInstance != null)
                {
                    var model = _mapper.Map<FightViewModel>(fightInstance);
                    return View(model);
                }

                return RedirectToAction("Game", "Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Game", "Index");

        }

        [HttpGet]
        public async Task<ActionResult> CommitPlayerAction()
        {

            try
            {
                var userId = _userService.GetUserId(User);
                var fightInstance = await _fightService.GetFightInstanceAsync();
                bool isCombatFinished = await _fightService.CommitActionAsync(userId);
                if (fightInstance != null)
                {
                    if (!isCombatFinished)
                    {
                        var model = _mapper.Map<FightViewModel>(fightInstance);
                        return View(model);
                    }
                    return RedirectToAction("Index", "StoryReader");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Game", "Index");
        }
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
