using Microsoft.AspNetCore.Http;
using sistema_facturacion_api.Data.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.Archivo
{
    public class Imagen
    {
        public string? NombreImagen { get; set; }
        [PesoArchivo(pesoMaximoEnMb: 5)]
        [TipoArchivo(Enums.EnumTipoDeArchivo.Imagen)]
        public IFormFile Archivo { get; set; }
    }
}
