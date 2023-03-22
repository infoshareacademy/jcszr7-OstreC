using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Services.Characters;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;
using OstreCWEB.Services.StoryService;
using OstreCWEB.ViewModel.Characters;
using OstreCWEB.ViewModel.Game;
using OstreCWEB.ViewModel.Identity;
using OstreCWEB.Services.StoryService.ModelsDto;

namespace OstreCWEB.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IStoryServices _storyService;
        private readonly IPlayableCharacterService _playableCharacterService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGameService _gameService;

        public GameController(IGameService gameService, IHttpContextAccessor httpContextAccessor, IUserService userService, IMapper mapper, IStoryServices storyService, IPlayableCharacterService playableCharacterService)
        {
            _userService = userService;
            _mapper = mapper;
            _storyService = storyService;
            _playableCharacterService = playableCharacterService;
            _httpContextAccessor = httpContextAccessor;
            _gameService = gameService;
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
                    model.ActiveCharacter = _mapper.Map<PlayableCharacterView>(await _playableCharacterService.GetById(Convert.ToInt32(activeCharacterCookies.ToList().FirstOrDefault().Value)));
                }
                if (activeStoryCookies.Any() && _storyService.Exists(Convert.ToInt32(activeStoryCookies.FirstOrDefault().Value)))
                {
                    model.ActiveStory = await _storyService.GetStoryByIdAsync(Convert.ToInt32(activeStoryCookies.ToList().FirstOrDefault().Value));
                }
            };

            var x = await _userService.GetUserById(_userService.GetUserId(User));
            model.User = _mapper.Map<UserView>(x);
            foreach (var gameSessionView in model.User.UserParagraphs)
            {
                var getStoryTask = _storyService.GetStoryById(gameSessionView.Paragraph.StoryId);
                gameSessionView.Story = _mapper.Map<StoryView>(getStoryTask);
            }
            Console.WriteLine();
            model.OtherUsersStories = _mapper.Map<List<StoryView>>(await _storyService.GetAllStories());
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
            _httpContextAccessor.HttpContext.Response.Cookies.Append("ActiveCharacter", $"{id}", options);
            return RedirectToAction(nameof(Index));
        }
    }
}