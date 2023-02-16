using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.SuperAdmin;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.Services.Seed;


namespace OstreCWEB.Controllers
{ //It's a temporary controller I'm using for test purposes. It includes pretty much everything and should disapear by end of sprint 2. 
    public class SuperAdminController : Controller
    {
        private readonly ISeeder _seeder;
        private readonly IStatusRepository _statusRepository;
        private readonly IAbilitiesRepository _characterActionsRepository;
        private readonly IPlayableCharacterRepository _playableCharacterRepository;
        private readonly ISuperAdminRepository _superAdminRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        // GET: SuperAdmin
        public SuperAdminController(SignInManager<User> signInManager, UserManager<User> userManager, ISuperAdminRepository superAdminRepository, IPlayableCharacterRepository playableCharacterRepository, ISeeder seeder, IMapper mapper, IStatusRepository statusRepository, IAbilitiesRepository characterActionsRepository)
        {
            _seeder = seeder;
            _mapper = mapper;
            _statusRepository = statusRepository;
            _characterActionsRepository = characterActionsRepository;
            _playableCharacterRepository = playableCharacterRepository;
            _superAdminRepository = superAdminRepository;
            _userManager = userManager;
            _signInManager = signInManager;



        }
        public ActionResult Test()
        {
            _superAdminRepository.Test();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Index()
        {

            return View();
        }

        // GET: SuperAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: SuperAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuperAdmin/Create
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

        // GET: SuperAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SuperAdmin/Edit/5
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

        // GET: SuperAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SuperAdmin/Delete/5
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
        public async Task Seed()
        {
            await _seeder.SeedCharactersDb();
        }
    }
}
