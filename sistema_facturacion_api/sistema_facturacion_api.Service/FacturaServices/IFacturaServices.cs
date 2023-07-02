using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.FacturaServices
{
    public interface IFacturaServices
    {
        Task<OperationResultRequest> GetAllFactura(int empresaId);
        Task<OperationResultRequest> GetFactura(int facturaId);
        Task<OperationResultRequest> BuscarFactura(int facturaId);
        Task<OperationResultRequest> GetCabeceraFactura(int facturaId);
        Task<OperationResultRequest> GetDetalleFactura(int facturaId);
        Task<OperationResultRequest> PostNuevoFactura(NuevaFacturaDTO nuevaFactura);
        Task<OperationResultRequest> DeleteFactura(int empresaId, int facturaId);
    }
}
