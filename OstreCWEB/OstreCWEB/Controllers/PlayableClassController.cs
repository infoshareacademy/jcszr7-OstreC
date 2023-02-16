using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Data.Repository.Characters.Interfaces;
using OstreCWEB.Data.Repository.ManyToMany;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.ViewModel.Characters;

namespace OstreCWEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class PlayableClassController : Controller
    {
        public ICharacterClassRepository _characterClassRepository { get; }
        public IItemRepository _ItemRepository { get; }
        public IMapper _Mapper { get; }
        public IUserParagraphRepository _userParagraphRepository { get; }
        public IAbilitiesRepository _CharacterActionsRepository { get; }

        public PlayableClassController(ICharacterClassRepository characterClassRepository, IMapper mapper, IUserParagraphRepository userParagraphRepository)
        {
            _characterClassRepository = characterClassRepository;
            _Mapper = mapper;
            _userParagraphRepository = userParagraphRepository;
        }
        // GET: ItemController
        public async Task<ActionResult> Index()
        {
            var classes = await _characterClassRepository.GetAllAsync();
            var model = _Mapper.Map<IEnumerable<PlayableClassView>>(classes);
            return View(model);
        }

        // GET: ItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemController/Create
        public async Task<ActionResult> Create()
        {
            var model = new PlayableClassView();
            return View(model);
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlayableClassView playableClass)
        {
            try
            {
                await _characterClassRepository.UpdateAsync(_Mapper.Map<PlayableClass>(playableClass));
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
            var model = _Mapper.Map<PlayableClassView>(await _characterClassRepository.GetByIdNoIncludesAsync(id));
            return View(model);
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PlayableClassView item)
        {
            try
            {
                await _characterClassRepository.UpdateAsync(_Mapper.Map<PlayableClass>(item));
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
                await _userParagraphRepository.DeleteInstanceBasedOnClass(id);
                await _characterClassRepository.DeleteAsync(id);
            }
            catch
            {
                //log error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
