using GenxAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenxAPI.Repository.IRepository
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategory();
        Category GetCategory(int categoryId);
        bool CategoryExists(string name);
        bool CategoryExists(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
    }
}
