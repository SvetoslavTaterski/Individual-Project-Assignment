using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data.Models.Beer;
using GepardOOD.Web.ViewModels.Beer;
using static GepardOOD.Common.NotificationMessagesConstants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GepardOOD.Web.Infrastructure.Extensions;

namespace GepardOOD.Web.Controllers
{
    [Authorize]
	public class BeerController : Controller
	{
		private readonly IBeerCategoryService _beerCategoryService;
		private readonly IAssociateService _associateService;
		private readonly IBeerService _beerService;

		public BeerController(IBeerCategoryService beerCategoryService,
			IAssociateService associateService,
			IBeerService beerService)
		{
			_beerCategoryService = beerCategoryService;
			_associateService = associateService;
			_beerService = beerService;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> All([FromQuery]AllBeerQueryModel queryModel)
		{
			AllBeersFilteredAndPagedServiceModel serviceModel =
				await _beerService.AllAsync(queryModel);

			queryModel.Beers = serviceModel.Beers;
			queryModel.TotalBeers = serviceModel.TotalBeersCount;
			queryModel.Categories = await _beerCategoryService.AllCategoryNamesAsync();

			return View(queryModel);
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

		[HttpPost]
		public async Task<IActionResult> Add(BeerFormModel model)
		{
			bool isAssociate = await _associateService.AssociateExistByUserIdAsync(this.User.GetId()!);

			if (!isAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to add new beers!";

				return RedirectToAction("Become", "Associate");
			}

			bool categoryExists = await _beerCategoryService.ExistsByIdAsync(model.CategoryId);

			if (!categoryExists)
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Selected category is invalid!");
			}

			if (!ModelState.IsValid)
			{
				model.BeerCategories = await _beerCategoryService.AllCategoriesAsync();

				return View(model);
			}

			try
			{
				string? associateId = await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

				await _beerService.CreateAsync(model, associateId!);
			}
			catch (Exception e)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new Beer! Please try again later or contact Administrator.");

				model.BeerCategories = await _beerCategoryService.AllCategoriesAsync();

				return View(model);
			}

			return RedirectToAction("All", "Beer");
		}
	}
}
