using Marketplace.Entities;
using Marketplace.Repository.Dapper;
using Marketplace.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Marketplace.Servicios
{
    public class ServicioCompra
    {

        private CompraDao compraDao;
        private DetalleCompraDao detalleCompraDao;
        private ProductoDao productoDao;
        private CarritoDao carritoDao;
        public ServicioCompra()
        {
            compraDao = new CompraDao();
            detalleCompraDao = new DetalleCompraDao();
            productoDao = new ProductoDao();
            carritoDao = new CarritoDao();
        }
        public void Guardar(Compra compra, List<DetalleCompra> detalleCompras)
        {
            try
            {
                var id = compraDao.Insertar(compra);

                foreach (var detalle in detalleCompras)
                {
                    detalle.IdCompra = id;

                    var producto = productoDao.BuscarPorId(detalle.IdProducto);

                    int nuevoStock = producto.Stock - detalle.Cantidad;
                    if ( nuevoStock < 0)
                    {
                        throw new Exception("No hay suficiente stock");
                    }

                    detalle.Precio = producto.Precio;

                    productoDao.ActualizarStock(producto.Id, nuevoStock);

                    detalleCompraDao.Insertar(detalle);

                    carritoDao.VaciarCarrito(compra.IdUsuario);
                }
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al guardar los datos de la compra: ", ex);
            }
        }

        public List<Compra> Buscar(int idUsuario)
        {
            try
            {
                var compras = (List<Compra>)compraDao.BuscarCompras(idUsuario);

                foreach (var compra in compras)
                {
                    compra.DetalleCompras = (List<DetalleCompra>)detalleCompraDao.BuscarDetalleCompras(compra.Id);
                    foreach (var det in compra.DetalleCompras)
                    {
                        det.Producto = productoDao.BuscarPorId(det.IdProducto);
                    }
                }

                return compras;
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al buscar compras: ", ex);
            }
        }
    }
}
