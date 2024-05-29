using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PazzYSalvoApp.Models;
using PazzYSalvoApp.Services;
// using PazzYSalvoApp.Context;

namespace PazzYSalvoApp.Controllers
{
    [Route("api/[controller]")] // <= Definir la ruta de acceso a los endpoints
    [ApiController]
    public class FacturaController : ControllerBase
    {   
        // GET: FacturaController
        // GET: api/<TarjetaCreditoController>

        private readonly FacturaService _facturaService;

        public FacturaController(FacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        // GET: api/Factura
        [HttpGet]
        public async Task<IActionResult> GetFacturas()
        {
            var facturas = await _facturaService.LeerTodos();
            return Ok(facturas);
        }

        // GET: api/Factura/n
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactura(int id)
        {
            var factura = await _facturaService.Leer(id);
            if (factura == null)
            {
                return NotFound();
            }
            return Ok(factura);
        }

        // POST: api/Factura
        [HttpPost]
        public async Task<IActionResult> CrearFactura([FromBody] Factura model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _facturaService.Insertar(model);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        // PUT: api/Factura/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarFactura(int id, [FromBody] Factura model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var result = await _facturaService.Actualizar(model);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // PATCH api/<TarjetaCreditoController>/5
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TarjetaCreditoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
