using PazYSalvoAPP.Models;

namespace PazYSalvoAPP.WebApp.ViewModels.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public DateTime? FechaDeCreacion { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}