using Marketplace.Entities;
using Marketplace.Repository.Dapper;
using Marketplace.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Servicios
{
    public class ServicioOferta
    {
        private OfertaDao ofertaDao;
        private DetalleOfertaDao detalleOfertaDao;
        private ProductoDao productoDao;
        public ServicioOferta()
        {
            ofertaDao = new OfertaDao();
            detalleOfertaDao = new DetalleOfertaDao();
            productoDao = new ProductoDao();
        }
        public void Guardar(Oferta oferta, List<DetalleOferta> detalleOfertas)
        {
            try
            {
                var id = ofertaDao.Insertar(oferta);   

                foreach(var detalle in detalleOfertas)
                {
                    detalle.IdOferta = id;

                    detalleOfertaDao.Insertar(detalle);
                }
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al guardar los datos de la oferta: ", ex);
            }
        }

        public List<Oferta> Buscar()
        {
            try
            {
                var ofertas = (List<Oferta>)ofertaDao.BuscarOfertas();

                foreach(var oferta in ofertas)
                {
                    oferta.DetalleOfertas = (List<DetalleOferta>)detalleOfertaDao.BuscarDetalleOfertas(oferta.Id);
                    foreach(var det in oferta.DetalleOfertas)
                    {
                        det.producto = productoDao.BuscarPorId(det.IdProducto);
                    }
                }

                return ofertas;
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al buscar ofertas: ", ex);
            }
        }

        public void Cancelar(int idOferta)
        {
            try
            {
                ofertaDao.Cancelar(idOferta);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al cancelar oferta: ", ex);
            }
        }

        public List<Oferta> BuscarTodasOfertas()
        {
            try
            {
                var ofertas = (List<Oferta>)ofertaDao.BuscarTodasOfertas();

                foreach (var oferta in ofertas)
                {
                    oferta.DetalleOfertas = (List<DetalleOferta>)detalleOfertaDao.BuscarDetalleOfertas(oferta.Id);
                    foreach (var det in oferta.DetalleOfertas)
                    {
                        det.producto = productoDao.BuscarPorId(det.IdProducto);
                    }
                }

                return ofertas;
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al buscar ofertas: ", ex);
            }
        }
    }
}
