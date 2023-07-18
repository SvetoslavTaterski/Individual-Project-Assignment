using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data.Models.Whiskey;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Associate;
using GepardOOD.Web.ViewModels.Whiskey;
using GepardOOD.Web.ViewModels.Whiskey.Enums;
using Whiskey = GepardOOD.Data.Models.Whiskey;

using Microsoft.EntityFrameworkCore;
using static GepardOOD.Common.EntityValidationConstants;
using GepardOOD.Web.ViewModels.Beer;

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

		public async Task<IEnumerable<WhiskeyAllViewModel>> AllByAssociateIdAsync(string associateId)
		{
			IEnumerable<WhiskeyAllViewModel> allAssociateWhiskeys = await _data
				.Whiskeys
				.Where(a => a.IsActive &&
							a.AssociateId.ToString() == associateId)
				.Select(b => new WhiskeyAllViewModel()
				{
					Id = b.Id,
					Name = b.Name,
					Manufacturer = b.Manufacturer,
					Description = b.Description,
					ImageUrl = b.ImageUrl,
					Price = b.Price
				})
				.ToArrayAsync();

			return allAssociateWhiskeys;
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

		public async Task<bool> ExistsByIdAsync(int whiskeyId)
		{
			bool result = await _data
			    .Whiskeys
				.Where(b => b.IsActive)
				.AnyAsync(b => b.Id == whiskeyId);

			return result;
		}

		public async Task<WhiskeyDetailsViewModel> GetDetailsByIdAsync(int whiskeyId)
		{
			Whiskey whiskey = await _data
				.Whiskeys
				.Include(b => b.WhiskeyCategory)
				.Include(b => b.Associate)
				.ThenInclude(a => a.User)
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == whiskeyId);

			return new WhiskeyDetailsViewModel()
			{
				Id = whiskey.Id,
				Name = whiskey.Name,
				Manufacturer = whiskey.Manufacturer,
				Description = whiskey.Description,
				ImageUrl = whiskey.ImageUrl,
				Price = whiskey.Price,
				Category = whiskey.WhiskeyCategory.Name,
				AssociateInfo = new AssociateInfo()
				{
					Email = whiskey.Associate.User.Email,
					PhoneNumber = whiskey.Associate.PhoneNumber
				}
			};
		}

		public async Task<WhiskeyFormModel> GetWhiskeyForEditByIdAsync(int whiskeyId)
		{
			Whiskey beer = await _data
				.Whiskeys
				.Include(b => b.WhiskeyCategory)
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == whiskeyId);

			return new WhiskeyFormModel()
			{
				Id = beer.Id,
				Name = beer.Name,
				Manufacturer = beer.Manufacturer,
				Description = beer.Description,
				ImageUrl = beer.ImageUrl,
				Price = beer.Price,
				CategoryId = beer.WhiskeyCategoryId,
			};
		}
	}
}
