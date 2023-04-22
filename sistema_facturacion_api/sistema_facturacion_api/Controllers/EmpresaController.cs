using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.EmpresaServices;
using sistema_facturacion_api.Useful;

namespace sistema_facturacion_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("policy")]
    public class EmpresaController : ControllerBase
    {
        private IEmpresaServices _empresaServices;
        private OperationResultRequest _request;
        public EmpresaController(IEmpresaServices empresaServices)
        {
            _empresaServices = empresaServices;
            _request = new OperationResultRequest();
        }

        [HttpGet("GetAllEmpresas/{pagina}/{cantidadDeElemento}")]
        public async Task<ActionResult<OperationResultRequest>> GetAllEmpresas(int pagina, int cantidadDeElemento)
        {
            _request = await _empresaServices.GetAllEmpresas(pagina, cantidadDeElemento);
            if (_request.Succcess)
            {
                return _request;
            }
            return _request;
        }

        [HttpGet("GetEmpresaPorId/{empresaId}")]
        public async Task<ActionResult<OperationResultRequest>> GetEmpresaPorId(int empresaId)
        {
            _request = await _empresaServices.GetEmpresaPorId(empresaId);
            if (_request.Succcess)
            {
                return _request;
            }
            return _request;
        }

        [HttpPost("PostNuevaEmpresa")]
        public async Task<ActionResult<OperationResultRequest>> PostNuevaEmpresa([FromForm] TblEmpresasDTO empresas)
        {
            _request = await _empresaServices.PostNuevaEmpresa(empresas);
            if (_request.Succcess)
            {
                return _request;
            }
            return _request;
        }

        [HttpPut("PutEditarEmpresa/{empresaId}")]
        public async Task<ActionResult<OperationResultRequest>> PutEditarEmpresa([FromBody] TblEmpresaDatosEditablesDesdeElFrontDTO empresas, int empresaId)
        {
            _request = await _empresaServices.PutEditarEmpresa(empresas, empresaId);
            if (_request.Succcess)
            {
                return _request;
            }
            return _request;
        }

        [HttpPut("PutEditarLogoEmpresa/{empresaId}")]
        public async Task<ActionResult<OperationResultRequest>> PutEditarLogoEmpresa([FromForm] TblEmpresasDTO empresas, int empresaId)
        {
            _request = await _empresaServices.PutEditarLogoEmpresa(empresas, empresaId);
            if (_request.Succcess)
            {
                return _request;
            }
            return _request;
        }
    }
}
