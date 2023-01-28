using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data
{
    [Table("TblLineasDeFacturas")]
    public class TblLineasDeFacturas
    {
        public int Id { get; set; }
        public string LineaDeFactura { get; set; }
        public DateTime FechaDeCreacion { get; set; }
    }
}
