using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Repository.Interfaces
{
    public interface IDetalleCompraDao
    {
        void Insertar(DetalleCompra detalleCompra);
        IEnumerable<DetalleCompra> BuscarDetalleCompras(int idCompra);
    }
}
