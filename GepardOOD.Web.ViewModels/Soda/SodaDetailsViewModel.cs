using GepardOOD.Web.ViewModels.Associate;

namespace GepardOOD.Web.ViewModels.Soda
{
	public class SodaDetailsViewModel : SodaAllViewModel
	{
		public string Category { get; set; } = null!;

		public AssociateInfo AssociateInfo { get; set; } = null!;
	}
}
