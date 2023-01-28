﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data
{
    [Table("TblMarca")]
    public class TblMarca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaDeCreacion { get; set; }
    }
}
