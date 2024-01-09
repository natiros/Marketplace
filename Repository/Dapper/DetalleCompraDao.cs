using Dapper;
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
    public class DetalleCompraDao : DAOBase, IDetalleCompraDao
    {
        private static readonly string _select;
        static DetalleCompraDao()
        {
            _select = @"SELECT id as Id
                              ,id_compra as IdCompra
                              ,id_producto as IdProducto
                              ,cantidad as Cantidad
                              ,precio as Precio
                          FROM detalle_compras ";
        }
        public IEnumerable<DetalleCompra> BuscarDetalleCompras(int idCompra)
        {
            IEnumerable<DetalleCompra> detalles = new List<DetalleCompra>();
            DynamicParameters parametros = new DynamicParameters();
            string where = @" WHERE id_compra = @IdCompra ";

            string sql = _select + where;

            parametros.Add("@IdCompra", idCompra);
            try
            {
                using (var conn = CrearConexion())
                {
                    detalles = conn.Query<DetalleCompra>(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar detalles", ex);
            }

            return detalles;
        }

        public void Insertar(DetalleCompra detalleCompra)
        {
            string sql = @" INSERT INTO detalle_compras( id_compra, id_producto, cantidad, precio)
                            VALUES( @IdCompra ,@IdProducto, @Cantidad, @Precio )";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, detalleCompra);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al guardar detalle compra", ex);
            }
        }
    }
}
