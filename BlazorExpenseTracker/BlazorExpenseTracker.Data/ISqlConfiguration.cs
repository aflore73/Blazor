using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data
{
    public interface ISqlConfiguration
    {
        Task<bool> Execute(string query);
        Task<bool> Execute(string query, object parameters = null);
        Task<IEnumerable<T>> ReturnClass<T>(string querycommand, params object[] args) where T : class;
        Task<T> GetItem<T>(string querycommand, object parameters = null);
    }
} 