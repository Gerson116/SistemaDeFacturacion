using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.IVAServices
{
    public interface IIVA
    {
        Task<OperationResultRequest> GetAllBuscarIVA(int empresaId);
        Task<OperationResultRequest> PostNuevoIVA(List<IVADTO> iva);
        Task<OperationResultRequest> PutEditarIVA(IVADTO impuestoAlValorAgregado, int empresaIVAId);
        Task<OperationResultRequest> DeleteEliminarIVA(int empresaIVAId);
    }
}
