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
        public int CantidadDeElementos { get; set; }
        public int TotalDePaginas { get; set; }

        public Pager(int numeroDePagina, int cantidadDeElementos, int totalElementos)
        {
            NumeroDePagina = numeroDePagina;
            CantidadDeElementos = cantidadDeElementos;
            TotalDePaginas = CalcularPaginas(totalElementos, cantidadDeElementos);  
        }
        private int CalcularPaginas(int totalElementos, int cantidadDeElementos)
        {
            int result = totalElementos / cantidadDeElementos;
            if (result <= 1)
            {
                return result = 1;
            }
            return result;
        }
    }
}
