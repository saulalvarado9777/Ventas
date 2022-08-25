using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    internal class Carrito
    {
        public int IdCarrito { get; set; }
        public Cliente OCliente { get; set; }
        public Producto OProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
