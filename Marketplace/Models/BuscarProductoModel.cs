namespace Marketplace.Models
{
    public class BuscarProductoModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioMin { get; set; }
        public decimal PrecioMax { get; set; }
    }
}
