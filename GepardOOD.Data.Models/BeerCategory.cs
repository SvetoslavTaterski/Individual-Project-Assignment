using System.ComponentModel.DataAnnotations;

using static GepardOOD.Common.EntityValidationConstants.BeerCategory;

namespace GepardOOD.Data.Models
{
    public class BeerCategory
    {
        public BeerCategory()
        {
            Beers = new HashSet<Beer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Beer> Beers { get; set; }
    }
}