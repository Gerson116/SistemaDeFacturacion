using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.ModuloServices
{
    public class SModuloServices : IModuloServices
    {
        private FacturacionDbContext _dbcontext;
        private IMapper _mapper;
        private TblModulo _modulo;
        private OperationResultRequest _result;
        public SModuloServices(IMapper mapper, FacturacionDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _modulo = new TblModulo();
            _mapper = mapper;
            _result = new OperationResultRequest();
        }

        public async Task<OperationResultRequest> DeleteEliminarModulo(int moduloId)
        {
            try
            {
                _modulo = await _dbcontext.Modulo.FindAsync(moduloId);
                if (_modulo != null)
                {
                    _modulo.Estado = false;
                    _dbcontext.Entry(_modulo).State = EntityState.Modified;
                    await _dbcontext.SaveChangesAsync();
                    _result.Succcess = true;
                    _result.Message = "Exito";
                }
                else
                {
                    _result.Succcess = false;
                    _result.Message = "No se encontro el modulo que esta buscando.";
                }
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
                List<TblModulo> modulos = new List<TblModulo>();
                IQueryable<TblModulo> query = _dbcontext.Modulo.AsQueryable();
                modulos = await query.ToListAsync();

                List<TblModuloDTO> moduloDTO = new List<TblModuloDTO>();
                moduloDTO = _mapper.Map<List<TblModuloDTO>>(modulos);

                _result.Succcess = true;
                _result.Message = "Exito";
                _result.Data = moduloDTO;
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
                if (nuevoModulo != null)
                {
                    List<TblModulo> modulos = new List<TblModulo>();
                    modulos = _mapper.Map<List<TblModulo>>(nuevoModulo);
                    modulos.ForEach(x => x.Estado = true);
                    modulos.ForEach(x => x.FechaDeCreacion = DateTime.Now);

                    await _dbcontext.Modulo.AddRangeAsync(modulos);
                    await _dbcontext.SaveChangesAsync();

                    _result.Succcess = true;
                    _result.Message = "Exito";
                    _result.Data = modulos;
                }
                else
                {
                    _result.Succcess = false;
                    _result.Message = "No se pueden enviar datos nulos";
                }
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
                _modulo = await _dbcontext.Modulo.FindAsync(moduloId);
                if (_modulo != null)
                {
                    _modulo.Nombre = editarModulo.Nombre;
                    _modulo.Ruta = editarModulo.Ruta;
                    _dbcontext.Entry(_modulo).State = EntityState.Modified;
                    await _dbcontext.SaveChangesAsync();

                    _result.Succcess = true;
                    _result.Message = "Exito";
                }
                else
                {
                    _result.Succcess = false;
                    _result.Message = "No se permiten datos nulos.";
                }
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
