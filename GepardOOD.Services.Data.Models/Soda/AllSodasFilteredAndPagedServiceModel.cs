using GepardOOD.Web.ViewModels.Soda;

namespace GepardOOD.Services.Data.Models.Soda
{
	public class AllSodasFilteredAndPagedServiceModel
	{
		public AllSodasFilteredAndPagedServiceModel()
		{
			Sodas = new HashSet<SodaAllViewModel>();
		}

		public int TotalSodasCount { get; set; }

		public IEnumerable<SodaAllViewModel> Sodas { get; set; }
	}
}
