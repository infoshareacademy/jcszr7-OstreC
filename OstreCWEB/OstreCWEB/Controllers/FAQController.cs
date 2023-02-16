using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Models;
using System.Diagnostics;
namespace OstreCWEB.Controllers
{
    public class FAQController : Controller
    {
        private readonly ILogger<FAQController> _logger;
        public FAQController(ILogger<FAQController> logger) => _logger = logger;

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}