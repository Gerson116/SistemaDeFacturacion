using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class DetalleDeSesion
    {
        public TblUsuarios Usuarios { get; set; }
        public List<TblPermiso> Permisos { get; set; }
    }
}
