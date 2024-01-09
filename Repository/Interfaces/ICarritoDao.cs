using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Repository.Interfaces
{
    internal interface ICarritoDao
    {
        void Insertar(Carrito carrito);
        void Eliminar(long id);
        IEnumerable<Carrito> Buscar(int idUsuario);

        void VaciarCarrito(int idUsuario);
    }
}
