using System.ComponentModel.DataAnnotations;


namespace GepardOOD.Web.ViewModels.Soda
{
	public class SodaAllViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string Manufacturer { get; set; } = null!;

		public string Description { get; set; } = null!;

		[Display(Name = "Image Link")]
		public string ImageUrl { get; set; } = null!;

		public decimal Price { get; set; }
	}
}
