using GepardOOD.Web.ViewModels.Category;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface ISodaCategoryService
	{
		Task<IEnumerable<SodaSelectCategoryFormModel>> AllCategoriesAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllCategoryNamesAsync();
	}
}
