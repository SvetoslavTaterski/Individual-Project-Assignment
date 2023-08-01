using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data;
using GepardOOD.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class WineCategoryServiceTests
	{
		private GepardOODDbContext _dbContext;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<GepardOODDbContext>()
				.UseInMemoryDatabase(databaseName: "GepardOODDbContext")
				.Options;

			_dbContext = new GepardOODDbContext(options);
		}

		[Test]
		public async Task Test_AllCategoriesAsync()
		{
			IWineCategoryService wineCategoryService = new WineCategoryService(_dbContext);

			var serviceResult = await wineCategoryService.AllCategoriesAsync();

			Assert.IsNotNull(serviceResult);
		}

		[Test]
		public async Task Test_AllCategoryNamesAsync()
		{
			IWineCategoryService wineCategoryService = new WineCategoryService(_dbContext);

			var serviceResult = await wineCategoryService.AllCategoryNamesAsync();

			var dbResult = await _dbContext.WineCategories.Select(b => b.Name).ToArrayAsync();

			Assert.AreEqual(serviceResult, dbResult);
		}

		[Test]
		[TestCase(1)]
		public async Task Test_ExistsByIdAsync(int id)
		{
			IWineCategoryService wineCategoryService = new WineCategoryService(_dbContext);

			var serviceResult = await wineCategoryService.ExistsByIdAsync(id);

			var dbResult = await _dbContext.WineCategories.AnyAsync(bc => bc.Id == id);

			Assert.AreEqual(serviceResult, dbResult);
		}
	}
}
