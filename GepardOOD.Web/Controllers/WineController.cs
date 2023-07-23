using static GepardOOD.Common.NotificationMessagesConstants;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.ViewModels.Wine;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using GepardOOD.Web.Infrastructure.Extensions;
using GepardOOD.Services.Data.Models.Wine;
using GepardOOD.Services.Data;


namespace GepardOOD.Web.Controllers
{
	[Authorize]
	public class WineController : Controller
	{
		private readonly IWineCategoryService _wineCategoryService;
		private readonly IWineService _wineService;
		private readonly IAssociateService _associateService;

		public WineController(IWineCategoryService wineCategoryService, IWineService wineService,
			IAssociateService associateService)
		{
			_associateService = associateService;
			_wineService = wineService;
			_wineCategoryService = wineCategoryService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllWineQueryModel queryModel)
		{
			AllWineFilteredAndPagedServiceModel serviceModel =
				await _wineService.AllAsync(queryModel);

			queryModel.Wines = serviceModel.Wines;
			queryModel.TotalWines = serviceModel.TotalWinesCount;
			queryModel.Categories = await _wineCategoryService.AllCategoryNamesAsync();

			return View(queryModel);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			bool isAssociate = await _associateService.AssociateExistByUserIdAsync(this.User.GetId()!);

			if (!isAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to add new wines!";

				return RedirectToAction("Become", "Associate");
			}

			try
			{
				WineFormModel model = new WineFormModel()
				{
					WineCategories = await _wineCategoryService.AllCategoriesAsync()
				};

				return View(model);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(WineFormModel model)
		{
			bool isAssociate = await _associateService.AssociateExistByUserIdAsync(this.User.GetId()!);

			if (!isAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to add new wines!";
				return RedirectToAction("Become", "Associate");
			}

			bool categoryExists = await _wineCategoryService.ExistsByIdAsync(model.CategoryId);

			if (!categoryExists)
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Selected category is invalid!");
			}

			if (!ModelState.IsValid)
			{
				model.WineCategories = await _wineCategoryService.AllCategoriesAsync();

				return View(model);
			}

			try
			{
				string? associateId = await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

				await _wineService.CreateAsync(model, associateId!);
			}
			catch (Exception e)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new Wine! Please try again later or contact Administrator.");

				model.WineCategories = await _wineCategoryService.AllCategoriesAsync();

				return View(model);
			}

			return RedirectToAction("All", "Wine");
		}

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			List<WineAllViewModel> myWines = new List<WineAllViewModel>();

			string userId = User.GetId()!;

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(userId);

			try
			{
				if (isUserAssociate)
				{
					string? associateId = await _associateService.GetAssociateIdByUserIdAsync(userId);

					myWines.AddRange(await _wineService.AllByAssociateIdAsync(associateId!));
				}
				else
				{
					TempData[ErrorMessage] = "You must become an associate in order to have added wines!";

					return RedirectToAction("Become", "Associate");
				}

				return View(myWines);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			bool wineExists = await _wineService.ExistsByIdAsync(id);

			if (!wineExists)
			{
				TempData[ErrorMessage] = "Wine with the provided Id does not exist!";

				return RedirectToAction("All", "Wine");
			}

			try
			{
				WineDetailsViewModel viewModel = await _wineService.GetDetailsByIdAsync(id);

				return View(viewModel);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			bool wineExists = await _wineService.ExistsByIdAsync(id);

			if (!wineExists)
			{
				TempData[ErrorMessage] = "Wine with the provided Id does not exist!";

				return RedirectToAction("All", "Wine");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit wine info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _wineService
				.IsAssociateWithIdOwnerOfWineWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the wine in order to edit it!";

				return RedirectToAction("Mine", "Wine");
			}

			try
			{
				WineFormModel formModel = await _wineService.GetWineForEditByIdAsync(id);

				formModel.WineCategories = await _wineCategoryService.AllCategoriesAsync();

				return View(formModel);
			}
			catch (Exception e)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}

		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, WineFormModel model)
		{
			if (!ModelState.IsValid)
			{
				model.WineCategories = await _wineCategoryService.AllCategoriesAsync();
				return View(model);
			}

			bool wineExists = await _wineService.ExistsByIdAsync(id);

			if (!wineExists)
			{
				TempData[ErrorMessage] = "Wine with the provided Id does not exist!";

				return RedirectToAction("All", "Wine");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit wine info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _wineService
				.IsAssociateWithIdOwnerOfWineWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the wine in order to edit it!";

				return RedirectToAction("Mine", "Wine");
			}

			try
			{
				await _wineService.EditWineByIdAndFormModelAsync(id, model);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to edit a wine! Please try again later or contact administrator.");

				model.WineCategories = await _wineCategoryService.AllCategoriesAsync();

				return View(model);
			}

			return RedirectToAction("Details", "Wine", new { id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool wineExists = await _wineService.ExistsByIdAsync(id);

			if (!wineExists)
			{
				TempData[ErrorMessage] = "Wine with the provided Id does not exist!";

				return RedirectToAction("All", "Wine");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit wine info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _wineService
				.IsAssociateWithIdOwnerOfWineWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the wine in order to edit it!";

				return RedirectToAction("Mine", "Wine");
			}

			try
			{
				WinePreDeleteViewModel viewModel = await _wineService.GetWineForDeleteByIdAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to delete a wine! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, WinePreDeleteViewModel model)
		{
			bool wineExists = await _wineService.ExistsByIdAsync(id);

			if (!wineExists)
			{
				TempData[ErrorMessage] = "Wine with the provided Id does not exist!";

				return RedirectToAction("All", "Wine");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit wine info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _wineService
				.IsAssociateWithIdOwnerOfWineWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the wine in order to edit it!";

				return RedirectToAction("Mine", "Wine");
			}

			try
			{
				await _wineService.DeleteWineByIdAsync(id);

				TempData[WarningMessage] = "The wine was successfully deleted!";

				return RedirectToAction("Mine", "Wine");
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to delete a wine! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}

		}

		[HttpGet]
		public async Task<IActionResult> Buy()
		{
			TempData[SuccessMessage] = "The wine was successfully bought!";

			return RedirectToAction("All", "Wine");
		}
	}
}
