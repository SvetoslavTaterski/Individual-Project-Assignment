using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Infrastructure;
using GepardOOD.Web.ViewModels.Soda;
using GepardOOD.Web.ViewModels.Wine;
using static GepardOOD.Common.NotificationMessagesConstants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GepardOOD.Services.Data;

namespace GepardOOD.Web.Controllers
{
	[Authorize]
	public class SodaController : Controller
	{
		private readonly IAssociateService _associateService;
		private readonly ISodaCategoryService _sodaCategoryService;
		private readonly ISodaService _sodaService;

		public SodaController(ISodaService sodaService, ISodaCategoryService sodaCategoryService,
			IAssociateService associateService)
		{
			_sodaCategoryService = sodaCategoryService;
			_associateService = associateService;
			_sodaService = sodaService;
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
				TempData[ErrorMessage] = "You must become an associate in order to add new sodas!";

				return RedirectToAction("Become", "Associate");
			}

			SodaFormModel model = new SodaFormModel()
			{
				SodaCategories = await _sodaCategoryService.AllCategoriesAsync()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(SodaFormModel model)
		{
			bool isAssociate = await _associateService.AssociateExistByUserIdAsync(this.User.GetId()!);

			if (!isAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to add new sodas!";
				return RedirectToAction("Become", "Associate");
			}

			bool categoryExists = await _sodaCategoryService.ExistsByIdAsync(model.CategoryId);

			if (!categoryExists)
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Selected category is invalid!");
			}

			if (!ModelState.IsValid)
			{
				model.SodaCategories = await _sodaCategoryService.AllCategoriesAsync();

				return View(model);
			}

			try
			{
				string? associateId = await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

				await _sodaService.CreateAsync(model, associateId!);
			}
			catch (Exception e)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new Soda! Please try again later or contact Administrator.");

				model.SodaCategories = await _sodaCategoryService.AllCategoriesAsync();

				return View(model);
			}

			return RedirectToAction("All", "Soda");
		}
	}
}
