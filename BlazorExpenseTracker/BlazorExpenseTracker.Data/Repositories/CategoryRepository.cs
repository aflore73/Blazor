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
        
        public Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var sql = @"select Id, Name from Categories";
            return await sqlconfiguration.ReturnClass<Category>(sql);
        }
        public async Task<Category> GetCategoryDetails(int id)
        {
            Category category = new Category() { Id = id};
            var properties = UtilitiesGenericClass.ParseProperties(category);
            var sqlPairs = UtilitiesGenericClass.GetSqlPairs(properties.IdNames, " AND ");
            var sql = string.Format("SELECT * FROM [{0}] WHERE {1}", "categories", sqlPairs);
            return await sqlconfiguration.GetItem<Category>(sql, properties.AllPairs);
        }
        public async Task<bool> Update<T>(T obj)
        {
            var propertyContainer = UtilitiesGenericClass.ParseProperties(obj);
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.ValueNames);
            var sql = string.Format("UPDATE [{0}] SET {1} WHERE {2}", typeof(T).Name, sqlValuePairs, sqlIdPairs);
            return await sqlconfiguration.Execute(sql, propertyContainer.ValuePairs);
        }

        public async Task<bool> Delete<T>(T obj)
        {
            var propertyContainer = UtilitiesGenericClass.ParseProperties(obj);
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("DELETE FROM [{0}] WHERE { 1}", typeof(T).Name, sqlIdPairs);
            return await sqlconfiguration.Execute(sql, propertyContainer.IdPairs);
        }
        //public async Task<IEnumerable<Category>> GetCategoryDetails(int id)
        //{    
        //    var sql = @"select Id, Name from Categories where Id = @id";
        //    return await sqlconfiguration.ReturnClass<Category>(sql, id);
        //}

        public async Task<bool>InsertCategory(Category category)
        {
            var propertyContainer = UtilitiesGenericClass.ParseProperties<Category>(category);
            //
            var sqlIdPairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = UtilitiesGenericClass.GetSqlPairs(propertyContainer.ValueNames);
            //
            var sql = string.Format("Insert Into [{0}] ({1}) Values(@{2})", "categories",
                string.Join(", ", propertyContainer.ValueNames),
                string.Join(",@", propertyContainer.ValueNames)) + " SELECT SCOPE_IDENTITY()";

            return await sqlconfiguration.Execute(sql, propertyContainer.ValuePairs);
            
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var sql = @"Update Categories Set Name = " + category.Name.ToString() + " Where Id = " + category.Id.ToString();
            return await sqlconfiguration.Execute(sql);
        }

    }
}

