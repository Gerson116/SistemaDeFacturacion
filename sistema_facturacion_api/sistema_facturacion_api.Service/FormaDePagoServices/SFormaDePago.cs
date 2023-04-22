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

namespace sistema_facturacion_api.Service.FormaDePagoServices
{
    public class SFormaDePago : IFormaDePago
    {
        private IMapper _mapper;
        private FacturacionDbContext _dbContext;
        private OperationResultRequest _request;
        private TblFormaDePago _formaDePago;

        public SFormaDePago(IMapper mapper, FacturacionDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _request = new OperationResultRequest();
        }

        public async Task<OperationResultRequest> DeleteFormaDePago(int formaDePagoId)
        {
            try
            {
                //...
                _formaDePago = await _dbContext.FormaDePago.FindAsync(formaDePagoId);
                if (_formaDePago != null)
                {
                    _formaDePago.Estado = false;
                    _dbContext.Entry(_formaDePago).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    _request.Succcess = true;
                    _request.Message = "Exito";
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "No se elimino el formato de pago debido a que no existe";
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

        public async Task<OperationResultRequest> GetAllFormaDePago()
        {
            try
            {
                IQueryable<TblFormaDePago> query = _dbContext.FormaDePago.AsQueryable();
                List<TblFormaDePago> formaDePago = new List<TblFormaDePago>();
                formaDePago = await query.ToListAsync();
                if (formaDePago != null)
                {
                    List<TblFormaDePagoDTO> formaDePagoDTO = new List<TblFormaDePagoDTO>();
                    formaDePagoDTO = _mapper.Map<List<TblFormaDePagoDTO>>(formaDePago);

                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = formaDePagoDTO;
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "No se encontraron los datos";
                    _request.Data = null;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error: {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> GetFormaDePagoId(int formaDePagoId)
        {
            try
            {
                _formaDePago = await _dbContext.FormaDePago.FindAsync(formaDePagoId);
                if (_formaDePago != null)
                {
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = _formaDePago;
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "No se encontro este metodo de pago";
                    _request.Data = null;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> PostFormaDePago(List<TblFormaDePagoDTO> formaDePago)
        {
            try
            {
                if (formaDePago != null)
                {
                    List<TblFormaDePago> nuevaFormaDePago = new List<TblFormaDePago>();
                    nuevaFormaDePago = _mapper.Map<List<TblFormaDePago>>(formaDePago);
                    nuevaFormaDePago.ForEach(x => x.FechaDeCreacion = DateTime.Now);
                    nuevaFormaDePago.ForEach(x => x.Estado = true);

                    await _dbContext.FormaDePago.AddRangeAsync(nuevaFormaDePago);
                    await _dbContext.SaveChangesAsync();

                    _request.Succcess = true;
                    _request.Message = "Exito";
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "Los datos no se encontraron";
                    _request.Data = null;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error: {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> ActivarFormaDePago(int formaDePagoId)
        {
            try
            {
                TblFormaDePago formaDePago = new TblFormaDePago();
                formaDePago = await _dbContext.FormaDePago.FindAsync(formaDePagoId);
                if (formaDePago != null)
                {
                    formaDePago.Estado = true;
                    _dbContext.Entry(formaDePago).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    _request.Succcess = true;
                    _request.Message = "Exito";
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> PutFormaDePago(TblFormaDePagoDTO formaDePago, int formaDePagoId)
        {
            try
            {
                _formaDePago = new TblFormaDePago();
                _formaDePago = await _dbContext.FormaDePago.FindAsync(formaDePagoId);
                if (_formaDePago != null)
                {
                    _formaDePago.Nombre = formaDePago.Nombre;
                    _dbContext.FormaDePago.Entry(_formaDePago).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "No encontro este metodo de pago.";
                    _request.Data = null;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
            }
            return _request;
        }
    }
}
