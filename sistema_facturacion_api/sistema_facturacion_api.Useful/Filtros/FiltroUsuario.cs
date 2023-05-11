using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Useful.Filtros
{
    public class FiltroUsuario : ParametrosDeBusqueda
    {
        public string? Identificacion { get; set; }
        public string? Pasaporte { get; set; }
    }
}
