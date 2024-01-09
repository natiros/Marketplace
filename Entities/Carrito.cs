using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Entities
{
    public class Carrito
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public Producto Producto { get; set; }
    }
}
