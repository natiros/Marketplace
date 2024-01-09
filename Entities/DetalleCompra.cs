using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Entities
{
    public class DetalleCompra
    {
        public long Id { get; set; }
        public long IdCompra { get; set; }
        public long IdProducto { get; set; }
        public Producto Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }     
    }
}
