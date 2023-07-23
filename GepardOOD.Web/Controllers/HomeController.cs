using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.ViewModels.Home;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GepardOOD.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBeerService _beerService;

        public HomeController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModel = await _beerService.ThreeBeersAsync();

            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
	        if (statusCode == 404 || statusCode == 400)
	        {
		        return this.View("Error404");
	        }

            if (statusCode == 401)
	        {
		        return this.View("Error401");
	        }

            return View();
        }
    }
}