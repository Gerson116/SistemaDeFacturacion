using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.FacturaServices;
using sistema_facturacion_api.Useful;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("policy")]
    public class FacturaController : ControllerBase
    {
        private OperationResultRequest _result;
        private IFacturaServices _facturaServices;

        public FacturaController(IFacturaServices facturaServices)
        {
            //...
            _result = new OperationResultRequest();
            _facturaServices = facturaServices;
        }

        [HttpGet("GetAllFactura/{empresaId}")]
        public async Task<ActionResult<OperationResultRequest>> GetAllFacturaEmpresa(int empresaId)
        {
            _result = await _facturaServices.GetAllFactura(empresaId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        [HttpGet("GetDetalleFactura/{facturaId}")]
        public async Task<ActionResult<OperationResultRequest>> GetDetalleFactura(int facturaId)
        {
            _result = await _facturaServices.GetDetalleFactura(facturaId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        [HttpPost("PostNuevoFactura")]
        public async Task<ActionResult<OperationResultRequest>> PostNuevoFactura([FromBody] NuevaFacturaDTO nuevaFactura)
        {
            _result = await _facturaServices.PostNuevoFactura(nuevaFactura);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }

        [HttpDelete("DeleteFactura/{empresaId}/{facturaId}")]
        public async Task<ActionResult<OperationResultRequest>> DeleteFactura(int empresaId, int facturaId)
        {
            _result = await _facturaServices.DeleteFactura(empresaId, facturaId);
            if (_result.Succcess)
            {
                return _result;
            }
            return _result;
        }
    }
}
