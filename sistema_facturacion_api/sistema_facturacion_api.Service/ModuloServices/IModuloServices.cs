using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.ModuloServices
{
    public interface IModuloServices
    {
        Task<OperationResultRequest> GetAllModulos();
        Task<OperationResultRequest> PostNuevoModulo(TblModuloDTO nuevoModulo);
        Task<OperationResultRequest> PutEditarModulo(TblModuloDTO editarModulo, int moduloId);
        Task<OperationResultRequest> DeleteEliminarModulo(int moduloId);
    }
}
