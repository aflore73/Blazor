using BlazorExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>>GetExpenses();
        Task<Expense> GetExpenseDetails(int id);
        Task<bool> InsertExpenseDetails(Expense expense);
        Task<bool> UpdateExpense(Expense expense);
        Task<bool> DeleteExpense<T>(Expense expense);
    }
}
