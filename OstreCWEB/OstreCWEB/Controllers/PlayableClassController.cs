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
    public class PlayableClassController : Controller
    {
        public ICharacterClassRepository<PlayableClass> _characterClassRepository { get; } 
        public IMapper _Mapper { get; }
        public IUserParagraphRepository<UserParagraph> _userParagraphRepository { get; }
        public ILogger<PlayableClassController> _logger { get; }

        public PlayableClassController(
            ICharacterClassRepository<PlayableClass> characterClassRepository,
            IMapper mapper,
            IUserParagraphRepository<UserParagraph> userParagraphRepository,
            ILogger<PlayableClassController> logger
            
            )
        {
            _characterClassRepository = characterClassRepository;
            _Mapper = mapper;
            _userParagraphRepository = userParagraphRepository;
            _logger = logger;
        } 
        public async Task<ActionResult> Index()
        {
            try
            {
                var classes = await _characterClassRepository.GetAllAsync();
                var model = _Mapper.Map<IEnumerable<PlayableClassView>>(classes);
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
                var model = new PlayableClassView();
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
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var model = _Mapper.Map<PlayableClassView>(await _characterClassRepository.GetByIdAsync(id));
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
        public async Task<ActionResult> Edit(PlayableClassView item)
        {
            try
            {
                await _characterClassRepository.UpdateAsync(_Mapper.Map<PlayableClass>(item));
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
                await _userParagraphRepository.DeleteInstanceBasedOnClass(id);
                await _characterClassRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
