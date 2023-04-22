using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class TblMarcaDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int EstadoId { get; set; }
        public DateTime? FechaDeCreacion { get; set; }
    }
}
