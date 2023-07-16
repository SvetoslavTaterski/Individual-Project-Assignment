using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data.Models.Soda;
using GepardOOD.Services.Data.Models.Wine;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Soda;
using GepardOOD.Web.ViewModels.Soda.Enums;
using GepardOOD.Web.ViewModels.Wine;
using GepardOOD.Web.ViewModels.Wine.Enums;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class WineService : IWineService
	{
		private readonly GepardOODDbContext _data;

		public WineService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task<AllWineFilteredAndPagedServiceModel> AllAsync(AllWineQueryModel wineModel)
		{
			IQueryable<Wine> wineQuery = _data.Wines.AsQueryable();

			if (!string.IsNullOrWhiteSpace(wineModel.Category))
			{
				wineQuery = wineQuery.Where(w => w.WineCategory.Name == wineModel.Category);
			}

			if (!string.IsNullOrWhiteSpace(wineModel.SearchString))
			{
				string wildCard = $"%{wineModel.SearchString.ToLower()}%";

				wineQuery = wineQuery
					.Where(s => EF.Functions.Like(s.Name, wildCard) ||
					            EF.Functions.Like(s.Manufacturer, wildCard) ||
					            EF.Functions.Like(s.Description, wildCard));
			}

			wineQuery = wineModel.WineSorting switch
			{
				WineSorting.PriceAscending => wineQuery.OrderByDescending(b => b.Price),
				WineSorting.PriceDescending => wineQuery.OrderBy(b => b.Price),
				_ => wineQuery
					.OrderBy(b => b.AssociateId != null)
			};

			IEnumerable<WineAllViewModel> allWines =
				await wineQuery.Skip((wineModel.CurrentPage - 1) * wineModel.WinesPerPage)
					.Take(wineModel.WinesPerPage)
					.Select(w => new WineAllViewModel()
					{
						Id = w.Id,
						Name = w.Name,
						Manufacturer = w.Manufacturer,
						Description = w.Description,
						ImageUrl = w.ImageUrl,
						Price = w.Price
					})
					.ToArrayAsync();

			int totalWines = wineQuery.Count();

			return new AllWineFilteredAndPagedServiceModel()
			{
				TotalWinesCount = totalWines,
				Wines = allWines
			};
		}

		public async Task CreateAsync(WineFormModel model, string associateId)
		{
			Wine newWine = new Wine()
			{
				Name = model.Name,
				Manufacturer = model.Manufacturer,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				Price = model.Price,
				WineCategoryId = model.CategoryId,
				AssociateId = Guid.Parse(associateId)
			};

			await _data.Wines.AddAsync(newWine);
			await _data.SaveChangesAsync();
		}
	}
}
