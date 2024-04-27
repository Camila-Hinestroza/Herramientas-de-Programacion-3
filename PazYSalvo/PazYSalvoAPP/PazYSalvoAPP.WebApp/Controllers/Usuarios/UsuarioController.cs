using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PazYSalvoAPP.WebApp.Controllers.Usuarios
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            IQueryable<Usuario>? consultaDeUsuarios = await _usuarioService.LeerTodos();

            List<UsuarioViewModel> listadoDeUsuarios = consultaDeUsuarios.Select(u => new UsuarioViewModel
            {
                Id = u.Id,
                PersonaId = u.PersonaId,
                NombreUsuario = u.NombreUsuario,
                Contrasena = u.Contrasena,
                FechaDeCreacion = u.FechaDeCreacion
            }).ToList();

            return PartialView("_ListadoDeUsuarios", listadoDeUsuarios);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarUsuario([FromBody] UsuarioViewModel model)
        {
            Usuario usuario = new Usuario()
            {
                NombreUsuario = model.NombreUsuario,
                Contrasena = model.Contrasena
            };

            bool response = await _usuarioService.Insertar(usuario);

            if (response)
            {
                return Json(new { success = true, message = "Usuario agregado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar el usuario" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarUsuario([FromBody] UsuarioViewModel model)
        {
            Usuario usuario = await _usuarioService.Leer(model.Id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Usuario no encontrado" });
            }

            usuario.NombreUsuario = model.NombreUsuario;
            usuario.Contrasena = model.Contrasena;

            bool response = await _usuarioService.Actualizar(usuario);

            if (response)
            {
                return Json(new { success = true, message = "Usuario actualizado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al actualizar el usuario" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarUsuario(int id)
        {
            Usuario usuario = await _usuarioService.Leer(id);
            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrado";
                return RedirectToAction("Index");
            }

            UsuarioViewModel usuarioviewModel = new UsuarioViewModel
            {
                Id = usuario.Id,
                PersonaId = usuario.PersonaId,
                NombreUsuario = usuario.NombreUsuario,
               Contraseña = usuario.Contrasena,
               FechaDeCreacion = usuario.FechaDeCreacion
            }

            return View("EditarUsuario", usuarioViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            Usuario usuario = await _usuarioService.Leer(id);
            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrado";
                return RedirectToAction("Index");
            }

            bool response = await _usuarioService.Eliminar(usuario);

            if (response)
            {
                TempData["SuccessMessage"] = "Usuario eliminado con éxito";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el usuario";
            }

            return RedirectToAction("Index");
        }
    }
}

