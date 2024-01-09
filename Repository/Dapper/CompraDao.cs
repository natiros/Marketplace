using Dapper;
using Entities;
using Marketplace.Entities;
using Marketplace.Repository.Conexion;
using Marketplace.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Repository.Dapper
{
    public class CompraDao : DAOBase, ICompraDao
    {
        private static readonly string _select;

        static CompraDao()
        {
            _select = @"SELECT id as Id
                              ,fecha as Fecha
                              ,id_usuario as IdUsuario  
                      FROM compras ";
        }
        public IEnumerable<Compra> BuscarCompras(int idUsuario)
        {
            IEnumerable<Compra> compras = new List<Compra>();
            DynamicParameters parametros = new DynamicParameters();
            string where = @" WHERE id_usuario = @IdUsuario";

            string sql = _select + where;

            parametros.Add("@IdUsuario", idUsuario);
            try
            {
                using (var conn = CrearConexion())
                {
                    compras = conn.Query<Compra>(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar compras", ex);
            }

            return compras;
        }

        public long Insertar(Compra compra)
        {
            long id = 0;
            string sql = @" INSERT INTO compras( id_usuario, fecha)
                            OUTPUT INSERTED.id
                            VALUES( @IdUsuario, @Fecha )";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    id = (long)conn.ExecuteScalar(sql, compra);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al guardar compra", ex);
            }

            return id;
        }
    }
}
