using Dapper;
using Entities;
using Marketplace.Entities;
using Marketplace.Repository.Conexion;
using Marketplace.Repository.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Marketplace.Repository.Dapper
{
    public class OfertaDao : DAOBase, IOfertaDao
    {
        private static readonly string _select;

        static OfertaDao()
        {
            _select = @"SELECT id as Id
                              ,nombre as Nombre
                              ,fecha_inicio as FechaInicio  
                              ,fecha_fin as FechaFin
                      FROM ofertas ";
        }
        public IEnumerable<Oferta> BuscarOfertas()
        {
            IEnumerable<Oferta> ofertas = new List<Oferta>();
            DynamicParameters parametros = new DynamicParameters();
            string where = @" WHERE fecha_inicio <= @Fecha 
                              AND   fecha_fin >= @Fecha ";

            string sql = _select + where;

            parametros.Add("@Fecha", DateTime.Now);
            try
            {
                using (var conn = CrearConexion())
                {
                    ofertas = conn.Query<Oferta>(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar ofertas", ex);
            }

            return ofertas;
        }

        public IEnumerable<Oferta> BuscarTodasOfertas()
        {
            IEnumerable<Oferta> ofertas = new List<Oferta>();

            string sql = _select;
            try
            {
                using (var conn = CrearConexion())
                {
                    ofertas = conn.Query<Oferta>(sql);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar ofertas", ex);
            }

            return ofertas;
        }

        public void Cancelar(int idOferta)
        {

            string sql = @" UPDATE ofertas
                            SET fecha_fin = fecha_inicio
                            WHERE id = @Id ";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, new { Id = idOferta});
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al cancelar oferta", ex);
            }
        }

        public long Insertar(Oferta oferta)
        {
            long id = 0;
            string sql = @" INSERT INTO ofertas( nombre, fecha_inicio, fecha_fin)
                            OUTPUT INSERTED.id
                            VALUES( @Nombre, @FechaInicio, @FechaFin)";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    id = (long)conn.ExecuteScalar(sql, oferta);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al guardar oferta", ex);
            }

            return id;
        }
    }
}
