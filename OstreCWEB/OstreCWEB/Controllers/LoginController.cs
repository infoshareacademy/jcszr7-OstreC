﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using OstreCWEB.Data.Repository.Identity;
using OstreCWEB.Services.Identity;
using System.Net.Mail;
using System.Net;
using NuGet.Packaging.Signing;

namespace OstreCWEB.Controllers
{

    public class LoginController : Controller
    {
        private readonly IUserAuthenticationService _service;
        private readonly IUserService _userService;


        public LoginController(IUserAuthenticationService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Registration model)
        {
            if (!ModelState.IsValid)
                return View(model);
            model.Role = "user";
            var result = await _service.RegisterAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Game");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.ChangePasswordAsync(model, _userService.GetUserId(User));
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(ChangePassword));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.ChangePasswordAsync(model, _userService.GetUserId(User));
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(ChangePassword));
            }
            return View(model);
        }






        // Admin account
        //public async Task<IActionResult> Reg()
        //{
        //    var model = new Registration
        //    {
        //        Username = "admin",
        //        Name = "admin",
        //        Email = "admin",
        //        Password = "Admin@1234"
        //    };
        //    model.Role = "admin";
        //    var result = await this._service.RegistrationAsync(model);
        //    return Ok(result);
        //}

    }
}
