using GepardOOD.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GepardOOD.Data.Configurations
{
    public class WineCategoryEntityConfiguration : IEntityTypeConfiguration<WineCategory>
    {
        public void Configure(EntityTypeBuilder<WineCategory> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private WineCategory[] GenerateCategories()
        {
            ICollection<WineCategory> categories = new HashSet<WineCategory>();

            WineCategory category;

            category = new WineCategory()
            {
                Id = 1,
                Name = "Red"
            };
            categories.Add(category);

            category = new WineCategory()
            {
                Id = 2,
                Name = "White"
            };
            categories.Add(category);

            category = new WineCategory()
            {
                Id = 3,
                Name = "Rose"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
