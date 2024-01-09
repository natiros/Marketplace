using Dapper;
using Entities;
using Marketplace.Entities;
using Marketplace.Repository.Conexion;
using Marketplace.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Marketplace.Repository.Dapper
{
    public class ProductoDao : DAOBase, IProductoDao
    {
        private static readonly string _select;

        static ProductoDao()
        {
            _select = @"SELECT id as Id
                              ,nombre as Nombre
                              ,descripcion as Descripcion  
                              ,precio as Precio
                              ,stock as Stock
                              ,activo as Activo
                              ,pausado as Pausado
                              ,archivo as Archivo
                              ,usuario as IdUsuario
                      FROM productos ";
        }

        public void ActualizarStock(long id, int stock)
        {
            string sql = @" UPDATE productos
                            SET stock = @Stock
                            WHERE id = @Id;";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, new {Stock = stock, Id = id});
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al actualizar stock", ex);
            }
        }

        public IEnumerable<Producto> Buscar(string? nombre, string? descripcion, decimal? precioMin, decimal? precioMax)
        {
            IEnumerable<Producto> productos = new List<Producto>();
            DynamicParameters parametros = new DynamicParameters();
            string sql = _select + @" WHERE 1=1 
                                      AND pausado = 0
                                      AND activo = 1 ";

            if (!string.IsNullOrEmpty(nombre))
            {
                sql += @" AND nombre Like @Nombre ";
                parametros.Add("@Nombre", "%" + nombre + "%");
            }

            if (!string.IsNullOrEmpty(descripcion))
            {
                sql += @" AND descripcion = @Descripcion ";
                parametros.Add("@Descripcion", "%" + descripcion + "%");
            }

            if (precioMin != null && precioMin >= 0){
                sql += @" AND precio >= @PrecioMin ";
                parametros.Add("@PrecioMin", precioMin);
            }
            
            if (precioMax != null && precioMax > precioMin)
            {
                sql += @" AND precio <= @PrecioMax ";
                parametros.Add("@PrecioMax", precioMax);
            }

            try
            {
                using (var conn = CrearConexion())
                {
                    productos = conn.Query<Producto>(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar productos", ex);
            }

            return productos;
        }

        public Producto BuscarPorId(long idProducto)
        {
            var producto = new Producto();
            string sql = _select + " WHERE id = @IdProducto; ";

            try
            {
                using (var conn = CrearConexion())
                {
                    producto = conn.QueryFirstOrDefault<Producto>(sql, new { IdProducto = idProducto });
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar producto", ex);
            }

            return producto;
        }

        public IEnumerable<Producto> BuscarTodos()
        {
            IEnumerable<Producto> productos = new List<Producto>();
            string sql = _select;

            try
            {
                using (var conn = CrearConexion())
                {
                    productos = conn.Query<Producto>(sql);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar productos", ex);
            }

            return productos;
        }

        public void Cancelar(int idProducto, int idUsuario)
        {
            string sql = @" UPDATE productos
                            SET activo = @Activo
                            WHERE id = @Id "; 

            if (idUsuario > 0)
            {
                sql = sql + " AND id_usuario = @IdUsuario ";
            }
                            

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, new { Id = idProducto, Activo = false, IdUsuario = idUsuario } );
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al cancelar producto", ex);
            }
        }

        public void Insertar(Producto producto)
        {
            string sql = @" INSERT INTO productos( nombre, descripcion, precio, stock, activo, archivo, usuario)
                            VALUES( @Nombre, @Descripcion, @Precio, @Stock, @Activo, @Archivo, @IdUsuario)";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, producto);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al guardar producto", ex);
            }
        }

        public void Modificar(Producto producto)
        {
            string sql = @" UPDATE productos
                            SET nombre = @Nombre
                               ,precio = @Precio
                               ,stock = @Stock
                               ,activo = @Activo
                               ,descripcion = @Descripcion
                               ,archivo = @Archivo
                            WHERE id = @Id;";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, producto);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al modificar producto", ex);
            }
        }

        public void Pausar(int idProducto,int idUsuario)
        {
            string sql = @" UPDATE productos
                            SET pausado = @Pausado
                            WHERE id = @Id
                            AND id_usuario = @IdUsuario; ";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, new { Id = idProducto, Pausado = true, IdUsuario = idUsuario });
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al pausar producto", ex);
            }
        }

        public void Relanzar(int idProducto, int stock, int idUsuario)
        {
            var relanzar = new { Id = idProducto, Pausado = false ,Stock = stock, IdUsuario = idUsuario};

            string sql = @" UPDATE productos
                            SET pausado = @Pausado ";

            if (stock > 0)
            {
                sql = sql + " , stock = @Stock ";
            }

            sql = sql + @" WHERE id = @Id AND id_usuario = @IdUsuario; ";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, relanzar);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al pausar producto", ex);
            }
        }
    }
}
