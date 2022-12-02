using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorExpenseTracker.Model;


namespace BlazorExpenseTracker.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Categories>> GetAllCategories();
        Task<Categories> GetCategoryDetails(int id);
        Task<bool>InsertCategory(Categories category);
        Task<bool>UpdateCategory(Categories category);
        Task<bool>DeleteCategory(int id);
    }
}
