using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Services.Characters;
using OstreCWEB.Services.Identity;
using OstreCWEB.ViewModel.Characters;

namespace OstreCWEB.Controllers
{
    [Authorize]
    public class PlayableCharacterController : Controller
    {
        private readonly IPlayableCharacterService _playableCharacterService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PlayableCharacterController(IPlayableCharacterService playableCharacterService, IUserService userService, IMapper mapper)
        {
            _playableCharacterService = playableCharacterService;
            _userService = userService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            //var model = _playableCharacterService.GetAll();
            return View();
        }

        // POST: CharacterCreatorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayableCharacterCreateView model)
        {
            try
            {
                var playableCharacter = _mapper.Map<PlayableCharacter>(model);
                playableCharacter.UserId = _userService.GetUserId(User);
                _playableCharacterService.Create(playableCharacter);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SetRace()
        {
            var racesDictionary = new Dictionary<int, string>();
            var modelCharacterList = _playableCharacterService.GetAllRaces();

            foreach (var race in modelCharacterList)
            {
                racesDictionary.Add(race.PlayableRaceId, race.RaceName);
            }

            var model = new PlayableCharacterCreateView();
            model.CharacterRaces = racesDictionary;

            string humanDescription = _playableCharacterService.GetRaceDescription(0);
            ViewBag.HumanDescription = humanDescription;

            string elfDescription = _playableCharacterService.GetRaceDescription(1);
            ViewBag.ElfDescription = elfDescription;

            string dwarfDescription = _playableCharacterService.GetRaceDescription(2);
            ViewBag.DwarfDescription = dwarfDescription;

            @ViewBag.RaceInfoHuman = "Strength: 1, Charisma: 1";
            @ViewBag.RaceInfoDwarf = "Strength: 1, Constitution: 1";
            @ViewBag.RaceInfoElf = "Intelligence: 1, Wisdom: 1";
            return View(model);
        }

        public ActionResult SetClass(PlayableCharacterCreateView model)
        {
            var classesDictionary = new Dictionary<int, string>();
            var modelCharacterList = _playableCharacterService.GetAllClasses();

            foreach (var charClass in modelCharacterList)
            {
                classesDictionary.Add(charClass.PlayableClassId, charClass.ClassName);
            }

            model.CharacterClasses = classesDictionary;

            string fighterDescription = _playableCharacterService.GetClassDescription(0);
            ViewBag.FighterDescription = fighterDescription;

            string wizardDescription = _playableCharacterService.GetClassDescription(1);
            ViewBag.WizardDescription = wizardDescription;

            string clericDescription = _playableCharacterService.GetClassDescription(2);
            ViewBag.ClericDescription = clericDescription;

            @ViewBag.ClassInfoFighter = "Strength: 1, Constitution: 1";
            @ViewBag.ClassInfoWizard = "Intelligence: 1";
            @ViewBag.ClassInfoCleric = "Wisdom: 1";

            return View(model);
        }

        public ActionResult SetAttributes(PlayableCharacterCreateView model)
        {
            ViewBag.Str = _playableCharacterService.RollDice();
            ViewBag.Dex = _playableCharacterService.RollDice();
            ViewBag.Con = _playableCharacterService.RollDice();
            ViewBag.Int = _playableCharacterService.RollDice();
            ViewBag.Wis = _playableCharacterService.RollDice();
            ViewBag.Cha = _playableCharacterService.RollDice();
            ViewBag.Sum = ViewBag.Str + ViewBag.Dex + ViewBag.Con + ViewBag.Int + ViewBag.Wis + ViewBag.Cha;
            ViewBag.AvailablePoints = 0;

            ViewBag.RaceId = model.RaceId;
            ViewBag.ClassId = model.PlayableClassId;

            var characterClasses = _playableCharacterService.GetAllClasses();
            var characterRaces = _playableCharacterService.GetAllRaces();

            var bonusClassStr = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.StrengthBonus).FirstOrDefault();
            var bonusClassDex = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.DexterityBonus).FirstOrDefault();
            var bonusClassCon = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.ConstitutionBonus).FirstOrDefault();
            var bonusClassInt = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.IntelligenceBonus).FirstOrDefault();
            var bonusClassWis = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.WisdomBonus).FirstOrDefault();
            var bonusClassCha = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.CharismaBonus).FirstOrDefault();

            var bonusRaceStr = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.StrengthBonus).FirstOrDefault();
            var bonusRaceDex = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.DexterityBonus).FirstOrDefault();
            var bonusRaceCon = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.ConstitutionBonus).FirstOrDefault();
            var bonusRaceInt = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.IntelligenceBonus).FirstOrDefault();
            var bonusRaceWis = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.WisdomBonus).FirstOrDefault();
            var bonusRaceCha = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.CharismaBonus).FirstOrDefault();

            var className = characterClasses.Where(c => c.PlayableClassId == model.RaceId).Select(c => c.ClassName).FirstOrDefault();

            ViewBag.BonusClassStr = bonusClassStr;
            ViewBag.BonusClassDex = bonusClassDex;
            ViewBag.BonusClassCon = bonusClassCon;
            ViewBag.BonusClassInt = bonusClassInt;
            ViewBag.BonusClassWis = bonusClassWis;
            ViewBag.BonusClassCha = bonusClassCha;

            ViewBag.BonusRaceStr = bonusRaceStr;
            ViewBag.BonusRaceDex = bonusRaceDex;
            ViewBag.BonusRaceCon = bonusRaceCon;
            ViewBag.BonusRaceInt = bonusRaceInt;
            ViewBag.BonusRaceWis = bonusRaceWis;
            ViewBag.BonusRaceCha = bonusRaceCha;

            ViewBag.BonusStr = bonusClassStr + bonusRaceStr;
            ViewBag.BonusDex = bonusClassDex + bonusRaceDex;
            ViewBag.BonusCon = bonusClassCon + bonusRaceCon;
            ViewBag.BonusInt = bonusClassInt + bonusRaceInt;
            ViewBag.BonusWis = bonusClassWis + bonusRaceWis;
            ViewBag.BonusCha = bonusClassCha + bonusRaceCha;




            ViewBag.ClassName = className;
            //
            return View(model);
        }

        public ActionResult SetName(PlayableCharacterCreateView model)
        {
            var listName = _playableCharacterService.GetAllNames();

            ViewBag.RaceId = model.RaceId;
            ViewBag.ClassId = model.PlayableClassId;
            ViewBag.Str = model.Strenght;
            ViewBag.Dex = model.Dexterity;
            ViewBag.Con = model.Constitution;
            ViewBag.Int = model.Intelligence;
            ViewBag.Wis = model.Wisdom;
            ViewBag.Cha = model.Charisma;

            ViewBag.MaleNames = listName;

            return View(model);
        }

        public ActionResult Summary(PlayableCharacterCreateView model)
        {
            var characterClasses = _playableCharacterService.GetAllClasses();
            var characterRaces = _playableCharacterService.GetAllRaces();

            var bonusClassStr = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.StrengthBonus).FirstOrDefault();
            var bonusClassDex = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.DexterityBonus).FirstOrDefault();
            var bonusClassCon = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.ConstitutionBonus).FirstOrDefault();
            var bonusClassInt = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.IntelligenceBonus).FirstOrDefault();
            var bonusClassWis = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.WisdomBonus).FirstOrDefault();
            var bonusClassCha = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.CharismaBonus).FirstOrDefault();

            var bonusRaceStr = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.StrengthBonus).FirstOrDefault();
            var bonusRaceDex = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.DexterityBonus).FirstOrDefault();
            var bonusRaceCon = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.ConstitutionBonus).FirstOrDefault();
            var bonusRaceInt = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.IntelligenceBonus).FirstOrDefault();
            var bonusRaceWis = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.WisdomBonus).FirstOrDefault();
            var bonusRaceCha = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.CharismaBonus).FirstOrDefault();

            var _str = model.Strenght + bonusClassStr + bonusRaceStr;
            var _dex = model.Dexterity + bonusClassDex + bonusRaceDex;
            var _con = model.Constitution + bonusClassCon + bonusRaceCon;
            var _int = model.Intelligence + bonusClassInt + bonusRaceInt;
            var _wis = model.Wisdom + bonusClassWis + bonusRaceWis;
            var _cha = model.Charisma + bonusClassCha + bonusRaceCha;

            var modStr = _playableCharacterService.CalculateModifier(_str);
            var modDex = _playableCharacterService.CalculateModifier(_dex);
            var modCon = _playableCharacterService.CalculateModifier(_con);
            var modInt = _playableCharacterService.CalculateModifier(_int);
            var modWis = _playableCharacterService.CalculateModifier(_wis);
            var modCha = _playableCharacterService.CalculateModifier(_cha);

            ViewBag.RaceId = model.RaceId;
            var raceName = characterRaces.Where(c => c.PlayableRaceId == model.RaceId).Select(c => c.RaceName).FirstOrDefault();
            ViewBag.RaceName = raceName;

            ViewBag.ClassId = model.PlayableClassId;
            var className = characterClasses.Where(c => c.PlayableClassId == model.PlayableClassId).Select(c => c.ClassName).FirstOrDefault();
            ViewBag.ClassName = className;

            ViewBag.Str = _str;
            ViewBag.Dex = _dex;
            ViewBag.Con = _con;
            ViewBag.Int = _int;
            ViewBag.Wis = _wis;
            ViewBag.Cha = _cha;

            ViewBag.ModStr = modStr;
            ViewBag.ModDex = modDex;
            ViewBag.ModCon = modCon;
            ViewBag.ModInt = modInt;
            ViewBag.ModWis = modWis;
            ViewBag.ModCha = modCha;

            var currentHealth = characterClasses.Where(x => x.PlayableClassId == model.PlayableClassId).Select(c => c.BaseHP).FirstOrDefault();
            ViewBag.Health = currentHealth + modCon;

            ViewBag.Name = model.CharacterName;

            return View(model);
        }
        public ActionResult RollAttributePoints(PlayableCharacter model)
        {
            _playableCharacterService.RollAttributes(model);
            return RedirectToAction(nameof(SetAttributes));
        }
    }
}
