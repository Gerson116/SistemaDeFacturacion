using Microsoft.AspNetCore.JsonPatch;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.ProductosServices
{
    public interface IProductosServices
    {
        Task<OperationResultRequest> GetAllProductos(int pagina, int cantidadDeElementos);
        Task<OperationResultRequest> GetProductosPorId(int productoId);
        Task<OperationResultRequest> PostNuevoProducto(List<TblProductosDTO> productos);
        Task<OperationResultRequest> PostBuscarProducto(ParametrosDeBusqueda parametros);
        Task<OperationResultRequest> PutEditarProducto(TblProductosDTO productos);
        Task<OperationResultRequest> PatchCambiarEstado(int productoId, JsonPatchDocument<TblProducto> jsonPatch);
    }
}
