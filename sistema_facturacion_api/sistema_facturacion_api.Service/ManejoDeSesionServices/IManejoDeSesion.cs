using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Data.ManejoDeSesion;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.ManejoDeSesionServices
{
    public interface IManejoDeSesion
    {
        Task<OperationResultLogin> ConsultarSesion(IniciarSesion iniciarSesion);
        Task<TokenUsuario> ConstruirToken(string email, string nombreDeUsuario);
        Task<OperationResultRequest> OlvideMiPassword(TblUsuariosDTO usuario, string passwordGenerico, string correo, string password);
    }
}
