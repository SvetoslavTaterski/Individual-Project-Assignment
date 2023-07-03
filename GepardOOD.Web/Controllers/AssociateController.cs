using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GepardOOD.Web.Controllers
{
    [Authorize]
    public class AssociateController : Controller
    {
        public async Task<IActionResult> Become()
        {
            return View();
        }
    }
}
