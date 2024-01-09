using Marketplace.Entities;

namespace Marketplace.Repository.Interfaces
{
    public interface IProductoDao
    {
        void Insertar(Producto producto);

        void Modificar(Producto producto);

        void Cancelar(int idProducto, int idUsuario);
        void Pausar(int idProducto, int idUsuario);
        Producto BuscarPorId(long idProducto);
        void Relanzar(int idProducto, int stock, int idUsuario);
        IEnumerable<Producto> Buscar(string? nombre, string? descripcion, decimal? precioMin, decimal? precioMax);
        IEnumerable<Producto> BuscarTodos();
        void ActualizarStock(long id, int stock);
    }
}
