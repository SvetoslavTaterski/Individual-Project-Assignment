using GepardOOD.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GepardOOD.Data.Configurations
{
    public class SodaCategoryEntityConfiguration : IEntityTypeConfiguration<SodaCategory>
    {
        public void Configure(EntityTypeBuilder<SodaCategory> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private SodaCategory[] GenerateCategories()
        {
            ICollection<SodaCategory> categories = new HashSet<SodaCategory>();

            SodaCategory category;

            category = new SodaCategory()
            {
                Id = 1,
                Name = "With Sugar"
            };
            categories.Add(category);

            category = new SodaCategory()
            {
                Id = 2,
                Name = "Zero Sugar"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
