using System.ComponentModel.DataAnnotations;

using GepardOOD.Web.ViewModels.Whiskey.Enums;
using static GepardOOD.Common.GeneralApplicationConstants;

namespace GepardOOD.Web.ViewModels.Whiskey
{
	public class AllWhiskeyQueryModel
	{
		public AllWhiskeyQueryModel()
		{
			Categories = new HashSet<string>();
			Whiskeys = new HashSet<WhiskeyAllViewModel>();

			CurrentPage = DefaultPage;
			WhiskeysPerPage = EntitiesPerPage;
		}

		public string? Category { get; set; }

		[Display(Name = "Search by word")]
		public string? SearchString { get; set; }

		[Display(Name = "Sort Whiskey By")]
		public WhiskeySorting WhiskeySorting { get; set; }

		public int CurrentPage { get; set; }

		[Display(Name = "Whiskeys Per Page")]
		public int WhiskeysPerPage { get; set; }

		public int TotalWhiskeys { get; set; }

		public IEnumerable<string> Categories { get; set; }

		public IEnumerable<WhiskeyAllViewModel> Whiskeys { get; set; }
	}
}
