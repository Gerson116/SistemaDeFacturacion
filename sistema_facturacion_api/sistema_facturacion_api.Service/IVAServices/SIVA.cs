using AutoMapper;
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

namespace sistema_facturacion_api.Service.IVAServices
{
    public class SIVA : IIVA
    {
        private FacturacionDbContext _dbcontext;
        private IMapper _mapper;
        private TblImpuestoAlValorAgregado _iva;
        private OperationResultRequest _result;

        public SIVA(IMapper mapper, FacturacionDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _iva = new TblImpuestoAlValorAgregado();
            _result = new OperationResultRequest();
        }

        public async Task<OperationResultRequest> GetAllBuscarIVA(int empresaId)
        {
            try
            {
                IQueryable<TblImpuestoAlValorAgregado> query = _dbcontext.ImpuestoAlValorAgregado.Where(x => x.EmpresaId == empresaId).AsQueryable();
                List<TblImpuestoAlValorAgregado> listadoDeIVA = await query.ToListAsync();
                if (listadoDeIVA != null)
                {
                    _result.Succcess = true;
                    _result.Message = "Exito";
                    _result.Data = listadoDeIVA;
                }
                else
                {
                    _result.Succcess = false;
                    _result.Message = "No se encontraron datos";
                    _result.Data = "No existe iva registrado para esta empresa, debe registrar uno.";
                }
            }
            catch (Exception ex)
            {
                _result.Succcess = false;
                _result.Message = $"Ocurrio un error {ex.Message}";
            }
            return _result;
        }

        public async Task<OperationResultRequest> PostNuevoIVA(List<IVADTO> iva)
        {
            try
            {
                List<TblImpuestoAlValorAgregado> nuevoIva = new List<TblImpuestoAlValorAgregado>();
                nuevoIva = _mapper.Map<List<TblImpuestoAlValorAgregado>>(iva);
                if (nuevoIva != null)
                {
                    nuevoIva.ForEach(x => x.Estado = true);
                    await _dbcontext.ImpuestoAlValorAgregado.AddRangeAsync(nuevoIva);
                    await _dbcontext.SaveChangesAsync();
                    _result.Succcess = true;
                    _result.Message = "Exito";
                    _result.Data = nuevoIva;
                }
                else
                {
                    _result.Succcess = false;
                    _result.Message = "Se requieren los datos para poder guardarlo.";
                }
            }
            catch (Exception ex)
            {
                _result.Succcess = false;
                _result.Message = $"Ocurrio un error {ex.Message}";
            }
            return _result;
        }

        public async Task<OperationResultRequest> PutEditarIVA(IVADTO impuestoAlValorAgregado, int empresaIVAId)
        {
            try
            {
                TblImpuestoAlValorAgregado editarIVA = new TblImpuestoAlValorAgregado();
                editarIVA = await _dbcontext.ImpuestoAlValorAgregado.FindAsync(empresaIVAId);
                if (editarIVA != null)
                {
                    editarIVA.IVA = impuestoAlValorAgregado.IVA;
                    _dbcontext.Entry(editarIVA).State = EntityState.Modified;
                    await _dbcontext.SaveChangesAsync();
                    _result.Succcess = true;
                    _result.Message = "Exito";
                }
                else
                {
                    _result.Succcess = false;
                    _result.Message = "No se encontro el dato que desea modificar.";
                }
            }
            catch (Exception ex)
            {
                _result.Succcess = false;
                _result.Message = $"Ocurrio un error {ex.Message}";
            }
            return _result;
        }

        public async Task<OperationResultRequest> DeleteEliminarIVA(int empresaIVAId)
        {
            try
            {
                TblImpuestoAlValorAgregado editarIVA = new TblImpuestoAlValorAgregado();
                editarIVA = await _dbcontext.ImpuestoAlValorAgregado.FindAsync(empresaIVAId);
                if (editarIVA != null)
                {
                    editarIVA.Estado = false;
                    _dbcontext.Entry(editarIVA).State = EntityState.Modified;
                    await _dbcontext.SaveChangesAsync();
                    _result.Succcess = true;
                    _result.Message = "Exito";
                }
                else
                {
                    _result.Succcess = false;
                    _result.Message = "No se encontro el dato que desea modificar.";
                }
            }
            catch (Exception ex)
            {
                _result.Succcess = false;
                _result.Message = $"Ocurrio un error {ex.Message}";
            }
            return _result;
        }
    }
}
