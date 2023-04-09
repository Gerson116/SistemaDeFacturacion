using Microsoft.AspNetCore.Http;
using sistema_facturacion_api.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.Validations
{
    public class TipoArchivo : ValidationAttribute
    {
        private string[] _tipoArchivos;
        public TipoArchivo(EnumTipoDeArchivo tipoDeArchivo)
        {
            _tipoArchivos = BuscarTipoDeArchivo((int)tipoDeArchivo);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;
            if (formFile == null) 
            {
                return ValidationResult.Success;
            }

            if (!_tipoArchivos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo de archivo que envio, no es permitido.");
            }

            return ValidationResult.Success;
        }

        private string[] BuscarTipoDeArchivo(int tipoArchivo)
        {
            string[] archivo = null;
            switch (tipoArchivo)
            {
                case (int)EnumTipoDeArchivo.Imagen:
                    archivo = new string[] { "image/jpg", "image/png", "image/gif", "image/jpeg" };
                    break;
                default:
                    archivo = null;
                    break;
            }
            return archivo;
        }
    }
}
