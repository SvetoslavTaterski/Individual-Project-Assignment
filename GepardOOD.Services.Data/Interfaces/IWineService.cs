using GepardOOD.Services.Data.Models.Wine;
using GepardOOD.Web.ViewModels.Wine;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IWineService
	{
		Task CreateAsync(WineFormModel model, string associateId);

		Task<AllWineFilteredAndPagedServiceModel> AllAsync(AllWineQueryModel wineModel);
	}
}
