using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.IVAServices;
using sistema_facturacion_api.Useful;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IVAController : ControllerBase
    {
        private IIVA _ivaServices;
        private OperationResultRequest _result;

        public IVAController(IIVA ivaServices)
        {
            //...Impuesto Al Valor Agregado
            _ivaServices = ivaServices;
            _result = new OperationResultRequest();
        }

        [HttpGet("GetAllBuscarIVA/{empresaId}")]
        public async Task<ActionResult<OperationResultRequest>> GetAllBuscarIVA(int empresaId)
        {
            _result = await _ivaServices.GetAllBuscarIVA(empresaId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        [HttpPost("PostNuevoIVA")]
        public async Task<ActionResult<OperationResultRequest>> PostNuevoIVA([FromBody]List<IVADTO> iva)
        {
            _result = await _ivaServices.PostNuevoIVA(iva);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        [HttpPut("PutEditarIVA/{empresaIVAId}")]
        public async Task<ActionResult<OperationResultRequest>> PutEditarIVA([FromBody]IVADTO iva, int empresaIVAId)
        {
            _result = await _ivaServices.PutEditarIVA(iva, empresaIVAId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        [HttpDelete("DeleteEliminarIVA/{empresaId}")]
        public async Task<ActionResult<OperationResultRequest>> DeleteEliminarIVA(int empresaId)
        {
            _result = await _ivaServices.DeleteEliminarIVA(empresaId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }
    }
}
