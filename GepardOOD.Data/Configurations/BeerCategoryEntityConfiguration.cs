using GepardOOD.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GepardOOD.Data.Configurations
{
    public class BeerCategoryEntityConfiguration : IEntityTypeConfiguration<BeerCategory>
    {
        public void Configure(EntityTypeBuilder<BeerCategory> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private BeerCategory[] GenerateCategories()
        {
            ICollection<BeerCategory> categories = new HashSet<BeerCategory>();

            BeerCategory category;

            category = new BeerCategory()
            {
                Id = 1,
                Name = "Lager"
            };
            categories.Add(category);

            category = new BeerCategory()
            {
                Id = 2,
                Name = "Ale"
            };
            categories.Add(category);

            category = new BeerCategory()
            {
                Id = 3,
                Name = "Hybrid"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
