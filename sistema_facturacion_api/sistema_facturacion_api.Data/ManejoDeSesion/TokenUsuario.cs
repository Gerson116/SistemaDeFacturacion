using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.ManejoDeSesion
{
    public class TokenUsuario
    {
        public string Token { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
