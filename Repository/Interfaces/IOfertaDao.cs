using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Repository.Interfaces
{
    internal interface IOfertaDao
    {
        long Insertar(Oferta oferta);
        IEnumerable<Oferta> BuscarOfertas();
        void Cancelar(int idOferta);
        IEnumerable<Oferta> BuscarTodasOfertas();
    }
}
