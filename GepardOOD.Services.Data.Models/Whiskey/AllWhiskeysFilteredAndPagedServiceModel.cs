using GepardOOD.Web.ViewModels.Whiskey;

namespace GepardOOD.Services.Data.Models.Whiskey
{
	public class AllWhiskeysFilteredAndPagedServiceModel
	{
		public AllWhiskeysFilteredAndPagedServiceModel()
		{
			Whiskeys = new HashSet<WhiskeyAllViewModel>();
		}

		public int TotalWhiskeysCount { get; set; }

		public IEnumerable<WhiskeyAllViewModel> Whiskeys { get; set; }
	}
}
