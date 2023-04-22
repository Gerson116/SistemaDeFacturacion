using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Data.Enums;
using sistema_facturacion_api.Useful;
using System.Linq;

namespace sistema_facturacion_api.Service.MarcaServices
{
    public class SMarcaServices : IMarcaServices
    {
        private FacturacionDbContext _dbContext;
        private IMapper _mapper;
        private TblMarcaDTO _marca;
        private OperationResultRequest _request;

        public SMarcaServices(FacturacionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _marca = new TblMarcaDTO();
            _request = new OperationResultRequest();
        }
        public async Task<OperationResultRequest> GetAllMarcas(int pagina, int cantidadDeElementos)
        {
            try
            {
                IQueryable<TblMarca> query = _dbContext.Marca.AsQueryable();
                List<TblMarca> listaDeMarcas = new List<TblMarca>();
                listaDeMarcas = await query
                    .Skip((pagina - 1) * cantidadDeElementos)
                    .Take(cantidadDeElementos).ToListAsync();

                if (listaDeMarcas.Count > 0)
                {
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = listaDeMarcas;
                    _request.Paginacion = new Pager(numeroDePagina: pagina,
                        cantidadDeElementos: cantidadDeElementos,
                        totalElementos: query.Count());
                }
                else
                {
                    _request.Succcess = false;
                    _request.Data = null;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = ex.Message;
                _request.Data = null;
            }
            return _request;
        }
        public async Task<OperationResultRequest> GetMarcaPorId(int marcaId)
        {
            try
            {
                TblMarca marca = new TblMarca();
                marca = await _dbContext.Marca.FindAsync(marcaId);
                _marca = _mapper.Map<TblMarcaDTO>(marca);
                if (_marca == null)
                {
                    _request.Succcess = false;
                    _request.Message = "No existe un producto con este ID";
                    _request.Data = _marca;
                }
                else
                {
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = _marca;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = ex.Message;
                _request.Data = null;
            }
            return _request;
        }
        public async Task<OperationResultRequest> PatchCambiarEstado(int marcaId, JsonPatchDocument<TblMarca> jsonPatch)
        {
            try
            {
                TblMarca tblMarca = new TblMarca();
                tblMarca = await _dbContext.Marca.FindAsync(marcaId);
                if (jsonPatch != null)
                {
                    jsonPatch.ApplyTo(tblMarca);
                    await _dbContext.SaveChangesAsync();

                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = null;
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "Ocurrio un error";
                    _request.Data = null;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = ex.Message;
                _request.Data = null;
            }
            return _request;
        }
        public async Task<OperationResultRequest> PostBuscarMarca(ParametrosDeBusqueda parametros)
        {
            try
            {
                TblMarca datos = new TblMarca();
                if (parametros.Id > 0)
                {
                    datos = await _dbContext.Marca.FindAsync(parametros.Id);
                    _marca = _mapper.Map<TblMarcaDTO>(datos);

                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = _marca;
                }
                if (parametros.Nombre != null)
                {
                    List<TblMarca> marcas = new List<TblMarca>();
                    marcas = await _dbContext.Marca.Where(x => x.Nombre.Contains(parametros.Nombre)).ToListAsync();
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = marcas;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = ex.Message;
                _request.Data = null;
            }
            return _request;
        }
        public async Task<OperationResultRequest> PostNuevaMarca(List<TblMarcaDTO> nuevaMarca)
        {
            try
            {
                if (nuevaMarca != null)
                {
                    List<TblMarca> marcas = new List<TblMarca>();
                    marcas = _mapper.Map<List<TblMarca>>(nuevaMarca);
                    marcas.ForEach(x => x.FechaDeCreacion = DateTime.Now);

                    await _dbContext.Marca.AddRangeAsync(marcas);
                    await _dbContext.SaveChangesAsync();

                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = marcas;
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "No se aceptan valores nulos";
                    _request.Data = null;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = ex.Message;
                _request.Data = null;
            }
            return _request;
        }
        public async Task<OperationResultRequest> PutEditarMarca(TblMarcaDTO editarMarca)
        {
            try
            {
                TblMarca marca = new TblMarca();
                marca = await _dbContext.Marca.FindAsync(editarMarca.Id);
                marca.Nombre = editarMarca.Nombre;

                _dbContext.Marca.Entry(marca).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                _request.Succcess = true;
                _request.Message = "Exito";
                _request.Data = marca;
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = ex.Message;
                _request.Data = null;
            }
            return _request;
        }
    }
}
