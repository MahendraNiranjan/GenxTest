using GenxAPI.Data;
using GenxAPI.Model;
using GenxAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GenxAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<Category> GetAllCategory()
        {
            return dbContext.Category.OrderBy(c => c.CategoryName).ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return dbContext.Category.FirstOrDefault(a => a.CategoryId == categoryId);
        }
        public bool CategoryExists(string categoryName)
        {
            return dbContext.Category.Any(a => a.CategoryName.ToLower().Trim() == categoryName.ToLower().Trim());
        }

        public bool CreateCategory(Category category)
        {
            dbContext.Category.Add(category);
            return Save();
        }

        public bool CategoryExists(int categoryId)
        {
           return dbContext.Category.Any(c => c.CategoryId == categoryId);
        }

        public bool UpdateCategory(Category category)
        {
            dbContext.Category.Update(category);
            return Save();
        }
        public bool DeleteCategory(Category category)
        {
            dbContext.Category.Remove(category);
            return Save();
        }  

        public bool Save()
        {
            return dbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}
