using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Useful
{
    public class OperationResultLogin
    {
        public bool Succcess { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public string Token { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
