using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class TblDetalleDeFacturasDTO
    {
        public int? Id { get; set; }
        public int? FacturaId { get; set; }
        public int? ProductoId { get; set; }
        public string? NombreProducto { get; set; }
        public decimal? PrecioUnidad { get; set; }
        public int? CantidadProducto { get; set; }
        public DateTime? FechaDeRegistro { get; set; }
    }
}
