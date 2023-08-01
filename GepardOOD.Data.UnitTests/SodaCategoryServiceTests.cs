using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data;
using GepardOOD.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class SodaCategoryServiceTests
	{
		public GepardOODDbContext _dbContext;

		[SetUp]
		public void SetUp()
		{
			var options = new DbContextOptionsBuilder<GepardOODDbContext>()
				.UseInMemoryDatabase(databaseName: "GepardOODDbContext")
				.Options;

			_dbContext = new GepardOODDbContext(options);
		}

		[Test]
		public async Task Test_AllCategoriesAsync()
		{
			ISodaCategoryService sodaCategoryService = new SodaCategoryService(_dbContext);

			var serviceResult = await sodaCategoryService.AllCategoriesAsync();

			Assert.IsNotNull(serviceResult);
		}

		[Test]
		public async Task Test_AllCategoryNamesAsync()
		{
			ISodaCategoryService sodaCategoryService = new SodaCategoryService(_dbContext);

			var serviceResult = await sodaCategoryService.AllCategoryNamesAsync();

			var dbResult = await _dbContext.SodaCategories.Select(b => b.Name).ToArrayAsync();

			Assert.AreEqual(serviceResult, dbResult);
		}

		[Test]
		[TestCase(1)]
		public async Task Test_ExistsByIdAsync(int id)
		{
			ISodaCategoryService sodaCategoryService = new SodaCategoryService(_dbContext);

			var serviceResult = await sodaCategoryService.ExistsByIdAsync(id);

			var dbResult = await _dbContext.SodaCategories.AnyAsync(bc => bc.Id == id);

			Assert.AreEqual(serviceResult, dbResult);
		}
	}
}
