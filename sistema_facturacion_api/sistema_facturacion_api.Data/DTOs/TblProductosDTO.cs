using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class TblProductosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int EstadoId { get; set; }
        public DateTime? FechaDeCreacion { get; set; }
    }
}
