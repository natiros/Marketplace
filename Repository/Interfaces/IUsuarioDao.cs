using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Repository.Interfaces
{
    public interface IUsuarioDao
    {
        Usuario BuscarPorId(int idUsuario);
        Usuario BuscarPorEmail(string email);
        void Insertar(Usuario usuario);
        IEnumerable<Usuario> Buscar();
        void Deshabilitar(int idUsuario);
        void ConvertirAdmin(int idUsuario);
    }
}
