using GepardOOD.Data.Models;
using GepardOOD.Web.Data;

namespace GepardOOD.Data.UnitTests.ServiceSeeders
{
	public static class WhiskeyDatabaseSeeder
	{
		public static void SeedDatabase(GepardOODDbContext dbContext)
		{
			Whiskey whiskey = new Whiskey
			{
				Id = 25,
				Name = "Whiskey",
				Manufacturer = "Manufacturer",
				Description = "Very cool description for a very cool whiskey ASkdasd sakd sakd",
				ImageUrl = "Random",
				Price = 23,
				IsActive = true,
				WhiskeyCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29")
			};

			Whiskey whiskey2 = new Whiskey
			{
				Id = 26,
				Name = "Whiskey2",
				Manufacturer = "Manufacturer2",
				Description = "Very cool description for a very cool whiskey ASkdasd sakd sakd",
				ImageUrl = "Random",
				Price = 23,
				IsActive = true,
				WhiskeyCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29")
			};

			Whiskey whiskey3 = new Whiskey
			{
				Id = 27,
				Name = "Whiskey3",
				Manufacturer = "Manufacturer3",
				Description = "Very cool description for a very cool whiskey ASkdasd sakd sakd",
				ImageUrl = "Random",
				Price = 23,
				IsActive = true,
				WhiskeyCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29")
			};

			dbContext.Whiskeys.Add(whiskey);
			dbContext.Whiskeys.Add(whiskey2);
			dbContext.Whiskeys.Add(whiskey3);

			dbContext.SaveChanges();
		}
	}
}
