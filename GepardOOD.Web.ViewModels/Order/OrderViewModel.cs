using GepardOOD.Web.ViewModels.Beer;
using GepardOOD.Web.ViewModels.Soda;
using GepardOOD.Web.ViewModels.Whiskey;
using GepardOOD.Web.ViewModels.Wine;

namespace GepardOOD.Web.ViewModels.Order
{
	using Data.Models;


	public class OrderViewModel
	{
		public Guid Id { get; set; }
		public string UserId { get; set; }

		public decimal TotalCost { get; set; }

		public ICollection<BeerAllViewModel> OrderedBeers { get; set; } = new List<BeerAllViewModel>();

		public ICollection<SodaAllViewModel> OrderedSodas { get; set; } = new List<SodaAllViewModel>();

		public ICollection<WineAllViewModel> OrderedWines { get; set; } = new List<WineAllViewModel>();

		public ICollection<WhiskeyAllViewModel> OrderedWhiskeys { get; set; } = new List<WhiskeyAllViewModel>();
	}
}
