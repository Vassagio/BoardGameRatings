using System;
using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.Tests.Extensions;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Models.Repositories
{
    public class CategoryRepositoryTest : IClassFixture<RepositoryFixture>, IDisposable
    {
        private readonly RepositoryFixture _fixture;

        public CategoryRepositoryTest(RepositoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.Begin();
        }


        public void Dispose()
        {
            _fixture.End();
        }

        [Fact]
        public void CreateANewCategoryRepository()
        {
            var categoryRepository = new CategoryRepository(_fixture.Context);
            Assert.NotNull(categoryRepository);
        }

        [Fact]
        public void GetAllCategories()
        {
            var categories = new List<Category>
            {
                new Category {Description = "Category 1"},
                new Category {Description = "Category 2"},
                new Category {Description = "Category 3"}
            };

            var categoryRepository = new CategoryRepository(_fixture.Context.CategoriesContain(categories));

            var result = categoryRepository.GetAll()
                .ToList();

            Assert.Equal(3, result.Count());
            Assert.Equal(categories, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void AddCategory()
        {
            var category = new Category
            {
                Description = "Category 1"
            };

            var categoryRepository = new CategoryRepository(_fixture.Context);

            var result = categoryRepository.Add(category);

            Assert.NotNull(result);
            Assert.Equal(category, result);
        }

        [Fact]
        public void DoesNotAddDuplicateCategory()
        {
            var category = new Category
            {
                Description = "Category 1"
            };

            var categoryRepository = new CategoryRepository(_fixture.Context);

            categoryRepository.Add(category);
            categoryRepository.Add(category);

            var result = categoryRepository.GetAll();

            Assert.Equal(1, result.Count());
            Assert.Equal(category, result.First());
        }

        [Fact]
        public void RemoveCategory()
        {
            var category1 = new Category {Description = "Category 1"};
            var category2 = new Category {Description = "Category 2"};
            var category3 = new Category {Description = "Category 3"};
            var categories = new List<Category> {category1, category2, category3};

            var categoryRepository = new CategoryRepository(_fixture.Context.CategoriesContain(categories));

            categoryRepository.Remove(category2);
            var result = categoryRepository.GetAll()
                .ToList();

            Assert.Equal(2, result.Count());
            Assert.Equal(new List<Category> {category1, category3}, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void GetCategoryById()
        {
            var category3 = new Category {Description = "Category 3"};
            var categories = new List<Category>
            {
                new Category {Description = "Category 1"},
                new Category {Description = "Category 2"},
                category3
            };

            var categoryRepository = new CategoryRepository(_fixture.Context.CategoriesContain(categories));

            var result = categoryRepository.GetBy(category3.Id);

            Assert.Equal(category3.Id, result.Id);
            Assert.Equal(category3.Description, result.Description);
        }

        [Theory]
        [InlineData("Category 3")]
        [InlineData("category 3")]
        public void GetCategoryByName(string description)
        {
            var category3 = new Category {Description = description};
            var categories = new List<Category>
            {
                new Category {Description = "Category 1"},
                new Category {Description = "Category 2"},
                category3
            };

            var categoryRepository = new CategoryRepository(_fixture.Context.CategoriesContain(categories));

            var result = categoryRepository.GetBy(category3.Description);

            Assert.Equal(category3.Id, result.Id);
            Assert.Equal(category3.Description, result.Description);
        }

        [Fact]
        public void UpdateCategory()
        {
            var categories = new List<Category>
            {
                new Category {Description = "Category 1"},
                new Category {Description = "Category 2"},
                new Category {Description = "Category 3"}
            };

            var categoryRepository = new CategoryRepository(_fixture.Context.CategoriesContain(categories));

            var category = categoryRepository.GetBy(1);
            category.Description = "Campaign";
            categoryRepository.Update(category);
            var result = categoryRepository.GetBy(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("Campaign", category.Description);
        }
    }
}