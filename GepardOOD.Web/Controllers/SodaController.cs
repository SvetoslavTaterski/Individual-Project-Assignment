using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data.Models.Soda;
using GepardOOD.Web.ViewModels.Soda;

using static GepardOOD.Common.NotificationMessagesConstants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using GepardOOD.Web.Infrastructure.Extensions;
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
		public async Task<IActionResult> All([FromQuery] AllSodaQueryModel queryModel)
		{
			AllSodasFilteredAndPagedServiceModel serviceModel =
				await _sodaService.AllAsync(queryModel);

			queryModel.Sodas = serviceModel.Sodas;
			queryModel.TotalSodas = serviceModel.TotalSodasCount;
			queryModel.Categories = await _sodaCategoryService.AllCategoryNamesAsync();

			return View(queryModel);
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

			try
			{
				SodaFormModel model = new SodaFormModel()
				{
					SodaCategories = await _sodaCategoryService.AllCategoriesAsync()
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

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			List<SodaAllViewModel> mySodas = new List<SodaAllViewModel>();

			string userId = User.GetId()!;

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(userId);

			try
			{
				if (isUserAssociate)
				{
					string? associateId = await _associateService.GetAssociateIdByUserIdAsync(userId);

					mySodas.AddRange(await _sodaService.AllByAssociateIdAsync(associateId!));
				}
				else
				{
					TempData[ErrorMessage] = "You must become an associate in order to have added sodas!";

					return RedirectToAction("Become", "Associate");
				}

				return View(mySodas);
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
			bool sodaExists = await _sodaService.ExistsByIdAsync(id);

			if (!sodaExists)
			{
				TempData[ErrorMessage] = "Soda with the provided Id does not exist!";

				return RedirectToAction("All", "Soda");
			}

			try
			{
				SodaDetailsViewModel viewModel = await _sodaService.GetDetailsByIdAsync(id);

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
			bool sodaExists = await _sodaService.ExistsByIdAsync(id);

			if (!sodaExists)
			{
				TempData[ErrorMessage] = "Soda with the provided Id does not exist!";

				return RedirectToAction("All", "Soda");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit soda info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _sodaService
				.IsAssociateWithIdOwnerOfSodaWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the soda in order to edit it!";

				return RedirectToAction("Mine", "Soda");
			}

			try
			{
				SodaFormModel formModel = await _sodaService.GetSodaForEditByIdAsync(id);

				formModel.SodaCategories = await _sodaCategoryService.AllCategoriesAsync();

				return View(formModel);
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
		public async Task<IActionResult> Edit(int id, SodaFormModel model)
		{
			if (!ModelState.IsValid)
			{
				model.SodaCategories = await _sodaCategoryService.AllCategoriesAsync();
				return View(model);
			}

			bool sodaExists = await _sodaService.ExistsByIdAsync(id);

			if (!sodaExists)
			{
				TempData[ErrorMessage] = "Soda with the provided Id does not exist!";

				return RedirectToAction("All", "Soda");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit soda info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _sodaService
				.IsAssociateWithIdOwnerOfSodaWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the soda in order to edit it!";

				return RedirectToAction("Mine", "Soda");
			}

			try
			{
				await _sodaService.EditSodaByIdAndFormModelAsync(id, model);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to edit a soda! Please try again later or contact administrator.");

				model.SodaCategories = await _sodaCategoryService.AllCategoriesAsync();

				return View(model);
			}

			return RedirectToAction("Details", "Soda", new { id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool sodaExists = await _sodaService.ExistsByIdAsync(id);

			if (!sodaExists)
			{
				TempData[ErrorMessage] = "Soda with the provided Id does not exist!";

				return RedirectToAction("All", "Soda");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit soda info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _sodaService
				.IsAssociateWithIdOwnerOfSodaWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the soda in order to edit it!";

				return RedirectToAction("Mine", "Soda");
			}

			try
			{
				SodaPreDeleteViewModel viewModel = await _sodaService.GetSodaForDeleteByIdAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to delete a soda! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, SodaPreDeleteViewModel viewModel)
		{
			bool sodaExists = await _sodaService.ExistsByIdAsync(id);

			if (!sodaExists)
			{
				TempData[ErrorMessage] = "Soda with the provided Id does not exist!";

				return RedirectToAction("All", "Soda");
			}

			bool isUserAssociate = await _associateService.AssociateExistByUserIdAsync(User.GetId()!);

			if (!isUserAssociate)
			{
				TempData[ErrorMessage] = "You must become an associate in order to edit soda info!";

				return RedirectToAction("Become", "Associate");
			}

			string? associateId =
				await _associateService.GetAssociateIdByUserIdAsync(User.GetId()!);

			bool isAssociateOwner = await _sodaService
				.IsAssociateWithIdOwnerOfSodaWithIdAsync(id, associateId!);

			if (!isAssociateOwner)
			{
				TempData[ErrorMessage] = "You must be the owner of the soda in order to edit it!";

				return RedirectToAction("Mine", "Soda");
			}

			try
			{
				await _sodaService.DeleteSodaByIdAsync(id);

				TempData[WarningMessage] = "The soda was successfully deleted!";

				return RedirectToAction("Mine", "Soda");
			}
			catch (Exception)
			{
				ModelState
					.AddModelError
						(string.Empty, "Unexpected error occurred while trying to delete a soda! Please try again later or contact administrator.");

				return RedirectToAction("Index", "Home");
			}

		}

		[HttpGet]
		public async Task<IActionResult> Buy()
		{
			TempData[SuccessMessage] = "The soda was successfully bought!";

			return RedirectToAction("All", "Soda");
		}
	}
}
