using System.Linq;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public class CategoriesContext : ICategoriesContext
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryMapper _mapper;

        public CategoriesContext(ICategoryRepository categoryRepository, ICategoryMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public CategoriesViewModel BuildViewModel()
        {
            var categories = _categoryRepository.GetAll().Select(category => _mapper.Map(category));
            return new CategoriesViewModel
            {
                Categories = categories
            };
        }

        public void Remove(int id)
        {
            var category = _categoryRepository.GetBy(id);
            _categoryRepository.Remove(category);
        }
    }
}