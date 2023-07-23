using GepardOOD.Data.Models;
using GepardOOD.Web.ViewModels.Order;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IOrderService
	{
		Task<OrderViewModel> GetOrderDetails();

		Task<OrderViewModel> AddItemToOrder(string userId, string itemId);
	}
}
