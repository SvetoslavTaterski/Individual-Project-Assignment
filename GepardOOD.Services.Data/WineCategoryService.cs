using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Category;

using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class WineCategoryService : IWineCategoryService
	{
		private readonly GepardOODDbContext _data;

		public WineCategoryService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task<IEnumerable<WineSelectCategoryFormModel>> AllCategoriesAsync()
		{
			IEnumerable<WineSelectCategoryFormModel> wineCategories = await _data
				.WineCategories
				.Select(b => new WineSelectCategoryFormModel()
				{
					Id = b.Id,
					Name = b.Name,
				})
				.ToArrayAsync();

			return wineCategories;
		}

		public async Task<IEnumerable<string>> AllCategoryNamesAsync()
		{
			IEnumerable<string> allNames = await _data
				.WineCategories
				.Select(c => c.Name)
				.ToArrayAsync();

			return allNames;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await _data
				.WineCategories.AnyAsync(c => c.Id == id);

			return result;
		}
	}
}
