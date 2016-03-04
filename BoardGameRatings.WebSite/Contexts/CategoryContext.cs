using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public class CategoryContext : ICategoryContext
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryMapper _mapper;

        public CategoryContext(ICategoryRepository categoryRepository, ICategoryMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public CategoryViewModel BuildViewModel(int? id = null)
        {
            if (id.HasValue)
            {
                var category = _categoryRepository.GetBy(id.Value);
                return _mapper.Map(category);
            }
            return new CategoryViewModel();
        }

        public void Save(CategoryViewModel model)
        {
            var category = _categoryRepository.GetBy(model.Id);
            if (category != null)
                Update(category, model);
            else
                Add(model);
        }

        private void Update(Category category, CategoryViewModel model)
        {
            category.Description = model.Description;
            _categoryRepository.Update(category);
        }

        private void Add(CategoryViewModel model)
        {
            var category = _mapper.Map(model);
            _categoryRepository.Add(category);
        }
    }
}