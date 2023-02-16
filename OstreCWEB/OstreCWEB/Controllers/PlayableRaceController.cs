using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.ViewModel.Characters;

namespace OstreCWEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class PlayableRaceController : Controller
    {
        public ICharacterRaceRepository _characterRaceRepository { get; }
        public IMapper _Mapper { get; }
        public IUserParagraphRepository _userParagraphRepository { get; }

        public PlayableRaceController(ICharacterRaceRepository characterRaceRepository, IMapper mapper, IUserParagraphRepository userParagraphRepository)
        {
            _characterRaceRepository = characterRaceRepository;
            _Mapper = mapper;
            _userParagraphRepository = userParagraphRepository;
        }
        // GET: ItemController
        public async Task<ActionResult> Index()
        {
            var classes = await _characterRaceRepository.GetAllAsync();
            var model = _Mapper.Map<IEnumerable<PlayableRaceView>>(classes);
            return View(model);
        }

        // GET: ItemController/Create
        public async Task<ActionResult> Create()
        {
            var model = new PlayableRaceView();
            return View(model);
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlayableRaceView playableRace)
        {
            try
            {
                await _characterRaceRepository.CreateAsync(_Mapper.Map<PlayableRace>(playableRace));
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
            var model = _Mapper.Map<PlayableRaceView>(await _characterRaceRepository.GetByIdAsync(id));
            return View(model);
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PlayableRaceView item)
        {
            try
            {
                await _characterRaceRepository.UpdateAsync(_Mapper.Map<PlayableRace>(item));
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
                await _userParagraphRepository.DeleteInstanceBasedOnRace(id);
                await _characterRaceRepository.DeleteAsync(id);
            }
            catch
            {
                //log error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
