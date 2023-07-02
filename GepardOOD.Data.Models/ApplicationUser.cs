using Microsoft.AspNetCore.Identity;

namespace GepardOOD.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            BoughtBeers = new HashSet<Beer>();
        }
        
        public virtual ICollection<Beer> BoughtBeers { get; set; }
    }
}
