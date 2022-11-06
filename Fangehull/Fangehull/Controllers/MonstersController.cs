using Fangehull.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fangehull.Controllers
{
    public class MonstersController : Controller
    {
        // GET: EnemiesController
        public ActionResult Index()
        {
            List<MonstersModel> enemies = new List<MonstersModel>()
            {
                new MonstersModel
                {
                    IdMonster = 0,
                    MonsterName = "Goblin",
                    MaxHealtPoints = 10,
                    Dificulty = Difficulty.Easy
                },
                new MonstersModel
                {
                    IdMonster = 1,
                    MonsterName = "Orc",
                    MaxHealtPoints = 20,
                    Dificulty = Difficulty.Normal
                },
                new MonstersModel
                {
                    IdMonster = 2,
                    MonsterName = "Bandit",
                    MaxHealtPoints = 50,
                    Dificulty = Difficulty.Hard
                },
                new MonstersModel
                {
                    IdMonster = 3,
                    MonsterName = "Golem",
                    MaxHealtPoints = 200,
                    Dificulty = Difficulty.Extreme
                },
                new MonstersModel
                {
                    IdMonster = 4,
                    MonsterName = "Dragon",
                    MaxHealtPoints = 450,
                    Dificulty = Difficulty.Insane
                }
            };
            return View(enemies);
        }

        // GET: EnemiesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EnemiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnemiesController/Create
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

        // GET: EnemiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EnemiesController/Edit/5
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

        // GET: EnemiesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EnemiesController/Delete/5
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
        }
    }
}
