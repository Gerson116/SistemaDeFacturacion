using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.Validations
{
    public class PesoArchivo : ValidationAttribute
    {
        private int _pesoMaximoEnMb = 5;

        public PesoArchivo(int pesoMaximoEnMb)
        {
            _pesoMaximoEnMb = pesoMaximoEnMb;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile file = value as IFormFile;
            if (file == null)
            {
                return ValidationResult.Success;
            }

            if (file.Length > _pesoMaximoEnMb * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo es superior a {_pesoMaximoEnMb} MB");
            }
            return ValidationResult.Success;
        }
    }
}
