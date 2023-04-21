using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.PermisosServices;
using sistema_facturacion_api.Useful;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("policy")]
    public class PermisosController : ControllerBase
    {
        private OperationResultRequest _result;
        private IPermisosServices _permisosServices;

        public PermisosController(IPermisosServices permisosServices)
        {
            _result = new OperationResultRequest();
            _permisosServices = permisosServices;
        }

        [HttpGet("GetAllPermisos/{usuarioId}")]
        public async Task<OperationResultRequest> GetAllPermisos(int usuarioId)
        {
            _result = await _permisosServices.GetAllPermisos(usuarioId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }
        [HttpPost("PostAgregarPermisos")]
        public async Task<OperationResultRequest> PostAgregarPermisos([FromBody] List<TblPermisoDTO> nuevoPermiso)
        {
            _result = await _permisosServices.PostAgregarPermisos(nuevoPermiso);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }
        [HttpPut("PutEditarPermisos/{permisoId}")]
        public async Task<OperationResultRequest> PutEditarPermisos([FromBody] TblPermisoDTO editarPermiso, int permisoId)
        {
            _result = await _permisosServices.PutEditarPermisos(editarPermiso, permisoId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }
        [HttpDelete("DeletePermisos/{usuarioId}/{permisoId}")]
        public async Task<OperationResultRequest> DeletePermisos(int usuarioId, int permisoId)
        {
            _result = await _permisosServices.DeletePermisos(usuarioId, permisoId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }
    }
}
