using GepardOOD.Web.ViewModels.Beer;

namespace GepardOOD.Services.Data.Models.Beer
{
	public class AllBeersFilteredAndPagedServiceModel
	{
		public AllBeersFilteredAndPagedServiceModel()
		{
			Beers = new HashSet<BeerAllViewModel>();
		}

		public int TotalBeersCount { get; set; }

		public IEnumerable<BeerAllViewModel> Beers { get; set; }
	}
}
