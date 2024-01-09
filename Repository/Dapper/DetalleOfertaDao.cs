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
    public class DetalleOfertaDao : DAOBase, IDetalleOfertaDao
    {
        private static readonly string _select;
        static DetalleOfertaDao()
        {
            _select = @"SELECT id as Id
                              ,id_oferta as IdOferta
                              ,id_producto as IdProducto
                              ,porcentaje_descuento as PorcentajeDescuento 
                      FROM detalle_ofertas ";
        }
        public IEnumerable<DetalleOferta> BuscarDetalleOfertas(int idOferta)
        {
            IEnumerable<DetalleOferta> detalles = new List<DetalleOferta>();
            DynamicParameters parametros = new DynamicParameters();
            string where = @" WHERE id_oferta = @IdOferta ";

            string sql = _select + where;

            parametros.Add("@IdOferta", idOferta);
            try
            {
                using (var conn = CrearConexion())
                {
                    detalles = conn.Query<DetalleOferta>(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar detalles", ex);
            }

            return detalles;
        }

        public void Insertar(DetalleOferta detalle)
        {
            string sql = @" INSERT INTO detalle_ofertas( id_oferta, id_producto, porcentaje_descuento)
                            VALUES( @IdOferta, @IdProducto, @PorcentajeDescuento)";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, detalle);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al guardar detalle oferta", ex);
            }
        }
    }
}
