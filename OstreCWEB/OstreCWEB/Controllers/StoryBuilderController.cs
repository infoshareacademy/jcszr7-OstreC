using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Enums;
using OstreCWEB.DomainModels.StoryModels.Properties;
using OstreCWEB.Services.Identity;
using OstreCWEB.Services.StoryServices;
using OstreCWEB.Services.StoryServices.Models;
using OstreCWEB.ViewModel.StoryBuilder;

namespace OstreCWEB.Controllers
{
    [Authorize]
    public class StoryBuilderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<StoryBuilderController> _logger;

        private readonly IStoryService _storyService;
        private readonly IUserService _userService;

        public StoryBuilderController(
            IMapper mapper,
            ILogger<StoryBuilderController> logger,
            IEnumerable<IStoryService> storyService,
            IUserService userService)
        {
            _mapper = mapper;
            _logger = logger;
            _storyService = storyService.First();
            _userService = userService;
        }

        /*
                |S|
                |T|
                |O|
                |R|
                |Y|
        */

        // GET: StoryBuilderController
        public async Task<ActionResult> Index()
        {
            _logger.LogWarning(this + " Index(24)", DateTime.Now);
            var stories = await _storyService.GetStoriesByUserId(_userService.GetUserId(User));
            var model = new List<StoriesView>();
            foreach (var item in stories)
            {
                model.Add(_mapper.Map<StoriesView>(item));
            }

            return View(model);
        }

        // GET: StoryBuilderController/CreateStory
        public async Task<ActionResult> CreateStory()
        {
            var model = new StoriesView();
            return View(model);
        }

        // POST: StoryBuilderController/CreateStory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStory(StoriesView story)
        {
            try
            {
                await _storyService.AddStory(_mapper.Map<Story>(story), _userService.GetUserId(User));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/DeleteStory/5
        public async Task<ActionResult> DeleteStory(int id)
        {
            return View(_mapper.Map<StoriesView>(await _storyService.GetStoryByIdAsync(id)));
        }

        // POST: StoryBuilderController/DeleteStory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteStory(StoriesView story)
        {
            try
            {
                await _storyService.DeleteStory(story.Id, _userService.GetUserId(User));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/EditStory/5
        public async Task<ActionResult> EditStory(int id)
        {
            return View(_mapper.Map<StoriesView>(await _storyService.GetStoryByIdAsync(id)));
        }

        // POST: StoryBuilderController/EditStory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditStory(StoriesView model)
        {
            try
            {
                await _storyService.UpdateStory(model.Id, model.Name, model.Description, _userService.GetUserId(User));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/StoryParagraphsList
        public async Task<ActionResult> StoryParagraphsList(int id)
        {
            var model = _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(id));
            return View(model);
        }

        /*
                |P|
                |A|
                |R|
                |A|
                |G|
                |R|
                |A|
                |P|
                |H|
        */

        // GET: StoryBuilderController/ParagraphDetails/5/1
        public async Task<ActionResult> ParagraphDetails(int id, int paragraphId)
        {
            var model = _mapper.Map<ParagraphDetailsView>(await _storyService.GetParagraphDetailsById(paragraphId, id));

            return View(model);
        }

        // GET: StoryBuilderController/CreatNewParagraph/1
        public async Task<ActionResult> CreatNewParagraph(int id)
        {
            var model = new CreatNewParagraphView();
            model.StoryId = id;
            return View(model);
        }

        // POST: StoryBuilderController/CreatNewParagraph
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatNewParagraph(CreatNewParagraphView paragraph)
        {
            try
            {
                if (paragraph.ParagraphType == ParagraphType.Test)
                {
                    return RedirectToAction(nameof(CreatParagraphTest), paragraph);
                }
                else if (paragraph.ParagraphType == ParagraphType.Fight)
                {
                    return RedirectToAction(nameof(CreatParagraphFight), paragraph);
                }
                else
                {
                    await _storyService.AddParagraph(_mapper.Map<Paragraph>(paragraph), _userService.GetUserId(User));
                    return RedirectToAction(nameof(StoryParagraphsList), _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(paragraph.StoryId)));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/CreatParagraphTest
        public async Task<ActionResult> CreatParagraphTest(CreatNewParagraphView paragraph)
        {
            var model = _mapper.Map<CreatParagraphTestView>(paragraph);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatParagraphTest(CreatParagraphTestView paragraphTest)
        {
            try
            {
                var paragraph = _mapper.Map<Paragraph>(paragraphTest);
                paragraph.TestProp = new TestProp
                {
                    AbilityScores = paragraphTest.AbilityScores,
                    TestDifficulty = paragraphTest.TestDifficulty
                };
                await _storyService.AddParagraph(paragraph, _userService.GetUserId(User));
                return RedirectToAction(nameof(StoryParagraphsList), _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(paragraphTest.StoryId)));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/CreatParagraphFight
        public async Task<ActionResult> CreatParagraphFight(CreatNewParagraphView paragraphFight)
        {
            var model = _mapper.Map<CreatParagraphFightView>(paragraphFight);

            var enemyDictionary = new Dictionary<int, string>();
            var enemiesList = await _storyService.GetAllEnemies();

            foreach (var enemy in enemiesList)
            {
                enemyDictionary.Add(enemy.Id, enemy.CharacterName);
            }

            model.Enemies = enemyDictionary;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatParagraphFight(CreatParagraphFightView paragraphFight)
        {
            try
            {
                var paragraph = _mapper.Map<Paragraph>(paragraphFight);

                paragraph.FightProp = new FightProp
                {
                    ParagraphEnemies = new List<EnemyInParagraph>
                    {
                        new EnemyInParagraph
                        {
                            EnemyId = paragraphFight.FirstEnemyId,
                            AmountOfEnemy = paragraphFight.FirstAmountOfEnemy
                        }
                    }
                };

                if (paragraphFight.SecondAmountOfEnemy != 0)
                {
                    paragraph.FightProp.ParagraphEnemies.Add(
                        new EnemyInParagraph
                        {
                            EnemyId = paragraphFight.SecondEnemyId,
                            AmountOfEnemy = paragraphFight.SecondAmountOfEnemy
                        });
                }

                if (paragraphFight.ThirdAmountOfEnemy != 0)
                {
                    paragraph.FightProp.ParagraphEnemies.Add(
                        new EnemyInParagraph
                        {
                            EnemyId = paragraphFight.ThirdEnemyId,
                            AmountOfEnemy = paragraphFight.ThirdAmountOfEnemy
                        });
                }

                await _storyService.AddParagraph(paragraph, _userService.GetUserId(User));
                return RedirectToAction(nameof(StoryParagraphsList), _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(paragraphFight.StoryId)));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/DeleteParagraph/5
        public async Task<ActionResult> DeleteParagraph(int id)
        {
            var model = _mapper.Map<ParagraphElementView>(await _storyService.GetParagraphById(id));

            return View(model);
        }

        // POST: StoryBuilderController/DeleteParagraph/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteParagraph(ParagraphElementView paragraph)
        {
            try
            {
                await _storyService.DeleteParagraph(paragraph.Id, _userService.GetUserId(User));
                return RedirectToAction(nameof(StoryParagraphsList), _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(paragraph.StoryId)));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/EditParagraph/5
        public async Task<ActionResult> EditParagraph(int id)
        {
            var model = _mapper.Map<EditParagraphView>(await _storyService.GetEditParagraphById(id));

            return View(model);
        }

        // POST: StoryBuilderController/EditParagraph
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditParagraph(EditParagraphView model)
        {
            try
            {
                await _storyService.UpdateParagraph(_mapper.Map<EditParagraph>(model), _userService.GetUserId(User));
                return RedirectToAction(nameof(StoryParagraphsList), _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(model.StoryId)));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/AddEnemyInParagraph/5/1
        public async Task<ActionResult> AddEnemyInParagraph(int fightParagraphId, int paragraphId)
        {
            var model = new EnemyInParagraphView();

            model.ParagraphId = paragraphId;
            model.FightPropId = fightParagraphId;

            var enemyDictionary = new Dictionary<int, string>();
            var enemiesList = await _storyService.GetAllEnemies();

            foreach (var enemy in enemiesList)
            {
                enemyDictionary.Add(enemy.Id, enemy.CharacterName);
            }

            model.Enemies = enemyDictionary;

            return View(model);
        }

        // POST: StoryBuilderController/EditStory/5/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEnemyInParagraph(EnemyInParagraphView model)
        {
            try
            {
                await _storyService.AddEnemyToParagraph(_mapper.Map<EnemyInParagraphService>(model));
                return RedirectToAction(nameof(EditParagraph), _mapper.Map<EditParagraphView>(await _storyService.GetEditParagraphById(model.ParagraphId)));
            }
            catch
            {
                return View();
            }
        }

        // POST: StoryBuilderController/DeleteEnemyFromParagraph/4/2
        public async Task<ActionResult> DeleteEnemyFromParagraph(int fightParagraphId, int paragraphId)
        {
            try
            {
                await _storyService.DeleteEnemyInParagraph(fightParagraphId);
                return RedirectToAction(nameof(EditParagraph), _mapper.Map<EditParagraphView>(await _storyService.GetEditParagraphById(paragraphId)));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/AddItemInParagraph/
        public async Task<ActionResult> AddItemInParagraph(int fightParagraphId, int paragraphId)
        {
            var model = new EnemyInParagraphView();

            model.ParagraphId = paragraphId;
            model.FightPropId = fightParagraphId;

            var enemyDictionary = new Dictionary<int, string>();
            var enemiesList = await _storyService.GetAllEnemies();

            foreach (var enemy in enemiesList)
            {
                enemyDictionary.Add(enemy.Id, enemy.CharacterName);
            }

            model.Enemies = enemyDictionary;

            return View(model);
        }

        // POST: StoryBuilderController/AddItemInParagraph/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddItemInParagraph(EnemyInParagraphView model)
        {
            try
            {
                //await _storyService.AddEnemyToParagraph(_mapper.Map<EnemyInParagraphService>(model));
                return RedirectToAction(nameof(EditParagraph), _mapper.Map<EditParagraphView>(await _storyService.GetEditParagraphById(model.ParagraphId)));
            }
            catch
            {
                return View();
            }
        }

        // POST: StoryBuilderController/DeleteItemFromParagraph
        public async Task<ActionResult> DeleteItemFromParagraph(int fightParagraphId, int paragraphId)
        {
            try
            {
                //await _storyService.DeleteEnemyInParagraph(fightParagraphId);
                return RedirectToAction(nameof(EditParagraph), _mapper.Map<EditParagraphView>(await _storyService.GetEditParagraphById(paragraphId)));
            }
            catch
            {
                return View();
            }
        }

        /*
                |C|
                |H|
                |O|
                |I|
                |C|
                |E|

        */

        // GET: StoryBuilderController/ChoiceDetails/5
        public async Task<ActionResult> ChoiceDetails(int id)
        {
            var choiceDetail = await _storyService.GetChoiceDetailsById(id);
            var model = _mapper.Map<ChoiceDetailsView>(choiceDetail);

            return View(model);
        }

        // GET: StoryBuilderController/ChooseSecondParagraph/1/2
        public async Task<ActionResult> ChooseSecondParagraph(int storyId, int firstParagraphId)
        {
            var model = _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(storyId));
            ViewBag.fightParagraphId = firstParagraphId;
            return View(model);
        }

        // GET: StoryBuilderController/ChooseSecondParagraph/1/2
        public async Task<ActionResult> CreatChoice(int firstParagraphId, int secondParagraphId)
        {
            var model = _mapper.Map<ChoiceCreatorView>(await _storyService.GetChoiceCreator(firstParagraphId, secondParagraphId));
            return View(model);
        }

        // POST: StoryBuilderController/CreatChoice/5/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatChoice(ChoiceCreatorView model)
        {
            try
            {
                await _storyService.AddChoice(_mapper.Map<ChoiceCreator>(model));
                return RedirectToAction(nameof(StoryParagraphsList), _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(model.StoryId)));
            }
            catch
            {
                return View();
            }
        }

        // POST: StoryBuilderController/DeleteEnemyFromParagraph/4/2
        public async Task<ActionResult> DeleteChoice(int choiceId, int storyId)
        {
            try
            {
                await _storyService.DeleteChoice(choiceId);
                return RedirectToAction(nameof(StoryParagraphsList), _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(storyId)));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/EditChoice/1
        public async Task<ActionResult> EditChoice(int id)
        {
            var model = _mapper.Map<ChoiceCreatorView>(await _storyService.GetChoiceCreatorById(id));
            return View(model);
        }

        // POST: StoryBuilderController/EditChoice/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditChoice(ChoiceCreatorView model)
        {
            try
            {
                await _storyService.UpdateChoice(_mapper.Map<ChoiceCreator>(model));
                return RedirectToAction(nameof(StoryParagraphsList), _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(model.StoryId)));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/ChooseSecondParagraph/1/2
        public async Task<ActionResult> ChangeSecondParagraph(int storyId, int choiceId)
        {
            var model = _mapper.Map<StoryParagraphsView>(await _storyService.GetStoryWithParagraphsById(storyId));
            ViewBag.choiceId = choiceId;
            return View(model);
        }

        // GET: StoryBuilderController/ChangeParagraph/1/2
        public async Task<ActionResult> ChangeParagraph(int id, int secondParagraphId)
        {
            var model = _mapper.Map<ChoiceCreatorView>(await _storyService.GetChoiceCreatorById(id, secondParagraphId));
            return View(model);
        }
    }
}