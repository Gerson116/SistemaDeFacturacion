using Azure;
using Microsoft.AspNetCore.JsonPatch;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.EmpresaServices
{
    public interface IEmpresaServices
    {
        Task<OperationResultRequest> GetAllEmpresas(int pagina = 1, int cantidadDeElementos = 10);
        Task<OperationResultRequest> GetEmpresaPorId(int empresaId);
        Task<OperationResultRequest> PostNuevaEmpresa(TblEmpresasDTO nuevaEmpresa);
        Task<OperationResultRequest> PutEditarEmpresa(TblEmpresaDatosEditablesDesdeElFrontDTO editarEmpresa, int empresaId);
        Task<OperationResultRequest> PutEditarLogoEmpresa(TblEmpresasDTO editarEmpresa, int empresaId);
    }
}
