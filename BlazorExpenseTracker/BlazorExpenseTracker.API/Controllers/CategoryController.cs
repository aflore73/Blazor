using Microsoft.AspNetCore.Mvc;
using BlazorExpenseTracker.Data;
using System.Threading.Tasks;
using BlazorExpenseTracker.Model;

namespace BlazorExpenseTracker.API.Controllers
{
    //MVC es inteligente para luego reemplazar [controller] por la clase correspondiente
    //El tipo de controller será ApiController
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        //declaramos una Interface del modelo Datos
        readonly ICategoryRepository _icategory;

        //El construcctor recibe una instancia de la Interface ICategoryRepository
        public CategoryController(ICategoryRepository icategory)
        {
            _icategory = icategory;
        }

        //Implemento los métodos de la Interface
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _icategory.GetAllCategories());
        }
        //toma de la URL el parámetro id que viene como get: loclahost/GetCategoryDetails/1
        //id es 1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryDetails(int id)
        {
            return Ok(await _icategory.GetCategoryDetails(id));
        }

        //[FromBody]: Como es un método POST, le decimos que desde el cuerpo de ese POST obtenga el modelo
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Categories category)
        {
            if (category == null)
            {
                //BadRequest-> return la página 404
                return BadRequest();
            }
            if (category.Name == string.Empty)
            {
                //add Error en el modelo "Category" que estoy recibiendo por parámetro [FromBody]
                ModelState.AddModelError("Name", "Category Name shouldn't be empty");
            }
            if (!ModelState.IsValid)
            {
                //Devuelvo´más info específica del error en los datos, ya que ModelState conoce xq es Inválido
                //y además sobre que datos está el error. Es un manejo abstracto y encapsulamiento del objeto que recibo
                return BadRequest(ModelState);
            }
            var created = await _icategory.InsertCategory(category);
            //
            return Created("created", created);
        }

        /*
            La diferencia entre el método PUT y el método POST es que PUT es un método idempotente: 
            llamarlo una o más veces de forma sucesiva tiene el mismo efecto (sin efectos secundarios), 
            mientras que una sucesión de peticiones POST idénticas pueden tener efectos adicionales, 
            como envíar una orden varias veces.
            Si el elemento de destino no existe y la petición PUT lo crea de forma satisfactoria, entonces el servidor debe informar al usuario enviando una respuesta 201 (Created) .

            HTTP/1.1 201 Created
            Content-Location: /nuevo.html
            Si el elemento existe actualmente y es modificado de forma satisfactoria, entonces el servidor de origen 
            debe enviar una respuesta 200 (OK) o una respuesta 204 (en-US) (No Content) para indicar que la modificación 
            del elemento se ha realizado sin problemas.
        */
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Categories category)
        {
            if (category == null)
            {
                //BadRequest-> return la página 404
                return BadRequest();
            }
            if (category.Name.Trim() == string.Empty)
            {
                ModelState.AddModelError("Name", "Category Name shouldn't be empty");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                //Devuelvo´más info específica del error en los datos, ya que ModelState conoce xq es Inválido
                //y además sobre que datos está el error. Es un manejo abstracto y encapsulamiento del objeto que recibo
                return BadRequest(ModelState);
            }
            await _icategory.UpdateCategory(category);
            //
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id == 0)
            {
                //BadRequest-> return la página 404
                return BadRequest();
            }

            var created = await _icategory.DeleteCategory(id);
            //
            return Created("deleted", created);
        }
    }
}