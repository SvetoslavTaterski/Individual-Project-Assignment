using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Infrastructure;
using GepardOOD.Web.ViewModels.Beer;
using static GepardOOD.Common.NotificationMessagesConstants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GepardOOD.Web.Controllers
{
    [Authorize]
    public class BeerController : Controller
    {
        private readonly IBeerCategoryService _beerCategoryService;
        private readonly IAssociateService _associateService;

        public BeerController(IBeerCategoryService beerCategoryService, IAssociateService associateService)
        {
	        _beerCategoryService = beerCategoryService;
            _associateService = associateService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
	        bool isAssociate = await _associateService.AssociateExistByUserIdAsync(this.User.GetId()!);

	        if (!isAssociate)
	        {
		        TempData[ErrorMessage] = "You must become an associate in order to add new beers!";

		        return RedirectToAction("Become", "Associate");
	        }

	        BeerFormModel model = new BeerFormModel()
	        {
                BeerCategories = await _beerCategoryService.AllCategoriesAsync()
	        };

            return View(model);
        }
    }
}
