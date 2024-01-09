using System.Data;

namespace Marketplace.Repository.Conexion
{
    public abstract class DAOBase
    {
        private DAOConexion _conexion;
        
        public DAOBase()
        {
            this._conexion = new DAOConexion();
        }

        public IDbConnection CrearConexion()
        {
            return _conexion.CrearConexion();
        }
    }
}
