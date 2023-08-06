using GepardOOD.Data.Models;
using GepardOOD.Services.Data;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using static GepardOOD.Data.UnitTests.ServiceSeeders.WhiskeyDatabaseSeeder;

using Microsoft.EntityFrameworkCore;
using GepardOOD.Web.ViewModels.Whiskey;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class WhiskeyServiceTests
	{
		private GepardOODDbContext dbContext;
		private DbContextOptions<GepardOODDbContext> dbOptions;

		private IWhiskeyService whiskeyService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			dbOptions = new DbContextOptionsBuilder<GepardOODDbContext>()
				.UseInMemoryDatabase(databaseName: "GepardOODDbContext")
				.Options;

			dbContext = new GepardOODDbContext(dbOptions);

			this.dbContext.Database.EnsureCreated();

			SeedDatabase(this.dbContext);

			whiskeyService = new WhiskeyService(dbContext);
		}

		[Test]
		[TestCase(25)]
		public async Task IsWhiskeyExistingByIdAsync(int whiskeyId)
		{
			IWhiskeyService whiskeyService = new WhiskeyService(dbContext);

			var serviceWhiskey = await whiskeyService.ExistsByIdAsync(whiskeyId);

			var dbWhiskey = await dbContext.Whiskeys.AnyAsync(b => b.Id == whiskeyId);

			Assert.AreEqual(serviceWhiskey, dbWhiskey);
		}

		[Test]
		[TestCase(1)]
		public async Task Test_DeleteWhiskeyByIdAsync(int id)
		{
			IWhiskeyService whiskeyService = new WhiskeyService(dbContext);

			var serviceResult = whiskeyService.DeleteWhiskeyByIdAsync(id);

			Assert.IsNotNull(serviceResult);

		}

		[Test]
		[TestCase(26, "48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_IsAssociateWithIdOwnerOfSodaWithIdAsync(int whiskeyId, string associateId)
		{
			IWhiskeyService whiskeyService = new WhiskeyService(dbContext);

			Whiskey whiskey = dbContext.Whiskeys.Where(b => b.IsActive).First(b => b.Id == whiskeyId);

			bool isAssociateIdSameService =
				await whiskeyService.IsAssociateWithIdOwnerOfWhiskeyWithIdAsync(whiskeyId, associateId);

			bool isAssociateIdSame = whiskey.AssociateId.ToString() == associateId;

			Assert.AreEqual(isAssociateIdSame, isAssociateIdSameService);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetDetailsByIdAsync(int whiskeyId)
		{
			IWhiskeyService whiskeyService = new WhiskeyService(dbContext);

			var result = whiskeyService.GetDetailsByIdAsync(whiskeyId);

			Assert.IsNotNull(result);

			Assert.IsInstanceOf<Task<WhiskeyDetailsViewModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetWhiskeyForEditByIdAsync(int whiskeyId)
		{
			IWhiskeyService whiskeyService = new WhiskeyService(dbContext);

			var result = whiskeyService.GetWhiskeyForEditByIdAsync(whiskeyId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<WhiskeyFormModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetWhiskeyForDeleteByIdAsync(int whiskeyId)
		{
			IWhiskeyService whiskeyService = new WhiskeyService(dbContext);

			var result = whiskeyService.GetWhiskeyForDeleteByIdAsync(whiskeyId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<WhiskeyPreDeleteViewModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_DeleteWhiskeyByIdAsyncIsWhiskeyPropertyChanged(int whiskeyId)
		{
			IWhiskeyService whiskeyService = new WhiskeyService(dbContext);

			Whiskey whiskey = await dbContext
				.Whiskeys
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == whiskeyId);

			whiskeyService.DeleteWhiskeyByIdAsync(whiskey.Id);

			Assert.IsFalse(whiskey.IsActive);
		}

		[Test]
		[TestCase("48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_AllByAssociateIdAsync(string associateId)
		{
			IWhiskeyService WhiskeyService = new WhiskeyService(dbContext);

			var result = WhiskeyService.AllByAssociateIdAsync(associateId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<IEnumerable<WhiskeyAllViewModel>>>(result);
		}
	}
}
