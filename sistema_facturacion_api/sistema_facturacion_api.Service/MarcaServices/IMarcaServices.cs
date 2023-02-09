using Microsoft.AspNetCore.JsonPatch;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.MarcaServices
{
    public interface IMarcaServices
    {
        Task<OperationResultRequest> GetAllMarcas(int pagina, int cantidadDeElementos);
        Task<OperationResultRequest> GetMarcaPorId(int marcaId);
        Task<OperationResultRequest> PostNuevaMarca(List<TblMarcaDTO> nuevaMarca);
        Task<OperationResultRequest> PostBuscarMarca(ParametrosDeBusqueda parametros);
        Task<OperationResultRequest> PutEditarMarca(TblMarcaDTO editarMarca);
        Task<OperationResultRequest> PatchCambiarEstado(int marcaId, JsonPatchDocument<TblMarca> jsonPatch);
    }
}
