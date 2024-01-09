using Marketplace.Configuracion;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Marketplace.Repository.Conexion
{
    public class DAOConexion
    {
        private static string cadenaConexion = "Server=DESKTOP-GKCFPHL\\sqlexpress;Database=marketplace;User Id=nati;Password=123456;";
  
        public IDbConnection CrearConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}