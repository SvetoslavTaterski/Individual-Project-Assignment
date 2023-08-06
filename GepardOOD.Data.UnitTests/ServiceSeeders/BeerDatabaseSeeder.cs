using GepardOOD.Data.Models;
using GepardOOD.Web.Data;

namespace GepardOOD.Data.UnitTests.BeerServiceSeeder
{
	public static class BeerDatabaseSeeder
	{
		public static void SeedDatabase(GepardOODDbContext dbContext)
		{
			Beer beer = new Beer
			{
				Id = 25,
				Name = "Beer1",
				Manufacturer = "Manufacturer1",
				Description = "Very cool description for the beer asd asdas dsadsada ",
				ImageUrl = "Random",
				Price = 2,
				IsActive = true,
				BeerCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29"),
			};

			Beer beer2 = new Beer
			{
				Id = 26,
				Name = "Beer2",
				Manufacturer = "Manufacturer2",
				Description = "Very cool description for the beer asd asdas dsadsada ",
				ImageUrl = "Random",
				Price = 2,
				IsActive = true,
				BeerCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29"),
			};

			Beer beer3 = new Beer
			{
				Id = 27,
				Name = "Beer3",
				Manufacturer = "Manufacturer3",
				Description = "Very cool description for the beer asd asdas dsadsada ",
				ImageUrl = "Random",
				Price = 2,
				IsActive = true,
				BeerCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29"),
			};

			dbContext.Beers.Add(beer);
			dbContext.Beers.Add(beer2);
			dbContext.Beers.Add(beer3);

			dbContext.SaveChanges();
		}
	}
}
