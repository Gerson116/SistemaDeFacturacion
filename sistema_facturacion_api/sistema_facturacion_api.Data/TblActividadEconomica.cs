using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data
{
    [Table("TblActividadEconomica")]
    public class TblActividadEconomica
    {
        public int Id { get; set; }
        public string NombreDeLaActividad { get; set; }
        public int CodigoDeLaActividad { get; set; }
        public DateTime FechaDeCreacion { get; set; }
    }
}
