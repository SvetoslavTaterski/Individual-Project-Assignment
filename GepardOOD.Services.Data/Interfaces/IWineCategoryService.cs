using GepardOOD.Web.ViewModels.Category;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IWineCategoryService
	{
		Task<IEnumerable<WineSelectCategoryFormModel>> AllCategoriesAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllCategoryNamesAsync();
	}
}
