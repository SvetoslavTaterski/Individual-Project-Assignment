namespace GepardOOD.Web.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	using GepardOOD.Services.Data.Interfaces;
    using static Common.NotificationMessagesConstants;
	using Infrastructure;
	using ViewModels.Associate;


	[Authorize]
    public class AssociateController : Controller
    {
        private readonly IAssociateService _associateService;

        public AssociateController(IAssociateService associateService)
        {
	        _associateService = associateService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
	        string? userId = this.User.GetId();

	        bool isAgent = await this._associateService.AssociateExistByUserIdAsync(userId);

	        if (isAgent)
	        {
		        TempData[ErrorMessage] = "You are already an Associate!";
                return RedirectToAction("Index","Home");
	        }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAssociateFormModel model)
        {
	        string? userId = this.User.GetId();

	        bool isAgent = await this._associateService.AssociateExistByUserIdAsync(userId);

	        if (isAgent)
	        {
		        TempData[ErrorMessage] = "You are already an Associate!";
		        return RedirectToAction("Index", "Home");
	        }

			bool isPhoneNumberTaken = await _associateService.AssociateExistByPhoneNumberAsync(model.PhoneNumber);

			if (isPhoneNumberTaken)
			{
				ModelState.AddModelError(nameof(model.PhoneNumber),
					"Associate with the provided phone number already exists!");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				await _associateService.Create(userId, model);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] =
					"Unexpected error occurred while registering you as an associate. Please try again later or contact administrator!";

				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("All", "Beer");
        }
    }
}
