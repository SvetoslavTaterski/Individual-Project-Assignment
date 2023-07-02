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

            builder.HasData(GenerateBeers());
        }

        private Beer[] GenerateBeers()
        {
            ICollection<Beer> beers = new HashSet<Beer>();

            Beer beer;

            beer = new Beer()
            {
                Id = 1,
                Name = "Pirinsko",
                Manufacturer = "Carlsberg",
                Description = "Pirinsko Lager is a refreshing pale lager with a well-balanced bitterness, fine hops aroma and a light amber colour. This beer is a thirst quencher and complements a wide variety of dishes well.",
                ImageUrl = "https://www.carlsberggroup.com/products/pirinsko/pirinsko-pivo/",
                Price = 2,
                BeerCategoryId = 1,
                AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
                ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29"),
            };
            beers.Add(beer);

            beer = new Beer()
            {
                Id = 2,
                Name = "Ariana",
                Manufacturer = "Zagorka",
                Description = "Ariana is a Bulgarian beer brand, produced by the Zagorka Brewery since 2004.",
                ImageUrl = "https://www.monde-selection.com/product/ariana-lager/",
                Price = 2,
                BeerCategoryId = 1,
                AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
                ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29"),
            };
            beers.Add(beer);

            beer = new Beer()
            {
                Id = 3,
                Name = "Heineken",
                Manufacturer = "Heineken N.V.",
                Description = "Heineken Lager Beer (Dutch: Heineken Pilsener), or simply Heineken (pronounced [ˈɦɛinəkə(n)]), is a Dutch pale lager beer with 5% alcohol by volume produced by the Dutch brewing company Heineken N.V.",
                ImageUrl = "https://www.wikiwand.com/en/Heineken",
                Price = 3,
                BeerCategoryId = 1,
                AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
                ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29"),
            };
            beers.Add(beer);

            return beers.ToArray();
        }
    }
}
