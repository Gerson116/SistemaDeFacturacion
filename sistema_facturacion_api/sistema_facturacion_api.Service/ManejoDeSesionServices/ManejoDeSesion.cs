using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Data.ManejoDeSesion;
using sistema_facturacion_api.Service.UsuarioService;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace sistema_facturacion_api.Service.ManejoDeSesionServices
{
    public class ManejoDeSesion : IManejoDeSesion
    {
        private FacturacionDbContext _dbContext;
        private OperationResultRequest _request;
        private OperationResultLogin _requestLogin;
        private TblUsuarios _usuarios;
        private List<TblPermiso> _permiso;
        private DetalleDeSesion _detalleDeSesion;
        private string _jwtkey = "jwtkey";
        private Encrypt _encrypt;
        private IMapper _mapper;
        private IUsuarioCRUD _usuarioServices;

        public ManejoDeSesion(FacturacionDbContext dbContext, IMapper mapper, IUsuarioCRUD usuarioServices)
        {
            _dbContext = dbContext;
            _request = new OperationResultRequest();
            _requestLogin = new OperationResultLogin();
            _usuarios = new TblUsuarios();
            _permiso = new List<TblPermiso>();
            _detalleDeSesion = new DetalleDeSesion();
            _encrypt = new Encrypt();
            _mapper = mapper;
            _usuarioServices = usuarioServices;
        }
        public async Task<TokenUsuario> ConstruirToken(string email, string nombreDeUsuario)
        {
            var claims = new[] 
            {
                new Claim("Email", email),
                new Claim("NombreUsuario", nombreDeUsuario)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(System.Environment.GetEnvironmentVariable(_jwtkey)));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime fechaDeExpiracion = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                issuer: "my_issuer",
                audience: "my_audience",
                claims: claims,
                expires: fechaDeExpiracion,
                signingCredentials: creds
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            string tokenString = tokenHandler.WriteToken(token);

            return new TokenUsuario { Token = tokenString, FechaExpiracion = fechaDeExpiracion };
        }

        public async Task<OperationResultLogin> ConsultarSesion(IniciarSesion iniciarSesion)
        {
            try
            {
                string pass = _encrypt.EncriptingPassword(iniciarSesion.Password);
                _usuarios = await _dbContext.Usuario
                    .Where(u => u.Email == iniciarSesion.Email && u.Password == pass)
                    .FirstAsync();
                TokenUsuario token = new TokenUsuario();
                if (_usuarios != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        //... Paso 1: se buscan los datos de los usuarios.
                        _detalleDeSesion.Usuarios = _usuarios;

                        //... Paso 2: se buscan los permisos.
                        _detalleDeSesion.Permisos = await _dbContext.Permiso.Where(x => x.UsuarioId == _usuarios.Id).ToListAsync();

                        _requestLogin.Succcess = true;
                        _requestLogin.Message = "Exito";
                        _requestLogin.Data = _detalleDeSesion;
                        token = await ConstruirToken(iniciarSesion.Email, _usuarios.NombreDeUsuario);
                        _requestLogin.Token = token.Token;
                        _requestLogin.FechaExpiracion = token.FechaExpiracion;
                    }
                }
                else
                {
                    _requestLogin.Succcess = false;
                    _requestLogin.Message = "Ocurrio un error";
                    _requestLogin.Data = "No se encontraron datos";
                    _requestLogin.Token = null;
                    _requestLogin.FechaExpiracion = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                _requestLogin.Succcess = false;
                _requestLogin.Message = $"Ocurrio un error {ex.Message}";
            }
            return _requestLogin;
        }

        public async Task<OperationResultRequest> OlvideMiPassword(string correoCliente, string passwordGenerico, string correoRecuperacion, string password)
        {
            try
            {
                _usuarios = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Email == correoCliente);
                if (_usuarios != null)
                {
                    Encrypt encrypt = new Encrypt();
                    string passwordGenericoEncriptada = encrypt.EncriptingPassword(passwordGenerico);
                    _usuarios.Password = passwordGenericoEncriptada;

                    _dbContext.Entry(_usuarios).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    await SendEmailResetPassword(_usuarios.Email, passwordGenerico, correoRecuperacion, password);
                    _request.Succcess = true;
                    _request.Message = "Exito";
                    _request.Data = "Se modifico exitosamente la contraseña.";
                }
            }
            catch (Exception ex)
            {
                _request.Succcess = false;
                _request.Message = $"Ocurrio un error {ex.Message}";
            }
            return _request;
        }

        private async Task SendEmailResetPassword(string email, string passwordGenerico, string correo, string password)
        {
            PasswordResetEmail passwordReset = new PasswordResetEmail();

            passwordReset.To = email;
            passwordReset.From = "gerson_santos_mateo3@outlook.es";
            passwordReset.Subject = "Cambio de contraseña";
            passwordReset.Body = $"Su nueva contraseña es: {passwordGenerico}";

            var smtpClient = new SmtpClient()
            {
                Host = "smtp.office365.com", //... Esto se descomenta en caso de que de error.
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(correo, password)
            };

            var mensaje = new MailMessage(passwordReset.From, passwordReset.To, passwordReset.Subject, passwordReset.Body);
            smtpClient.Send(mensaje);
            smtpClient.Dispose();
        }

        private async Task<bool> ValidarExistenciaDelEmail(string email)
        {
            TblUsuarios usuarios = new TblUsuarios();
            bool resp = false;
            try
            {
                resp = await _dbContext.Usuario.AnyAsync(u => u.Email.Contains(email));
                if (resp)
                {
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp = false;
            }
            return resp;
        }
    }
}
