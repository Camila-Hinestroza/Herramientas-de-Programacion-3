using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PazYSalvoAPP.WebApp.Controllers.Roles
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarRoles()
        {
            IQueryable<Role>? consultaDeRoles = await _roleService.LeerTodos();

            List<RoleViewModel> listadoDeRoles = consultaDeRoles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Activo = r.Activo,
                FechaDeCreacion = r.FechaDeCreacion
            }).ToList();

            return PartialView("_ListadoDeRoles", listadoDeRoles);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarRole([FromBody] RoleViewModel model)
        {
            Role role = new Role()
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo
            };

            bool response = await _roleService.Insertar(role);

            if (response)
            {
                return Json(new { success = true, message = "Role agregado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar el role" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarRole([FromBody] RoleViewModel model)
        {
            Role role = await _roleService.Leer(model.Id);
            if (role == null)
            {
                return Json(new { success = false, message = "Role no encontrado" });
            }

            role.Nombre = model.Nombre;
            role.Descripcion = model.Descripcion;
            role.Activo = model.Activo;

            bool response = await _roleService.Actualizar(role);

            if (response)
            {
                return Json(new { success = true, message = "Role actualizado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al actualizar el role" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarRole(int id)
        {
            Role role = await _roleService.Leer(id);
            if (role == null)
            {
                TempData["ErrorMessage"] = "Role no encontrado";
                return RedirectToAction("Index");
            }

            RoleViewModel roleviewModel = new RoleViewModel
            {
                Id = role.Id,
                Nombre = role.Nombre,
                Descripcion = role.Descripcion,
                Activo = role.Activo,
                FechaDeCreacion = role.FechaDeCreacion
            };

            return View("EditarRole", roleviewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarRole(int id)
        {
            Role role = await _roleService.Leer(id);
            if (role == null)
            {
                TempData["ErrorMessage"] = "Role no encontrado";
                return RedirectToAction("Index");
            }

            bool response = await _roleService.Eliminar(role);

            if (response)
            {
                TempData["SuccessMessage"] = "Role eliminado con éxito";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el role";
            }

            return RedirectToAction("Index");
        }
    }
}