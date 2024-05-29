using Microsoft.EntityFrameworkCore;
using PazzYSalvoApp.Context;
using PazzYSalvoApp.Models;
using System.Globalization;

namespace PazzYSalvoApp.Services
{
    public class FacturaService
    {
        // inyección de dependencias bd y contexto
        private readonly PazSalvoApiContext _context;

        public FacturaService(PazSalvoApiContext context)
        {
            _context = context;
        }

        public async Task<bool> Actualizar(Factura model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            int facturaId = model.Id;

            if (facturaId == 0 || facturaId == null) return result;

            try
            {
                Factura factura = await Leer(facturaId);

                decimal saldo;
                saldo = Convert.ToDecimal(model.Saldo, CultureInfo.InvariantCulture);

                factura.Saldo = saldo;
                factura.ClienteId = model.ClienteId;                
                factura.MedioDePagoId = model.MedioDePagoId;
                factura.EstadoId = model.EstadoId;

                _context.Facturas.Update(factura); // Actualización de la factura en el contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se actualizó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }

        }

        // Método para eliminar una factura de la base de datos por su ID
        public async Task<bool> Eliminar(int id)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            if (id == default(int)) return result;

            var factura = _context.Facturas.FirstOrDefault(f => f.Id == id); // Buscar la factura por su ID

            if (factura == null) return result; // Si la factura no se encontró, devolver el valor por defecto de result (false)

            try
            {
                _context.Facturas.Remove(factura); // Eliminar la factura del contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se eliminó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para insertar una nueva factura en la base de datos
        public async Task<bool> Insertar(Factura model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            try
            {
                _context.Facturas.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para leer una factura de la base de datos por su ID
        public async Task<Factura> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null

            Factura factura = await _context.Facturas.FirstOrDefaultAsync(f => f.Id == id);  // Buscar la factura por su ID

            if (factura == null) return null; // Si la factura no se encontró, devolver null

            return factura; // Devolver la factura encontrada
        }

        // Método para leer todas las facturas de la base de datos
        public async Task<IQueryable<Factura>> LeerTodos()
        {
            IQueryable<Factura> listaDeFacturas = _context.Facturas; // Obtener todas las facturas del contexto


            return listaDeFacturas; // Devolver la lista de facturas
        }
        
        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> listaDeClientes = _context.Clientes.ToList();

            return listaDeClientes;
        }

        public List<Estado> ObtenerEstados()
        {
            List<Estado> listaDeEstados = _context.Estados.ToList();

            return listaDeEstados;
        }

        public List<MediosDePago> ObtenerMediosDePago()
        {
            List<MediosDePago> listaDeMediosDePago = _context.MediosDePagos.ToList();

            return listaDeMediosDePago;
        }

    }
}
