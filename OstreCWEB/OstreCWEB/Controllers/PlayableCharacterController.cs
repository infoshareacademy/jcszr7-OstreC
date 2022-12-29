using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OstreCWEB.Controllers
{
    public class PlayableCharacterController : Controller
    {
        CreateCharacterService _createCharacterService;

        // GET: PlayableCharacterController
        public ActionResult Index()
        {
            return View();
        }

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
        }
    }
}
