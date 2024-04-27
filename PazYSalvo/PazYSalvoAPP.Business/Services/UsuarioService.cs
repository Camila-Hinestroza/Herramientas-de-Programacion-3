using Microsoft.EntityFrameworkCore;
using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PazYSalvoAPP.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        // 1- Instancia al contexto => Para conectarme a BD
        private readonly PazSalvoContext _context;
        public UsuarioService(PazSalvoContext context)
        {
            _context = context;
        }


        public async Task<bool> Actualizar(Usuario model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result


            int usuarioId = model.Id;


            if (usuarioIdId == 0 || usuarioIdId == null) return result;


            try
            {
                Usuario Usuario = await Leer(usuarioId);


                Usuario.NombreUsuario = model.NombreUsuario;
                Usuario.Contraseña = model.Contraseña;


                _context.Usuario.Update(usuario); // Actualización de la factura en el contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos


                return !result; // Devolver el valor inverso de result (true si se actualizó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }


           
        }


        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> Insertar(Usuario model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result


            try
            {
                _context.Usuario.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos


                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }


        public async Task<Usuario> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null


            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(f => f.Id == id);  // Buscar la factura por su ID


            if (usuario == null) return null; // Si la factura no se encontró, devolver null


            return usuario; // Devolver la factura encontrada
        }


        public async Task<IQueryable<Usuario>> LeerTodos()
        {
            IQueryable<Usuario> listaDeUsuario = _context.Usuario; // Obtener todas las facturas del contexto


            return listaDeUsuario; // Devolver la lista de facturas
        }
    }
}
