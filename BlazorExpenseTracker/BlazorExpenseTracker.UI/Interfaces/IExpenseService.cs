using BlazorExpenseTracker.Model;

namespace BlazorExpenseTracker.UI.Interfaces
{
    public interface IExpenseService
    {
        Task SaveExpense(Expense expense);
        //El servicio determinará cuando es un Update o un Insert
        //Task<bool> UpdateCategory(Category category);
        Task DeleteExpense(int id);

        Task<IEnumerable<dynamic>> GetAllExpenses();

        Task<IEnumerable<Expense>> GetExpenses();
        
        Task<Expense> GetExpenseDetails(int id);
    }
}
