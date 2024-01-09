using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Entities
{
    public class DetalleOferta
    {
        public int Id { get; set; }
        public long IdOferta { get; set; }
        public int IdProducto { get; set; }
        public Producto producto { get; set; }     
        public decimal PorcentajeDescuento { get; set; }
    }
}
