namespace Marketplace.Models
{
    public class OfertaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<DetalleOfertaModel> Productos { get; set; }
    }

    public class DetalleOfertaModel
    {
        public int Id { get; set; }
        public int IdOferta { get; set; }
        public int IdProducto { get; set; }
        public decimal PorcentajeDescuento { get; set; }
    }
}
