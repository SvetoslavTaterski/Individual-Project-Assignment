using GepardOOD.Web.ViewModels.Associate;

namespace GepardOOD.Web.ViewModels.Beer
{
	public class BeerDetailsViewModel : BeerAllViewModel
	{
		public string Category { get; set; } = null!;

		public AssociateInfo AssociateInfo { get; set; } = null!;
	}
}
