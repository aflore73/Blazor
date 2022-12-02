using System.Reflection;
using System.Data.SqlClient;
using Dapper;

namespace WebApplication1
{
    public static class Utilities
    {
        //public static void ExecuteQuery<T>(string query, object parameters = null)
        //{
        //    string stringconnection = "Data Source=AFLORENTIN;Initial Catalog=PruebaNet;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //    SqlConnection cnn = new SqlConnection(stringconnection);
        //    Type type = typeof(T);
        //    PropertyInfo[] propertyInfos = type.GetProperties();

        //    int Id = cnn.ExecuteScalar<int>(query, parameters, commandType: System.Data.CommandType.Text);
        //    foreach (PropertyInfo propertyInfo in propertyInfos)
        //    {
        //        var property = propertyInfo.GetGetMethod();
        //        var valor = propertyInfo.GetValue(parameters, null);

        //        //var isclass = (valor.GetType().GetConstructor(new Type[0]) != null);
        //        //if (isclass)
        //        //{
        //        //    IEnumerable<string> query = Enumerable<valor.GetType()>().OrderBy(fruit => fruit).Select(fruit => fruit);
        //        //    var code = Enumerable.Cast<valor.GetType()>();
        //        //    RecorrerT<code>(valor);
        //        //}
        //        //else
        //        Console.WriteLine("PropertyName: {0}, Valor: {1}", property.Name, valor);
        //    }
         
        //}
    }
    
}
