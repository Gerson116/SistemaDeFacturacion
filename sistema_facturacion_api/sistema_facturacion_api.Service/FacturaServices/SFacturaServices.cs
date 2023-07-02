using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Data.Enums;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace sistema_facturacion_api.Service.FacturaServices
{
    public class SFacturaServices : IFacturaServices
    {
        private FacturacionDbContext _dbContext;
        private IMapper _mapper;
        private OperationResultRequest _request;
        private List<TblFacturasDTO> _listFacturaDTO;
        private List<TblDetalleDeFacturas> _listDetalleFacturas;
        private List<TblDetalleDeFacturasDTO> _listDetalleFacturasDTO;
        private TblFacturas _factura;
        private TblDetalleDeFacturas _detalleFactura;
        private decimal _descuentoAplicable;

        public SFacturaServices(FacturacionDbContext dbContext, IMapper mapper)
        {
            //...
            _dbContext = dbContext;
            _mapper = mapper;
            _request = new OperationResultRequest();
            _listFacturaDTO = new List<TblFacturasDTO>();

            _factura = new TblFacturas();
            _detalleFactura = new TblDetalleDeFacturas();

            _listDetalleFacturas = new List<TblDetalleDeFacturas>();
            _listDetalleFacturasDTO = new List<TblDetalleDeFacturasDTO>();

            _descuentoAplicable = 0;
        }

        public async Task<OperationResultRequest> DeleteFactura(int empresaId, int facturaId)
        {
            try
            {
                _factura = await _dbContext.Factura.Where(x => x.EmpresaId == empresaId && x.Id == facturaId).FirstOrDefaultAsync();
                if (_factura != null)
                {
                    _factura.EstadoFacturaId = (int)EnumEstadoFacturas.Eliminada;
                    _dbContext.Entry(_factura).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = _factura;
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error: {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> GetAllFactura(int empresaId)
        {
            try
            {
                List<TblFacturas> factura = new List<TblFacturas>();
                IQueryable<TblFacturas> query = _dbContext.Factura
                    .Where(x => x.EmpresaId == empresaId && x.EstadoFacturaId == (int)EnumEstadoFacturas.Activo)
                    .AsQueryable();
                factura = await query.ToListAsync();
                _listFacturaDTO = _mapper.Map<List<TblFacturasDTO>>(factura);
                _request.Succcess = true;
                _request.Message = "Exito";
                _request.Data = _listFacturaDTO;
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error: {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> GetCabeceraFactura(int facturaId)
        {
            try
            {
                _factura = await _dbContext.Factura.Where(x => x.Id == facturaId).FirstOrDefaultAsync();

                if (_factura != null)
                {
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = _factura;
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "No se encontraron datos relacionados a la factura";
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error: {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> GetDetalleFactura(int facturaId)
        {
            try
            {
                IQueryable<TblDetalleDeFacturas> query = _dbContext.DetalleDeFactura.Where(x => x.FacturaId == facturaId)
                                                                   .Include(detalleFactura => detalleFactura.Producto)
                                                                   .AsQueryable();
                _listDetalleFacturasDTO = _mapper.Map<List<TblDetalleDeFacturasDTO>>(await query.ToListAsync());

                if (_listDetalleFacturasDTO.Count > 0)
                {
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = _listDetalleFacturasDTO;
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "No se encontraron datos relacionados a la factura";
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error: {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> GetFactura(int facturaId)
        {
            try
            {
                _factura = await _dbContext.Factura.Where(x => x.Id == facturaId).FirstOrDefaultAsync();
                IQueryable<TblDetalleDeFacturas> query = _dbContext.DetalleDeFactura.Where(x => x.FacturaId == facturaId)
                                                                   .Include(detalleFactura => detalleFactura.Producto)
                                                                   .AsQueryable();
                _listDetalleFacturasDTO = _mapper.Map<List<TblDetalleDeFacturasDTO>>(await query.ToListAsync());
                NuevaFacturaDTO factura = new NuevaFacturaDTO();
                factura.Factura = _mapper.Map<TblFacturasDTO>(_factura);
                factura.DetalleFactura = _listDetalleFacturasDTO;
                _request.Succcess = true;
                _request.Message = "Exito";
                _request.Data = factura;
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error: {ex.Message}";
            }
            return _request;
        }
        public async Task<OperationResultRequest> BuscarFactura(int facturaId)
        {
            try
            {
                List<TblFacturas> facturas = await _dbContext.Factura.Where(x => x.Id == facturaId).ToListAsync();
                _request.Succcess = true;
                _request.Message = "Exito";
                _request.Data = facturas;
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error: {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> PostNuevoFactura(NuevaFacturaDTO nuevaFactura)
        {
            try
            {
                _factura = _mapper.Map<TblFacturas>(nuevaFactura.Factura);
                _listDetalleFacturas = _mapper.Map<List<TblDetalleDeFacturas>>(nuevaFactura.DetalleFactura);

                _factura.FechaDeCreacion = DateTime.Now;

                //... Esta condicion verifica si le aplica descuento a la factura.
                if (_factura.Descuento > _descuentoAplicable)
                {
                    decimal subtotal = PorcentajeDeDescuentoAplicadoEnLaCompra(_factura.Descuento, _factura.SubTotal);
                    _factura.SubTotal = subtotal;
                    _factura.IVA = CalcularIVA(_factura.SubTotal, _factura.IVA);
                    _factura.LineaDePago = GenerarLineaDePago();
                    _factura.TotalPagado = _factura.SubTotal + _factura.IVA;
                }
                else
                {
                    _factura.IVA = CalcularIVA(_factura.SubTotal, _factura.IVA);
                    _factura.LineaDePago = GenerarLineaDePago();
                    _factura.TotalPagado = _factura.SubTotal + _factura.IVA;
                }
                _factura.EstadoFacturaId = (int)EnumEstadoFacturas.Activo;

                //... Calculando porcentaje del descuento.
                await _dbContext.Factura.AddAsync(_factura);
                await _dbContext.SaveChangesAsync();

                int facturaId = _factura.Id;

                _listDetalleFacturas.ForEach(x => x.FacturaId = _factura.Id);
                _listDetalleFacturas.ForEach(x => x.FechaDeRegistro = DateTime.Now);
                _listDetalleFacturas.ForEach(x => x.Producto = null);
                await _dbContext.DetalleDeFactura.AddRangeAsync(_listDetalleFacturas);
                await _dbContext.SaveChangesAsync();

                _request.Succcess = true;
                _request.Message = "Exito";
                //_request.Data
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error: {ex.Message}";
            }
            return _request;
        }

        private decimal CalcularIVA(decimal subTotal, decimal iva)
        {
            decimal ivaAplicado = (subTotal * iva) / 100;
            return ivaAplicado;
        }

        private string GenerarLineaDePago()
        {
            //... Este bloque de codigo genera la linea de pago correspondiente al año fiscal.
            //... ejemplo 20231 "Primero el año" y despues el numero total de factura +1
            string lineaDePago = string.Empty;
            try
            {
                int anoFiscal = DateTime.Now.Year;
                int facturasEmitidas = _dbContext.Factura.Count() + 1;
                lineaDePago = $"{anoFiscal}{facturasEmitidas}";
                int consultaLineaDePagoExistente = _dbContext.Factura.Where(x => x.LineaDePago == lineaDePago).Count();
                if (consultaLineaDePagoExistente > 0)
                {
                    lineaDePago = $"{anoFiscal}{facturasEmitidas + 1}";
                }
            }
            catch (Exception ex)
            {
            }

            return lineaDePago;
        }

        private decimal PorcentajeDeDescuentoAplicadoEnLaCompra(decimal descuento, decimal subTotal)
        {
            decimal porcentaje = (subTotal * descuento) / 100;
            return subTotal - porcentaje;
        }
    }
}
