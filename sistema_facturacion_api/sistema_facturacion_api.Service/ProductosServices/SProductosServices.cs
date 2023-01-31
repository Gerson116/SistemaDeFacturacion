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
        private TblProductos _productos;
        private IMapper _mapper;
        private FacturacionDbContext _dbContext;

        public SProductosServices(IMapper mapper, FacturacionDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<OperationResultRequest> GetAllProductos(int pagina, int cantidadDeElementos)
        {
            _operationResultRequest = new OperationResultRequest();
            List<TblProductos> productos = new List<TblProductos>();
            try
            {
                IQueryable<TblProductos> query = _dbContext.Producto.AsQueryable();
                productos = await query.Skip((pagina - 1) * cantidadDeElementos).Take(cantidadDeElementos).ToListAsync();
                _operationResultRequest.Succcess = true;
                _operationResultRequest.Message = "Exito";
                _operationResultRequest.Data = productos;
            }
            catch (Exception ex)
            {
                _operationResultRequest.Succcess = false;
                _operationResultRequest.Message = ex.Message;
                _operationResultRequest.Data = null;
            }
            throw new NotImplementedException();
        }

        public Task<OperationResultRequest> GetProductosPorId(int productoId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultRequest> PatchCambiarEstado(int productoId, JsonPatchDocument<TblProductos> jsonPatch)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultRequest> PostBuscarProducto(TblProductosDTO productos)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultRequest> PostNuevoProducto(List<TblProductosDTO> productos)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultRequest> PutEditarProducto(TblProductosDTO productos)
        {
            throw new NotImplementedException();
        }
    }
}
