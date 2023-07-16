using GepardOOD.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GepardOOD.Data.Configurations
{
    public class WineEntityConfigurations : IEntityTypeConfiguration<Wine>
    {
        public void Configure(EntityTypeBuilder<Wine> builder)
        {
            builder.HasOne(b => b.WineCategory)
                .WithMany(c => c.Wines)
                .HasForeignKey(c => c.WineCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.IsActive)
	            .HasDefaultValue(true);

			builder.HasOne(b => b.Associate)
                .WithMany(a => a.Wines)
                .HasForeignKey(b => b.AssociateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
