using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data.Models.Beer;
using GepardOOD.Web.ViewModels.Beer;
using GepardOOD.Web.Infrastructure.Extensions;
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
		public async Task<IActionResult> All([FromQuery] AllBeerQueryModel queryModel)
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

			try
			{
				BeerFormModel model = new BeerFormModel()
				{
					BeerCategories = await _beerCategoryService.AllCategoriesAsync()
				};

				return View(model);
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new Beer! Please try again later or contact Administrator.");

				return RedirectToAction("Index", "Home");
			}
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

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			List<BeerAllViewModel> myBeers = new List<BeerAllViewModel>();

			string userId = User.GetId()!;

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(userId);

			try
			{
				if (isUserAssociate)
				{
					string? associateId = await _associateService.GetAssociateIdByUserIdAsync(userId);

					myBeers.AddRange(await _beerService.AllByAssociateIdAsync(associateId!));
				}
				else
				{
					TempData[ErrorMessage] = "You must become an associate in order to have added beers!";

					return RedirectToAction("Become", "Associate");
				}

				return View(myBeers);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected error occurred! Please try again later or contact administrator.";

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			bool beerExists = await _beerService.ExistsByIdAsync(id);

			if (!beerExists)
			{
				TempData[ErrorMessage] = "Beer with the provided Id does not exist!";

				return RedirectToAction("All", "Beer");
			}

			try
			{
				BeerDetailsViewModel viewModel = await _beerService.GetDetailsByIdAsync(id);

				return View(viewModel);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected error occurred! Please try again later or contact administrator.";

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			bool beerExists = await _beerService.ExistsByIdAsync(id);

			if (!beerExists)
			{
				TempData[ErrorMessage] = "Beer with the provided Id does not exist!";

				return RedirectToAction("All", "Beer");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit beer info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _beerService
				.IsAssociateWithIdOwnerOfBeerWithIdAsync(id, associateId!);

			if (!isAssociateOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the owner of the beer in order to edit it!";

				return RedirectToAction("Mine", "Beer");
			}


			try
			{
				BeerFormModel formModel = await _beerService.GetBeerForEditByIdAsync(id);

				formModel.BeerCategories = await _beerCategoryService.AllCategoriesAsync();

				return View(formModel);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected error occurred while trying to edit a beer! Please try again later or contact administrator.";

				return RedirectToAction("Index", "Home");
			}

		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, BeerFormModel model)
		{
			if (!ModelState.IsValid)
			{
				model.BeerCategories = await _beerCategoryService.AllCategoriesAsync();
				return View(model);
			}

			bool beerExists = await _beerService.ExistsByIdAsync(id);

			if (!beerExists)
			{
				TempData[ErrorMessage] = "Beer with the provided Id does not exist!";

				return RedirectToAction("All", "Beer");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit beer info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _beerService
				.IsAssociateWithIdOwnerOfBeerWithIdAsync(id, associateId!);

			if (!isAssociateOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the owner of the beer in order to edit it!";

				return RedirectToAction("Mine", "Beer");
			}

			try
			{
				await _beerService.EditBeerByIdAndFormModelAsync(id, model);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to edit a beer! Please try again later or contact administrator.");

				model.BeerCategories = await _beerCategoryService.AllCategoriesAsync();

				return View(model);
			}

			return RedirectToAction("Details", "Beer", new { id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool beerExists = await _beerService.ExistsByIdAsync(id);

			if (!beerExists)
			{
				TempData[ErrorMessage] = "Beer with the provided Id does not exist!";

				return RedirectToAction("All", "Beer");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must become an associate in order to delete beer info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _beerService
				.IsAssociateWithIdOwnerOfBeerWithIdAsync(id, associateId!);

			if (!isAssociateOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the owner of the beer in order to delete it!";

				return RedirectToAction("Mine", "Beer");
			}

			try
			{
				BeerPreDeleteViewModel viewModel = await _beerService.GetBeerForDeleteByIdAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to delete a beer! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, BeerPreDeleteViewModel model)
		{
			bool beerExists = await _beerService.ExistsByIdAsync(id);

			if (!beerExists)
			{
				TempData[ErrorMessage] = "Beer with the provided Id does not exist!";

				return RedirectToAction("All", "Beer");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must become an associate in order to delete beer info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _beerService
				.IsAssociateWithIdOwnerOfBeerWithIdAsync(id, associateId!);

			if (!isAssociateOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the owner of the beer in order to delete it!";

				return RedirectToAction("Mine", "Beer");
			}

			try
			{
				await _beerService.DeleteBeerByIdAsync(id);

				TempData[WarningMessage] = "The beer was successfully deleted!";

				return RedirectToAction("Mine", "Beer");
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to delete a beer! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Buy()
		{
			TempData[SuccessMessage] = "The beer was successfully bought!";

			return RedirectToAction("All", "Beer");
		}
	}
}
