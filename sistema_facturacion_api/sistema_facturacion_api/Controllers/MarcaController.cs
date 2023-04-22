using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Service.MarcaServices;
using sistema_facturacion_api.Useful;
using sistema_facturacion_api.Validation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("policy")]
    public class MarcaController : ControllerBase
    {
        private IMarcaServices _marcaServices;
        private OperationResultRequest _operationResultRequest;

        public MarcaController(IMarcaServices marcaServices)
        {
            _marcaServices = marcaServices;
            _operationResultRequest = new OperationResultRequest();
        }
        [HttpGet("GetAllMarcas/{pagina}/{cantidadDeElementos}")]
        public async Task<ActionResult<OperationResultRequest>> GetAllMarcas(int pagina, int cantidadDeElementos)
        {
            _operationResultRequest = await _marcaServices.GetAllMarcas(pagina: pagina, cantidadDeElementos: cantidadDeElementos);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpGet("GetMarcaPorId/{marcaId}")]
        public async Task<ActionResult<OperationResultRequest>> GetMarcaPorId(int marcaId)
        {
            _operationResultRequest = await _marcaServices.GetMarcaPorId(marcaId: marcaId);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpPost("PostNuevaMarca")]
        public async Task<ActionResult<OperationResultRequest>> PostNuevaMarca([FromBody] List<TblMarcaDTO> marcas)
        {
            MarcaValidation validation = new MarcaValidation();
            foreach (var item in marcas)
            {
                var result = validation.Validate(item);
                if (!result.IsValid)
                {
                    _operationResultRequest.Succcess = true;
                    _operationResultRequest.Message = "Ocurrio un error";
                    _operationResultRequest.Data = await Validaciones(result.Errors);
                    return _operationResultRequest;
                }
            }
            _operationResultRequest = await _marcaServices.PostNuevaMarca(marcas);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpPost("PostBuscarMarca")]
        public async Task<ActionResult<OperationResultRequest>> PostBuscarMarca([FromBody] ParametrosDeBusqueda parametros)
        {
            _operationResultRequest = await _marcaServices.PostBuscarMarca(parametros);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpPut("PutEditarMarca")]
        public async Task<ActionResult<OperationResultRequest>> PutEditarMarca([FromBody] TblMarcaDTO marcas)
        {
            _operationResultRequest = await _marcaServices.PutEditarMarca(marcas);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpPatch("PatchCambiarEstado/{marcaId}")]
        public async Task<ActionResult<OperationResultRequest>> PatchCambiarEstado(int marcaId,
            [FromBody] JsonPatchDocument<TblMarca> jsonPatch)
        {
            _operationResultRequest = await _marcaServices.PatchCambiarEstado(marcaId, jsonPatch);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        private async Task<List<ErrorMessageValidation>> Validaciones(List<ValidationFailure> validation)
        {
            List<ErrorMessageValidation> errors = new List<ErrorMessageValidation>();
            foreach (var item in validation)
            {
                errors.Add(new ErrorMessageValidation { Message = item.ErrorMessage, PropertyName = item.PropertyName });
            }
            return errors;
        }
    }
}
