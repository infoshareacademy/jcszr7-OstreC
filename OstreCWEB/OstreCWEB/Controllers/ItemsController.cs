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
        public ICharacterClassRepository _characterClassRepository { get; }
        public IItemRepository<Item> _ItemRepository { get; }
        public IMapper _Mapper { get; }
        public IAbilitiesRepository _CharacterActionsRepository { get; }

        public ItemsController(ICharacterClassRepository characterClassRepository, IItemRepository<Item> itemRepository, IMapper mapper, IAbilitiesRepository characterActionsRepository)
        {
            _characterClassRepository = characterClassRepository;
            _ItemRepository = itemRepository;
            _Mapper = mapper;
            _CharacterActionsRepository = characterActionsRepository;
        }
        // GET: ItemController
        public async Task<ActionResult> Index(string sortOrder, int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentSort"] = sortOrder;

            int pageSize = 5;
            var items = await _ItemRepository.GetPaginatedListAsync();
            return View(
                  await PaginatedList<Item>.CreateAsync(items, pageNumber ?? 1, pageSize)
                );
        }
       
        // GET: ItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemController/Create
        public async Task<ActionResult> Create()
        {
            var model = new ItemEditView();
            var allActions = await _CharacterActionsRepository.GetAllAsync();
            var allClasses = await _characterClassRepository.GetAllAsync();
            model.AllExistingActions = new Dictionary<int, string>();
            model.AllExistingClasses = new Dictionary<int, string>();
            allActions.ForEach(x => model.AllExistingActions.Add(x.AbilityId, x.AbilityName));
            allClasses.ForEach(x => model.AllExistingClasses.Add(x.PlayableClassId, x.ClassName));
            return View(model);
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
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ItemController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = _Mapper.Map<ItemEditView>(await _ItemRepository.GetByIdAsync(id));
            var allActions = await _CharacterActionsRepository.GetAllAsync();
            var allClasses = await _characterClassRepository.GetAllAsync();
            model.AllExistingActions = new Dictionary<int, string>();
            model.AllExistingClasses = new Dictionary<int, string>();
            allActions.ForEach(x => model.AllExistingActions.Add(x.AbilityId, x.AbilityName));
            allClasses.ForEach(x => model.AllExistingClasses.Add(x.PlayableClassId, x.ClassName));
            return View(model);
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
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ItemController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _ItemRepository.DeleteAsync(id);

            }
            catch
            {
                //log error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
