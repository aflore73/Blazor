using BlazorExpenseTracker.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data
{
    public class ExpenseRepository : IExpenseRepository
    {
        private ISqlConfiguration sqlconfiguration { get; set; }

        public ExpenseRepository(ISqlConfiguration connectionString)
        {
            sqlconfiguration = connectionString;
        }


        public async Task<bool> DeleteExpense<T>(Expense expense)
        {
            var propertyContainer = UtilitiesGenericClass.ParseProperties(expense);
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("DELETE FROM [{0}] WHERE {1}", typeof(T).Name, sqlIdPairs);
            return await sqlconfiguration.Execute(sql, propertyContainer.IdPairs);
        }

        public async Task<DbDataReader> GetAllExpenses()
        {
            var sql = @"Select e.*, c.Id Category_Id, c.Name Category_Name from Expense e Inner Join categories c On(c.Id = e.Id)";
            return await sqlconfiguration.ReturnObjectList(sql);
        }

        public async Task<bool> InsertExpenseDetails(Expense expense)
        {
            var propertyContainer = UtilitiesGenericClass.ParseProperties<Expense>(expense);
            //
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.ValueNames);
            //
            var sql = string.Format("Insert Into [{0}] ({1}) Values(@{2})", "Expense",
                string.Join(", ", propertyContainer.ValueNames),
                string.Join(",@", propertyContainer.ValueNames)) + " SELECT SCOPE_IDENTITY()";

            return await sqlconfiguration.Execute(sql, propertyContainer.ValuePairs);
        }

        public async Task<bool> UpdateExpense(Expense expense)
        {
            var propertyContainer = UtilitiesGenericClass.ParseProperties(expense);
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.ValueNames);
            var sql = string.Format("UPDATE [{0}] SET {1} WHERE {2}", "Expense", sqlValuePairs, sqlIdPairs);
            return await sqlconfiguration.Execute(sql, propertyContainer.AllPairs);
        }

        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            SqlConnection db = sqlconfiguration.dbConnection();

            var sql = @"
                        SELECT e.Id, e.Amount, e.CategoryId, e.ExpenseType, e.TransactionDate, 
                               c.Id, c.Name
                        FROM Expense e
                            INNER JOIN Categories c ON e.CategoryId = c.Id ";

            var result = await db.QueryAsync<Expense, Categories, Expense>(sql,
                ((expense, category) =>
                {
                    expense.Category = category;
                    return expense;
                }), new { }, splitOn: "Id");
            //SplitOn es la clave que Dupper utilzará para separar los objetos

            return result;
        }
        public async Task<Expense> GetExpenseDetails(int id)
        {
            Categories category = new Categories() { Id = id };
            var properties = UtilitiesGenericClass.ParseProperties(category);
            var sqlPairs = UtilitiesGenericClass.GetSqlPairs(properties.IdNames, " AND ");
            var sql = string.Format("SELECT * FROM [{0}] WHERE {1}", "Expense", sqlPairs);
            return await sqlconfiguration.GetItem<Expense>(sql, properties.AllPairs);
        }
    }
}
