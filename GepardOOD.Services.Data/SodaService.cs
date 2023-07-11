using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Soda;

namespace GepardOOD.Services.Data
{
	public class SodaService : ISodaService
	{
		private readonly GepardOODDbContext _data;

		public SodaService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task CreateAsync(SodaFormModel model, string associateId)
		{
			Soda newSoda = new Soda()
			{
				Name = model.Name,
				Manufacturer = model.Manufacturer,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				Price = model.Price,
				SodaCategoryId = model.CategoryId,
				AssociateId = Guid.Parse(associateId)
			};

			await _data.Sodas.AddAsync(newSoda);
			await _data.SaveChangesAsync();
		}
	}
}
