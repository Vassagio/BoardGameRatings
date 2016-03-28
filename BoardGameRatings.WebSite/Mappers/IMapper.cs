using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface IMapper<TEntity, TViewModel> where TEntity : IEntity where TViewModel : IViewModel
    {
        TEntity Map(TViewModel viewModel);
        TViewModel Map(TEntity entity);
    }
}