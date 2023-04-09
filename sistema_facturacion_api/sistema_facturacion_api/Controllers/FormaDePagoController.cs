using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.FormaDePagoServices;
using sistema_facturacion_api.Useful;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormaDePagoController : ControllerBase
    {
        private IFormaDePago _formaDePagoServices;
        private OperationResultRequest _resultRequest;

        public FormaDePagoController(IFormaDePago formaDePagoServices)
        {
            //...
            _formaDePagoServices = formaDePagoServices;
            _resultRequest = new OperationResultRequest();
        }

        [HttpGet("GetAllFormaDePago")]
        public async Task<ActionResult<OperationResultRequest>> GetAllFormaDePago()
        {
            _resultRequest = await _formaDePagoServices.GetAllFormaDePago();
            if (_resultRequest.Succcess)
            {
                return Ok(_resultRequest);
            }
            return BadRequest(_resultRequest);
        }

        [HttpPost("PostFormaDePago")]
        public async Task<ActionResult<OperationResultRequest>> PostFormaDePago([FromBody]List<TblFormaDePagoDTO> formaDePago)
        {
            _resultRequest = await _formaDePagoServices.PostFormaDePago(formaDePago);
            if (_resultRequest.Succcess)
            {
                return Ok(_resultRequest);
            }
            return BadRequest(_resultRequest);
        }

        [HttpPut("PutFormaDePago/{formaDePagoId}")]
        public async Task<ActionResult<OperationResultRequest>> PutFormaDePago(TblFormaDePagoDTO formaDePago, int formaDePagoId)
        {
            _resultRequest = await _formaDePagoServices.GetAllFormaDePago();
            if (_resultRequest.Succcess)
            {
                return Ok(_resultRequest);
            }
            return BadRequest(_resultRequest);
        }

        [HttpDelete("DeleteFormaDePago/{formaDePagoId}")]
        public async Task<ActionResult<OperationResultRequest>> DeleteFormaDePago(int formaDePagoId)
        {
            _resultRequest = await _formaDePagoServices.DeleteFormaDePago(formaDePagoId);
            if (_resultRequest.Succcess)
            {
                return Ok(_resultRequest);
            }
            return BadRequest(_resultRequest);
        }

        [HttpPut("PutActivarFormaDePago/{formaDePagoId}")]
        public async Task<ActionResult<OperationResultRequest>> ActivarFormaDePago(int formaDePagoId)
        {
            _resultRequest = await _formaDePagoServices.ActivarFormaDePago(formaDePagoId);
            if (_resultRequest.Succcess)
            {
                return Ok(_resultRequest);
            }
            return BadRequest(_resultRequest);
        }
    }
}
