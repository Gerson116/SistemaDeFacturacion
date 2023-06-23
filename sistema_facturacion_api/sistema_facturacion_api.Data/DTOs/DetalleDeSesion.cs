using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class DetalleDeSesion
    {
        public UsuarioSesionDTO Usuarios { get; set; }
        public int RolId { get; set; }
        public List<TblPermiso> Permisos { get; set; }
        public List<TblModuloDTO> Modulos { get; set; }
    }
}
