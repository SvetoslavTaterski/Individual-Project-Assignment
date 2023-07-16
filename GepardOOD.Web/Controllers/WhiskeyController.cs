﻿using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.ViewModels.Whiskey;
using static GepardOOD.Common.NotificationMessagesConstants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GepardOOD.Services.Data;
using GepardOOD.Web.ViewModels.Wine;
using GepardOOD.Web.Infrastructure.Extensions;

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
				TempData[ErrorMessage] = "You must become an associate in order to add new whiskeys!";

				return RedirectToAction("Become", "Associate");
			}

			WhiskeyFormModel model = new WhiskeyFormModel()
			{
				WhiskeyCategories = await _whiskeyCategoryService.AllCategoriesAsync()
			};

			return View(model);
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
	}
}