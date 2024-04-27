using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PazYSalvoAPP.WebApp.Controllers.Clientes
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            IQueryable<Cliente>? consultaDeClientes = await _clienteService.LeerTodos();

            List<ClienteViewModel> listadoDeClientes = consultaDeClientes.Select(c => new ClienteViewModel
            {
                Id = c.Id,
                PersonaId = c.PersonaId,
                RolId = c.RolId,
                FechaDeCreacion = c.FechaDeCreacion,
                Persona = c.Persona,
                Rol = c.Rol
            }).ToList();

            return PartialView("_ListadoDeCliente", listadoDeClientes);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarCliente([FromBody] ClienteViewModel model)
        {
            Cliente cliente = new Cliente()
            {
                Persona = model.Persona,
                Rol = model.Rol
            };

            bool response = await _clienteService.Insertar(cliente);

            if (response)
            {
                return Json(new { success = true, message = "Cliente agregado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar el cliente" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCliente([FromBody] ClienteViewModel model)
        {
            Cliente cliente = await _clienteService.Leer(model.Id);
            if (cliente == null)
            {
                return Json(new { success = false, message = "Cliente no encontrado" });
            }

            cliente.PersonaId = model.PersonaId;
            cliente.RolId = model.RolId;
            cliente.FechaDeCreacion = model.FechaDeCreacion;
            cliente.Persona = model.Persona;
            cliente.Rol = model.Rol;

            bool response = await _clienteService.Actualizar(cliente);

            if (response)
            {
                return Json(new { success = true, message = "Cliente actualizado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al actualizar el cliente" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarCliente(int id)
        {
            Cliente cliente = await _clienteService.Leer(id);
            if (cliente == null)
            {
                TempData["ErrorMessage"] = "Cliente no encontrado";
                return RedirectToAction("Index");
            }

            ClienteViewModel clienteviewModel = new ClienteViewModel
            {
                Id = cliente.Id,
                PersonaId = cliente.PersonaId,
                RolId = cliente.RolId,
                FechaDeCreacion = cliente.FechaDeCreacion,
                Persona = cliente.Persona,
                Rol = cliente.Rol
            };

            return View("ClienteEstado", clienteviewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            Cliente cliente = await _clienteService.Leer(id);
            if (cliente == null)
            {
                TempData["ErrorMessage"] = "Cliente no encontrado";
                return RedirectToAction("Index");
            }
            
            bool response = await _clienteService.Eliminar(cliente);

            if (response)
            {
                TempData["SuccessMessage"] = "Cliente eliminado con éxito";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el cliente";
            }
            
            return RedirectToAction("Index");
            
        }
    }
}    
