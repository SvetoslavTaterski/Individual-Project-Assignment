using GepardOOD.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GepardOOD.Data.Configurations
{
    public class BeerEntityConfigurations : IEntityTypeConfiguration<Beer>
    {
        public void Configure(EntityTypeBuilder<Beer> builder)
        {
            builder.HasOne(b => b.BeerCategory)
                .WithMany(c => c.Beers)
                .HasForeignKey(c => c.BeerCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Associate)
                .WithMany(a => a.Beers)
                .HasForeignKey(b => b.AssociateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
