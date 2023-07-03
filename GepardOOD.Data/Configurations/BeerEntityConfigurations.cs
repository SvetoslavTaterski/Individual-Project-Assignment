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
                ImageUrl = "https://www.sid-shop.com/media/catalog/product/cache/c0afec666176687e071d6d0731b8af90/p/i/pirinsko-ken_doyif7byweomzrew.png",
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
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/3a/Ariana_beer.BG.2.JPG",
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
                ImageUrl = "https://images.unsplash.com/photo-1618885472179-5e474019f2a9?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=387&q=80",
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
