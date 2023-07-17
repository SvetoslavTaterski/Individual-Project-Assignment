using GepardOOD.Services.Data.Models.Wine;
using GepardOOD.Web.ViewModels.Soda;
using GepardOOD.Web.ViewModels.Wine;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IWineService
	{
		Task CreateAsync(WineFormModel model, string associateId);

		Task<AllWineFilteredAndPagedServiceModel> AllAsync(AllWineQueryModel wineModel);

		Task<IEnumerable<WineAllViewModel>> AllByAssociateIdAsync(string associateId);

		Task<WineDetailsViewModel?> GetDetailsByIdAsync(int wineId);
	}
}
