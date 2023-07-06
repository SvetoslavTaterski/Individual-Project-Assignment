namespace GepardOOD.Web.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	using GepardOOD.Services.Data.Interfaces;
    using static Common.NotificationMessagesConstants;
	using Infrastructure;


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

	        bool isAgent = await this._associateService.AssociateExistByUserId(userId);

	        if (isAgent)
	        {
		        TempData[ErrorMessage] = "You are already an Associate!";
                return RedirectToAction("Index","Home");
	        }

            return View();
        }
    }
}
