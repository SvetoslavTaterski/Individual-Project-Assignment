using System.ComponentModel.DataAnnotations;

namespace GepardOOD.Web.ViewModels.Soda
{
	public class SodaPreDeleteViewModel
	{
		public string Name { get; set; } = null!;

		public string Manufacturer { get; set; } = null!;

		[Display(Name = "Image Link")]
		public string ImageUrl { get; set; } = null!;
	}
}
