using Marketplace.Entities;
using Marketplace.Repository.Dapper;
using Marketplace.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Repository.Interfaces;

namespace Marketplace.Servicios
{
    public class ServicioCarrito
    {
        private CarritoDao carritoDao;
        private ProductoDao productoDao;
        public ServicioCarrito()
        {
            carritoDao = new CarritoDao();
            productoDao = new ProductoDao();    
        }
        public List<Carrito> Buscar(int idUsuario)
        {
            var carrito = new List<Carrito>();
            try
            {
                carrito = carritoDao.Buscar(idUsuario).ToList();

                foreach(var car in carrito)
                {
                    car.Producto = productoDao.BuscarPorId(car.IdProducto);
                }
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al buscar los datos del carrito: ", ex);
            }

            return carrito;
        }
        public void Guardar(Carrito carrito)
        {
            try
            {
                carritoDao.Insertar(carrito);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al guardar los datos del carrito: ", ex);
            }
        }

        public void Eliminar(long id)
        {
            try
            {
                carritoDao.Eliminar(id);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al eliminar datos del carrito: ", ex);
            }
        }
    }
}
