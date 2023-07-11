using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Wine;

namespace GepardOOD.Services.Data
{
	public class WineService : IWineService
	{
		private readonly GepardOODDbContext _data;

		public WineService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task CreateAsync(WineFormModel model, string associateId)
		{
			Wine newWine = new Wine()
			{
				Name = model.Name,
				Manufacturer = model.Manufacturer,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				Price = model.Price,
				WineCategoryId = model.CategoryId,
				AssociateId = Guid.Parse(associateId)
			};

			await _data.Wines.AddAsync(newWine);
			await _data.SaveChangesAsync();
		}
	}
}
