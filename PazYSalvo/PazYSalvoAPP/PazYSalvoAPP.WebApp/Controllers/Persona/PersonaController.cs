using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PazYSalvoAPP.WebApp.Controllers.Personas
{
    public class PersonaController : Controller
    {
        private readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarPersonas()
        {
            IQueryable<Persona>? consultaDePersonas = await _personaService.LeerTodos();

            List<PersonaViewModel> listadoDePersonas = consultaDePersonas.Select(p => new PersonaViewModel
            {
                Id = p.Id,
                Nombres = p.Nombres,
                Telefono = p.Telefono,
                CorreoElectronico = p.CorreoElectronico,
                DocumentoIdentificacion = p.DocumentoIdentificacion,
                FechaDeCreacion = p.FechaDeCreacion
            }).ToList();

            return PartialView("_ListadoDePersonas", listadoDePersonas);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPersona([FromBody] PersonaViewModel model)
        {
            Persona persona = new Persona()
            {
                Nombres = model.Nombres,
                Telefono = model.Telefono,
                CorreoElectronico = model.CorreoElectronico,
                DocumentoIdentificacion = model.DocumentoIdentificacion
            };

            bool response = await _personaService.Insertar(persona);

            if (response)
            {
                return Json(new { success = true, message = "Persona agregada con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar la persona" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarPersona([FromBody] PersonaViewModel model)
        {
            Persona persona = await _personaService.Leer(model.Id);
            if (persona == null)
            {
                return Json(new { success = false, message = "Persona no encontrada" });
            }

            persona.Nombres = model.Nombres;
            persona.Telefono = model.Telefono;
            persona.CorreoElectronico = model.CorreoElectronico;
            persona.DocumentoIdentificacion = model.DocumentoIdentificacion;

            bool response = await _personaService.Actualizar(persona);

            if (response)
            {
                return Json(new { success = true, message = "Persona actualizada con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al actualizar la persona" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarPersona(int id)
        {
            Persona persona = await _personaService.Leer(id);
            if (persona == null)
            {
                TempData["ErrorMessage"] = "Persona no encontrada";
                return RedirectToAction("Index");
            }

            PersonaViewModel personaviewModel = new PersonaViewModel
            {
                Id = persona.Id,
                Nombres = persona.Nombres,
                Telefono = persona.Telefono,
                CorreoElectronico = persona.CorreoElectronico,
                DocumentoIdentificacion = persona.DocumentoIdentificacion,
                FechaDeCreacion = persona.FechaDeCreacion
            };

            return View("EditarPersona", personaviewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarPersona(int id)
        {
            Persona persona = await _personaService.Leer(id);
            if (persona == null)
            {
                TempData["ErrorMessage"] = "Persona no encontrada";
                return RedirectToAction("Index");
            }

            bool response = await _personaService.Eliminar(persona);

            if (response)
            {
                TempData["SuccessMessage"] = "Persona eliminada con éxito";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar la persona";
            }

            return RedirectToAction("Index");
        }
    }
}