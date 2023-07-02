using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.EntityValidationConstants.SodaCategory;

namespace GepardOOD.Data.Models
{
    public class SodaCategory
    {
        public SodaCategory()
        {
            Sodas = new HashSet<Soda>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Soda> Sodas { get; set; }
    }
}
