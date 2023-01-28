using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data
{
    [Table("TblPermiso")]
    public class TblPermiso
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public virtual TblUsuarios Usuarios { get; set; }
        public int RolId { get; set; }
        public virtual TblRol Rol { get; set; }
        public int ModuloId { get; set; }
        public virtual TblModulo Modulo { get; set; }
        public bool C { get; set; }
        public bool R { get; set; }
        public bool U { get; set; }
        public bool D { get; set; }
        public DateTime FechaDeCreacion { get; set; }
    }
}
