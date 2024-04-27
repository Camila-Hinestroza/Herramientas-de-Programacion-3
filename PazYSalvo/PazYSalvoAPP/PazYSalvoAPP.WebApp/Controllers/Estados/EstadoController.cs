using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;

namespace PazYSalvoAPP.WebApp.Controllers.Estados
{
    public class EstadoController : Controller
    {
        private readonly IEstadoService _estadoService;
        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarEstados()
        {
            IQueryable<Estado>? consultaDeEstados = await _estadoService.LeerTodos();

            List<EstadoViewModel> listadoDeEstados = consultaDeEstados.Select(e => new EstadoViewModel
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
            }).ToList();

            return PartialView("_ListadoDeEstado", listadoDeEstados);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEstado([FromBody] EstadoViewModel model)
        {
            Estado estado = new Estado()
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
            };

            bool response = await _estadoService.Insertar(estado);

            if (response)
            {
                return Json(new { success = true, message = "Estado agregado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar el estado" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarEstado([FromBody] EstadoViewModel model)
        {
            Estado estado = await _estadoService.Leer(model.Id);
            if (estado == null)
            {
                return Json(new { success = false, message = "Estado no encontrado" });
            }

            estado.Nombre = model.Nombre;
            estado.Descripcion = model.Descripcion;

            bool response = await _estadoService.Actualizar(estado);

            if (response)
            {
                return Json(new { success = true, message = "Estado actualizado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al actualizar el estado" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarEstado(int id)
        {
            Estado estado = await _estadoService.Leer(id);
            if (estado == null)
            {
                TempData["ErrorMessage"] = "Estado no encontrado";
                return RedirectToAction("Index");
            }

            EstadoViewModel estadoViewModel = new EstadoViewModel
            {
                Id = estado.Id,
                Nombre = estado.Nombre,
                Descripcion = estado.Descripcion,
            };

            return View("EditarEstado", estadoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarEstado(int id)
        {
            Estado estado = await _estadoService.Leer(id);
            if (estado == null)
            {
                TempData["ErrorMessage"] = "Estado no encontrado";
                return RedirectToAction("Index");
            }

            bool response = await _estadoService.Eliminar(estado);

            if (response)
            {
                TempData["SuccessMessage"] = "Estado eliminado con éxito";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el estado";
            }

            return RedirectToAction("Index");
        }
    }
}