using GepardOOD.Web.ViewModels.Category;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IWhiskeyCategoryService
	{
		Task<IEnumerable<WhiskeySelectCategoryFormModel>> AllCategoriesAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllCategoryNamesAsync();
	}
}
