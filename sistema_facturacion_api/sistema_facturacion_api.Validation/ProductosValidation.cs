using FluentValidation;
using sistema_facturacion_api.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Validation
{
    public class ProductosValidation : AbstractValidator<TblProductosDTO>
    {
        private string _campoRequerido = "Este campo es requerido";
        public ProductosValidation()
        {
            RuleFor(x => x.Nombre).NotEmpty();
        }
    }
}
