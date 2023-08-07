using GepardOOD.Data.Models;
using GepardOOD.Services.Data;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Beer;
using static GepardOOD.Data.UnitTests.BeerServiceSeeder.BeerDatabaseSeeder;

using Microsoft.EntityFrameworkCore;
using GepardOOD.Web.ViewModels.Soda;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class BeerServiceTests
	{
		private GepardOODDbContext dbContext;
		private DbContextOptions<GepardOODDbContext> dbOptions;

		private IBeerService beerService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			dbOptions = new DbContextOptionsBuilder<GepardOODDbContext>()
				.UseInMemoryDatabase(databaseName: "GepardOODDbContext")
				.Options;

			dbContext = new GepardOODDbContext(dbOptions);

			this.dbContext.Database.EnsureCreated();

			SeedDatabase(this.dbContext);

			beerService = new BeerService(dbContext);

		}

		[Test]
		[TestCase(25)]
		public async Task IsBeerExistingByIdAsync(int beerId)
		{
			IBeerService beerService = new BeerService(dbContext);

			var serviceBeer = await beerService.ExistsByIdAsync(beerId);

			var dbBeer = await dbContext.Beers.AnyAsync(b => b.Id == beerId);

			Assert.AreEqual(serviceBeer, dbBeer);
		}

		[Test]
		public async Task ReturnsExactlyThreeBeersAsync()
		{
			IBeerService beerService = new BeerService(dbContext);

			var serviceBeers = await beerService.ThreeBeersAsync();

			var dbBeers = await dbContext.Beers
				.Where(b => b.IsActive)
				.OrderByDescending(b => b.Id)
				.Take(3)
				.ToArrayAsync();

			Assert.AreEqual(serviceBeers.Count(), dbBeers.Length);
		}

		[Test]
		[TestCase(1)]
		public async Task Test_DeleteBeerByIdAsync(int id)
		{
			IBeerService beerService = new BeerService(dbContext);

			var serviceResult = beerService.DeleteBeerByIdAsync(id);

			Assert.IsNotNull(serviceResult);

		}

		[Test]
		[TestCase(26, "48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_IsAssociateWithIdOwnerOfBeerWithIdAsync(int beerId, string associateId)
		{
			IBeerService beerService = new BeerService(dbContext);

			Beer beer = dbContext.Beers.Where(b => b.IsActive).First(b => b.Id == beerId);

			bool isAssociateIdSameService =
				await beerService.IsAssociateWithIdOwnerOfBeerWithIdAsync(beerId, associateId);

			bool isAssociateIdSame = beer.AssociateId.ToString() == associateId;

			Assert.AreEqual(isAssociateIdSame, isAssociateIdSameService);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetDetailsByIdAsync(int beerId)
		{
			IBeerService beerService = new BeerService(dbContext);

			var result = beerService.GetDetailsByIdAsync(beerId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<BeerDetailsViewModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetBeerForEditByIdAsync(int beerId)
		{
			IBeerService beerService = new BeerService(dbContext);

			var result = beerService.GetBeerForEditByIdAsync(beerId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<BeerFormModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_GetBeerForDeleteByIdAsync(int beerId)
		{
			IBeerService beerService = new BeerService(dbContext);

			var result = beerService.GetBeerForDeleteByIdAsync(beerId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<BeerPreDeleteViewModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_DeleteBeerByIdAsyncIsBeerPropertyChanged(int beerId)
		{
			IBeerService beerService = new BeerService(dbContext);

			Beer beer = await dbContext
				.Beers
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == beerId);

			beerService.DeleteBeerByIdAsync(beer.Id);

			Assert.IsFalse(beer.IsActive);
		}

		[Test]
		[TestCase("48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_AllByAssociateIdAsync(string associateId)
		{
			IBeerService beerService = new BeerService(dbContext);

			var result = beerService.AllByAssociateIdAsync(associateId);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Task<IEnumerable<BeerAllViewModel>>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task GetBeerForEditByIdAsyncReturnsCorrectType(int id)
		{
			IBeerService beerService = new BeerService(dbContext);

			var result = beerService.GetBeerForEditByIdAsync(id);

			Assert.IsInstanceOf<Task<BeerFormModel>>(result);
		}

		[Test]
		[TestCase(25)]
		public async Task Test_EditBeerByIdAndFormModelAsync(int id)
		{
			IBeerService beerService = new BeerService(dbContext);

			BeerFormModel model = new BeerFormModel
			{
				Id = 25,
				Name = "New Name",
				Manufacturer = "New Manufacturer",
				Description = "New very cool Description for very cool item!",
				ImageUrl = "Random",
				Price = 2,
				CategoryId = 1,
			};

			var result = beerService.EditBeerByIdAndFormModelAsync(id, model);

			Assert.IsInstanceOf<Task>(result);
		}

		[Test]
		[TestCase("48942044-CE1F-4743-9FEC-15C6808BB427")]
		public async Task Test_CreateAsync(string id)
		{
			IBeerService beerService = new BeerService(dbContext);

			BeerFormModel model = new BeerFormModel
			{
				Id = 25,
				Name = "New Name",
				Manufacturer = "New Manufacturer",
				Description = "New very cool Description for very cool item!",
				ImageUrl = "Random",
				Price = 2,
				CategoryId = 1,
			};

			var result = beerService.CreateAsync(model, id);

			Assert.IsInstanceOf<Task>(result);
		}
	}
}
