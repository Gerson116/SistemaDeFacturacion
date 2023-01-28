using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.UsuarioService;
using sistema_facturacion_api.Useful;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioCRUD _usuarioCRUD;
        private OperationResultRequest _operationResultRequest;

        public UsuarioController(IUsuarioCRUD usuarioCRUD)
        {
            _usuarioCRUD = usuarioCRUD;
        }
        [HttpGet("ListadoDeUsuarios")]
        public async Task<ActionResult<OperationResultRequest>> ListadoDeUsuarios()
        {
            _operationResultRequest = new OperationResultRequest();
            _operationResultRequest = await _usuarioCRUD.ListadoDeUsuarios();
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
            _operationResultRequest = await _usuarioCRUD.AgregarUsuario(usuario);
            return _operationResultRequest;
        }
        [HttpPut("EditarUsuario")]
        public async Task<ActionResult<OperationResultRequest>> EditarUsuario([FromBody] TblUsuariosDTO usuario)
        {
            _operationResultRequest = new OperationResultRequest();
            _operationResultRequest = await _usuarioCRUD.EditarUsuario(usuario);
            return _operationResultRequest;
        }
        [HttpPatch("EliminarUsuario/{usuarioId}")]
        public async Task<ActionResult<OperationResultRequest>> EliminarUsuario(int usuarioId, 
            [FromBody] JsonPatchDocument<TblUsuarios> usuariosPatch)
        {
            _operationResultRequest = new OperationResultRequest();
            if (usuariosPatch != null)
            {
                _operationResultRequest = await _usuarioCRUD.EliminarUsuario(usuarioId, usuariosPatch);
                return Ok(_operationResultRequest);
            }
            _operationResultRequest.Succcess = false;
            _operationResultRequest.Message = "Ocurrio un error al guardar la información";
            return BadRequest(_operationResultRequest);
        }
    }
}
