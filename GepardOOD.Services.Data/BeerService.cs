using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Beer;
using GepardOOD.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class BeerService : IBeerService
	{
		private readonly GepardOODDbContext _dbContext;

		public BeerService(GepardOODDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task CreateAsync(BeerFormModel model, string associateId)
		{
			Beer newBeer = new Beer()
			{
				Name = model.Name,
				Manufacturer = model.Manufacturer,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				Price = model.Price,
				BeerCategoryId = model.CategoryId,
				AssociateId = Guid.Parse(associateId)
			};

			await _dbContext.Beers.AddAsync(newBeer);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<IndexViewModel>> ThreeBeersAsync()
		{
			IEnumerable<IndexViewModel> threeBeers = await _dbContext
				.Beers
				.OrderByDescending(b => b.Id)
				.Take(3)
				.Select(b => new IndexViewModel()
				{
					Id = b.Id,
					Name = b.Name,
					ImageUrl = b.ImageUrl
				})
				.ToArrayAsync();

			return threeBeers;
		}
	}
}
