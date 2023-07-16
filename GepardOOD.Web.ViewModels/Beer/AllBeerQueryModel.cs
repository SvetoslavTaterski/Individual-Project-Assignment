using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.GeneralApplicationConstants;
using GepardOOD.Web.ViewModels.Beer.Enums;

namespace GepardOOD.Web.ViewModels.Beer
{
	public class AllBeerQueryModel
	{
		public AllBeerQueryModel()
		{
			Categories = new HashSet<string>();
			Beers = new HashSet<BeerAllViewModel>();

			CurrentPage = DefaultPage;
			BeersPerPage = EntitiesPerPage;
		}

		public string? Category { get; set; }

		[Display(Name = "Search by word")]
		public string? SearchString { get; set; }

		[Display(Name = "Sort Beers By")]
		public BeerSorting BeerSorting { get; set; }

		public int CurrentPage { get; set; }

		[Display(Name = "Beers Per Page")]
		public int BeersPerPage { get; set; }

		public int TotalBeers { get; set; }

		public IEnumerable<string> Categories { get; set; }

		public IEnumerable<BeerAllViewModel> Beers { get; set; }
	}
}
