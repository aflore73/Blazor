using BlazorExpenseTracker.Data;
using BlazorExpenseTracker.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : Controller
    {
        //declaramos una Interface del modelo Datos
        readonly IExpenseRepository _iexpense;

        //El construcctor recibe una instancia de la Interface ICategoryRepository
        public ExpenseController(IExpenseRepository iexpense)
        {
            _iexpense = iexpense;
        }
        [HttpGet]
        public async Task<IActionResult> GetExpense()
        {
            return Ok(await _iexpense.GetExpenses());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseDetails(int id)
        {
            return Ok(await _iexpense.GetExpenseDetails(id));
        }

        //[FromBody]: Como es un método POST, le decimos que desde el cuerpo de ese POST obtenga el modelo
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
        {
            if (expense == null)
            {
                //BadRequest-> return la página 404
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                //Devuelvo´más info específica del error en los datos, ya que ModelState conoce xq es Inválido
                //y además sobre que datos está el error. Es un manejo abstracto y encapsulamiento del objeto que recibo
                return BadRequest(ModelState);
            }
            var created = await _iexpense.InsertExpenseDetails(expense);
            //
            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpense([FromBody] Expense expense)
        {
            if (expense == null)
            {
                //BadRequest-> return la página 404
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                //Devuelvo´más info específica del error en los datos, ya que ModelState conoce xq es Inválido
                //y además sobre que datos está el error. Es un manejo abstracto y encapsulamiento del objeto que recibo
                return BadRequest(ModelState);
            }
            await _iexpense.UpdateExpense(expense);
            //
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            if (id == 0)
            {
                //BadRequest-> return la página 404
                return BadRequest();
            }
            Expense exp = new Expense() { Id = id };
            var created = await _iexpense.DeleteExpense<Expense>(exp);
            //
            return Created("deleted", created);
        }
    }
}
