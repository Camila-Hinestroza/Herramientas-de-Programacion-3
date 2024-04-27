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
    public class RoleService : IRoleService
    {
        // 1- Instancia al contexto => Para conectarme a BD
        private readonly PazSalvoContext _context;
        public RoleService(PazSalvoContext context)
        {
            _context = context;
        }


        public async Task<bool> Actualizar(Role model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result


            int RoleId = model.Id;


            if (RoleIdId == 0 || RoleIdId == null) return result;


            try
            {
                Role role = await Leer(roleId);


                role.Nombre = model.Nombre;
                role.Descripcion = model.Descripcion;
                role.Activo  = model.Activo ;


                _context.Role.Update(role); // Actualización de la factura en el contexto
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


        public async Task<bool> Insertar(Role model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result


            try
            {
                _context.Role.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos


                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }


        public async Task<Role> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null


            Role role = await _context.Role.FirstOrDefaultAsync(f => f.Id == id);  // Buscar la factura por su ID


            if (role == null) return null; // Si la factura no se encontró, devolver null


            return role; // Devolver la factura encontrada
        }


        public async Task<IQueryable<Role>> LeerTodos()
        {
            IQueryable<Role> listaDeRole = _context.Role; // Obtener todas las facturas del contexto


            return listaDeRole; // Devolver la lista de facturas
        }
    }
}
