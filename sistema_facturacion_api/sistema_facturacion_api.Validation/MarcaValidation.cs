using FluentValidation;
using sistema_facturacion_api.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Validation
{
    public class MarcaValidation : AbstractValidator<TblMarcaDTO>
    {
        private string _campoRequerido = "Este campo es requerido";
        public MarcaValidation()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage(_campoRequerido);
            RuleFor(x => x.EstadoId).NotEmpty().WithMessage(_campoRequerido);
        }
    }
}
