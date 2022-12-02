using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorExpenseTracker.Model;


namespace BlazorExpenseTracker.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryDetails(int id);
        Task<bool>InsertCategory(Category category);
        Task<bool>UpdateCategory(Category category);
        Task<bool>DeleteCategory(int id);
    }
}
