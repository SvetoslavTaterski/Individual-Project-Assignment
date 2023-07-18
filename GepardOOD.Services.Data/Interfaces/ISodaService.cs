using GepardOOD.Services.Data.Models.Soda;

using GepardOOD.Web.ViewModels.Soda;


namespace GepardOOD.Services.Data.Interfaces
{
	public interface ISodaService
	{
		Task CreateAsync(SodaFormModel model, string associateId);

		Task<AllSodasFilteredAndPagedServiceModel> AllAsync(AllSodaQueryModel sodaModel);

		Task<IEnumerable<SodaAllViewModel>> AllByAssociateIdAsync(string associateId);

		Task<SodaDetailsViewModel> GetDetailsByIdAsync(int sodaId);

		Task<bool> ExistsByIdAsync(int sodaId);

		Task<SodaFormModel> GetSodaForEditByIdAsync(int sodaId);
	}
}
