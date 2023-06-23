using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.ModuloServices;
using sistema_facturacion_api.Useful;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("policy")]
    public class ModuloController : ControllerBase
    {
        private OperationResultRequest _result;
        private IModuloServices _moduloServices;

        public ModuloController(IModuloServices moduloServices)
        {
            _result = new OperationResultRequest();
            _moduloServices = moduloServices;
        }

        [HttpGet("GetAllModulos")]
        public async Task<ActionResult<OperationResultRequest>> GetAllModulos()
        {
            _result = await _moduloServices.GetAllModulos();
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        //[HttpGet("")]
        //public async Task<ActionResult<OperationResultRequest>> GetAllModulos()
        //{
        //    _result = await _moduloServices.GetAllModulos();
        //    if (_result.Succcess)
        //    {
        //        return _result;
        //    }
        //    return _result;
        //}

        [HttpPost("PostNuevoModulo")]
        public async Task<ActionResult<OperationResultRequest>> PostNuevoModulo([FromBody] TblModuloDTO nuevoModulo)
        {
            _result = await _moduloServices.PostNuevoModulo(nuevoModulo);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        [HttpPut("PutEditarModulo/{moduloId}")]
        public async Task<ActionResult<OperationResultRequest>> PutEditarModulo([FromBody] TblModuloDTO editarModulo, int moduloId)
        {
            _result = await _moduloServices.PutEditarModulo(editarModulo, moduloId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        [HttpDelete("DeleteEliminarModulo/{moduloId}")]
        public async Task<ActionResult<OperationResultRequest>> DeleteEliminarModulo(int moduloId)
        {
            _result = await _moduloServices.DeleteEliminarModulo(moduloId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }
    }
}
