using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class IVADTO
    {
        public int? Id { get; set; }
        [Required]
        public decimal IVA { get; set; }
        public int? EmpresaId { get; set; }
    }
}
