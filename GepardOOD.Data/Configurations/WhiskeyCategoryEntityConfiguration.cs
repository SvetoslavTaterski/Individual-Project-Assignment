using GepardOOD.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GepardOOD.Data.Configurations
{
    public class WhiskeyCategoryEntityConfiguration : IEntityTypeConfiguration<WhiskeyCategory>
    {
        public void Configure(EntityTypeBuilder<WhiskeyCategory> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private WhiskeyCategory[] GenerateCategories()
        {
            ICollection<WhiskeyCategory> categories = new HashSet<WhiskeyCategory>();

            WhiskeyCategory category;

            category = new WhiskeyCategory()
            {
                Id = 1,
                Name = "Scotch"
            };
            categories.Add(category);

            category = new WhiskeyCategory()
            {
                Id = 2,
                Name = "Bourbon"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
