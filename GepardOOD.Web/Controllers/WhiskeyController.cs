using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.ViewModels.Whiskey;
using static GepardOOD.Common.NotificationMessagesConstants;
using GepardOOD.Web.Infrastructure.Extensions;
using GepardOOD.Services.Data.Models.Whiskey;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GepardOOD.Services.Data;


namespace GepardOOD.Web.Controllers
{
    [Authorize]
	public class WhiskeyController : Controller
	{
		private readonly IAssociateService _associateService;
		private readonly IWhiskeyCategoryService _whiskeyCategoryService;
		private readonly IWhiskeyService _whiskeyService;

		public WhiskeyController(IAssociateService associateService,IWhiskeyCategoryService whiskeyCategoryService,
			IWhiskeyService whiskeyService)
		{
			_associateService = associateService;
			_whiskeyService = whiskeyService;
			_whiskeyCategoryService = whiskeyCategoryService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllWhiskeyQueryModel queryModel)
		{
			AllWhiskeysFilteredAndPagedServiceModel serviceModel =
				await _whiskeyService.AllAsync(queryModel);

			queryModel.Whiskeys = serviceModel.Whiskeys;
			queryModel.TotalWhiskeys = serviceModel.TotalWhiskeysCount;
			queryModel.Categories = await _whiskeyCategoryService.AllCategoryNamesAsync();

			return View(queryModel);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			bool isAssociate = await _associateService.AssociateExistByUserIdAsync(this.User.GetId()!);

			if (!isAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to add new whiskeys!";

				return RedirectToAction("Become", "Associate");
			}

			try
			{
				WhiskeyFormModel model = new WhiskeyFormModel()
				{
					WhiskeyCategories = await _whiskeyCategoryService.AllCategoriesAsync()
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
		public async Task<IActionResult> Add(WhiskeyFormModel model)
		{
			bool isAssociate = await _associateService.AssociateExistByUserIdAsync(this.User.GetId()!);

			if (!isAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to add new whiskeys!";
				return RedirectToAction("Become", "Associate");
			}

			bool categoryExists = await _whiskeyCategoryService.ExistsByIdAsync(model.CategoryId);

			if (!categoryExists)
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Selected category is invalid!");
			}

			if (!ModelState.IsValid)
			{
				model.WhiskeyCategories = await _whiskeyCategoryService.AllCategoriesAsync();

				return View(model);
			}

			try
			{
				string? associateId = await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

				await _whiskeyService.CreateAsync(model, associateId!);
			}
			catch (Exception e)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new Whiskey! Please try again later or contact Administrator.");

				model.WhiskeyCategories = await _whiskeyCategoryService.AllCategoriesAsync();

				return View(model);
			}

			return RedirectToAction("All", "Whiskey");
		}

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			List<WhiskeyAllViewModel> myWhiskeys = new List<WhiskeyAllViewModel>();

			string userId = User.GetId()!;

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(userId);

			try
			{
				if (isUserAssociate)
				{
					string? associateId = await _associateService.GetAssociateIdByUserIdAsync(userId);

					myWhiskeys.AddRange(await _whiskeyService.AllByAssociateIdAsync(associateId!));
				}
				else
				{
					TempData[ErrorMessage] = "You must become an associate in order to have added whiskeys!";

					return RedirectToAction("Become", "Associate");
				}

				return View(myWhiskeys);
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
			bool whiskeyExists = await _whiskeyService.ExistsByIdAsync(id);

			if (!whiskeyExists)
			{
				TempData[ErrorMessage] = "Whiskey with the provided Id does not exist!";

				return RedirectToAction("All", "Whiskey");
			}

			try
			{
				WhiskeyDetailsViewModel viewModel = await _whiskeyService.GetDetailsByIdAsync(id);

				return View(viewModel);
			}
			catch (Exception )
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
			bool whiskeyExists = await _whiskeyService.ExistsByIdAsync(id);

			if (!whiskeyExists)
			{
				TempData[ErrorMessage] = "Whiskey with the provided Id does not exist!";

				return RedirectToAction("All", "Whiskey");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit whiskey info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _whiskeyService
				.IsAssociateWithIdOwnerOfWhiskeyWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the whiskey in order to edit it!";

				return RedirectToAction("Mine", "Whiskey");
			}

			try
			{
				WhiskeyFormModel formModel = await _whiskeyService.GetWhiskeyForEditByIdAsync(id);

				formModel.WhiskeyCategories = await _whiskeyCategoryService.AllCategoriesAsync();

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
		public async Task<IActionResult> Edit(int id, WhiskeyFormModel model)
		{
			if (!ModelState.IsValid)
			{
				model.WhiskeyCategories = await _whiskeyCategoryService.AllCategoriesAsync();
				return View(model);
			}

			bool whiskeyExists = await _whiskeyService.ExistsByIdAsync(id);

			if (!whiskeyExists)
			{
				TempData[ErrorMessage] = "Whiskey with the provided Id does not exist!";

				return RedirectToAction("All", "Whiskey");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit whiskey info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _whiskeyService
				.IsAssociateWithIdOwnerOfWhiskeyWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the whiskey in order to edit it!";

				return RedirectToAction("Mine", "Whiskey");
			}

			try
			{
				await _whiskeyService.EditWhiskeyByIdAndFormModelAsync(id, model);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to edit a whiskey! Please try again later or contact administrator.");

				model.WhiskeyCategories = await _whiskeyCategoryService.AllCategoriesAsync();

				return View(model);
			}

			return RedirectToAction("Details", "Whiskey", new { id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool whiskeyExists = await _whiskeyService.ExistsByIdAsync(id);

			if (!whiskeyExists)
			{
				TempData[ErrorMessage] = "Whiskey with the provided Id does not exist!";

				return RedirectToAction("All", "Whiskey");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit whiskey info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _whiskeyService
				.IsAssociateWithIdOwnerOfWhiskeyWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the whiskey in order to edit it!";

				return RedirectToAction("Mine", "Whiskey");
			}

			try
			{
				WhiskeyPreDeleteViewModel viewModel = await _whiskeyService.GetWhiskeyForDeleteByIdAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to delete a whiskey! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, WhiskeyPreDeleteViewModel model)
		{
			bool whiskeyExists = await _whiskeyService.ExistsByIdAsync(id);

			if (!whiskeyExists)
			{
				TempData[ErrorMessage] = "Whiskey with the provided Id does not exist!";

				return RedirectToAction("All", "Whiskey");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit whiskey info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _whiskeyService
				.IsAssociateWithIdOwnerOfWhiskeyWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the whiskey in order to edit it!";

				return RedirectToAction("Mine", "Whiskey");
			}

			try
			{
				await _whiskeyService.DeleteWhiskeyByIdAsync(id);

				TempData[WarningMessage] = "The whiskey was successfully deleted!";

				return RedirectToAction("Mine", "Whiskey");
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to delete a whiskey! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}
		}
	}
}
