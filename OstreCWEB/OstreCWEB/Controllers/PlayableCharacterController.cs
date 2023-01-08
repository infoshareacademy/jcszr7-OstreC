using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Data.Repository.Characters.CoreClasses;
using OstreCWEB.Data.Repository.Characters.Enums;
using OstreCWEB.Services.PlayableCharacterService;

namespace OstreCWEB.Controllers
{
    public class PlayableCharacterController : Controller
    {
        PlayableCharacterService _playableCharacterService;

        public PlayableCharacterController()
        {
            _playableCharacterService = new PlayableCharacterService();
        }

        // GET: PlayableCharacterController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PlayableCharacterRace()
        {
            var model = _playableCharacterService.GetRace();
            return View(model);
        }
        public ActionResult PlayableCharacterClass()
        {
            var model = _playableCharacterService.GetClass();
            return View(model);
        }
        public ActionResult PlayableCharacterAttr()
        {
            var model = _playableCharacterService.GetAll();
            return View(model);
        }
        public ActionResult PlayableCharacterSkills()
        {
            var model = _playableCharacterService.GetSkills();
            return View(model);
        }
        public ActionResult PlayableCharacterSummary()
        {
            var model = _playableCharacterService.GetAll();
            return View(model);
        }
        public ActionResult ChangeValue(PlayableCharacter characterModel, string operation, AbilityScores attribute)
        {
            //var model = _playableCharacterService.GetAll();
            _playableCharacterService.Update(characterModel, operation, attribute);
            return RedirectToAction(nameof(PlayableCharacterAttr));
        }
        public ActionResult ChangeSkillValue(CharacterSkills characterModel, string operation, Skills skill)
        {
            //var model = _playableCharacterService.GetAll();
            _playableCharacterService.UpdateSkill(characterModel, operation, skill);
            return RedirectToAction(nameof(PlayableCharacterSkills));
        }
        public ActionResult RollAttributePoints(PlayableCharacter calculatorModel)
        {
            //var model = _playableCharacterService.GetAll();
            _playableCharacterService.RollAttributes(calculatorModel);
            return RedirectToAction(nameof(PlayableCharacterAttr));
        }

        /*

        // GET: PlayableCharacterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlayableCharacterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlayableCharacterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlayableCharacterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlayableCharacterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlayableCharacterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlayableCharacterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
