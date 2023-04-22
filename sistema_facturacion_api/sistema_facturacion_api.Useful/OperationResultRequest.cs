using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Useful
{
    public class OperationResultRequest
    {
        public bool Succcess { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public Pager Paginacion { get; set; }
    }
}
