using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.ProductosServices
{
    public class SProductosServices : IProductosServices
    {
        private OperationResultRequest _operationResultRequest;
        private TblProductosDTO _productos;
        private List<TblProductosDTO> _listadoDeProductos;
        private IMapper _mapper;
        private FacturacionDbContext _dbContext;

        public SProductosServices(IMapper mapper, FacturacionDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _operationResultRequest = new OperationResultRequest();
            _productos = new TblProductosDTO();
            _listadoDeProductos = new List<TblProductosDTO>();
        }

        public async Task<OperationResultRequest> GetAllProductos(int pagina, int cantidadDeElementos)
        {
            List<TblProducto> productos = new List<TblProducto>();
            try
            {
                IQueryable<TblProducto> query = _dbContext.Producto.AsQueryable();
                productos = await query.Skip((pagina - 1) * cantidadDeElementos).Take(cantidadDeElementos).ToListAsync();
                if (productos.Count > 0)
                {
                    _operationResultRequest.Succcess = true;
                    _operationResultRequest.Message = "Exito";
                    _operationResultRequest.Data = productos;
                }
                else
                {
                    _operationResultRequest.Succcess = false;
                    _operationResultRequest.Data = null;
                }
            }
            catch (Exception ex)
            {
                _operationResultRequest.Succcess = false;
                _operationResultRequest.Message = ex.Message;
                _operationResultRequest.Data = null;
            }
            return _operationResultRequest;
        }
        public async Task<OperationResultRequest> GetProductosPorId(int productoId)
        {
            try
            {
                TblProducto datos = new TblProducto();
                datos = await _dbContext.Producto.FindAsync(productoId);
                _productos = _mapper.Map<TblProductosDTO>(datos);
                if (datos != null)
                {
                    _operationResultRequest.Succcess = true;
                    _operationResultRequest.Message = "Exito";
                    _operationResultRequest.Data = _productos;
                }
                else
                {
                    _operationResultRequest.Succcess = false;
                    _operationResultRequest.Message = $"El id: {productoId} no existe";
                    _operationResultRequest.Data = null;
                }
            }
            catch (Exception ex)
            {
                _operationResultRequest.Succcess = false;
                _operationResultRequest.Message = ex.Message;
                _operationResultRequest.Data = null;
            }
            return _operationResultRequest;
        }
        public async Task<OperationResultRequest> PatchCambiarEstado(int productoId, JsonPatchDocument<TblProducto> jsonPatch)
        {
            try
            {
                TblProducto datos = new TblProducto();
                datos = await _dbContext.Producto.FindAsync(productoId);
                if (jsonPatch != null)
                {
                    jsonPatch.ApplyTo(datos);
                    await _dbContext.SaveChangesAsync();

                    _operationResultRequest.Succcess = true;
                    _operationResultRequest.Message = "Exito";
                    _operationResultRequest.Data = null;
                }
                else
                {
                    _operationResultRequest.Succcess = false;
                    _operationResultRequest.Message = "Ocurrio un error";
                    _operationResultRequest.Data = null;
                }
            }
            catch (Exception ex)
            {
                _operationResultRequest.Succcess = false;
                _operationResultRequest.Message = ex.Message;
                _operationResultRequest.Data = null;
            }
            return _operationResultRequest;
        }
        public async Task<OperationResultRequest> PostBuscarProducto(ParametrosDeBusqueda parametros)
        {
            try
            {
                TblProducto datos = new TblProducto();
                if (parametros.Id > 0)
                {
                    datos = await _dbContext.Producto.FindAsync(parametros.Id);
                    _productos = _mapper.Map<TblProductosDTO>(datos);

                    _operationResultRequest.Succcess = true;
                    _operationResultRequest.Message = "Exito";
                    _operationResultRequest.Data = _productos;
                }
                if (parametros != null)
                {
                    List<TblProducto> productos = new List<TblProducto>();
                    productos = await _dbContext.Producto.Where(p => p.Nombre.Contains(parametros.Nombre)).ToListAsync();
                    _listadoDeProductos = _mapper.Map<List<TblProductosDTO>>(productos);

                    _operationResultRequest.Succcess = true;
                    _operationResultRequest.Message = "Exito";
                    _operationResultRequest.Data = _listadoDeProductos;
                }
            }
            catch (Exception ex)
            {
                _operationResultRequest.Succcess = false;
                _operationResultRequest.Message = ex.Message;
                _operationResultRequest.Data = null;
            }
            return _operationResultRequest;
        }
        public async Task<OperationResultRequest> PostNuevoProducto(List<TblProductosDTO> productos)
        {
            try
            {
                List<TblProducto> datos = new List<TblProducto>();
                datos = _mapper.Map<List<TblProducto>>(productos);
                datos.ForEach(x => x.FechaDeCreacion = DateTime.Now);
                await _dbContext.Producto.AddRangeAsync(datos);
                await _dbContext.SaveChangesAsync();

                _operationResultRequest.Succcess = true;
                _operationResultRequest.Message = "Exito";
                _operationResultRequest.Data = datos;
            }
            catch (Exception ex)
            {
                _operationResultRequest.Succcess = false;
                _operationResultRequest.Message = ex.Message;
                _operationResultRequest.Data = null;
            }
            return _operationResultRequest;
        }
        public async Task<OperationResultRequest> PutEditarProducto(TblProductosDTO productos)
        {
            try
            {
                TblProducto datos = new TblProducto();
                datos = _mapper.Map<TblProducto>(productos);
                _dbContext.Producto.Entry(datos).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                _operationResultRequest.Succcess = true;
                _operationResultRequest.Message = "Exito";
                _operationResultRequest.Data = datos;
            }
            catch (Exception ex)
            {
                _operationResultRequest.Succcess = false;
                _operationResultRequest.Message = ex.Message;
                _operationResultRequest.Data = null;
            }
            return _operationResultRequest;
        }
    }
}
