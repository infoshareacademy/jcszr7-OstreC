using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.DomainModels.CharacterModels;
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
                racesDictionary.Add(race.Id, race.RaceName);
            }

            var model = new PlayableCharacterCreateView();
            model.CharacterRaces = racesDictionary;

            int humanId = racesDictionary.FirstOrDefault(x => x.Value == "Human").Key -1;
            int elfId = racesDictionary.FirstOrDefault(x => x.Value == "Elf").Key -1;
            int dwarfId = racesDictionary.FirstOrDefault(x => x.Value == "Dwarf").Key -1;


            ViewBag.HumanDescription = _playableCharacterService.GetRaceDescription(humanId);
            ViewBag.ElfDescription = _playableCharacterService.GetRaceDescription(elfId);
            ViewBag.DwarfDescription = _playableCharacterService.GetRaceDescription(dwarfId);

            ViewBag.RaceInfoHuman = _playableCharacterService.GetRaceBonus(humanId);
            ViewBag.RaceInfoElf = _playableCharacterService.GetRaceBonus(elfId);
            ViewBag.RaceInfoDwarf = _playableCharacterService.GetRaceBonus(dwarfId);            
            return View(model);
        }

        public ActionResult SetClass(PlayableCharacterCreateView model)
        {
            var classesDictionary = new Dictionary<int, string>();
            var modelCharacterList = _playableCharacterService.GetAllClasses();

            foreach (var charClass in modelCharacterList)
            {
                classesDictionary.Add(charClass.Id, charClass.ClassName);
            }

            model.CharacterClasses = classesDictionary;

            int fighterId = classesDictionary.FirstOrDefault(x => x.Value == "Fighter").Key - 1;
            int wizardId = classesDictionary.FirstOrDefault(x => x.Value == "Wizard").Key - 1;
            int clericId = classesDictionary.FirstOrDefault(x => x.Value == "Cleric").Key - 1;

            ViewBag.FighterDescription = _playableCharacterService.GetClassDescription(fighterId);
            ViewBag.WizardDescription = _playableCharacterService.GetClassDescription(wizardId);
            ViewBag.ClericDescription = _playableCharacterService.GetClassDescription(clericId);

            ViewBag.ClassInfoFighter = _playableCharacterService.GetClassBonus(fighterId);
            ViewBag.ClassInfoWizard = _playableCharacterService.GetClassBonus(wizardId);
            ViewBag.ClassInfoCleric = _playableCharacterService.GetClassBonus(clericId);
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

            var bonusClassStr = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.StrengthBonus).FirstOrDefault();
            var bonusClassDex = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.DexterityBonus).FirstOrDefault();
            var bonusClassCon = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.ConstitutionBonus).FirstOrDefault();
            var bonusClassInt = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.IntelligenceBonus).FirstOrDefault();
            var bonusClassWis = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.WisdomBonus).FirstOrDefault();
            var bonusClassCha = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.CharismaBonus).FirstOrDefault();

            var bonusRaceStr = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.StrengthBonus).FirstOrDefault();
            var bonusRaceDex = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.DexterityBonus).FirstOrDefault();
            var bonusRaceCon = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.ConstitutionBonus).FirstOrDefault();
            var bonusRaceInt = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.IntelligenceBonus).FirstOrDefault();
            var bonusRaceWis = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.WisdomBonus).FirstOrDefault();
            var bonusRaceCha = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.CharismaBonus).FirstOrDefault();

            var className = characterClasses.Where(c => c.Id == model.RaceId).Select(c => c.ClassName).FirstOrDefault();

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
            return View(model);
        }

        public ActionResult Summary(PlayableCharacterCreateView model)
        {
            var characterClasses = _playableCharacterService.GetAllClasses();
            var characterRaces = _playableCharacterService.GetAllRaces();

            var bonusClassStr = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.StrengthBonus).FirstOrDefault();
            var bonusClassDex = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.DexterityBonus).FirstOrDefault();
            var bonusClassCon = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.ConstitutionBonus).FirstOrDefault();
            var bonusClassInt = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.IntelligenceBonus).FirstOrDefault();
            var bonusClassWis = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.WisdomBonus).FirstOrDefault();
            var bonusClassCha = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.CharismaBonus).FirstOrDefault();

            var bonusRaceStr = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.StrengthBonus).FirstOrDefault();
            var bonusRaceDex = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.DexterityBonus).FirstOrDefault();
            var bonusRaceCon = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.ConstitutionBonus).FirstOrDefault();
            var bonusRaceInt = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.IntelligenceBonus).FirstOrDefault();
            var bonusRaceWis = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.WisdomBonus).FirstOrDefault();
            var bonusRaceCha = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.CharismaBonus).FirstOrDefault();

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
            var raceName = characterRaces.Where(c => c.Id == model.RaceId).Select(c => c.RaceName).FirstOrDefault();
            ViewBag.RaceName = raceName;

            ViewBag.ClassId = model.PlayableClassId;
            var className = characterClasses.Where(c => c.Id == model.PlayableClassId).Select(c => c.ClassName).FirstOrDefault();
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

            var currentHealth = characterClasses.Where(x => x.Id == model.PlayableClassId).Select(c => c.BaseHP).FirstOrDefault();
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
