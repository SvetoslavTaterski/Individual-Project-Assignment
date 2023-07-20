using GepardOOD.Services.Data.Models.Beer;
using GepardOOD.Web.ViewModels.Beer;
using GepardOOD.Web.ViewModels.Home;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IBeerService
	{
		Task<IEnumerable<IndexViewModel>> ThreeBeersAsync();

		Task CreateAsync(BeerFormModel model, string associateId);

		Task<AllBeersFilteredAndPagedServiceModel> AllAsync(AllBeerQueryModel beerModel);

		Task<IEnumerable<BeerAllViewModel>> AllByAssociateIdAsync(string associateId);

		Task<BeerDetailsViewModel> GetDetailsByIdAsync(int beerId);

		Task<bool> ExistsByIdAsync(int beerId);

		Task<BeerFormModel> GetBeerForEditByIdAsync(int beerId);

		Task<bool> IsAssociateWithIdOwnerOfBeerWithIdAsync(int beerId, string associateId);

		Task EditBeerByIdAndFormModelAsync(int beerId, BeerFormModel model);

		Task<BeerPreDeleteViewModel> GetBeerForDeleteByIdAsync(int beerId);

		Task DeleteBeerByIdAsync(int beerId);
	}
}
