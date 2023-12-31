﻿using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.EntityValidationConstants.BeerCategory;

namespace GepardOOD.Web.ViewModels.Category
{
	public class BeerSelectCategoryFormModel
	{
		public int Id { get; set; }

		[StringLength(NameMaxLength,MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;
	}
}
