﻿using GepardOOD.Web.ViewModels.Category;
using static GepardOOD.Common.EntityValidationConstants.Soda;

using System.ComponentModel.DataAnnotations;


namespace GepardOOD.Web.ViewModels.Soda
{
	public class SodaFormModel
	{
		public SodaFormModel()
		{
			SodaCategories = new HashSet<SodaSelectCategoryFormModel>();
		}

		public int Id { get; set; }

		[Required]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(ManufacturerMaxLength, MinimumLength = ManufacturerMinLength)]
		public string Manufacturer { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required]
		[StringLength(ImageUrlMaxLength)]
		[Display(Name = "Image link")]
		public string ImageUrl { get; set; } = null!;

		[Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
		public decimal Price { get; set; }

		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		public IEnumerable<SodaSelectCategoryFormModel> SodaCategories { get; set; }
	}
}
