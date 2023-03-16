using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.ViewModel.Characters;

namespace OstreCWEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class CharacterActionController : Controller
    {
        public ICharacterClassRepository<PlayableClass> _characterClassRepository { get; }
        public ILogger<CharacterActionController> _logger { get; }
        public IMapper _Mapper { get; }
        public IAbilitiesRepository<Ability> _characterActionsRepository { get; }
        public IStatusRepository<Status> _statusRepository { get; }

        public CharacterActionController(
            IMapper mapper,
            IAbilitiesRepository<Ability> characterActionsRepository,
            IStatusRepository<Status> status,
            ICharacterClassRepository<PlayableClass> characterClassRepository,
            ILogger<CharacterActionController> logger
            
            )
        {
            _Mapper = mapper;
            _characterActionsRepository = characterActionsRepository;
            _statusRepository = status;
            _characterClassRepository = characterClassRepository;
            _logger = logger;
        }
        // GET: ItemController
        public async Task<ActionResult> Index()
        {
            try
            {
                var actions = await _characterActionsRepository.GetAllAsync();
                var model = _Mapper.Map<IEnumerable<AbilityView>>(actions);
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
            
        } 

        // GET: ItemController/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                var model = new AbilityEditView();
                model.AllStatuses = new Dictionary<int, string>();
                model.AllClasses = new Dictionary<int, string>();
                var statuses = await _statusRepository.GetAllAsync();
                var classes = await _characterClassRepository.GetAllAsync();
                statuses.ForEach(x => model.AllStatuses.Add(x.Id, x.StatusType.ToString()));
                classes.ForEach(x => model.AllClasses.Add(x.Id, x.ClassName));
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
            
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AbilityEditView item)
        {
            try
            {
                await _characterActionsRepository.UpdateAsync(_Mapper.Map<Ability>(item));
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ItemController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var model = _Mapper.Map<AbilityEditView>(await _characterActionsRepository.GetByIdAsync(id, x => x.Status, x => x.LinkedCharacter));
                model.AllStatuses = new Dictionary<int, string>();
                model.AllClasses = new Dictionary<int, string>();
                var statuses = await _statusRepository.GetAllAsync();
                var classes = await _characterClassRepository.GetAllAsync();
                statuses.ForEach(x => model.AllStatuses.Add(x.Id, x.StatusType.ToString()));
                classes.ForEach(x => model.AllClasses.Add(x.Id, x.ClassName));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
            
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AbilityEditView item)
        {
            try
            {
                await _characterActionsRepository.UpdateAsync(_Mapper.Map<Ability>(item));
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ItemController/DeleteAsync/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _characterActionsRepository.DeleteAsync(id); 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
