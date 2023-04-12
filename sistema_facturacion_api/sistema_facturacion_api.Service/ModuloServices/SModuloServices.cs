using AutoMapper;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.ModuloServices
{
    public class SModuloServices : IModuloServices
    {
        private FacturacionDbContext _dbcontext;
        private IMapper _mapper;
        private TblModulo _modulo;
        private IModuloServices _moduloServices;
        private OperationResultRequest _result;
        public SModuloServices(IModuloServices moduloServices, IMapper mapper, FacturacionDbContext dbcontext)
        {
            _moduloServices = moduloServices;
            _dbcontext = dbcontext;
            _modulo = new TblModulo();
            _mapper = mapper;
        }

        public async Task<OperationResultRequest> DeleteEliminarModulo(int moduloId)
        {
            try
            {
            }
            catch (Exception ex)
            {
                _result.Succcess = false;
                _result.Message = ex.Message;
            }
            return _result;
        }

        public async Task<OperationResultRequest> GetAllModulos()
        {
            try
            {
            }
            catch (Exception ex)
            {
                _result.Succcess = false;
                _result.Message = ex.Message;
            }
            return _result;
        }

        public async Task<OperationResultRequest> PostNuevoModulo(List<TblModuloDTO> nuevoModulo)
        {
            try
            {
            }
            catch (Exception ex)
            {
                _result.Succcess = false;
                _result.Message = ex.Message;
            }
            return _result;
        }

        public async Task<OperationResultRequest> PutEditarModulo(TblModuloDTO editarModulo, int moduloId)
        {
            try
            {
            }
            catch (Exception ex)
            {
                _result.Succcess = false;
                _result.Message = ex.Message;
            }
            return _result;
        }
    }
}
