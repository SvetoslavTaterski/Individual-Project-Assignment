using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Associate;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class AssociateService : IAssociateService
	{
		private readonly GepardOODDbContext _data;

		public AssociateService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task<bool> AssociateExistByPhoneNumberAsync(string phoneNumber)
		{
			bool result = await _data
				.Associates
				.AnyAsync(a => a.PhoneNumber == phoneNumber);

			return result;
		}

		public async Task<bool> AssociateExistByUserIdAsync(string userId)
		{
			bool result = await _data
				.Associates
				.AnyAsync(a => a.UserId.ToString() == userId);

			return result;
		}

		public async Task Create(string userId, BecomeAssociateFormModel model)
		{
			Associate associate = new Associate()
			{
				PhoneNumber = model.PhoneNumber,
				UserId = Guid.Parse(userId)
			};

			await _data.Associates.AddAsync(associate);
			await _data.SaveChangesAsync();


		}
	}
}
