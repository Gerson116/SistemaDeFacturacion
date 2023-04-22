using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class NuevaFacturaDTO
    {
        public TblFacturasDTO Factura { get; set; }
        public List<TblDetalleDeFacturasDTO> DetalleFactura { get; set; }
    }
}
