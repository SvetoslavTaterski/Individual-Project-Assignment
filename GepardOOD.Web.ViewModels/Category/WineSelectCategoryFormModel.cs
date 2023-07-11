using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.EntityValidationConstants.WineCategory;

namespace GepardOOD.Web.ViewModels.Category
{
	public class WineSelectCategoryFormModel
	{
		public int Id { get; set; }

		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;
	}
}
