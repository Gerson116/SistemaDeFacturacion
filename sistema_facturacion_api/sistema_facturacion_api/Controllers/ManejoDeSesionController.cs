using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Data.ManejoDeSesion;
using sistema_facturacion_api.Service.ManejoDeSesionServices;
using sistema_facturacion_api.Service.UsuarioService;
using sistema_facturacion_api.Useful;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManejoDeSesionController : ControllerBase
    {
        private OperationResultLogin _result;
        private OperationResultRequest _resultRequest;
        private IManejoDeSesion _manejoDeSesion;
        private IUsuarioCRUD _usuarioServices;

        public ManejoDeSesionController(IManejoDeSesion manejoDeSesion, IUsuarioCRUD usuarioServices)
        {
            _result = new OperationResultLogin();
            _resultRequest = new OperationResultRequest();
            _manejoDeSesion = manejoDeSesion;
            _usuarioServices = usuarioServices;
        }

        [HttpPost("IniciarSesion")]
        public async Task<OperationResultLogin> IniciarSesion([FromBody] IniciarSesion iniciarSesion)
        {
            _result = await _manejoDeSesion.ConsultarSesion(iniciarSesion);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        [HttpPost("OlvideMiPass")]
        public async Task<OperationResultRequest> OlvideMiPass([FromBody] CambiarPasswordOlvidado cambiarPassword)
        {
            string passwordGenerico = System.Environment.GetEnvironmentVariable("passwordGenerico");
            string correoDesarrollo = System.Environment.GetEnvironmentVariable("correoDesarrollo");
            string passwordCorreoDesarrollo = System.Environment.GetEnvironmentVariable("passwordCorreoDesarrollo");
            
            _resultRequest = await _manejoDeSesion.OlvideMiPassword(cambiarPassword.Email, passwordGenerico, correoDesarrollo, passwordCorreoDesarrollo);
            if (_result.Succcess)
            {
                return _resultRequest;
            }
            return _resultRequest;
        }
    }
}
