using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Xsl;
using System.Linq.Expressions;
using System.Data.Common;

namespace BlazorExpenseTracker.Data
{
    public class SqlConfiguration : ISqlConfiguration
    {
        private SqlProperties _sqlproperties;
        //private SqlCommand sqlcommand;
        public SqlConfiguration(SqlProperties properties)
        {
            _sqlproperties = properties;
        }
        public string SqlConnectionString { get => _sqlproperties.SqlConnectionString; }
        public SqlConnection dbConnection()
        {
            return new SqlConnection(_sqlproperties.SqlConnectionString);
        }
        public async Task<bool> Execute(string query)
        {
            var sqlconnection = dbConnection();
            var task = sqlconnection.ExecuteAsync(query);
            int result = await task;
            return result > 0;
        }

        public async Task<bool>Execute(string query, object parameters = null)
        {
            int count = 0;
            try
            {
                var sqlconnection = dbConnection();
                count = await sqlconnection.ExecuteScalarAsync<int>(query, parameters, commandType: System.Data.CommandType.Text);
            }
            catch (SqlException ex)
            {
                
            }
           
            return count > 0;
        }
        public async Task<T> GetItem<T>(string querycommand, object parameters = null)
        {
            Task<T> clsreturn;
            var sqlconnection = dbConnection();
            if (parameters != null)
            {
                clsreturn = sqlconnection.QueryFirstOrDefaultAsync<T>(querycommand, parameters);
            }
            else
                clsreturn = sqlconnection.QueryFirstOrDefaultAsync<T>(querycommand);
            return await clsreturn;
        }

        public async Task<IEnumerable<T>> ReturnClass<T>(string querycommand, params object[] args) where T : class
        {
            Task<IEnumerable<T>> clsreturn;
            var sqlconnection = dbConnection();
            if (args.Length > 0)
            {
                clsreturn = sqlconnection.QueryAsync<T>(querycommand, args);
            }
            else
                clsreturn = sqlconnection.QueryAsync<T>(querycommand);
            return await clsreturn;
        }
        Task<IEnumerable<TReturn>> ISqlConfiguration.ReturnMultiClass<TFirst, TSecond, TReturn>(string querycommand, Func<TFirst, TSecond, TReturn> args)
        {
            var sqlconnection = dbConnection();
            
            return sqlconnection.QueryAsync(querycommand, args);
            
        }

        public async Task<DbDataReader> ReturnObjectList(string querycommand)
        {
            var sqlconnection = dbConnection();

            return await sqlconnection.ExecuteReaderAsync(querycommand);
        }

    }
}
