using GepardOOD.Services.Data;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Data.UnitTests
{
	[TestFixture]
	public class AssociateServiceTests
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
		[TestCase("+359000000000")]
		public async Task Test_AssociateExistById(string phoneNumber)
		{
			IAssociateService associateService = new AssociateService(_dbContext);

			var serviceResult = await associateService.AssociateExistByPhoneNumberAsync(phoneNumber);

			var dbResult = await _dbContext.Associates.AnyAsync(a => a.PhoneNumber == phoneNumber);

			Assert.AreEqual(serviceResult, dbResult);
		}

		[Test]
		[TestCase("C42F9C78-FC1F-4D2B-9BE2-90AFBFD6F66B")]
		public async Task Test_AssociateExistByUserId(string userId)
		{
			IAssociateService associateService = new AssociateService(_dbContext);

			var serviceResult = await associateService.AssociateExistByUserIdAsync(userId);

			var dbResult = await _dbContext.Associates.AnyAsync(a => a.User.Id == Guid.Parse(userId));

			Assert.AreEqual(serviceResult, dbResult);
		}

		[Test]
		[TestCase("C42F9C78-FC1F-4D2B-9BE2-90AFBFD6F66B")]
		public async Task Test_GetAssociateIdByUserIdAsync(string userId)
		{
			IAssociateService associateService = new AssociateService(_dbContext);

			var serviceResult = await associateService.GetAssociateIdByUserIdAsync(userId);

			var dbResult = await _dbContext.Associates.FirstOrDefaultAsync(a => a.User.Id == Guid.Parse(userId));

			Assert.AreEqual(serviceResult, dbResult);
		}
	}
}