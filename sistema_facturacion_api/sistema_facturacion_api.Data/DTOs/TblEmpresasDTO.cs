using sistema_facturacion_api.Data.Archivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class TblEmpresasDTO : Imagen
    {
        public int? Id { get; set; }
        public int TblUsuariosId { get; set; }
        public virtual TblUsuarios? Usuarios { get; set; }
        public string? rutaImagen { get; set; }
        public string? Nombre { get; set; }
    }
}
