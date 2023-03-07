using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorExpenseTracker.Data
{
    public class SqlProperties
    {
        public string SqlConnectionString { get; }
        //En Startup se declara el servicio como Singleton:
        //var sqlporperties = new SqlProperties(Configuration.GetConnectionString("SqlConnection"));
        //services.AddSingleton(sqlporperties);
        public SqlProperties(string cnn) 
        {
            SqlConnectionString = cnn;  
        }
    }
}
