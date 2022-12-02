using BlazorExpenseTracker.Model;
using BlazorExpenseTracker.UI.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace BlazorExpenseTracker.UI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpclient;

        public CategoryService(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }
        public async Task DeleteCategory(int id)
        {
            //Request Method que se corresponde con el Method [HttpDelete("{id}")] de CategoryController
            //Solo el nombre sin la palabra Controller (MVC)  [Route("/api/[controller]")]
            await _httpclient.DeleteAsync($"api/Category/{id}");

        }

        public async Task<IEnumerable<Categories>> GetAllCategories()
        {
            //Get Method que se corresponde con el Method [HttpGet] de CategoryController sin parámetros
            //La respuesta de la API no es exactamente el modelo que envíamos, sino que es un HTTP Response en formato Json
            var response = await _httpclient.GetStreamAsync($"api/category");
           IEnumerable<Categories>? enumerable = JsonSerializer.Deserialize<IEnumerable<Categories>>(response, 
               new JsonSerializerOptions() {PropertyNameCaseInsensitive = true });
            return enumerable;
        }

        public async Task<Categories> GetCategoryDetails(int id)
        {
            return JsonSerializer.Deserialize<Categories>(await _httpclient.GetStreamAsync($"api/Category/{id}"),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task SaveCategory(Categories category)
        {
            //En este método es  la inversa, debemos Serializar el objeto para realizar el POS
            var serializercat = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
            //Sí el Id es cero -> es un Alta, sino un Update
            if (category.Id == 0)
            {
                //Con JsonAsync es posible que no sea necesario la Serialización
                //task = _httpclient.PostAsJsonAsync($"api/category",category); 
                await _httpclient.PostAsync($"api/category", serializercat);
            }
            else
                await _httpclient.PutAsJsonAsync($"api/Category", category);
        }

    }
}
