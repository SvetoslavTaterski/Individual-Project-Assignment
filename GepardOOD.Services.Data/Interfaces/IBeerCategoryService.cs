using GepardOOD.Web.ViewModels.Category;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IBeerCategoryService
	{
		Task<IEnumerable<BeerSelectCategoryFormModel>> AllCategoriesAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllCategoryNamesAsync();
	}
}
