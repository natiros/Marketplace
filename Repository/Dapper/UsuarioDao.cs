using Dapper;
using Entities;
using Marketplace.Entities;
using Marketplace.Repository.Conexion;
using Marketplace.Repository.Interfaces;

namespace Marketplace.Repository.Dapper
{
    public class UsuarioDao : DAOBase, IUsuarioDao
    {
        private static readonly string _select;

        static UsuarioDao()
        {
            _select = @"SELECT usu.id as Id
                                ,usu.nombre as Nombre
                                ,usu.apellido as Apellido
                                ,usu.email as Email
                                ,usu.password as Password
                                ,usu.tipo_usuario as IdTipoUsuario   
		                        ,tu.nombre as TipoUsuario
                                ,usu.activo as Activo
                        FROM usuarios usu
                        JOIN tipos_usuarios tu ON usu.tipo_usuario = tu.id ";
        }

        public Usuario BuscarPorId(int idUsuario)
        {
            var usuario = new Usuario();
            string sql = _select + " WHERE id = @IdUsuario ";

            try
            {
                using (var conn = CrearConexion())
                {
                    usuario = conn.QueryFirstOrDefault<Usuario>(sql, new { IdUsuario = idUsuario });
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar usuario", ex);
            }

            return usuario;
        }

        public Usuario BuscarPorEmail(string email)
        {
            var usuario = new Usuario();
            string sql = _select + " WHERE email = @Email ";

            try
            {
                using (var conn = CrearConexion())
                {
                    usuario = conn.QueryFirstOrDefault<Usuario>(sql, new { Email = email });
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar usuario", ex);
            }

            return usuario;
        }

        public void Insertar(Usuario usuario)
        {
            string sql = @" INSERT INTO usuarios( nombre, apellido, email, password, tipo_usuario )
                            VALUES( @Nombre, @Apellido, @Email, @Password, @IdTipoUsuario )";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, usuario);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error guardar usuario", ex);
            }
        }

        public IEnumerable<Usuario> Buscar()
        {
            var usuarios = new List<Usuario>();
            string sql = _select;

            try
            {
                using (var conn = CrearConexion())
                {
                    usuarios = conn.Query<Usuario>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al buscar usuarios", ex);
            }

            return usuarios;
        }

        public void Deshabilitar(int idUsuario)
        {
            string sql = @" UPDATE usuarios
                            SET activo = @Activo
                            WHERE id = @IdUsuario";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, new { IdUsuario = idUsuario, Activo = false});
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error deshabilitar usuario", ex);
            }
        }

        public void ConvertirAdmin(int idUsuario)
        {
            string sql = @" UPDATE usuarios
                            SET tipo_usuario = @TipoUsuario
                            WHERE id = @IdUsuario";

            try
            {
                using (var conn = base.CrearConexion())
                {
                    conn.Execute(sql, new { IdUsuario = idUsuario, TipoUsuario = TipoUsuario.Admin });
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error convertir usuario en Administrador", ex);
            }
        }
    }
}
