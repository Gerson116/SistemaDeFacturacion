using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Useful
{
    public class Pager
    {
        public int NumeroDePagina { get; set; }
        public int CantidadDePaginas { get; set; }
        public int TotalDePaginas { get; set; }

        public Pager(int numeroDePagina, int cantidadDePaginas, int totalDePaginas)
        {
            NumeroDePagina = numeroDePagina;
            CantidadDePaginas = cantidadDePaginas;
            TotalDePaginas = totalDePaginas;    
        }
    }
}
