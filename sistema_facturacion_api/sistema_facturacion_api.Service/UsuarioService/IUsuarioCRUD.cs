
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

namespace sistema_facturacion_api.Service.UsuarioService
{
    public interface IUsuarioCRUD
    {
        Task<OperationResultRequest> ListadoDeUsuarios(int page, int cantidadDeElemento);
        Task<OperationResultRequest> PerfilUsuario(int usuarioId);
        Task<OperationResultRequest> AgregarUsuario(TblUsuariosDTO usuario);
        Task<OperationResultRequest> EditarUsuario(TblUsuariosDTO usuario);
        Task<OperationResultRequest> CambiarEstadoUsuario(int usuarioId, JsonPatchDocument<TblUsuarios> usuariosPatch);
    }
}
