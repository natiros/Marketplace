using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Repository.Interfaces
{
    public interface ICompraDao
    {
        long Insertar(Compra compra);
        IEnumerable<Compra> BuscarCompras(int idUsuario);
    }
}
