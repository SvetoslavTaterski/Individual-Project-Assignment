using System.ComponentModel.DataAnnotations;
using GepardOOD.Web.ViewModels.Category;
using Microsoft.Win32.SafeHandles;
using static GepardOOD.Common.EntityValidationConstants.Beer;

namespace GepardOOD.Web.ViewModels.Beer
{
	public class BeerFormModel
	{
		public BeerFormModel()
		{
			BeerCategories = new HashSet<BeerSelectCategoryFormModel>();
		}

		public int Id { get; set; }

		[Required]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(ManufacturerMaxLength, MinimumLength = ManufacturerMinLength)]
		public string Manufacturer { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength,MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required]
		[StringLength(ImageUrlMaxLength)]
		[Display(Name = "Image link")]
		public string ImageUrl { get; set; } = null!;

		[Range(typeof(decimal),PriceMinValue,PriceMaxValue)]
		public decimal Price { get; set; }

		public int CategoryId { get; set; }

		public IEnumerable<BeerSelectCategoryFormModel> BeerCategories { get; set; }
	}
}
