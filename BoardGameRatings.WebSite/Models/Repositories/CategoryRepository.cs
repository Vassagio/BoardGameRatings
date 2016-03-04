using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category Add(Category player)
        {
            var found = GetBy(player.Description);
            if (found != null)
                return found;

            _context.Categories.Add(player);
            _context.SaveChanges();
            return player;
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public Category GetBy(int categoryId)
        {
            return _context.Categories.FirstOrDefault(g => g.Id == categoryId);
        }

        public Category GetBy(string description)
        {
            return
                _context.Categories.FirstOrDefault(
                    g => g.Description.Equals(description, StringComparison.CurrentCultureIgnoreCase));
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}