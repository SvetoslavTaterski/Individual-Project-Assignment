using GepardOOD.Data.Models;
using GepardOOD.Services.Data;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class BeerCategoryServiceTests
	{
		private GepardOODDbContext _dbContext;

		[SetUp]
		public void Setup()
		{
			BeerCategory testBeerCategory = new BeerCategory
			{
				Id = 29,
				Name = "Qka",
				Beers = null
			};

			var options = new DbContextOptionsBuilder<GepardOODDbContext>()
				.UseInMemoryDatabase(databaseName: "GepardOODDbContext")
				.Options;

			_dbContext = new GepardOODDbContext(options);
		}

		[Test]
		public async Task Test_AllCategoriesAsync()
		{
			IBeerCategoryService beerCategoryService = new BeerCategoryService(_dbContext);

			var serviceResult = await beerCategoryService.AllCategoriesAsync();

			Assert.IsNotNull(serviceResult);
		}

		[Test]
		public async Task Test_AllCategoryNamesAsync()
		{
			IBeerCategoryService beerCategoryService = new BeerCategoryService(_dbContext);

			var serviceResult = await beerCategoryService.AllCategoryNamesAsync();

			var dbResult = await _dbContext.BeerCategories.Select(b => b.Name).ToArrayAsync();

			Assert.AreEqual(serviceResult, dbResult);
		}

		[Test]
		[TestCase(1)]
		public async Task Test_ExistsByIdAsync(int id)
		{
			IBeerCategoryService beerCategoryService = new BeerCategoryService(_dbContext);

			var serviceResult = await beerCategoryService.ExistsByIdAsync(id);

			var dbResult = await _dbContext.BeerCategories.AnyAsync(bc => bc.Id == id);

			Assert.AreEqual(serviceResult,dbResult);
		}
	}
}
