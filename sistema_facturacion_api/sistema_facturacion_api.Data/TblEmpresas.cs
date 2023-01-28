using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data
{
    [Table("TblEmpresas")]
    public class TblEmpresas
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public virtual TblUsuarios Usuarios { get; set; }
        public string Nombre { get; set; }
        public string rutaImagen { get; set; }
        public DateTime FechaDeCreacion { get; set; }
    }
}
