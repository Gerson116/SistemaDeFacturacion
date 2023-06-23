using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.PermisosServices
{
    public interface IPermisosServices
    {
        Task<OperationResultRequest> GetAllPermisos(int usuarioId);
        Task<OperationResultRequest> PostAgregarPermisos(List<TblPermisoDTO> nuevoPermiso);
        Task<OperationResultRequest> PutEditarPermisos(TblPermisoDTO editarPermiso, int permisoId);
        Task<OperationResultRequest> EditarYAgregarPermisosExistentes(List<TblPermisoDTO> permisosUsuarios);
        Task<OperationResultRequest> DeletePermisos(int usuarioId, int permisoId);
    }
}
