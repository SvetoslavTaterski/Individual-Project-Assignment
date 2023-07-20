using System.ComponentModel.DataAnnotations;

namespace GepardOOD.Web.ViewModels.Beer
{
	public class BeerPreDeleteViewModel
	{
		public string Name { get; set; } = null!;

		public string Manufacturer { get; set; } = null!;

		[Display(Name = "Image Link")]
		public string ImageUrl { get; set; } = null!;
	}
}
