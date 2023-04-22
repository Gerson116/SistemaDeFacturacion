using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.FormaDePagoServices
{
    public interface IFormaDePago
    {
        Task<OperationResultRequest> GetAllFormaDePago();
        Task<OperationResultRequest> GetFormaDePagoId(int formaDePagoId);
        Task<OperationResultRequest> PostFormaDePago(List<TblFormaDePagoDTO> formaDePago);
        Task<OperationResultRequest> PutFormaDePago(TblFormaDePagoDTO formaDePago, int formaDePagoId);
        Task<OperationResultRequest> DeleteFormaDePago(int formaDePagoId);
        Task<OperationResultRequest> ActivarFormaDePago(int formaDePagoId);
    }
}
