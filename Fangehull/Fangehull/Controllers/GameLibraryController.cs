using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fangehull.Controllers
{
    public class GameLibraryController : Controller
    {
        // GET: GameDictionaryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GameDictionaryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GameDictionaryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameDictionaryController/Create
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

        // GET: GameDictionaryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GameDictionaryController/Edit/5
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

        // GET: GameDictionaryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GameDictionaryController/Delete/5
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
