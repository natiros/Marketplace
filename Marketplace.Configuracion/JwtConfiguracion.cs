namespace Marketplace.Configuracion
{
    public class JwtConfiguracion
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}