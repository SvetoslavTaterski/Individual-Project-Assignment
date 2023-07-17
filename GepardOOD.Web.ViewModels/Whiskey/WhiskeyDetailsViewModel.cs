using GepardOOD.Web.ViewModels.Associate;

namespace GepardOOD.Web.ViewModels.Whiskey
{
	public class WhiskeyDetailsViewModel : WhiskeyAllViewModel
	{
		public string Category { get; set; } = null!;

		public AssociateInfo AssociateInfo { get; set; } = null!;
	}
}
