using GepardOOD.Web.ViewModels.Beer;
using GepardOOD.Web.ViewModels.Home;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IBeerService
	{
		Task<IEnumerable<IndexViewModel>> ThreeBeersAsync();

		Task CreateAsync(BeerFormModel model, string associateId);
	}
}
