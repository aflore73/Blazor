using BlazorExpenseTracker.Model;

namespace BlazorExpenseTracker.UI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryDetails(int id);
        Task SaveCategory(Category category);
        //El servicio determinará cuando es un Update o un Insert
        //Task<bool> UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}
