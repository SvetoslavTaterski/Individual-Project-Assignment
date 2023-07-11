using static GepardOOD.Common.NotificationMessagesConstants;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Infrastructure;
using GepardOOD.Web.ViewModels.Wine;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
		public async Task<IActionResult> All()
		{
			return Ok();
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
				TempData[ErrorMessage] = "You must become an associate in order to add new beers!";
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
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new Beer! Please try again later or contact Administrator.");

				model.WineCategories = await _wineCategoryService.AllCategoriesAsync();

				return View(model);
			}

			return RedirectToAction("All", "Wine");
		}
	}
}
