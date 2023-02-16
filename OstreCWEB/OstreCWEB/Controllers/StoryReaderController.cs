using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.DomainModels.StoryModels.Enums;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;
using OstreCWEB.Services.StoryServices;
using OstreCWEB.ViewModel.StoryReader;

namespace OstreCWEB.Controllers
{
    [Authorize]
    public class StoryReaderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<StoryReaderController> _logger;
        private readonly IGameService _gameService;
        private readonly IUserParagraphRepository _userParagraphRepository;
        private readonly IStoryService _storyService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StoryReaderController(
            IMapper mapper,
            ILogger<StoryReaderController> logger,
            IGameService gameService,
            IUserParagraphRepository userParagraphRepository,
            IStoryService storyService,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _mapper = mapper;
            _logger = logger;
            _gameService = gameService;
            _userParagraphRepository = userParagraphRepository;
            _storyService = storyService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ActionResult> Index()
        {
            var model = new GameStageView();

            var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(_userService.GetUserId(User));
            model.CurrentParagraph = _mapper.Map<CurrentParagraphView>(userParagraph.Paragraph);
            model.CurrentCharacter = _mapper.Map<CurrentCharacterView>(userParagraph.ActiveCharacter);

            model.Rest = userParagraph.Rest;

            if (model.CurrentParagraph.ParagraphType == ParagraphType.Test)
            {
                model.TestParagraphView = new TestParagraphView();
                model.TestParagraphView.Description = $"Roll the dice for {userParagraph.Paragraph.TestProp.AbilityScores}";
                if (_httpContextAccessor.HttpContext.Request.Cookies.Any())
                {
                    var rollCookies = _httpContextAccessor.HttpContext.Request.Cookies.Where(c => c.Key == "Throw");
                    var modifireCookies = _httpContextAccessor.HttpContext.Request.Cookies.Where(c => c.Key == "Modifire");

                    var roll = Convert.ToInt32(rollCookies.ToList().FirstOrDefault().Value);
                    var modifire = Convert.ToInt32(modifireCookies.ToList().FirstOrDefault().Value);
                    if (roll > 0)
                    {
                        model.TestParagraphView.Throw = roll;
                        model.TestParagraphView.ThrowModifier = modifire;
                    }
                }
            }
            else
            {
                CookieOptions options = new CookieOptions();
                //This cookie will expire on session end.
                options.Expires = default(DateTime?);
                options.Path = "/";
                //Bypasses consent policy checks.
                options.IsEssential = true;
                _httpContextAccessor.HttpContext.Response.Cookies.Append("Throw", $"{0}", options);
            }
            return View(model);
        }

        public async Task<ActionResult> CommitNextParagraph(int id)
        {
            await _gameService.NextParagraphAsync(_userService.GetUserId(User), id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> RollTheDice()
        {
            int[] result = await _gameService.ThrowDice(20, _userService.GetUserId(User));

            CookieOptions options = new CookieOptions();
            //This cookie will expire on session end.
            options.Expires = default(DateTime?);
            options.Path = "/";
            //Bypasses consent policy checks.
            options.IsEssential = true;
            _httpContextAccessor.HttpContext.Response.Cookies.Append("Throw", $"{result[0]}", options);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("Modifire", $"{result[1]}", options);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> TestThrow()
        {
            var rollCookies = _httpContextAccessor.HttpContext.Request.Cookies.Where(c => c.Key == "Throw");
            var modifireCookies = _httpContextAccessor.HttpContext.Request.Cookies.Where(c => c.Key == "Modifire");

            var roll = Convert.ToInt32(rollCookies.ToList().FirstOrDefault().Value);
            var modifire = Convert.ToInt32(modifireCookies.ToList().FirstOrDefault().Value);

            int resultOfThrow = await _gameService.TestThrowAsync(_userService.GetUserId(User), roll, modifire);
            await _gameService.NextParagraphAsync(_userService.GetUserId(User), resultOfThrow);
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> HealCharacter()
        {
            await _gameService.HealCharacterAsync(_userService.GetUserId(User));
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> UnequipItem(int id)
        {
            await _gameService.UnequipItemAsync(id, _userService.GetUserId(User));
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Equipitem(int id)
        {
            try
            {
                await _gameService.EquipItemAsync(id, _userService.GetUserId(User));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
