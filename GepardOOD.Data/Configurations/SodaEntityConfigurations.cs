using GepardOOD.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GepardOOD.Data.Configurations
{
    public class SodaEntityConfigurations : IEntityTypeConfiguration<Soda>
    {
        public void Configure(EntityTypeBuilder<Soda> builder)
        {
            builder.HasOne(b => b.SodaCategory)
                .WithMany(c => c.Sodas)
                .HasForeignKey(c => c.SodaCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Associate)
                .WithMany(a => a.Sodas)
                .HasForeignKey(b => b.AssociateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
