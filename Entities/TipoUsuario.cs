using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Entities
{
    public class TipoUsuario
    {
        public const int Admin = 1;
        public const int Usuario = 2;
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
