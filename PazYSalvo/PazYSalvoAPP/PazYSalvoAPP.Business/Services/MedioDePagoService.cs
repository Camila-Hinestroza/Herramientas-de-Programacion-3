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
    public class MediosDePagoService : IMediosDePagoService
    {
        // 1- Instancia al contexto => Para conectarme a BD
        private readonly PazSalvoContext _context;
        public MediosDePagoService(PazSalvoContext context)
        {
            _context = context;
        }


        public async Task<bool> Actualizar(MediosDePago model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result


            int mediosDePagoId = model.Id;


            if (mediosDePagoIdId == 0 || mediosDePagoIdId == null) return result;


            try
            {
                MediosDePago mediosDePago = await Leer(MediosDePagoId);


                mediosDePago.Nombre = model.Nombre;
                mediosDePago.Descripcion = model.Rol;


                _context.MediosDePago.Update(mediosDePago); // Actualización de la factura en el contexto
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


        public async Task<bool> Insertar(MediosDePago model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result


            try
            {
                _context.MediosDePago.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos


                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }


        public async Task<MediosDePago> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null


            MediosDePago mediosDePago = await _context.MediosDePago.FirstOrDefaultAsync(f => f.Id == id);  // Buscar la factura por su ID


            if (mediosDePago == null) return null; // Si la factura no se encontró, devolver null


            return mediosDePago; // Devolver la factura encontrada
        }


        public async Task<IQueryable<MediosDePago>> LeerTodos()
        {
            IQueryable<MediosDePago> listaDeMediosDePago = _context.MediosDePago; // Obtener todas las facturas del contexto


            return listaDeMediosDePago; // Devolver la lista de facturas
        }
    }
}
