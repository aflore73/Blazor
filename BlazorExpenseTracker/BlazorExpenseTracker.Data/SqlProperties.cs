using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorExpenseTracker.Data
{
    public class SqlProperties
    {
        public string SqlConnectionString { get; }
        public SqlProperties(string cnn) 
        {
            SqlConnectionString = cnn;  
        }
    }
}
