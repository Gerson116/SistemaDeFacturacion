﻿using AutoMapper;
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

namespace sistema_facturacion_api.Service.PermisosServices
{
    public class SPermisosServices : IPermisosServices
    {
        private FacturacionDbContext _dbContext;
        private IMapper _mapper;
        private OperationResultRequest _request;
        private List<TblPermiso> _listadoPermisos;

        public SPermisosServices(IMapper mapper, FacturacionDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _request = new OperationResultRequest();
            _listadoPermisos = new List<TblPermiso>();
        }
        public async Task<OperationResultRequest> DeletePermisos(int usuarioId, int permisoId)
        {
            try
            {
                TblPermiso permiso = new TblPermiso();
                permiso = _dbContext.Permiso.Where(x => x.UsuarioId== usuarioId && x.Id == permisoId).FirstOrDefault();
                if (permiso != null)
                {
                    permiso.Estado = false;
                    _dbContext.Entry(permiso).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = "Se elimino el permiso del afiliado.";
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> EditarYAgregarPermisosExistentes(List<TblPermisoDTO> permisosUsuarios)
        {
            TblPermiso objPermiso = new TblPermiso();
            List<TblPermiso> listPermisos = new List<TblPermiso>();
            listPermisos = _mapper.Map<List<TblPermiso>>(permisosUsuarios);
            TblUsuarios objUsuario = new TblUsuarios();

            try
            {
                var element = listPermisos.FirstOrDefault();
                var permisosExistentes = await _dbContext.Permiso
                                                         .Where(x => x.UsuarioId == element.UsuarioId)
                                                         .ToListAsync();
                var usuario = await _dbContext.Usuario.FindAsync(element.UsuarioId);
                if (permisosExistentes.Count > 0)
                {
                    //... En caso de ingresar aqui, se editara los datos y 
                    foreach (var item in listPermisos)
                    {
                        var datosExistente = permisosExistentes.Where(x => x.ModuloId == item.ModuloId).FirstOrDefault();
                        if (datosExistente != null)
                        {
                            datosExistente.UsuarioId = item.UsuarioId;
                            datosExistente.ModuloId = item.ModuloId;
                            datosExistente.RolId = usuario.RolId;
                            datosExistente.C = item.C;
                            datosExistente.R = item.R;
                            datosExistente.U = item.U;
                            datosExistente.D = item.D;

                            _dbContext.Permiso.Entry(datosExistente).State = EntityState.Modified;
                            await _dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            item.FechaDeCreacion = DateTime.Now;
                            item.Estado = true;
                            _dbContext.Permiso.Add(item);
                            _dbContext.SaveChanges();
                        }
                    }
                }
                else
                {
                    listPermisos.ForEach(x => x.Estado = true);
                    await _dbContext.Permiso.AddRangeAsync(listPermisos);
                    await _dbContext.SaveChangesAsync();
                }
                _request.Succcess = true;
                _request.Message = "Exito";
                _request.Data = permisosUsuarios;
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error ${ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> GetAllPermisos(int usuarioId)
        {
            try
            {
                IQueryable<TblPermiso> query = _dbContext.Permiso.Where(x => x.UsuarioId == usuarioId).AsQueryable();
                _listadoPermisos = await query.ToListAsync();
                if (_listadoPermisos != null)
                {
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = _listadoPermisos;
                }
                else
                {
                    _request.Succcess = false;
                    _request.Message = "El afiliado no tiene permisos asignados.";
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> PostAgregarPermisos(List<TblPermisoDTO> nuevoPermiso)
        {
            try
            {
                _listadoPermisos = _mapper.Map<List<TblPermiso>>(nuevoPermiso);
                _listadoPermisos.ForEach(x => x.FechaDeCreacion = DateTime.Now);
                _listadoPermisos.ForEach(x => x.Estado = true);
                await _dbContext.Permiso.AddRangeAsync(_listadoPermisos);
                await _dbContext.SaveChangesAsync();

                _request.Succcess = true;
                _request.Message = "Los datos fueron almacenados con exito.";
                _request.Data = _listadoPermisos;
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
            }
            return _request;
        }

        public async Task<OperationResultRequest> PutEditarPermisos(TblPermisoDTO editarPermiso, int permisoId)
        {
            try
            {
                TblPermiso permiso = new TblPermiso();
                permiso = await _dbContext.Permiso.FindAsync(permisoId);
                if (permiso != null)
                {
                    permiso.UsuarioId = (int)editarPermiso.UsuarioId;
                    permiso.C = (bool)editarPermiso.C;
                    permiso.R = (bool)editarPermiso.R;
                    permiso.U = (bool)editarPermiso.U;
                    permiso.D = (bool)editarPermiso.D;
                    permiso.Estado = (bool)editarPermiso.Estado;
                    _dbContext.Entry(permiso).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = "Se aplicaron los cambios con exito";
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
