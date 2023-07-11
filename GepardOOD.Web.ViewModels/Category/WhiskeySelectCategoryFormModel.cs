using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.EntityValidationConstants.WhiskeyCategory;

namespace GepardOOD.Web.ViewModels.Category
{
	public class WhiskeySelectCategoryFormModel
	{
		public int Id { get; set; }

		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;
	}
}
