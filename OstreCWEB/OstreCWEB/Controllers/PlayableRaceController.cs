using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.ViewModel.Characters;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class PlayableRaceController : Controller
    {
        public ICharacterRaceRepository<PlayableRace> _characterRaceRepository { get; }
        public IMapper _Mapper { get; }
        public IUserParagraphRepository<UserParagraph> _userParagraphRepository { get; }
        private readonly ILogger _logger;

        public PlayableRaceController(
            ICharacterRaceRepository<PlayableRace> characterRaceRepository,
            IMapper mapper,
            IUserParagraphRepository<UserParagraph> userParagraphRepository,
            ILogger logger
            
            )
        {
            _characterRaceRepository = characterRaceRepository;
            _Mapper = mapper;
            _userParagraphRepository = userParagraphRepository;
            _logger = logger;
        } 
        public async Task<ActionResult> Index()
        {
            try
            {
                var classes = await _characterRaceRepository.GetAllAsync();
                var model = _Mapper.Map<IEnumerable<PlayableRaceView>>(classes);
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
            
        } 
        public async Task<ActionResult> Create()
        {
            try
            {
                var model = new PlayableRaceView();
                return View(model);
            } 
             catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlayableRaceView playableRace)
        {
            try
            {
                await _characterRaceRepository.AddAsync(_Mapper.Map<PlayableRace>(playableRace));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
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
                var model = _Mapper.Map<PlayableRaceView>(await _characterRaceRepository.GetByIdAsync(id));
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
        public async Task<ActionResult> Edit(PlayableRaceView item)
        {
            try
            {
                await _characterRaceRepository.UpdateAsync(_Mapper.Map<PlayableRace>(item));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
         
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userParagraphRepository.DeleteInstanceBasedOnRace(id);
                await _characterRaceRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); 
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
