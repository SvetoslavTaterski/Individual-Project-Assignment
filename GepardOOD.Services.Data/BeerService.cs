using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data.Models.Beer;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Beer;
using GepardOOD.Web.ViewModels.Beer.Enums;
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

		public async Task<AllBeersFilteredAndPagedServiceModel> AllAsync(AllBeerQueryModel beerModel)
		{
			IQueryable<Beer> beerQuery = _dbContext.Beers.AsQueryable();

			if (!string.IsNullOrWhiteSpace(beerModel.Category))
			{
				beerQuery = beerQuery.Where(b => b.BeerCategory.Name == beerModel.Category);
			}

			if (!string.IsNullOrWhiteSpace(beerModel.SearchString))
			{
				string wildCard = $"%{beerModel.SearchString.ToLower()}%";

				beerQuery = beerQuery
					.Where(b => EF.Functions.Like(b.Name, wildCard) ||
					            EF.Functions.Like(b.Manufacturer, wildCard) ||
					            EF.Functions.Like(b.Description, wildCard));
			}

			beerQuery = beerModel.BeerSorting switch
			{
				BeerSorting.PriceAscending => beerQuery.OrderByDescending(b => b.Price),
				BeerSorting.PriceDescending => beerQuery.OrderBy(b => b.Price),
				_ => beerQuery
					.OrderBy(b => b.AssociateId != null)
			};

			IEnumerable<BeerAllViewModel> allBeers =
				await beerQuery
					.Where(b => b.IsActive)
					.Skip((beerModel.CurrentPage - 1) * beerModel.BeersPerPage)
					.Take(beerModel.BeersPerPage)
					.Select(b => new BeerAllViewModel
					{
						Id = b.Id,
						Name = b.Name,
						Manufacturer = b.Manufacturer,
						Description = b.Description,
						ImageUrl = b.ImageUrl,
						Price = b.Price
					})
					.ToArrayAsync();

			int totalBeers = beerQuery.Count();

			return new AllBeersFilteredAndPagedServiceModel()
			{
				TotalBeersCount = totalBeers,
				Beers = allBeers
			};
		}

		public async Task<IEnumerable<BeerAllViewModel>> AllByAssociateIdAsync(string associateId)
		{
			IEnumerable<BeerAllViewModel> allAssociateBeers = await this._dbContext
				.Beers
				.Where(a =>a.IsActive &&
				           a.AssociateId.ToString() == associateId)
				.Select(b => new BeerAllViewModel
				{
					Id = b.Id,
					Name = b.Name,
					Manufacturer = b.Manufacturer,
					Description = b.Description,
					ImageUrl = b.ImageUrl,
					Price = b.Price
				})
				.ToArrayAsync();

			return allAssociateBeers;
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
				.Where(b => b.IsActive)
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
