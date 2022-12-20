using BlazorExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WebApplication1;

namespace BlazorExpenseTracker.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private ISqlConfiguration sqlconfiguration { get; set; }
        public CategoryRepository(ISqlConfiguration _sqlconfiguration)
        {
            sqlconfiguration = _sqlconfiguration;
        }
        
        public async Task<bool> DeleteCategory(int id)
        {
            Categories category = new Categories() { Id = id };
            var propertyContainer = UtilitiesGenericClass.ParseProperties(category);
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("DELETE FROM [{0}] WHERE {1}", "categories", sqlIdPairs);
            return await sqlconfiguration.Execute(sql, propertyContainer.IdPairs);
        }

        public async Task<IEnumerable<Categories>> GetAllCategories()
        {
            var sql = @"select Id, Name from Categories";
            return await sqlconfiguration.ReturnClass<Categories>(sql);
        }
        public async Task<Categories> GetCategoryDetails(int id)
        {
            Categories category = new Categories() { Id = id};
            var properties = UtilitiesGenericClass.ParseProperties(category);
            var sqlPairs = UtilitiesGenericClass.GetSqlPairs(properties.IdNames, " AND ");
            var sql = string.Format("SELECT * FROM [{0}] WHERE {1}", "categories", sqlPairs);
            return await sqlconfiguration.GetItem<Categories>(sql, properties.AllPairs);
        }
        public async Task<bool> UpdateCategory(Categories category)
        {
            var propertyContainer = UtilitiesGenericClass.ParseProperties(category);
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.ValueNames);
            var sql = string.Format("UPDATE [{0}] SET {1} WHERE {2}", "categories", sqlValuePairs, sqlIdPairs);
            return await sqlconfiguration.Execute(sql, propertyContainer.AllPairs);
        }

        public async Task<bool> Delete<T>(T obj)
        {
            var propertyContainer = UtilitiesGenericClass.ParseProperties(obj);
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("DELETE FROM [{0}] WHERE {1}", typeof(T).Name, sqlIdPairs);
            return await sqlconfiguration.Execute(sql, propertyContainer.IdPairs);
            //db.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
        }
        //public async Task<IEnumerable<Category>> GetCategoryDetails(int id)
        //{    
        //    var sql = @"select Id, Name from Categories where Id = @id";
        //    return await sqlconfiguration.ReturnClass<Category>(sql, id);
        //}

        public async Task<bool>InsertCategory(Categories category)
        {
            var propertyContainer = UtilitiesGenericClass.ParseProperties<Categories>(category);
            //
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.ValueNames);
            //
            var sql = string.Format("Insert Into [{0}] ({1}) Values(@{2})", "categories",
                string.Join(", ", propertyContainer.ValueNames),
                string.Join(",@", propertyContainer.ValueNames)) + " SELECT SCOPE_IDENTITY()";

            return await sqlconfiguration.Execute(sql, propertyContainer.ValuePairs);
            
        }

        //public async Task<bool> UpdateCategory(Category category)
        //{
        //    var sql = @"Update Categories Set Name = " + category.Name.ToString() + " Where Id = " + category.Id.ToString();
        //    return await sqlconfiguration.Execute(sql);
        //}

    }
}

