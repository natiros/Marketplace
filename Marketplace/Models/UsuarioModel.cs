namespace Marketplace.Models
{
    public class UsuarioModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TipoUsuario { get; set; }
    }
}
