using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.EntityValidationConstants.WineCategory;

namespace GepardOOD.Data.Models
{
    public class WineCategory
    {
        public WineCategory()
        {
            Wines = new HashSet<Wine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Wine> Wines { get; set; }
    }
}
