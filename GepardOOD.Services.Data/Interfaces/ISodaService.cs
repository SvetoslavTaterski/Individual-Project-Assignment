using GepardOOD.Web.ViewModels.Soda;


namespace GepardOOD.Services.Data.Interfaces
{
	public interface ISodaService
	{
		Task CreateAsync(SodaFormModel model, string associateId);
	}
}
