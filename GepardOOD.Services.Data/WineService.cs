﻿using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Services.Data.Models.Wine;
using GepardOOD.Web.Data;
using GepardOOD.Web.ViewModels.Associate;
using GepardOOD.Web.ViewModels.Wine;
using GepardOOD.Web.ViewModels.Wine.Enums;
using Wine = GepardOOD.Data.Models.Wine;

using Microsoft.EntityFrameworkCore;
using GepardOOD.Web.ViewModels.Soda;

namespace GepardOOD.Services.Data
{
	public class WineService : IWineService
	{
		private readonly GepardOODDbContext _data;

		public WineService(GepardOODDbContext data)
		{
			_data = data;
		}

		public async Task<AllWineFilteredAndPagedServiceModel> AllAsync(AllWineQueryModel wineModel)
		{
			IQueryable<Wine> wineQuery = _data.Wines.AsQueryable();

			if (!string.IsNullOrWhiteSpace(wineModel.Category))
			{
				wineQuery = wineQuery.Where(w => w.WineCategory.Name == wineModel.Category);
			}

			if (!string.IsNullOrWhiteSpace(wineModel.SearchString))
			{
				string wildCard = $"%{wineModel.SearchString.ToLower()}%";

				wineQuery = wineQuery
					.Where(s => EF.Functions.Like(s.Name, wildCard) ||
								EF.Functions.Like(s.Manufacturer, wildCard) ||
								EF.Functions.Like(s.Description, wildCard));
			}

			wineQuery = wineModel.WineSorting switch
			{
				WineSorting.PriceAscending => wineQuery.OrderByDescending(b => b.Price),
				WineSorting.PriceDescending => wineQuery.OrderBy(b => b.Price),
				_ => wineQuery
					.OrderBy(b => b.AssociateId != null)
			};

			IEnumerable<WineAllViewModel> allWines =
				await wineQuery
					.Where(b => b.IsActive)
					.Skip((wineModel.CurrentPage - 1) * wineModel.WinesPerPage)
					.Take(wineModel.WinesPerPage)
					.Select(w => new WineAllViewModel()
					{
						Id = w.Id,
						Name = w.Name,
						Manufacturer = w.Manufacturer,
						Description = w.Description,
						ImageUrl = w.ImageUrl,
						Price = w.Price
					})
					.ToArrayAsync();

			int totalWines = wineQuery.Count();

			return new AllWineFilteredAndPagedServiceModel()
			{
				TotalWinesCount = totalWines,
				Wines = allWines
			};
		}

		public async Task<IEnumerable<WineAllViewModel>> AllByAssociateIdAsync(string associateId)
		{
			IEnumerable<WineAllViewModel> allAssociateWines = await _data
				.Wines
				.Where(a => a.IsActive &&
							a.AssociateId.ToString() == associateId)
				.Select(b => new WineAllViewModel()
				{
					Id = b.Id,
					Name = b.Name,
					Manufacturer = b.Manufacturer,
					Description = b.Description,
					ImageUrl = b.ImageUrl,
					Price = b.Price
				})
				.ToArrayAsync();

			return allAssociateWines;
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

		public async Task DeleteWineByIdAsync(int wineId)
		{
			Wine wine = await _data
				.Wines
				.Where(w => w.IsActive)
				.FirstAsync(w => w.Id == wineId);

			wine.IsActive = false;

			await _data.SaveChangesAsync();
		}

		public async Task EditWineByIdAndFormModelAsync(int wineId, WineFormModel model)
		{
			Wine wine = await _data
				.Wines
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == wineId);

			wine.Name = model.Name;
			wine.Manufacturer = model.Manufacturer;
			wine.Description = model.Description;
			wine.ImageUrl = model.ImageUrl;
			wine.Price = model.Price;
			wine.WineCategoryId = model.CategoryId;

			await _data.SaveChangesAsync();
		}

		public async Task<bool> ExistsByIdAsync(int wineId)
		{
			bool result = await _data
				.Wines
				.Where(b => b.IsActive)
				.AnyAsync(b => b.Id == wineId);

			return result;
		}

		public async Task<WineDetailsViewModel> GetDetailsByIdAsync(int wineId)
		{
			Wine wine = await _data
				.Wines
				.Include(b => b.WineCategory)
			    .Include(b => b.Associate)
				.ThenInclude(a => a.User)
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == wineId);

			return new WineDetailsViewModel()
			{
				Id = wine.Id,
				Name = wine.Name,
				Manufacturer = wine.Manufacturer,
				Description = wine.Description,
				ImageUrl = wine.ImageUrl,
				Price = wine.Price,
				Category = wine.WineCategory.Name,
				AssociateInfo = new AssociateInfo()
				{
					Email = wine.Associate.User.Email,
					PhoneNumber = wine.Associate.PhoneNumber
				}
			};
		}

		public async Task<WinePreDeleteViewModel> GetWineForDeleteByIdAsync(int wineId)
		{
			Wine wine = await _data
				.Wines
				.Where(w => w.IsActive)
				.FirstAsync(w => w.Id == wineId);

			return new WinePreDeleteViewModel()
			{
				Name = wine.Name,
				Manufacturer = wine.Manufacturer,
				ImageUrl = wine.ImageUrl
			};
		}

		public async Task<WineFormModel> GetWineForEditByIdAsync(int wineId)
		{
			Wine wine = await _data
				.Wines
				.Include(b => b.WineCategory)
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == wineId);

			return new WineFormModel()
			{
				Id = wine.Id,
				Name = wine.Name,
				Manufacturer = wine.Manufacturer,
				Description = wine.Description,
				ImageUrl = wine.ImageUrl,
				Price = wine.Price,
				CategoryId = wine.WineCategoryId,
			};
		}

		public async Task<bool> IsAssociateWithIdOwnerOfWineWithIdAsync(int wineId, string associateId)
		{
			Wine wine = await _data
				.Wines
				.Where(b => b.IsActive)
				.FirstAsync(b => b.Id == wineId);

			return wine.AssociateId.ToString() == associateId;
		}
	}
}
