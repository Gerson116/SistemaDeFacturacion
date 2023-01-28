using FluentValidation;
using sistema_facturacion_api.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Validation
{
    public class UsuariosValidation : AbstractValidator<TblUsuariosDTO>
    {
        private string _campoRequerido = "Este campo es requerido";
        public UsuariosValidation()
        {
            RuleFor(x => x.Apellidos).NotEmpty().WithName(_campoRequerido);
            RuleFor(x => x.Nombres).NotEmpty().WithName(_campoRequerido);
            RuleFor(x => x.NombreDeUsuario).NotEmpty().WithName(_campoRequerido);
            RuleFor(x => x.FechaDeNacimiento).NotEmpty().WithName(_campoRequerido);
            RuleFor(x => x.Email).NotEmpty().WithName(_campoRequerido);
            RuleFor(x => x.Password).NotEmpty().WithName(_campoRequerido);
            RuleFor(x => x.RolId).NotEmpty().WithName(_campoRequerido);
            RuleFor(x => x.EstadoId).NotEmpty().WithName(_campoRequerido);
        }
    }
}
