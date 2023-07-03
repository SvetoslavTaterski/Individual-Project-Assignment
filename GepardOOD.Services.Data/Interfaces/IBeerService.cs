using GepardOOD.Web.ViewModels.Home;

namespace GepardOOD.Services.Data.Interfaces
{
    public interface IBeerService
    {
        Task<IEnumerable<IndexViewModel>> ThreeBeersAsync();
    }
}
