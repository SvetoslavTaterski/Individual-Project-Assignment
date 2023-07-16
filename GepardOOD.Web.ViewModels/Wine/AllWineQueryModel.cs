using System.ComponentModel.DataAnnotations;

using GepardOOD.Web.ViewModels.Wine.Enums;
using static GepardOOD.Common.GeneralApplicationConstants;

namespace GepardOOD.Web.ViewModels.Wine
{
	public class AllWineQueryModel
	{
		public AllWineQueryModel()
		{
			Categories = new HashSet<string>();
			Wines = new HashSet<WineAllViewModel>();

			CurrentPage = DefaultPage;
			WinesPerPage = EntitiesPerPage;
		}

		public string? Category { get; set; }

		[Display(Name = "Search by word")]
		public string? SearchString { get; set; }

		[Display(Name = "Sort Wine By")]
		public WineSorting WineSorting { get; set; }

		public int CurrentPage { get; set; }

		[Display(Name = "Wines Per Page")]
		public int WinesPerPage { get; set; }

		public int TotalWines { get; set; }

		public IEnumerable<string> Categories { get; set; }

		public IEnumerable<WineAllViewModel> Wines { get; set; }
	}
}
