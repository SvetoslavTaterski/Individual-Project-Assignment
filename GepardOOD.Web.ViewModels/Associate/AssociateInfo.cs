using System.ComponentModel.DataAnnotations;

namespace GepardOOD.Web.ViewModels.Associate
{
	public class AssociateInfo
	{
		public string Email { get; set; } = null!;

		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; } = null!;

	}
}
