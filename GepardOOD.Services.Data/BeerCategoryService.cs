using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Category;

using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class BeerCategoryService : IBeerCategoryService
	{
		private readonly GepardOODDbContext _dbContext;

		public BeerCategoryService(GepardOODDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<BeerSelectCategoryFormModel>> AllCategoriesAsync()
		{
			IEnumerable<BeerSelectCategoryFormModel> beerCategories = await _dbContext
				.BeerCategories
				.Select(b => new BeerSelectCategoryFormModel()
				{
					Id = b.Id,
					Name = b.Name,
				})
				.ToArrayAsync();

			return beerCategories;
		}
	}
}
