using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fangehull.Controllers
{
    public class FAQController : Controller
    {
        // GET: FAQController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FAQController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FAQController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FAQController/Create
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

        // GET: FAQController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FAQController/Edit/5
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

        // GET: FAQController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FAQController/Delete/5
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
