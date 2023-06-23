using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.UsuarioService;
using sistema_facturacion_api.Useful;
using sistema_facturacion_api.Useful.Filtros;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("policy")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioCRUD _usuarioCRUD;
        private OperationResultRequest _operationResultRequest;

        public UsuarioController(IUsuarioCRUD usuarioCRUD)
        {
            _usuarioCRUD = usuarioCRUD;
        }
        [HttpGet("ListadoDeUsuarios/{page}/{cantidadDeElemento}")]
        public async Task<ActionResult<OperationResultRequest>> ListadoDeUsuarios(int page, int cantidadDeElemento)
        {
            _operationResultRequest = new OperationResultRequest();
            _operationResultRequest = await _usuarioCRUD.ListadoDeUsuarios(page, cantidadDeElemento);
            return _operationResultRequest;
        }
        [HttpGet("PerfilUsuario/{usuarioId}")]
        public async Task<ActionResult<OperationResultRequest>> PerfilUsuario(int usuarioId)
        {
            _operationResultRequest = new OperationResultRequest();
            _operationResultRequest = await _usuarioCRUD.PerfilUsuario(usuarioId);
            return _operationResultRequest;
        }
        [HttpPost("AgregarUsuario")]
        public async Task<ActionResult<OperationResultRequest>> AgregarUsuario([FromBody] TblUsuariosDTO usuario)
        {
            _operationResultRequest = new OperationResultRequest();
            if (usuario.TarjetaDeIdentificacion == null && usuario.Pasaporte == null)
            {
                _operationResultRequest.Succcess = false;
                _operationResultRequest.Message = "Los campos cedula y/o pasaporte son requeridos.";
                return BadRequest(_operationResultRequest);
            }
            _operationResultRequest = await _usuarioCRUD.AgregarUsuario(usuario);
            return _operationResultRequest;
        }
        
        [HttpPost("BuscarUsuarios")]
        public async Task<ActionResult<OperationResultRequest>> BuscarUsuarios([FromBody] FiltroUsuario parametros)
        {
            _operationResultRequest = new OperationResultRequest();
            _operationResultRequest = await _usuarioCRUD.BuscarUsuarios(parametros);
            return _operationResultRequest;
        }

        [HttpGet("BuscarUsuariosPorCedula/{cedula}")]
        public async Task<ActionResult<OperationResultRequest>> BuscarUsuariosPorCedula(string cedula)
        {
            _operationResultRequest = new OperationResultRequest();
            _operationResultRequest = await _usuarioCRUD.BuscarUsuariosPorCedula(cedula);
            return _operationResultRequest;
        }

        [HttpPut("EditarUsuario")]
        public async Task<ActionResult<OperationResultRequest>> EditarUsuario([FromBody] TblUsuariosDTO usuario)
        {
            _operationResultRequest = new OperationResultRequest();
            if (usuario.TarjetaDeIdentificacion == null && usuario.Pasaporte == null)
            {
                _operationResultRequest.Succcess = false;
                _operationResultRequest.Message = "Los campos cedula y/o pasaporte son requeridos.";
                return BadRequest(_operationResultRequest);
            }
            _operationResultRequest = await _usuarioCRUD.EditarUsuario(usuario);
            return _operationResultRequest;
        }
        [HttpPatch("CambiarEstadoUsuario/{usuarioId}")]
        public async Task<ActionResult<OperationResultRequest>> CambiarEstadoUsuario(int usuarioId, 
            [FromBody] JsonPatchDocument<TblUsuarios> usuariosPatch)
        {
            _operationResultRequest = new OperationResultRequest();
            if (usuariosPatch != null)
            {
                _operationResultRequest = await _usuarioCRUD.CambiarEstadoUsuario(usuarioId, usuariosPatch);
                return Ok(_operationResultRequest);
            }
            _operationResultRequest.Succcess = false;
            _operationResultRequest.Message = "Ocurrio un error al guardar la información";
            return BadRequest(_operationResultRequest);
        }
    }
}
