using GepardOOD.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GepardOOD.Data.Configurations
{
    public class WhiskeyEntityConfigurations : IEntityTypeConfiguration<Whiskey>
    {
        public void Configure(EntityTypeBuilder<Whiskey> builder)
        {
            builder.HasOne(b => b.WhiskeyCategory)
                .WithMany(c => c.Whiskeys)
                .HasForeignKey(c => c.WhiskeyCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Associate)
                .WithMany(a => a.Whiskeys)
                .HasForeignKey(b => b.AssociateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
