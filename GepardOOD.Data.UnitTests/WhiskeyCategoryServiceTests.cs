using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data;
using GepardOOD.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class WhiskeyCategoryServiceTests
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
			IWhiskeyCategoryService whiskeyCategoryService = new WhiskeyCategoryService(_dbContext);

			var serviceResult = await whiskeyCategoryService.AllCategoriesAsync();

			Assert.IsNotNull(serviceResult);
		}

		[Test]
		public async Task Test_AllCategoryNamesAsync()
		{
			IWhiskeyCategoryService whiskeyCategoryService = new WhiskeyCategoryService(_dbContext);

			var serviceResult = await whiskeyCategoryService.AllCategoryNamesAsync();

			var dbResult = await _dbContext.WhiskeyCategories.Select(b => b.Name).ToArrayAsync();

			Assert.AreEqual(serviceResult, dbResult);
		}

		[Test]
		[TestCase(1)]
		public async Task Test_ExistsByIdAsync(int id)
		{
			IWhiskeyCategoryService whiskeyCategoryService = new WhiskeyCategoryService(_dbContext);

			var serviceResult = await whiskeyCategoryService.ExistsByIdAsync(id);

			var dbResult = await _dbContext.WhiskeyCategories.AnyAsync(bc => bc.Id == id);

			Assert.AreEqual(serviceResult, dbResult);
		}
	}
}
