using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.EntityValidationConstants.WhiskeyCategory;

namespace GepardOOD.Data.Models
{
    public class WhiskeyCategory
    {
        public WhiskeyCategory()
        {
            Whiskeys = new HashSet<Whiskey>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Whiskey> Whiskeys { get; set; }
    }
}
