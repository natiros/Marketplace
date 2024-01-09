namespace Entities
{
    public class Usuario
    {
        public int ID { get; set; }  
        public string Nombre { get; set; }
        public string Apellido { get; set;}
        public string Email { get; set;}
        public string Password { get; set; }
        public int IdTipoUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public bool Activo { get; set; }
    }
}