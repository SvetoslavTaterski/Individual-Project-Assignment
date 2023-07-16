using GepardOOD.Web.ViewModels.Soda.Enums;
using static GepardOOD.Common.GeneralApplicationConstants;

using System.ComponentModel.DataAnnotations;


namespace GepardOOD.Web.ViewModels.Soda
{
	public class AllSodaQueryModel
	{
		public AllSodaQueryModel()
		{
			Categories = new HashSet<string>();
			Sodas = new HashSet<SodaAllViewModel>();

			CurrentPage = DefaultPage;
			SodasPerPage = EntitiesPerPage;
		}

		public string? Category { get; set; }

		[Display(Name = "Search by word")]
		public string? SearchString { get; set; }

		[Display(Name = "Sort Sodas By")]
		public SodaSorting SodaSorting { get; set; }

		public int CurrentPage { get; set; }

		[Display(Name = "Sodas Per Page")]
		public int SodasPerPage { get; set; }

		public int TotalSodas { get; set; }

		public IEnumerable<string> Categories { get; set; }

		public IEnumerable<SodaAllViewModel> Sodas { get; set; }
	}
}
