using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Useful
{
    public class OperationResultLogin
    {
        public bool Succcess { get; set; }
        public string Message { get; set; }
        public dynamic Usuario { get; set; }
        public dynamic ModelosDisponibles { get; set; }
        public dynamic PermisosDelUsuario { get; set; }
        public string Token { get; set; }
        public DateTime TiempoDeVida { get; set; }
    }
}
