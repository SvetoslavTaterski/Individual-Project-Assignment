using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Beer;
using GepardOOD.Web.ViewModels.Order;

using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class OrderService : IOrderService
	{
		private readonly GepardOODDbContext _data;

		public OrderService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task<OrderViewModel> AddItemToOrder(string userId, string itemId)
		{
			

			return null;
		}

		public async Task<Order> GetOrderByIdAsync(string orderId)
		{
			return await _data.Orders
				.Include(b => b.OrderedBeers)
				.Include(s => s.OrderedSoda)
				.Include(w => w.OrderedWines)
				.Include(w => w.OrderedWhiskey)
				.FirstOrDefaultAsync(o => o.Id.ToString() == orderId);
		}

		public async Task<OrderViewModel> GetOrderDetails()
		{
			return new OrderViewModel()
			{
				Id = Guid.NewGuid(),
				UserId = null,
				TotalCost = 0,
				OrderedBeers = null,
				OrderedSodas = null,
				OrderedWines = null,
				OrderedWhiskeys = null
			};
		}
	}
}
