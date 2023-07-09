using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Data.Enums;
using sistema_facturacion_api.Useful;
using sistema_facturacion_api.Useful.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace sistema_facturacion_api.Service.UsuarioService
{
    public class UsuarioCRUD : IUsuarioCRUD
    {
        private FacturacionDbContext _dbContext;
        private IMapper _mapper;
        private TblUsuarios _usuarios;
        private OperationResultRequest _operationResult;
        private string _mesajeExitoso = "Exito";
        private Encrypt _encriptarPass;
        public UsuarioCRUD(FacturacionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _usuarios = new TblUsuarios();
            _encriptarPass = new Encrypt();
        }

        public async Task<OperationResultRequest> AgregarUsuario(TblUsuariosDTO usuario)
        {
            _operationResult = new OperationResultRequest();
            try
            {
                _usuarios = _mapper.Map<TblUsuarios>(usuario);
                _usuarios.FechaDeCreacion = DateTime.Now;
                _usuarios.Password = _encriptarPass.EncriptingPassword(usuario.Password);

                bool resp = await ValidarDocumento(documentoDeIdentidad: _usuarios.TarjetaDeIdentificacion,
                    pasaporte: _usuarios.Pasaporte, usuario.Email);
                if (!resp)
                {
                    await _dbContext.Usuario.AddAsync(_usuarios);
                    await _dbContext.SaveChangesAsync();
                    _operationResult.Succcess = true;
                    _operationResult.Message = _mesajeExitoso;
                    _operationResult.Data = usuario;
                }
                else if (_usuarios.TarjetaDeIdentificacion == null && _usuarios.Pasaporte == null)
                {
                    _operationResult.Succcess = false;
                    _operationResult.Message = "Ocurrio un error";
                    _operationResult.Data = $"Los campos TarjetaDeIdentificacion o Pasaporte debe tener datos.";
                }
                else if (_usuarios.Email == null)
                {
                    _operationResult.Succcess = false;
                    _operationResult.Message = "Ocurrio un error";
                    _operationResult.Data = $"Debe ingresar un email.";
                }
                else
                {
                    _operationResult.Succcess = false;
                    _operationResult.Message = "Ocurrio un error";
                    _operationResult.Data = $"La cedula: {_usuarios.TarjetaDeIdentificacion}, el pasaporte: {_usuarios.Pasaporte} o el email: {_usuarios.Email} ya existe";
                }
            }
            catch (Exception ex)
            {
                _operationResult.Succcess = false;
                _operationResult.Message = ex.Message;
            }
            return _operationResult;
        }
        public async Task<OperationResultRequest> EditarUsuario(TblUsuariosDTO usuario)
        {
            _operationResult = new OperationResultRequest();
            try
            {
                _usuarios = _mapper.Map<TblUsuarios>(usuario);
                _usuarios.Password = _encriptarPass.EncriptingPassword(usuario.Password);

                _dbContext.Usuario.Entry(_usuarios).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                _operationResult.Succcess = true;
                _operationResult.Data = _usuarios;
                _operationResult.Message = _mesajeExitoso;
            }
            catch (Exception ex)
            {
                _operationResult.Succcess = false;
                _operationResult.Message = ex.Message;
            }
            return _operationResult;
        }
        public async Task<OperationResultRequest> CambiarEstadoUsuario(int usuarioId, JsonPatchDocument<TblUsuarios> usuariosPatch)
        {
            _operationResult = new OperationResultRequest();
            _usuarios = new TblUsuarios();
            try
            {
                _usuarios = await _dbContext.Usuario.FindAsync(usuarioId);
                if (usuariosPatch != null)
                {
                    if (_usuarios != null)
                    {
                        usuariosPatch.ApplyTo(_usuarios);
                        await _dbContext.SaveChangesAsync();
                        _operationResult.Succcess = true;
                        _operationResult.Message = "OK";
                        _operationResult.Data = _usuarios;
                    }
                }
            }
            catch (Exception ex)
            {
                _operationResult.Succcess = false;
                _operationResult.Message = ex.Message;
            }
            return _operationResult;
        }
        public async Task<OperationResultRequest> ListadoDeUsuarios(int page, int cantidadDeElemento)
        {
            _operationResult = new OperationResultRequest();
            try
            {
                List<TblUsuarios> listadoUsuarios = new List<TblUsuarios>();
                IQueryable<TblUsuarios> query = _dbContext.Usuario.AsQueryable();
                listadoUsuarios = await query
                    .Where(x => x.EstadoId == (int)EnumEstadoUsuario.Activo)
                    .Skip((page - 1) * cantidadDeElemento)
                    .Take(cantidadDeElemento)
                    .ToListAsync();
                _operationResult.Succcess = true;
                _operationResult.Data = listadoUsuarios;
                _operationResult.Message = _mesajeExitoso;
                _operationResult.Paginacion = new Pager(page, cantidadDeElemento, listadoUsuarios.Count());
            }
            catch (Exception ex)
            {
                _operationResult.Succcess = false;
                _operationResult.Message = ex.Message;
            }
            return _operationResult;
        }
        public async Task<OperationResultRequest> PerfilUsuario(int usuarioId)
        {
            _operationResult = new OperationResultRequest();
            try
            {
                _usuarios = await _dbContext.Usuario.FindAsync(usuarioId);
                if (_usuarios != null)
                {
                    _operationResult.Succcess = true;
                    _operationResult.Data = _usuarios;
                    _operationResult.Message = _mesajeExitoso;
                }
                else
                {
                    _operationResult.Succcess = false;
                    _operationResult.Message = $"El usuario que intenta buscar no existe.";
                }
            }
            catch (Exception ex)
            {
                _operationResult.Succcess = false;
                _operationResult.Message = ex.Message;
            }
            return _operationResult;
        }
        private async Task<bool> ValidarDocumento(string documentoDeIdentidad = null, string pasaporte = null, string email = null)
        {
            bool resp = false;
            bool validarDocumentoDeIdentidad = false;
            bool validarPasaporte = false;
            bool validarExistenciaDelEmail = false;

            try
            {
                if (documentoDeIdentidad != null && email != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        //... Primer paso: se valida si existe el documento de identidad.
                        validarDocumentoDeIdentidad = await _dbContext.Usuario.AnyAsync(u => u.TarjetaDeIdentificacion.Contains(documentoDeIdentidad));

                        //... Segundo paso: Se valida si existe el pasaporte
                        validarPasaporte = await _dbContext.Usuario.AnyAsync(u => u.Pasaporte.Contains(pasaporte));

                        //... Tercer paso: se valida si existe algun email
                        validarExistenciaDelEmail = await _dbContext.Usuario.AnyAsync(x => x.Email.Contains(email));
                        if (validarDocumentoDeIdentidad)
                        {
                            resp = true;
                        }
                        else if (validarExistenciaDelEmail)
                        {
                            resp = true;
                        }
                        else
                        {
                            resp = false;
                        }
                        scope.Complete();
                    }
                    return resp;
                }
                if (pasaporte != null && email != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        //... Primer paso: se valida si existe el pasaporte.
                        validarDocumentoDeIdentidad = await _dbContext.Usuario.AnyAsync(u => u.Pasaporte.Contains(pasaporte));

                        //... Segundo paso: se valida si existe algun email
                        validarExistenciaDelEmail = await _dbContext.Usuario.AnyAsync(x => x.Email.Contains(email));
                        resp = (validarDocumentoDeIdentidad == validarExistenciaDelEmail) ? false : true;
                        scope.Complete();
                    }
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp = false;
            }
            return resp;
        }

        public async Task<TblUsuariosDTO> GetUsuarioPorEmail(string email)
        {
            TblUsuariosDTO objUsuario = new TblUsuariosDTO();
            try
            {
                _usuarios = await _dbContext.Usuario.Where(x => x.Email == email).FirstAsync();
                objUsuario = _mapper.Map<TblUsuariosDTO>(_usuarios);
            }
            catch (Exception ex)
            {
                objUsuario = null;
            }
            return objUsuario;
        }

        public async Task<OperationResultRequest> BuscarUsuarios(FiltroUsuario parametros)
        {
            _operationResult = new OperationResultRequest();
            try
            {
                int page = 1;
                int cantidadDeElemento = 10;

                if (parametros.Id > 0 && parametros.Id != null)
                {
                    IQueryable<TblUsuarios> query = _dbContext.Usuario.Where(x => x.Id == parametros.Id).AsQueryable();
                    List<TblUsuarios> listadoUsuarios = new List<TblUsuarios>();
                    listadoUsuarios = await query
                                                .Skip((page - 1) * cantidadDeElemento)
                                                .Take(cantidadDeElemento)
                                                .ToListAsync();
                    _operationResult.Succcess = true;
                    _operationResult.Data = listadoUsuarios;
                    _operationResult.Message = _mesajeExitoso;
                    _operationResult.Paginacion = new Pager(page, cantidadDeElemento, listadoUsuarios.Count());
                }

                if (parametros.Nombre != string.Empty && parametros.Nombre != "" && parametros.Nombre != null)
                {
                    IQueryable<TblUsuarios> query = _dbContext.Usuario.Where(x => x.Nombres.Contains(parametros.Nombre)).AsQueryable();
                    List<TblUsuarios> listadoUsuarios = new List<TblUsuarios>();
                    listadoUsuarios = await query
                                                .Skip((page - 1) * cantidadDeElemento)
                                                .Take(cantidadDeElemento)
                                                .ToListAsync();
                    _operationResult.Succcess = true;
                    _operationResult.Data = listadoUsuarios;
                    _operationResult.Message = _mesajeExitoso;
                    _operationResult.Paginacion = new Pager(page, cantidadDeElemento, listadoUsuarios.Count());
                }

                if (parametros.Identificacion != string.Empty && parametros.Identificacion != "" && parametros.Identificacion != null)
                {
                    IQueryable<TblUsuarios> query = _dbContext.Usuario.Where(x => x.TarjetaDeIdentificacion.Contains(parametros.Identificacion)).AsQueryable();
                    List<TblUsuarios> listadoUsuarios = new List<TblUsuarios>();
                    listadoUsuarios = await query
                                                .Skip((page - 1) * cantidadDeElemento)
                                                .Take(cantidadDeElemento)
                                                .ToListAsync();
                    _operationResult.Succcess = true;
                    _operationResult.Data = listadoUsuarios;
                    _operationResult.Message = _mesajeExitoso;
                    _operationResult.Paginacion = new Pager(page, cantidadDeElemento, listadoUsuarios.Count());
                }

                if (parametros.Pasaporte != string.Empty && parametros.Pasaporte != "" && parametros.Pasaporte != null)
                {
                    IQueryable<TblUsuarios> query = _dbContext.Usuario.Where(x => x.Pasaporte.Contains(parametros.Pasaporte)).AsQueryable();
                    List<TblUsuarios> listadoUsuarios = new List<TblUsuarios>();
                    listadoUsuarios = await query
                                                .Skip((page - 1) * cantidadDeElemento)
                                                .Take(cantidadDeElemento)
                                                .ToListAsync();
                    _operationResult.Succcess = true;
                    _operationResult.Data = listadoUsuarios;
                    _operationResult.Message = _mesajeExitoso;
                    _operationResult.Paginacion = new Pager(page, cantidadDeElemento, listadoUsuarios.Count());
                }

            }
            catch (Exception ex)
            {
                _operationResult.Succcess = false;
                _operationResult.Message = ex.Message;
            }
            return _operationResult;
        }

        public async Task<OperationResultRequest> BuscarUsuariosPorCedula(string cedula)
        {
            _operationResult = new OperationResultRequest();
            try
            {
                if (cedula != string.Empty && cedula != "" && cedula != null)
                {
                    TblUsuarios usuarios = new TblUsuarios();
                    usuarios = await _dbContext.Usuario.Where(x => x.TarjetaDeIdentificacion == cedula).FirstAsync();

                    _operationResult.Succcess = true;
                    _operationResult.Data = usuarios;
                    _operationResult.Message = "Exito";
                }
            }
            catch (Exception ex)
            {
                _operationResult.Succcess = false;
                _operationResult.Message = ex.Message;
            }
            return _operationResult;
        }
    }
}
