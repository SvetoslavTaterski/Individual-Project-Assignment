using GepardOOD.Services.Data.Models.Whiskey;
using GepardOOD.Web.ViewModels.Whiskey;


namespace GepardOOD.Services.Data.Interfaces
{
	public interface IWhiskeyService
	{
		Task CreateAsync(WhiskeyFormModel model, string associateId);

		Task<AllWhiskeysFilteredAndPagedServiceModel> AllAsync(AllWhiskeyQueryModel whiskeyModel);

		Task<IEnumerable<WhiskeyAllViewModel>> AllByAssociateIdAsync(string associateId);

		Task<WhiskeyDetailsViewModel> GetDetailsByIdAsync(int whiskeyId);

		Task<bool> ExistsByIdAsync(int whiskeyId);

		Task<WhiskeyFormModel> GetWhiskeyForEditByIdAsync(int whiskeyId);
	}
}
