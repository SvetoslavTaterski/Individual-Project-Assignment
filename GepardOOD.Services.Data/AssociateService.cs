using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
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

		public async Task<bool> AssociateExistByUserId(string userId)
		{
			bool result = await _data
				.Associates
				.AnyAsync(a => a.UserId.ToString() == userId);

			return result;
		}
	}
}
