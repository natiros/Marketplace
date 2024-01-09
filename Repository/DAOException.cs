using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Repository
{
    public class DAOException : Exception
    {
        public DAOException(string mensaje) : base(mensaje) { }
        public DAOException(string mensaje, Exception causa) : base(mensaje, causa) { }
    }
}
