using GepardOOD.Data.Models;
using GepardOOD.Web.Data;

namespace GepardOOD.Data.UnitTests.ServiceSeeders
{
	public static class SodaDatabaseSeeder
	{
		public static void SeedDatabase(GepardOODDbContext dbContext)
		{
			Soda soda = new Soda
			{
				Id = 25,
				Name = "Soda",
				Manufacturer = "Manufacturer",
				Description = "Very cool description for a very cool soda asdadsa dsad sad sad",
				ImageUrl = "Random",
				Price = 2,
				IsActive = true,
				SodaCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29")
			};

			Soda soda2 = new Soda
			{
				Id = 26,
				Name = "Soda2",
				Manufacturer = "Manufacturer2",
				Description = "Very cool description for a very cool soda asdadsa dsad sad sad",
				ImageUrl = "Random",
				Price = 2,
				IsActive = true,
				SodaCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29")
			};

			Soda soda3 = new Soda
			{
				Id = 27,
				Name = "Soda3",
				Manufacturer = "Manufacturer3",
				Description = "Very cool description for a very cool soda asdadsa dsad sad sad",
				ImageUrl = "Random",
				Price = 2,
				IsActive = true,
				SodaCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29")
			};

			dbContext.Sodas.Add(soda);
			dbContext.Sodas.Add(soda2);
			dbContext.Sodas.Add(soda3);

			dbContext.SaveChanges();
		}
	}
}
