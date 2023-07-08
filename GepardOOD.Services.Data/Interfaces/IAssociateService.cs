using GepardOOD.Web.ViewModels.Associate;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IAssociateService
	{
		Task<bool> AssociateExistByUserIdAsync(string userId);

		Task<bool> AssociateExistByPhoneNumberAsync(string phoneNumber);

		Task Create(string userId, BecomeAssociateFormModel model);

		Task<string?> GetAssociateIdByUserIdAsync(string userId);
	}
}
