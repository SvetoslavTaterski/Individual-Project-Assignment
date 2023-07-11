using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class SodaCategoryService : ISodaCategoryService
	{
		private readonly GepardOODDbContext _data;

		public SodaCategoryService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task<IEnumerable<SodaSelectCategoryFormModel>> AllCategoriesAsync()
		{
			IEnumerable<SodaSelectCategoryFormModel> sodaCategories = await _data
				.SodaCategories
				.Select(b => new SodaSelectCategoryFormModel()
				{
					Id = b.Id,
					Name = b.Name,
				})
				.ToArrayAsync();

			return sodaCategories;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await _data
				.SodaCategories.AnyAsync(c => c.Id == id);

			return result;
		}
	}
}
