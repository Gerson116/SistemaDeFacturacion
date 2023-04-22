using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.Archivo;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.CargarArchivoServices;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.EmpresaServices
{
    public class SEmpresaServices : IEmpresaServices
    {
        private IMapper _mapper;
        private FacturacionDbContext _dbContext;
        private OperationResultRequest _request;
        private ICargarArchivo _cargarArchivo;
        private readonly string _logoEmpresa = "logo-empresa";

        public SEmpresaServices(IMapper mapper, FacturacionDbContext dbContext, ICargarArchivo cargarArchivo)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _request = new OperationResultRequest();
            _cargarArchivo = cargarArchivo;
        }

        public async Task<OperationResultRequest> GetAllEmpresas(int pagina, int cantidadDeElementos)
        {
            try
            {
                IQueryable<TblEmpresas> query = _dbContext.Empresa
                    .Skip((pagina - 1) * cantidadDeElementos)
                    .Take(cantidadDeElementos)
                    .AsQueryable();

                List<TblEmpresasDTO> listadoDeEmpresas = new List<TblEmpresasDTO>();
                listadoDeEmpresas = _mapper.Map<List<TblEmpresasDTO>>(query.ToList());

                _request = RespuestaEsperada(listadoDeEmpresas, pagina, cantidadDeElementos, listadoDeEmpresas.Count);
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
                _request.Data = null;
            }
            return _request;
        }

        public async Task<OperationResultRequest> GetEmpresaPorId(int empresaId)
        {
            TblEmpresasDTO empresasDTO = new TblEmpresasDTO();
            try
            {
                TblEmpresas empresa = new TblEmpresas();
                empresa = await _dbContext.Empresa.FindAsync(empresaId);
                empresasDTO = _mapper.Map<TblEmpresasDTO>(empresa);

                _request = RespuestaEsperada(empresasDTO);
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
                _request.Data = null;
            }
            return _request;
        }

        public async Task<OperationResultRequest> PostNuevaEmpresa(TblEmpresasDTO nuevaEmpresa)
        {
            try
            {
                TblEmpresas empresa = new TblEmpresas();
                empresa = _mapper.Map<TblEmpresas>(nuevaEmpresa);
                byte[] contenido = await GenerarCopiaDelArchivo(nuevaEmpresa);
                empresa.rutaImagen = await _cargarArchivo.GuardarArchivo(
                    contenido: contenido,
                    extension: nuevaEmpresa.Archivo.FileName,
                    contenedor: _logoEmpresa,
                    contentType: nuevaEmpresa.Archivo.ContentType);
                empresa.FechaDeCreacion = DateTime.Now;
                await _dbContext.Empresa.AddAsync(empresa);
                await _dbContext.SaveChangesAsync();

                _request.Succcess = true;
                _request.Message = "Los datos fueron registrado con exito";
                _request.Data = empresa;
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
                _request.Data = null;
            }
            return _request;
        }

        public async Task<OperationResultRequest> PutEditarEmpresa(TblEmpresaDatosEditablesDesdeElFrontDTO editarEmpresa, int empresaId)
        {
            try
            {
                TblEmpresas empresas = new TblEmpresas();
                empresas = await _dbContext.Empresa.FindAsync(empresaId);

                if (empresas != null)
                {
                    empresas.TblUsuariosId = editarEmpresa.UsuarioId;
                    empresas.Nombre = editarEmpresa.Nombre;
                    _dbContext.Entry(empresas).State = EntityState.Modified;
                    _dbContext.SaveChangesAsync();
                    _request.Succcess = true;
                    _request.Message = "Se aplicaron los cambios de manera exitosa";
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "La empresa que esta intentando editar no existe.";
                    _request.Data = null;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
                _request.Data = null;
            }
            return _request;
        }

        public async Task<OperationResultRequest> PutEditarLogoEmpresa(TblEmpresasDTO editarEmpresa, int empresaId)
        {
            //...Este método debe permite editar las imagenes de las empresas que fueron subidas.
            try
            {
                TblEmpresas objEmpresa = new TblEmpresas();
                objEmpresa = await _dbContext.Empresa.FindAsync(empresaId);

                if (objEmpresa != null)
                {
                    TblEmpresas empresas = new TblEmpresas();
                    empresas = objEmpresa;
                    byte[] contenido = await GenerarCopiaDelArchivo(editarEmpresa);
                    empresas.rutaImagen = await _cargarArchivo.EditarArchivo(
                                                contenido: contenido,
                                                extension: editarEmpresa.Archivo.FileName,
                                                contenedor: _logoEmpresa,
                                                ruta: objEmpresa.rutaImagen,
                                                contentType: editarEmpresa.Archivo.ContentType);

                    _dbContext.Empresa.Entry(empresas).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    _request.Succcess = true;
                    _request.Message = "La imagen se modifico";
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "La imagen no fue modificada";
                    _request.Data = null;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
                _request.Data = null;
            }
            return _request;
        }

        private OperationResultRequest RespuestaEsperada(dynamic data)
        {
            if(data != null)
            {
                _request.Succcess = true;
                _request.Message = "Exito";
                _request.Data = data;
            }
            else
            {
                _request.Succcess = false;
                _request.Message = "No se encontraron datos registrados";
                _request.Data = null;
            }
            return _request;
        }

        private OperationResultRequest RespuestaEsperada(dynamic data, int pagina, int cantidadDeElementos, int totalDePaginas)
        {
            if (data != null && pagina >= 1 && cantidadDeElementos >= 5 && totalDePaginas >= 1)
            {
                _request.Succcess = true;
                _request.Message = "Exito";
                _request.Data = data;
                _request.Paginacion = new Pager(pagina, cantidadDeElementos, totalDePaginas);
            }
            else
            {
                _request.Succcess = false;
                _request.Message = "No se encontraron datos registrados";
                _request.Data = null;
            }
            return _request;
        }

        private async Task<byte[]> GenerarCopiaDelArchivo(Imagen imagen)
        {
            byte[] contenido = null;
            using (MemoryStream stream = new MemoryStream())
            {
                await imagen.Archivo.CopyToAsync(stream);
                contenido = stream.ToArray();
            }
            return contenido;
        }
    }
}
