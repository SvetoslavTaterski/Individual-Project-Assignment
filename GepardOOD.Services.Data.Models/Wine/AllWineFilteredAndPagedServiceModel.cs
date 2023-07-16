using GepardOOD.Web.ViewModels.Wine;

namespace GepardOOD.Services.Data.Models.Wine
{
	public class AllWineFilteredAndPagedServiceModel
	{
		public AllWineFilteredAndPagedServiceModel()
		{
			Wines = new HashSet<WineAllViewModel>();
		}

		public int TotalWinesCount { get; set; }

		public IEnumerable<WineAllViewModel> Wines { get; set; }
	}
}
