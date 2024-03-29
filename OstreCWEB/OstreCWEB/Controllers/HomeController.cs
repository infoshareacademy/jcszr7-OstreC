﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Data.DataBase;
using OstreCWEB.Models;
using System.Diagnostics;
using OstreCWEB.Data.Repository.Identity;
using Microsoft.AspNetCore.Authorization;

namespace OstreCWEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger )
        {
            _logger = logger; 
        }

        public ActionResult Index()
        { 
            return View();
        }
        //public ActionResult InitializeFight()
        //{
        //    return RedirectToAction(nameof(FightView));
        //}
       
        
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