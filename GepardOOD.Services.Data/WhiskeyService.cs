using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Whiskey;

namespace GepardOOD.Services.Data
{
	public class WhiskeyService : IWhiskeyService
	{
		private readonly GepardOODDbContext _data;

		public WhiskeyService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task CreateAsync(WhiskeyFormModel model, string associateId)
		{
			Whiskey newWhiskey = new Whiskey()
			{
				Name = model.Name,
				Manufacturer = model.Manufacturer,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				Price = model.Price,
				WhiskeyCategoryId = model.CategoryId,
				AssociateId = Guid.Parse(associateId)
			};

			await _data.Whiskeys.AddAsync(newWhiskey);
			await _data.SaveChangesAsync();
		}
	}
}
