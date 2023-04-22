using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.UsuarioService;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion.Test.UsuariosServices
{
    public class PerfilUsuarioTest : BasePrueba
    {
        [Fact]
        protected async Task BuscarPerfilUsuario()
        {
            //...Arrange
            var nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            var resp = new OperationResultRequest();
            var usuariosServices = new UsuarioCRUD(context, mapper);

            var objUsuario1 = new TblUsuariosDTO
            {
                Apellidos = "Mateo Landa",
                Nombres = "Marinelva",
                NombreDeUsuario = "mmateo",
                FechaDeNacimiento = Convert.ToDateTime("1997-09-20"),
                TarjetaDeIdentificacion = "00100130558",
                Email = "mmateo@gmail.com",
                Password = "mari125",
                RolId = 2,
                EstadoId = 1
            };
            var objUsuario2 = new TblUsuariosDTO
            {
                Apellidos = "Santos Garcia",
                Nombres = "Gerson Dario",
                NombreDeUsuario = "gdsantos",
                FechaDeNacimiento = Convert.ToDateTime("1997-09-20"),
                TarjetaDeIdentificacion = "00100130517",
                Email = "gdsantos@gmail.com",
                Password = "mari125",
                RolId = 2,
                EstadoId = 1
            };
            int usuarioId = 1;

            //...Action
            await usuariosServices.AgregarUsuario(objUsuario1);
            await usuariosServices.AgregarUsuario(objUsuario2);
            resp = await usuariosServices.PerfilUsuario(usuarioId);

            //...Assert
            Assert.True(resp.Succcess);
        }

        [Fact]
        protected async Task BuscarPerfilUsuarioNoExistente()
        {
            //...Arrange
            var nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            var resp = new OperationResultRequest();
            var usuariosServices = new UsuarioCRUD(context, mapper);

            var objUsuario1 = new TblUsuariosDTO
            {
                Apellidos = "Mateo Landa",
                Nombres = "Marinelva",
                NombreDeUsuario = "mmateo",
                FechaDeNacimiento = Convert.ToDateTime("1997-09-20"),
                TarjetaDeIdentificacion = "00100130558",
                Email = "mmateo@gmail.com",
                Password = "mari125",
                RolId = 2,
                EstadoId = 1
            };
            var objUsuario2 = new TblUsuariosDTO
            {
                Apellidos = "Santos Garcia",
                Nombres = "Gerson Dario",
                NombreDeUsuario = "gdsantos",
                FechaDeNacimiento = Convert.ToDateTime("1997-09-20"),
                TarjetaDeIdentificacion = "00100130517",
                Email = "gdsantos@gmail.com",
                Password = "mari125",
                RolId = 2,
                EstadoId = 1
            };
            int usuarioId = 1000;

            //...Action
            await usuariosServices.AgregarUsuario(objUsuario1);
            await usuariosServices.AgregarUsuario(objUsuario2);
            resp = await usuariosServices.PerfilUsuario(usuarioId);

            //...Assert
            Assert.False(resp.Succcess);
        }
    }
}
