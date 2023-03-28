using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Services.Api;
using OstreCWEB.ViewModel.Api;

namespace OstreCWEB.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFithEditionApiClient _fithEditionApiClient;
        private readonly IMapper _mapper;

        public LibraryController(ILogger<HomeController> logger,IFithEditionApiClient fithEditionApiClient,IMapper mapper)
        {
            _logger = logger;
            _fithEditionApiClient = fithEditionApiClient;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult>  Index(int chosenPage)
        { 
            var model = _mapper.Map<FithEditionApiResponseView>(await _fithEditionApiClient.GetSpells(chosenPage)); 
            return View(model); 
        }
        [HttpPost] 
        [ActionName("Index")]
        public async Task<ActionResult> IndexPost([Bind("SearchByInt,ParamToOrder,SearchByName,Limit")] FilterView? filters, int chosenPage)
        { 
            var model = _mapper.Map<FithEditionApiResponseView>(await _fithEditionApiClient.GetSpells(_mapper.Map<Filter>(filters), chosenPage));

            return View(model);
        }
        [HttpGet]
        [ActionName("ChangePage")]
        public async Task<ActionResult> ChangePage(int chosenPage, [Bind("SearchByInt,ParamToOrder,SearchByName,Limit")] FilterView? filters)
        { 
            var model = _mapper.Map<FithEditionApiResponseView>(await _fithEditionApiClient.GetSpells( _mapper.Map<Filter>(filters), chosenPage));

            return View("Index",model);
        }


    }
}