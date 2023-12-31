﻿using GepardOOD.Data.Models;
using GepardOOD.Web.Data;

namespace GepardOOD.Data.UnitTests.ServiceSeeders
{
	public static class WineDatabaseSeeder
	{
		public static void SeedDatabase(GepardOODDbContext dbContext)
		{
			Wine wine = new Wine
			{
				Id = 25,
				Name = "Wine",
				Manufacturer = "Manufacturer",
				Description = "Very cool description for very cool wine asdas dsad asd sad",
				ImageUrl = "Random",
				Price = 15,
				IsActive = true,
				WineCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29")
			};

			Wine wine2 = new Wine
			{
				Id = 26,
				Name = "Wine2",
				Manufacturer = "Manufacturer2",
				Description = "Very cool description for very cool wine asdas dsad asd sad",
				ImageUrl = "Random",
				Price = 15,
				IsActive = true,
				WineCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29")
			};

			Wine wine3 = new Wine
			{
				Id = 27,
				Name = "Wine3",
				Manufacturer = "Manufacturer3",
				Description = "Very cool description for very cool wine asdas dsad asd sad",
				ImageUrl = "Random",
				Price = 15,
				IsActive = true,
				WineCategoryId = 1,
				AssociateId = Guid.Parse("48942044-CE1F-4743-9FEC-15C6808BB427"),
				ClientId = Guid.Parse("61A22398-32D9-4ADB-EDAF-08DB7B0B2A29")
			};

			dbContext.Wines.Add(wine);
			dbContext.Wines.Add(wine2);
			dbContext.Wines.Add(wine3);

			dbContext.SaveChanges();
		}
	}
}
