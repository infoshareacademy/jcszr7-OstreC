using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWeb.DomainModels.Collections;
using OstreCWEB.Services.Api;
using OstreCWEB.ViewModel;
using OstreCWEB.ViewModel.Api;

namespace OstreCWEB.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFithEditionApiClient _client;
        private readonly IMapper _mapper;

        public LibraryController(ILogger<HomeController> logger,IFithEditionApiClient client,IMapper mapper)
        {
            _logger = logger;
            _client = client;
            _mapper = mapper;
        }
        [HttpGet]
        
        public ActionResult Index(string sortOrder)
        { 
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                var model = _mapper.Map<PagedListView<SpellView>>(_client.GetSpells()); 
            switch (sortOrder)
                {
                    case "name_desc":
                        students = students.OrderByDescending(s => s.LastName);
                        break;
                    case "Date":
                        students = students.OrderBy(s => s.EnrollmentDate);
                        break;
                    case "date_desc":
                        students = students.OrderByDescending(s => s.EnrollmentDate);
                        break;
                    default:
                        students = students.OrderBy(s => s.LastName);
                        break;
                } 
                
                return View(); 
        }  
        
    }
}