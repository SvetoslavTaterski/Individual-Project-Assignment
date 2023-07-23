using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GepardOOD.Data.Models
{
	public class Order
	{
		public Order()
		{
			Id = new Guid();
		}

		[Key]
		public Guid Id { get; set; }

		public bool IsCompleted { get; set; } = false;

		[ForeignKey(nameof(User))]
		public Guid UserId { get; set; }

		public ApplicationUser User { get; set; }

		[Required]
		public decimal TotalCost { get; set; }

		public ICollection<Beer> OrderedBeers { get; set; } = new List<Beer>();

		public ICollection<Wine> OrderedWines { get; set; } = new List<Wine>();

		public ICollection<Soda> OrderedSoda { get; set; } = new List<Soda>();

		public ICollection<Whiskey> OrderedWhiskey { get; set; } = new List<Whiskey>();
	}
}
