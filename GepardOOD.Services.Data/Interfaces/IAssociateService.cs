namespace GepardOOD.Services.Data.Interfaces
{
	public interface IAssociateService
	{
		Task<bool> AssociateExistByUserId(string userId);
	}
}
