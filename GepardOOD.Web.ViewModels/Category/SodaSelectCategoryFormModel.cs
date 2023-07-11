using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.EntityValidationConstants.SodaCategory;

namespace GepardOOD.Web.ViewModels.Category
{
	public class SodaSelectCategoryFormModel
	{
		public int Id { get; set; }

		[StringLength(NameMaxLength,MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;
	}
}
