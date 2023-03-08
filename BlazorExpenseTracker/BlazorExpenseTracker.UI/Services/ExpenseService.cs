using System.Net.Http;
using System.Text.Json;
using System.Text;
using BlazorExpenseTracker.Model;
using BlazorExpenseTracker.UI.Interfaces;

namespace BlazorExpenseTracker.UI.Services
{
    public class ExpenseService: IExpenseService
    {
        private readonly HttpClient _httpclient;

        public ExpenseService(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<IEnumerable<dynamic>> GetAllExpenses()
        {
            //Get Method que se corresponde con el Method [HttpGet] de CategoryController sin parámetros
            //La respuesta de la API no es exactamente el modelo que envíamos, sino que es un HTTP Response en formato Json
            var response = await _httpclient.GetStreamAsync($"api/Expense");
            IEnumerable<dynamic>? enumerable = JsonSerializer.Deserialize<IEnumerable<dynamic>>(response,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return enumerable.ToList();
        }
        public async Task DeleteExpense(int id)
        {
            await _httpclient.DeleteAsync($"api/Expense/{id}");
        }
        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Expense>>(
                await _httpclient.GetStreamAsync($"api/Expense"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Expense> GetExpenseDetails(int id)
        {
            return JsonSerializer.Deserialize<Expense>(await _httpclient.GetStreamAsync($"api/Expense/{id}"),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task SaveExpense(Expense expense)
        {
            //En este método es  la inversa, debemos Serializar el objeto para realizar el POS
            var serializercat = new StringContent(JsonSerializer.Serialize(expense), Encoding.UTF8, "application/json");
            //Sí el Id es cero -> es un Alta, sino un Update
            if (expense.Id == 0)
            {
                //Con JsonAsync es posible que no sea necesario la Serialización
                //task = _httpclient.PostAsJsonAsync($"api/category",category); 
                await _httpclient.PostAsync($"api/Expense", serializercat);
            }
            else
                await _httpclient.PutAsJsonAsync($"api/Expense", expense);
        }

    }
}
