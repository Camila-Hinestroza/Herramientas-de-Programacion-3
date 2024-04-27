using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PazYSalvoAPP.WebApp.Controllers.Pagos
{
    public class PagoController : Controller
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarPagos()
        {
            IQueryable<Pago>? consultaDePagos = await _pagoService.LeerTodos();

            List<PagoViewModel> listadoDePagos = consultaDePagos.Select(p => new PagoViewModel
            {
                Id = p.Id,
                MontoDePago = p.MontoDePago,
                FacturaId = p.FacturaId,
                Activo = p.Activo,
                FechaDeCreacion = p.FechaDeCreacion,
                Factura = p.Factura
            }).ToList();

            return PartialView("_ListadoDePagos", listadoDePagos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPago([FromBody] PagoViewModel model)
        {
            Pago pago = new Pago()
            {
                MontoDePago = model.MontoDePago,
                FacturaId = model.FacturaId,
                Activo = model.Activo,
                Factura = model.Factura
            };

            bool response = await _pagoService.Insertar(pago);

            if (response)
            {
                return Json(new { success = true, message = "Pago agregado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar el pago" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarPago([FromBody] PagoViewModel model)
        {
            Pago pago = await _pagoService.Leer(model.Id);
            if (pago == null)
            {
                return Json(new { success = false, message = "Pago no encontrado" });
            }

            pago.MontoDePago = model.MontoDePago;
            pago.FacturaId = model.FacturaId;
            pago.Activo = model.Activo;
            pago.Factura = model.Factura;

            bool response = await _pagoService.Actualizar(pago);

            if (response)
            {
                return Json(new { success = true, message = "Pago actualizado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al actualizar el pago" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarPago(int id)
        {
            Pago pago = await _pagoService.Leer(id);
            if (pago == null)
            {
                TempData["ErrorMessage"] = "Pago no encontrado";
                return RedirectToAction("Index");
            }

            PagoViewModel pagoviewModel = new PagoViewModel
            {
                Id = pago.Id,
                MontoDePago = pago.MontoDePago,
                FacturaId = pago.FacturaId,
                Activo = pago.Activo,
                FechaDeCreacion = pago.FechaDeCreacion,
                Factura = pago.Factura
            };

            return View("EditarPago", pagoviewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarPago(int id)
        {
            Pago pago = await _pagoService.Leer(id);
            if (pago == null)
            {
                TempData["ErrorMessage"] = "Pago no encontrado";
                return RedirectToAction("Index");
            }

            bool response = await _pagoService.Eliminar(pago);

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


