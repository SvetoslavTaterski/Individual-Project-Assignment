using GepardOOD.Services.Data;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using static GepardOOD.Data.UnitTests.ServiceSeeders.WineDatabaseSeeder;


using Microsoft.EntityFrameworkCore;

using GepardOOD.Data.Models;
using GepardOOD.Web.ViewModels.Wine;
using GepardOOD.Web.ViewModels.Soda;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class WineServiceTests
	{
		private GepardOODDbContext dbContext;
		private DbContextOptions<GepardOODDbContext> dbOptions;

		private IWineService wineService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			dbOptions = new DbContextOptionsBuilder<GepardOODDbContext>()
				.UseInMemoryDatabase(databaseName: "GepardOODDbContext")
				.Options;

			dbContext = new GepardOODDbContext(dbOptions);

			this.dbContext.Database.EnsureCreated();

			SeedDatabase(this.dbContext);

			wineService = new WineService(dbContext);

		}

		[Test]
		[TestCase(25)]
		public async Task IsWineExistingByIdAsync(int wineId)
		{
			IWineService wineService = new WineService(dbContext);

			var serviceWine = await wineService.ExistsByIdAsync(wineId);

			var dbWine = await dbContext.Wines.AnyAsync(b => b.Id == wineId);

			Assert.AreEqual(serviceWine, dbWine);
		}

		[Test]
		[TestCase(1)]
		public async Task Test_DeleteWineByIdAsync(int id)
		{
			IWineService wineService = new WineService(dbContext);

			var serviceResult = wineService.DeleteWineByIdAsync(id);

			Assert.IsNotNull(serviceResult);

		}

		[Test]
		[TestCase(26, "48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_IsAssociateWithIdOwnerOfWineWithIdAsync(int wineId, string associateId)
		{
			IWineService wineService = new WineService(dbContext);

			Wine wine = dbContext.Wines.Where(b => b.IsActive).First(b => b.Id == wineId);

			bool isAssociateIdSameService =
				await wineService.IsAssociateWithIdOwnerOfWineWithIdAsync(wineId, associateId);

			bool isAssociateIdSame = wine.AssociateId.ToString() == associateId;

			Assert.AreEqual(isAssociateIdSame, isAssociateIdSameService);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetDetailsByIdAsync(int wineId)
		{
			IWineService wineService = new WineService(dbContext);

			var result = wineService.GetDetailsByIdAsync(wineId);

			Assert.IsNotNull(result);

			Assert.IsInstanceOf<Task<WineDetailsViewModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetWineForEditByIdAsync(int wineId)
		{
			IWineService wineService = new WineService(dbContext);

			var result = wineService.GetWineForEditByIdAsync(wineId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<WineFormModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetWineForDeleteByIdAsync(int wineId)
		{
			IWineService wineService = new WineService(dbContext);

			var result = wineService.GetWineForDeleteByIdAsync(wineId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<WinePreDeleteViewModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_DeleteWineByIdAsyncIsBeerPropertyChanged(int wineId)
		{
			IWineService wineService = new WineService(dbContext);

			Wine wine = await dbContext
				.Wines
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == wineId);

			wineService.DeleteWineByIdAsync(wine.Id);

			Assert.IsFalse(wine.IsActive);
		}

		[Test]
		[TestCase("48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_AllByAssociateIdAsync(string associateId)
		{
			IWineService wineService = new WineService(dbContext);

			var result = wineService.AllByAssociateIdAsync(associateId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<IEnumerable<WineAllViewModel>>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task GetWineForEditByIdAsyncReturnsCorrectType(int id)
		{
			IWineService wineService = new WineService(dbContext);

			var result = wineService.GetWineForEditByIdAsync(id);

			Assert.IsInstanceOf<Task<WineFormModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_EditWineByIdAndFormModelAsync(int id)
		{
			IWineService wineService = new WineService(dbContext);

			WineFormModel model = new WineFormModel
			{
				Id = 25,
				Name = "New Name",
				Manufacturer = "New Manufacturer",
				Description = "New very cool Description for very cool item!",
				ImageUrl = "Random",
				Price = 2,
				CategoryId = 1,
			};

			var result = wineService.EditWineByIdAndFormModelAsync(id, model);

			Assert.IsInstanceOf<Task>(result);
		}

		[Test]
		[TestCase("48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_CreateAsync(string id)
		{
			IWineService wineService = new WineService(dbContext);

			WineFormModel model = new WineFormModel
			{
				Id = 25,
				Name = "New Name",
				Manufacturer = "New Manufacturer",
				Description = "New very cool Description for very cool item!",
				ImageUrl = "Random",
				Price = 2,
				CategoryId = 1,
			};

			var result = wineService.CreateAsync(model, id);

			Assert.IsInstanceOf<Task>(result);
		}
	}
}
