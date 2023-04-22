using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class TblFacturasDTO
    {
        public int? Id { get; set; }
        public int? EmpresaId { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? FechaDeCreacion { get; set; }
        public string? LineaDePago { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? IVA { get; set; }
        public decimal? TotalPagado { get; set; }
        public int? FormaDePagoId { get; set; }
    }
}
