﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data
{
    [Table("TblFormaDePago")]
    public class TblFormaDePago
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public bool Estado { get; set; }
    }
}
