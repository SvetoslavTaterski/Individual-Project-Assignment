using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class WhiskeyCategoryService : IWhiskeyCategoryService
	{
		private readonly GepardOODDbContext _dbContext;

		public WhiskeyCategoryService(GepardOODDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<WhiskeySelectCategoryFormModel>> AllCategoriesAsync()
		{
			IEnumerable<WhiskeySelectCategoryFormModel> whiskeyCategories = await _dbContext
				.WhiskeyCategories
				.Select(b => new WhiskeySelectCategoryFormModel()
				{
					Id = b.Id,
					Name = b.Name,
				})
				.ToArrayAsync();

			return whiskeyCategories;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await _dbContext
				.WhiskeyCategories.AnyAsync(c => c.Id == id);

			return result;
		}
	}
}
