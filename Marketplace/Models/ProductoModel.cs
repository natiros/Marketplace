﻿namespace Marketplace.Models
{
    public class ProductoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Archivo { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
    }
}
