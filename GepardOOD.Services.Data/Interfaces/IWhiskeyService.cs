﻿using GepardOOD.Web.ViewModels.Whiskey;

namespace GepardOOD.Services.Data.Interfaces
{
	public interface IWhiskeyService
	{
		Task CreateAsync(WhiskeyFormModel model, string associateId);
	}
}