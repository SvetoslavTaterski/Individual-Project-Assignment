using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data.Models.Soda;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Associate;
using GepardOOD.Web.ViewModels.Soda;
using GepardOOD.Web.ViewModels.Soda.Enums;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Services.Data
{
	public class SodaService : ISodaService
	{
		private readonly GepardOODDbContext _data;

		public SodaService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task<AllSodasFilteredAndPagedServiceModel> AllAsync(AllSodaQueryModel sodaModel)
		{
			IQueryable<Soda> sodaQuery = _data.Sodas.AsQueryable();

			if (!string.IsNullOrWhiteSpace(sodaModel.Category))
			{
				sodaQuery = sodaQuery.Where(b => b.SodaCategory.Name == sodaModel.Category);
			}

			if (!string.IsNullOrWhiteSpace(sodaModel.SearchString))
			{
				string wildCard = $"%{sodaModel.SearchString.ToLower()}%";

				sodaQuery = sodaQuery
					.Where(s => EF.Functions.Like(s.Name, wildCard) ||
								EF.Functions.Like(s.Manufacturer, wildCard) ||
								EF.Functions.Like(s.Description, wildCard));
			}

			sodaQuery = sodaModel.SodaSorting switch
			{
				SodaSorting.PriceAscending => sodaQuery.OrderByDescending(b => b.Price),
				SodaSorting.PriceDescending => sodaQuery.OrderBy(b => b.Price),
				_ => sodaQuery
					.OrderBy(b => b.AssociateId != null)
			};

			IEnumerable<SodaAllViewModel> allSodas =
				await sodaQuery
					.Where(s => s.IsActive)
					.Skip((sodaModel.CurrentPage - 1) * sodaModel.SodasPerPage)
					.Take(sodaModel.SodasPerPage)
					.Select(s => new SodaAllViewModel()
					{
						Id = s.Id,
						Name = s.Name,
						Manufacturer = s.Manufacturer,
						Description = s.Description,
						ImageUrl = s.ImageUrl,
						Price = s.Price
					})
					.ToArrayAsync();

			int totalSodas = sodaQuery.Count();

			return new AllSodasFilteredAndPagedServiceModel()
			{
				TotalSodasCount = totalSodas,
				Sodas = allSodas
			};
		}

		public async Task<IEnumerable<SodaAllViewModel>> AllByAssociateIdAsync(string associateId)
		{
			IEnumerable<SodaAllViewModel> allAssociateSodas = await _data
				.Sodas
				.Where(a => a.IsActive &&
							a.AssociateId.ToString() == associateId)
				.Select(b => new SodaAllViewModel()
				{
					Id = b.Id,
					Name = b.Name,
					Manufacturer = b.Manufacturer,
					Description = b.Description,
					ImageUrl = b.ImageUrl,
					Price = b.Price
				})
				.ToArrayAsync();

			return allAssociateSodas;
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

		public async Task<SodaDetailsViewModel?> GetDetailsByIdAsync(int sodaId)
		{
			Soda? soda = await _data
				.Sodas
				.Include(b => b.SodaCategory)
				.Include(b => b.Associate)
				.ThenInclude(a => a.User)
				.Where(b => b.IsActive)
				.FirstOrDefaultAsync(b => b.Id == sodaId);

			if (soda == null)
			{
				return null;
			}

			return new SodaDetailsViewModel()
			{
				Id = soda.Id,
				Name = soda.Name,
				Manufacturer = soda.Manufacturer,
				Description = soda.Description,
				ImageUrl = soda.ImageUrl,
				Price = soda.Price,
				Category = soda.SodaCategory.Name,
				AssociateInfo = new AssociateInfo()
				{
					Email = soda.Associate.User.Email,
					PhoneNumber = soda.Associate.PhoneNumber
				}
			};
		}
	}
}
