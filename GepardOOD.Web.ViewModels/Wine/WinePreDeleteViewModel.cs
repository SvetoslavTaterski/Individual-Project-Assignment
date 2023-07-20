using System.ComponentModel.DataAnnotations;

namespace GepardOOD.Web.ViewModels.Wine
{
	public class WinePreDeleteViewModel
	{
		public string Name { get; set; } = null!;

		public string Manufacturer { get; set; } = null!;

		[Display(Name = "Image Link")]
		public string ImageUrl { get; set; } = null!;
	}
}
