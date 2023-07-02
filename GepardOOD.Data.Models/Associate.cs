using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.EntityValidationConstants.Associate;

namespace GepardOOD.Data.Models
{
    public class Associate
    {
        public Associate()
        {
            Beers = new HashSet<Beer>();
            Sodas = new HashSet<Soda>();
            Wines = new HashSet<Wine>();
            Whiskeys = new HashSet<Whiskey>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Beer> Beers { get; set; }

        public virtual ICollection<Soda> Sodas { get; set; }

        public virtual ICollection<Wine> Wines { get; set; }

        public virtual ICollection<Whiskey> Whiskeys { get; set; }
    }
}
