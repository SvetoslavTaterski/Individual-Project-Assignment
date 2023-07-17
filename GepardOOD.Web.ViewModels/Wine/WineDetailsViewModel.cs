using GepardOOD.Web.ViewModels.Associate;

namespace GepardOOD.Web.ViewModels.Wine
{
	public class WineDetailsViewModel : WineAllViewModel
	{
		public string Category { get; set; } = null!;

		public AssociateInfo AssociateInfo { get; set; } = null!;
	}
}
