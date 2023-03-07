using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data
{
    public interface ISqlConfiguration
    {
        Task<bool> Execute(string query);
        Task<bool> Execute(string query, object parameters = null);
        Task<IEnumerable<T>> ReturnClass<T>(string querycommand, params object[] args) where T : class;
        Task<IEnumerable<TReturn>> ReturnMultiClass<TFirst, TSecond, TReturn>(string querycommand, Func<TFirst, TSecond, TReturn> args);
        Task<T> GetItem<T>(string querycommand, object parameters = null);
        Task<DbDataReader> ReturnObjectList(string querycommand);
        SqlConnection dbConnection();
    }
} 