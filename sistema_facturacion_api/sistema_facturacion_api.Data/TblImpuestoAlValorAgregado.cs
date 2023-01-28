using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data
{
    [Table("TblImpuestoAlValorAgregado")]
    public class TblImpuestoAlValorAgregado
    {
        public int Id { get; set; }
        public decimal IVA { get; set; }
        public int EmpresaId { get; set; }
    }
}
