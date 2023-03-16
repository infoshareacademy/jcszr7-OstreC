using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OstreCWeb.DomainModels.Collections;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.ViewModel.Characters;

namespace OstreCWEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class ItemsController : Controller
    {
        public ICharacterClassRepository<PlayableClass> _characterClassRepository { get; }
        public IItemRepository<Item> _ItemRepository { get; }
        public IMapper _Mapper { get; }
        public IAbilitiesRepository<Ability> _CharacterActionsRepository { get; }
        public ILogger<ItemsController> _logger { get; }

        public ItemsController(
            ICharacterClassRepository<PlayableClass> characterClassRepository,
            IItemRepository<Item> itemRepository,
            IMapper mapper,
            IAbilitiesRepository<Ability> characterActionsRepository,
            ILogger<ItemsController> logger
            )
        {
            _characterClassRepository = characterClassRepository;
            _ItemRepository = itemRepository;
            _Mapper = mapper;
            _CharacterActionsRepository = characterActionsRepository;
            _logger = logger;
        }
        // GET: ItemController 
        [HttpGet]
        public async Task<ActionResult> Index(string sortOrder, int? pageNumber)
        {
            try
            {
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["CurrentSort"] = sortOrder;

                int pageSize = 5;
                var items = await _ItemRepository.GetPaginatedListAsync();
                return View(
                      await PaginatedList<Item>.CreateAsync(items, pageNumber ?? 1, pageSize)
                    );
            }
            catch(Exception ex)
            { 
                _logger.LogError(ex.Message); 
                return RedirectToAction(nameof(Index));
            }
          
        }

        // GET: ItemController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                var model = new ItemEditView();
                var allActions = await _CharacterActionsRepository.GetAllAsync();
                var allClasses = await _characterClassRepository.GetAllAsync();
                model.AllExistingActions = new Dictionary<int, string>();
                model.AllExistingClasses = new Dictionary<int, string>();
                allActions.ForEach(x => model.AllExistingActions.Add(x.Id, x.AbilityName));
                allClasses.ForEach(x => model.AllExistingClasses.Add(x.Id, x.ClassName));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemEditView item)
        {
            try
            {
                await _ItemRepository.UpdateAsync(_Mapper.Map<Item>(item));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ItemController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var model = _Mapper.Map<ItemEditView>(await _ItemRepository.GetByIdAsync(id));
                var allActions = await _CharacterActionsRepository.GetAllAsync();
                var allClasses = await _characterClassRepository.GetAllAsync();
                model.AllExistingActions = new Dictionary<int, string>();
                model.AllExistingClasses = new Dictionary<int, string>();
                allActions.ForEach(x => model.AllExistingActions.Add(x.Id, x.AbilityName));
                allClasses.ForEach(x => model.AllExistingClasses.Add(x.Id, x.ClassName));
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
           
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ItemEditView item)
        {
            try
            {
                await _ItemRepository.UpdateAsync(_Mapper.Map<Item>(item));
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
                await _ItemRepository.DeleteAsync(id);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
