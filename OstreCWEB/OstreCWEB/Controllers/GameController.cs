using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.Identity;
using OstreCWEB.Repository.Repository.StoryRepo;
using OstreCWEB.Services.Characters;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;
using OstreCWEB.Services.StoryService;
using OstreCWEB.Services.StoryService.ModelsDto;
using OstreCWEB.ViewModel.Characters;
using OstreCWEB.ViewModel.Game;
using OstreCWEB.ViewModel.Identity;

namespace OstreCWEB.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IStoryServices _storyService;
        private readonly IPlayableCharacterService _playableCharacterService;
        private readonly IPlayableCharacterRepository<PlayableCharacter> _playableCharacterRepo;
        private readonly IIdentityRepository<User> _identityRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGameService _gameService;
        private readonly IStoryRepository<Story> _storyRepository;

        public GameController(IGameService gameService,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService, IMapper mapper,
            IStoryRepository<Story> storyRepository,
            IPlayableCharacterService playableCharacterService,
            IPlayableCharacterRepository<PlayableCharacter> playableCharacterRepo,
            IIdentityRepository<User> identityRepository,
            IStoryServices storyService
            )
        {
            _userService = userService;
            _mapper = mapper;
            _storyRepository = storyRepository;
            _playableCharacterService = playableCharacterService;
            _playableCharacterRepo = playableCharacterRepo;
            _identityRepository = identityRepository;
            _httpContextAccessor = httpContextAccessor;
            _gameService = gameService;
            _storyService = storyService;
        }

        // GET: GameController
        [HttpGet]
        public async Task<ActionResult> StartGame()
        {
            try
            {
                var activeCharacterCookies = _httpContextAccessor.HttpContext.Request.Cookies.Where(c => c.Key == "ActiveCharacter");
                var activeStoryCookies = _httpContextAccessor.HttpContext.Request.Cookies.Where(c => c.Key == "ActiveStory");
                if (activeCharacterCookies != null && activeCharacterCookies.FirstOrDefault().Key != null && activeStoryCookies != null && activeStoryCookies.FirstOrDefault().Key != null)
                {
                    if (_playableCharacterService.Exists(Convert.ToInt32(activeCharacterCookies.FirstOrDefault().Value)) && _storyService.Exists(Convert.ToInt32(activeStoryCookies.FirstOrDefault().Value)))
                    {
                        var gameInstance = await _gameService.CreateNewGameInstanceAsync(
                            _userService.GetUserId(User),
                            Convert.ToInt32(activeCharacterCookies.FirstOrDefault().Value),
                            Convert.ToInt32(activeStoryCookies.FirstOrDefault().Value));
                    }

                    return RedirectToAction("Index", "StoryReader");
                }
                else
                {
                    TempData["msg"] = "You didn't select both a character and a story to play!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "" +
                    "You chose a character and a story but starting a new game failed. | " +
                    "Your max save games limit is 5 | " +
                    "If the issue persists contact ostreCGame@gmail.com";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> DeleteGame(int id)
        {
            await _gameService.DeleteGameInstanceAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> LoadGame(int id)
        {
            await _gameService.SetActiveGameInstanceAsync(id, _userService.GetUserId(User));
            return RedirectToAction("Index", "StoryReader");
        }

        public async Task<ActionResult> Index()
        {
            var model = new StartGameView();
            if (_httpContextAccessor.HttpContext.Request.Cookies.Any())
            {
                var activeCharacterCookies = _httpContextAccessor.HttpContext.Request.Cookies.Where(c => c.Key == "ActiveCharacter");
                var activeStoryCookies = _httpContextAccessor.HttpContext.Request.Cookies.Where(c => c.Key == "ActiveStory");

                if (activeCharacterCookies.Any() && _playableCharacterService.Exists(Convert.ToInt32(activeCharacterCookies.FirstOrDefault().Value)))
                {
                    var character = await _playableCharacterService.GetById(Convert.ToInt32(activeCharacterCookies.ToList().FirstOrDefault().Value));
                    model.ActiveCharacterName = character.CharacterName;
                }
                if (activeStoryCookies.Any() && _storyService.Exists(Convert.ToInt32(activeStoryCookies.FirstOrDefault().Value)))
                {
                    var story = await _storyService.GetStoryByIdAsync(Convert.ToInt32(activeStoryCookies.ToList().FirstOrDefault().Value));
                    model.ActiveStoryName = story.Name;
                }
            } 
            var user = await _identityRepository.GetUserByIdForLobbyAsync(_userService.GetUserId(User));
            var allCharacter = await _playableCharacterRepo.GetAllTemplatesForLobby(user.Id);
            var stories = await _storyRepository.GetAllStoriesAsyncNoTrackingAsync();

            model.UserParagraphs = _mapper.Map<List<UserParagraphView>>(user.UserParagraphs);

            var gr1 = allCharacter.GroupBy(x => x.UserId == user.Id).ToList();
            foreach (var list in gr1)
            {
                if (list.Key) { model.UserCharacters = _mapper.Map<List<PlayableCharacterRow>>(list); }
                if (!list.Key) { model.OtherUsersCharacters = _mapper.Map<List<PlayableCharacterRow>>(list); }
            }

            var gr2 = stories.GroupBy(x => x.UserId == user.Id).ToList();
            foreach (var list in gr2)
            {
                if (list.Key) { model.UserStories = _mapper.Map<List<StoryView>>(list); }
                if (!list.Key) { model.OtherUsersStories = _mapper.Map<List<StoryView>>(list); }
            }
            //foreach (var gameSessionView in model.User.UserParagraphs)
            //{
            //    var getStoryTask = await _storyService.GetStoryByIdAsync(gameSessionView.Paragraph.StoryId);
            //    gameSessionView.Story = _mapper.Map<StoryView>(getStoryTask);
            //}
            Console.WriteLine();
            model.OtherUsersStories = await _storyService.GetAllStories();
            model.OtherUsersCharacters = _mapper.Map<List<PlayableCharacterRow>>(await _playableCharacterService.GetAllTemplates(_userService.GetUserId(User)));
            return View(model);
        }
        private async Task BuildUserParagraphStoriesView()
        {
        }
        [HttpGet]
        public ActionResult SetActiveStory(int id)
        {

            CookieOptions options = new CookieOptions();
            //This cookie will expire on session end.
            options.Expires = default(DateTime?);
            options.Path = "/";
            //Bypasses consent policy checks.
            options.IsEssential = true;
            _httpContextAccessor.HttpContext.Response.Cookies.Append("ActiveStory", $"{id}", options);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult SetActiveCharacter(int id)
        {
            CookieOptions options = new CookieOptions();
            //This cookie will expire on session end.
            options.Expires = default(DateTime?);
            options.Path = "/";
            //Bypasses consent policy checks.
            options.IsEssential = true;
            return RedirectToAction(nameof(Index));
        }
    }
}