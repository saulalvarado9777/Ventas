using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public Cliente OCliente { get; set; }
        public int TotalProducto { get; set; }
        public decimal Total { get; set; }
        public string Contacto { get; set; }
        public Localidad OLocalidad { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string IdTransaccion { get; set; }

        public List<DetalleVenta> ODetalleVenta { get; set; }
    }
}
