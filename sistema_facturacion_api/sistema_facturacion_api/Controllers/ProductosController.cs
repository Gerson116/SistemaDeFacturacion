using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        public ProductosController()
        {
        }
        [HttpGet("GetAllProductos")]
        public async Task<OperationResultRequest> GetAllProductos(int pagina, int cantidadDeElementos)
        {
            throw new NotImplementedException();
        }
        [HttpGet("GetProductosPorId/{productoId}")]
        public async Task<OperationResultRequest> GetProductosPorId(int productoId)
        {
            throw new NotImplementedException();
        }
        [HttpPost("PostNuevoProducto")]
        public async Task<OperationResultRequest> PostNuevoProducto([FromBody] List<TblProductosDTO> productos)
        {
            throw new NotImplementedException();
        }
        [HttpPost("PostBuscarProducto")]
        public async Task<OperationResultRequest> PostBuscarProducto([FromBody] TblProductosDTO productos)
        {
            throw new NotImplementedException();
        }
        [HttpPut("PutEditarProducto")]
        public async Task<OperationResultRequest> PutEditarProducto([FromBody] TblProductosDTO productos)
        {
            throw new NotImplementedException();
        }
        [HttpPatch("PatchCambiarEstado/{productoId}")]
        public async Task<OperationResultRequest> PatchCambiarEstado(int productoId, 
            [FromBody] JsonPatchDocument<TblProductos> jsonPatch)
        {
            throw new NotImplementedException();
        }
    }
}
