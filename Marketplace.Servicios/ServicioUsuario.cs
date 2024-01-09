using Entities;
using Marketplace.Repository;
using Marketplace.Repository.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Servicios
{
    public class ServicioUsuario
    {
        private UsuarioDao usuarioDao;
        public ServicioUsuario()
        {
            usuarioDao = new UsuarioDao();
        }
        public void GuardarUsuario(Usuario usuario)
        {
            try
            {
                usuario.Password = PasswordHelper.HashPassword(usuario.Password);

                usuarioDao.Insertar(usuario);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al guardar los datos.", ex);
            }
        }

        public Usuario BuscarUsuarioPorEmail(string email)
        {
            try
            {
                var usuario = usuarioDao.BuscarPorEmail(email);

                return usuario;
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al buscar los datos.", ex);
            }
        }

        public List<Usuario> Buscar()
        {
            try
            {
                var usuarios = (List<Usuario>)usuarioDao.Buscar();

                return usuarios;
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al buscar los datos.", ex);
            }
        }

        public void Deshabilitar(int idUsuario)
        {
            try
            {
                usuarioDao.Deshabilitar(idUsuario);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al Deshabilitar el usuario.", ex);
            }
        }

        public void ConvertirAdmin(int idUsuario)
        {
            try
            {
                usuarioDao.ConvertirAdmin(idUsuario);
            }
            catch (DAOException ex)
            {
                throw new Exception("Se produjo un error al convertir en admin el usuario.", ex);
            }
        }

    }
}
