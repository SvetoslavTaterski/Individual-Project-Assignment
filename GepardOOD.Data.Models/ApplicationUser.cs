using Microsoft.AspNetCore.Identity;

namespace GepardOOD.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            BoughtBeers = new HashSet<Beer>();
        }
        
        public virtual ICollection<Beer> BoughtBeers { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
	}
}
