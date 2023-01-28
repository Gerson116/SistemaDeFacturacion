using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
        }
        public async Task<OperationResultRequest> AgregarUsuario(TblUsuariosDTO usuario)
        {
            _operationResult = new OperationResultRequest();
            _encriptarPass = new Encrypt();
            try
            {
                _usuarios = _mapper.Map<TblUsuarios>(usuario);
                _usuarios.FechaDeCreacion = DateTime.Now;
                _usuarios.Password = _encriptarPass.EncriptingPassword(usuario.Password);
                await _dbContext.Usuario.AddAsync(_usuarios);
                await _dbContext.SaveChangesAsync();

                _operationResult.Succcess = true;
                _operationResult.Message = _mesajeExitoso;
                _operationResult.Data = usuario;
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
                _dbContext.Entry(_usuarios).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                _operationResult.Succcess = true;
                _operationResult.Data = usuario;
                _operationResult.Message = _mesajeExitoso;
            }
            catch (Exception ex)
            {
                _operationResult.Succcess = false;
                _operationResult.Message = ex.Message;
            }
            return _operationResult;
        }
        public async Task<OperationResultRequest> EliminarUsuario(int usuarioId, JsonPatchDocument<TblUsuarios> usuariosPatch)
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
        public async Task<OperationResultRequest> ListadoDeUsuarios()
        {
            _operationResult = new OperationResultRequest();
            try
            {
                List<TblUsuarios> listadoUsuarios = new List<TblUsuarios>();
                IQueryable<TblUsuarios> query = _dbContext.Usuario.AsQueryable();
                listadoUsuarios = await query.ToListAsync();
                _operationResult.Succcess = true;
                _operationResult.Data = listadoUsuarios;
                _operationResult.Message = _mesajeExitoso;
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
    }
}
