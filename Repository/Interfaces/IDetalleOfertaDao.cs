using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Repository.Interfaces
{
    internal interface IDetalleOfertaDao
    {
        void Insertar(DetalleOferta detalle);
        IEnumerable<DetalleOferta> BuscarDetalleOfertas(int idOferta);
    }
}
