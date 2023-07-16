using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data.Models.Soda;
using GepardOOD.Services.Data.Models.Whiskey;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Soda;
using GepardOOD.Web.ViewModels.Soda.Enums;
using GepardOOD.Web.ViewModels.Whiskey;
using GepardOOD.Web.ViewModels.Whiskey.Enums;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class WhiskeyService : IWhiskeyService
	{
		private readonly GepardOODDbContext _data;

		public WhiskeyService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task<AllWhiskeysFilteredAndPagedServiceModel> AllAsync(AllWhiskeyQueryModel whiskeyModel)
		{
			IQueryable<Whiskey> whiskeyQuery = _data.Whiskeys.AsQueryable();

			if (!string.IsNullOrWhiteSpace(whiskeyModel.Category))
			{
				whiskeyQuery = whiskeyQuery.Where(b => b.WhiskeyCategory.Name == whiskeyModel.Category);
			}

			if (!string.IsNullOrWhiteSpace(whiskeyModel.SearchString))
			{
				string wildCard = $"%{whiskeyModel.SearchString.ToLower()}%";

				whiskeyQuery = whiskeyQuery
					.Where(s => EF.Functions.Like(s.Name, wildCard) ||
					            EF.Functions.Like(s.Manufacturer, wildCard) ||
					            EF.Functions.Like(s.Description, wildCard));
			}

			whiskeyQuery = whiskeyModel.WhiskeySorting switch
			{
				WhiskeySorting.PriceAscending => whiskeyQuery.OrderByDescending(b => b.Price),
				WhiskeySorting.PriceDescending => whiskeyQuery.OrderBy(b => b.Price),
				_ => whiskeyQuery
					.OrderBy(b => b.AssociateId != null)
			};

			IEnumerable<WhiskeyAllViewModel> allWhiskeys =
				await whiskeyQuery
					.Where(b => b.IsActive)
					.Skip((whiskeyModel.CurrentPage - 1) * whiskeyModel.WhiskeysPerPage)
					.Take(whiskeyModel.WhiskeysPerPage)
					.Select(w => new WhiskeyAllViewModel()
					{
						Id = w.Id,
						Name = w.Name,
						Manufacturer = w.Manufacturer,
						Description = w.Description,
						ImageUrl = w.ImageUrl,
						Price = w.Price
					})
					.ToArrayAsync();

			int totalWhiskeys = whiskeyQuery.Count();

			return new AllWhiskeysFilteredAndPagedServiceModel()
			{
				TotalWhiskeysCount = totalWhiskeys,
				Whiskeys = allWhiskeys
			};
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
