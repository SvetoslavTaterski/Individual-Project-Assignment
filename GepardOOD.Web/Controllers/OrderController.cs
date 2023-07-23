using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Infrastructure.Extensions;
using GepardOOD.Web.ViewModels.Order;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GepardOOD.Web.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		public async Task<IActionResult> AddItemToOrder(string id)
		{
			return Ok();
		}


		[HttpGet]
		public async Task<IActionResult> GetOrderDetails()
		{
			OrderViewModel viewModel = await _orderService.GetOrderDetails();

			viewModel.UserId = User.GetId()!;

			return View(viewModel);
		}
	}
}
