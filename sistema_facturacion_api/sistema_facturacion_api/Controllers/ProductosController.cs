using Azure;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.ProductosServices;
using sistema_facturacion_api.Useful;
using sistema_facturacion_api.Validation;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("policy")]
    public class ProductosController : ControllerBase
    {
        private IProductosServices _productosServices;
        private OperationResultRequest _operationResultRequest;

        public ProductosController(IProductosServices productosServices)
        {
            _productosServices = productosServices;
            _operationResultRequest = new OperationResultRequest();
        }
        [HttpGet("GetAllProductos/{pagina}/{cantidadDeElementos}")]
        public async Task<ActionResult<OperationResultRequest>> GetAllProductos(int pagina, int cantidadDeElementos)
        {
            _operationResultRequest = await _productosServices.GetAllProductos(pagina: pagina, cantidadDeElementos: cantidadDeElementos);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpGet("GetProductosPorId/{productoId}")]
        public async Task<ActionResult<OperationResultRequest>> GetProductosPorId(int productoId)
        {
            _operationResultRequest = await _productosServices.GetProductosPorId(productoId: productoId);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpPost("PostNuevoProducto")]
        public async Task<ActionResult<OperationResultRequest>> PostNuevoProducto([FromBody] List<TblProductosDTO> productos)
        {
            ProductosValidation validation = new ProductosValidation();
            foreach (var item in productos)
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
            _operationResultRequest = await _productosServices.PostNuevoProducto(productos);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpPost("PostBuscarProducto")]
        public async Task<ActionResult<OperationResultRequest>> PostBuscarProducto([FromBody] ParametrosDeBusqueda parametros)
        {
            _operationResultRequest = await _productosServices.PostBuscarProducto(parametros);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpPut("PutEditarProducto")]
        public async Task<ActionResult<OperationResultRequest>> PutEditarProducto([FromBody] TblProductosDTO productos)
        {
            _operationResultRequest = await _productosServices.PutEditarProducto(productos);
            if (_operationResultRequest.Succcess == true)
            {
                return Ok(_operationResultRequest);
            }
            return BadRequest(_operationResultRequest);
        }
        [HttpPatch("PatchCambiarEstado/{productoId}")]
        public async Task<ActionResult<OperationResultRequest>> PatchCambiarEstado(int productoId, 
            [FromBody] JsonPatchDocument<TblProducto> jsonPatch)
        {
            _operationResultRequest = await _productosServices.PatchCambiarEstado(productoId, jsonPatch);
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
                errors.Add(new ErrorMessageValidation { Message = item.ErrorMessage, PropertyName = item.PropertyName});
            }
            return errors;
        }
    }
}
