using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PazYSalvoAPP.WebApp.Controllers.MediosDePago
{
    public class MediosDePagoController : Controller
    {
        private readonly IMediosDePagoService _mediosDePagoService;

        public MediosDePagoController(IMediosDePagoService mediosDePagoService)
        {
            _mediosDePagoService = mediosDePagoService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarMediosDePago()
        {
            IQueryable<MediosDePago>? consultaDeMediosDePago = await _mediosDePagoService.LeerTodos();

            List<MediosDePagoViewModel> listadoDeMediosDePago = consultaDeMediosDePago.Select(m => new MediosDePagoViewModel
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Descripcion = m.Descripcion,
                FechaDeCreacion = m.FechaDeCreacion
            }).ToList();

            return PartialView("_ListadoDeMediosDePago", listadoDeMediosDePago);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarMedioDePago([FromBody] MediosDePagoViewModel model)
        {
            MediosDePago medioDePago = new MediosDePago()
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };

            bool response = await _mediosDePagoService.Insertar(medioDePago);

            if (response)
            {
                return Json(new { success = true, message = "Medio de pago agregado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar el medio de pago" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarMedioDePago([FromBody] MediosDePagoViewModel model)
        {
            MediosDePago medioDePago = await _mediosDePagoService.Leer(model.Id);
            if (medioDePago == null)
            {
                return Json(new { success = false, message = "Medio de pago no encontrado" });
            }

            medioDePago.Nombre = model.Nombre;
            medioDePago.Descripcion = model.Descripcion;

            bool response = await _mediosDePagoService.Actualizar(medioDePago);

            if (response)
            {
                return Json(new { success = true, message = "Medio de pago actualizado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al actualizar el medio de pago" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarMedioDePago(int id)
        {
            MediosDePago medioDePago = await _mediosDePagoService.Leer(id);
            if (medioDePago == null)
            {
                TempData["ErrorMessage"] = "Medio de pago no encontrado";
                return RedirectToAction("Index");
            }

            MediosDePagoViewModel mediosDePagoviewModel = new MediosDePagoViewModel
            {
                Id = medioDePago.Id,
                Nombre = medioDePago.Nombre,
                Descripcion = medioDePago.Descripcion,
                FechaDeCreacion = medioDePago.FechaDeCreacion

            };

            return View("EditarMedioDePago", medioDePagoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarMedioDePago(int id)
        {
            MediosDePago medioDePago = await _mediosDePagoService.Leer(id);
            if (medioDePago == null)
            {
                TempData["ErrorMessage"] = "Medio de pago no encontrado";
                return RedirectToAction("Index");
            }

            bool response = await _mediosDePagoService.Eliminar(medioDePago);

            if (response)
            {
                TempData["SuccessMessage"] = "Medio de pago eliminado con éxito";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el medio de pago";
            }

            return RedirectToAction("Index");
        }
    }
}