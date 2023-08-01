using GepardOOD.Data.Models;
using GepardOOD.Services.Data;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Beer;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class BeerServiceTests
	{
		private GepardOODDbContext _data;

		[SetUp]
		public void SetUp()
		{
			var options = new DbContextOptionsBuilder<GepardOODDbContext>()
				.UseInMemoryDatabase(databaseName: "GepardOODDbContext")
				.Options;

			_data = new GepardOODDbContext(options);
		}

		[Test]
		[TestCase(1)]
		public async Task IsBeerExistingByIdAsync(int beerId)
		{
			IBeerService beerService = new BeerService(_data);

			var serviceBeer = await beerService.ExistsByIdAsync(beerId);

			var dbBeer = await _data.Beers.AnyAsync(b => b.Id == beerId);

			Assert.AreEqual(serviceBeer, dbBeer);
		}

		[Test]
		public async Task ReturnsExactlyThreeBeersAsync()
		{
			IBeerService beerService = new BeerService(_data);

			var serviceBeers = await beerService.ThreeBeersAsync();

			var dbBeers = await _data.Beers
				.Where(b => b.IsActive)
				.OrderByDescending(b => b.Id)
				.Take(3)
				.ToArrayAsync();

			Assert.AreEqual(serviceBeers.Count(),dbBeers.Length);
		}

		[Test]
		[TestCase(1)]
		public async Task AreBeerIdsEqualFromGetBeerForDeleteMethod(int beerId)
		{
			Beer beer = await _data
				.Beers
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == beerId);

			BeerPreDeleteViewModel beerPreDeleteViewModel = new BeerPreDeleteViewModel()
			{
				Name = beer.Name,
				Manufacturer = beer.Manufacturer,
				ImageUrl = beer.ImageUrl
			};

			IBeerService beerService = new BeerService(_data);

			var serviceResult = await beerService.GetBeerForDeleteByIdAsync(beerId);

			Assert.AreEqual(beerPreDeleteViewModel.Name,serviceResult.Name);

		}
	}
}
