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
    public class CarritoDao : DAOBase, ICarritoDao
    {
        private static readonly string _select;
        static CarritoDao()
        {
            _select = @"SELECT id as Id
                              ,cantidad as Cantidad
                              ,id_producto as IdProducto  
                              ,id_usuario as IdUsuario
                      FROM carrito ";
        }
        public IEnumerable<Carrito> Buscar(int idUsuario)
        {
            IEnumerable<Carrito> carrito = new List<Carrito>();
            DynamicParameters parametros = new DynamicParameters();
            string where = @" WHERE id_usuario = @IdUsuario ";

            string sql = _select + where;

            parametros.Add("@IdUsuario", idUsuario);
            try
            {
                using (var conn = CrearConexion())
                {
                    carrito = conn.Query<Carrito>(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar carrito", ex);
            }

            return carrito;
        }

        public void Eliminar(long id)
        {
            string sql = @" DELETE FROM carrito
                            WHERE id = @Id";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al eliminar item del carrito", ex);
            }
        }

        public void Insertar(Carrito carrito)
        {
            string sql = @" INSERT INTO carrito( cantidad, id_producto, id_usuario)
                            VALUES( @Cantidad, @IdProducto, @IdUsuario )";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, carrito);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al guardar carrito", ex);
            }
        }

        public void VaciarCarrito(int idUsuario)
        {
            string sql = @" DELETE FROM carrito
                            WHERE id_usuario = @IdUsuario";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, new { IdUsuario = idUsuario });
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al vaciar carrito", ex);
            }
        }
    }
}
