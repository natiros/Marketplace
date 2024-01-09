using Entities;
using Marketplace.Repository.Dapper;
using Marketplace.Entities;
using Marketplace.Repository;

namespace Marketplace.Servicios
{
    public class ServicioProducto
    {
        private ProductoDao productoDao;
        public ServicioProducto()
        {
            productoDao = new ProductoDao();
        }
        public void Guardar(Producto producto)
        {
            try
            {
                productoDao.Insertar(producto);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al guardar los datos.", ex);
            }
        }

        public void Modificar(Producto producto)
        {
            try
            {
                productoDao.Modificar(producto);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al Modificar los datos.", ex);
            }
        }

        public void Cancelar(int idProducto, int idUsuario)
        {
            try
            {
                productoDao.Cancelar(idProducto, idUsuario);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al Cancelar producto.", ex);
            }
        }

        public void Cancelar(int idProducto)
        {
            try
            {
                productoDao.Cancelar(idProducto, 0);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al Cancelar producto.", ex);
            }
        }

        public void Pausar(int idProducto, int idUsuario)
        {
            try
            {
                productoDao.Pausar(idProducto, idUsuario);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al Pausar producto.", ex);
            }
        }

        public void Relanzar(int idProducto, int stock, int idUsuario)
        {
            try
            {
                var producto = productoDao.BuscarPorId(idProducto);

                if(producto.Stock > 0 && stock > 0)
                {
                    throw new Exception("Se quiere setear un nuevo stock a un producto que ya tiene");
                }

                productoDao.Relanzar(idProducto, stock, idUsuario);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al Relanzar el producto.", ex);
            }
        }

        public IEnumerable<Producto> Buscar(string? nombre, string? descripcion, decimal? precioMin, decimal? precioMax)
        {
            try
            {
                var productos = productoDao.Buscar(nombre, descripcion, precioMin, precioMax);

                return productos;
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al hacer una busqueda de productos.", ex);
            }            
        }

        public IEnumerable<Producto> BuscarTodosActivos()
        {
            try
            {
                var productos = productoDao.Buscar(null, null, null, null);

                return productos;
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al Buscar productos.", ex);
            }
        }

        public IEnumerable<Producto> BuscarTodos()
        {
            try
            {
                var productos = productoDao.BuscarTodos();

                return productos;
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al Buscar productos.", ex);
            }
        }

        public Producto BuscarPorId(int idProducto)
        {
            try
            {
                var producto = productoDao.BuscarPorId(idProducto);

                return producto;
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al Buscar el producto.", ex);
            }
        }
    }
}
