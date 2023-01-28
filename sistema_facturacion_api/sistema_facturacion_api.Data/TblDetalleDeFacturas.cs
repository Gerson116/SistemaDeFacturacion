using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data
{
    [Table("TblDetalleDeFacturas")]
    public class TblDetalleDeFacturas
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public virtual TblFacturas Facturas { get; set; }
        public int ProductoId { get; set; }
        public virtual TblProductos Producto { get; set; }
        public decimal PrecioUnidad { get; set; }
        public int CantidadProducto { get; set; }
        public DateTime FechaDeRegistro { get; set; }
    }
}
