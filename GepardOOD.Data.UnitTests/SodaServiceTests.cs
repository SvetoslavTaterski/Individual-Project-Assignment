using GepardOOD.Data.Models;
using GepardOOD.Services.Data;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Soda;
using GepardOOD.Web.ViewModels.Soda.Enums;
using static GepardOOD.Data.UnitTests.ServiceSeeders.SodaDatabaseSeeder;

using Microsoft.EntityFrameworkCore;
using GepardOOD.Services.Data.Models.Soda;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class SodaServiceTests
	{
		private GepardOODDbContext dbContext;
		private DbContextOptions<GepardOODDbContext> dbOptions;

		private ISodaService sodaService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			dbOptions = new DbContextOptionsBuilder<GepardOODDbContext>()
				.UseInMemoryDatabase(databaseName: "GepardOODDbContext")
				.Options;

			dbContext = new GepardOODDbContext(dbOptions);

			this.dbContext.Database.EnsureCreated();

			SeedDatabase(this.dbContext);

			sodaService = new SodaService(dbContext);
		}

		[Test]
		[TestCase(25)]
		public async Task IsSodaExistingByIdAsync(int sodaId)
		{
			ISodaService sodaService = new SodaService(dbContext);

			var serviceSoda = await sodaService.ExistsByIdAsync(sodaId);

			var dbSoda = await dbContext.Sodas.AnyAsync(b => b.Id == sodaId);

			Assert.AreEqual(serviceSoda, dbSoda);
		}

		[Test]
		[TestCase(1)]
		public async Task Test_DeleteSodaByIdAsync(int id)
		{
			ISodaService sodaService = new SodaService(dbContext);

			var serviceResult = sodaService.DeleteSodaByIdAsync(id);

			Assert.IsNotNull(serviceResult);

		}

		[Test]
		[TestCase(26, "48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_IsAssociateWithIdOwnerOfSodaWithIdAsync(int sodaId, string associateId)
		{
			ISodaService sodaService = new SodaService(dbContext);

			Soda soda = dbContext.Sodas.Where(b => b.IsActive).First(b => b.Id == sodaId);

			bool isAssociateIdSameService =
				await sodaService.IsAssociateWithIdOwnerOfSodaWithIdAsync(sodaId, associateId);

			bool isAssociateIdSame = soda.AssociateId.ToString() == associateId;

			Assert.AreEqual(isAssociateIdSame, isAssociateIdSameService);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetDetailsByIdAsync(int wineId)
		{
			ISodaService sodaService = new SodaService(dbContext);

			var result = sodaService.GetDetailsByIdAsync(wineId);

			Assert.IsNotNull(result);

			Assert.IsInstanceOf<Task<SodaDetailsViewModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetSodaForEditByIdAsync(int sodaId)
		{
			ISodaService sodaService = new SodaService(dbContext);

			var result = sodaService.GetSodaForEditByIdAsync(sodaId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<SodaFormModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetSodaForDeleteByIdAsync(int sodaId)
		{
			ISodaService sodaService = new SodaService(dbContext);

			var result = sodaService.GetSodaForDeleteByIdAsync(sodaId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<SodaPreDeleteViewModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_DeleteSodaByIdAsyncIsSodaPropertyChanged(int sodaId)
		{
			ISodaService sodaService = new SodaService(dbContext);

			Soda soda = await dbContext
				.Sodas
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == sodaId);

			sodaService.DeleteSodaByIdAsync(soda.Id);

			Assert.IsFalse(soda.IsActive);
		}

		[Test]
		[TestCase("48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_AllByAssociateIdAsync(string associateId)
		{
			ISodaService sodaService = new SodaService(dbContext);

			var result = sodaService.AllByAssociateIdAsync(associateId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<IEnumerable<SodaAllViewModel>>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task GetSodaForEditByIdAsyncReturnsCorrectType(int id)
		{
			ISodaService sodaService = new SodaService(dbContext);

			var result = sodaService.GetSodaForEditByIdAsync(id);

			Assert.IsInstanceOf<Task<SodaFormModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_EditSodaByIdAndFormModelAsync(int id)
		{
			ISodaService sodaService = new SodaService(dbContext);

			SodaFormModel model = new SodaFormModel
			{
				Id = 25,
				Name = "New Name",
				Manufacturer = "New Manufacturer",
				Description = "New very cool Description for very cool item!",
				ImageUrl = "Random",
				Price = 2,
				CategoryId = 1,
			};

			var result = sodaService.EditSodaByIdAndFormModelAsync(id, model);

			Assert.IsInstanceOf<Task>(result);
		}

		[Test]
		[TestCase("48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_CreateAsync(string id)
		{
			ISodaService sodaService = new SodaService(dbContext);

			SodaFormModel model = new SodaFormModel
			{
				Id = 25,
				Name = "New Name",
				Manufacturer = "New Manufacturer",
				Description = "New very cool Description for very cool item!",
				ImageUrl = "Random",
				Price = 2,
				CategoryId = 1,
			};

			var result = sodaService.CreateAsync(model, id);

			Assert.IsInstanceOf<Task>(result);
		}

		[Test]
		public async Task Test_AllAsync()
		{
			ISodaService sodaService = new SodaService(dbContext);

			AllSodaQueryModel model = new AllSodaQueryModel
			{
				Category = null,
				SearchString = null,
				SodaSorting = (SodaSorting)0,
				CurrentPage = 0,
				SodasPerPage = 0,
				TotalSodas = 0,
				Categories = null,
				Sodas = null
			};

			var result = sodaService.AllAsync(model);

			Assert.IsInstanceOf<Task<AllSodasFilteredAndPagedServiceModel>>(result);
		}
	}
}
