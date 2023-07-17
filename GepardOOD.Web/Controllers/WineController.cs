using static GepardOOD.Common.NotificationMessagesConstants;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.ViewModels.Wine;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using GepardOOD.Web.Infrastructure.Extensions;
using GepardOOD.Services.Data.Models.Wine;


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

			WineFormModel model = new WineFormModel()
			{
				WineCategories = await _wineCategoryService.AllCategoriesAsync()
			};

			return View(model);
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

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			WineDetailsViewModel? viewModel = await _wineService.GetDetailsByIdAsync(id);

			if (viewModel == null)
			{
				TempData[ErrorMessage] = "Wine with the provided Id does not exist!";

				return RedirectToAction("All", "Wine");
			}

			return View(viewModel);
		}
	}
}
