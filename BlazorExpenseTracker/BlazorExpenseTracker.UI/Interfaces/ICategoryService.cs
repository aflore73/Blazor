using BlazorExpenseTracker.Model;

namespace BlazorExpenseTracker.UI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Categories>> GetAllCategories();
        Task<Categories> GetCategoryDetails(int id);
        Task SaveCategory(Categories category);
        //El servicio determinará cuando es un Update o un Insert
        //Task<bool> UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}
