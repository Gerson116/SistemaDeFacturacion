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
    public class EditarUsuarioTest : BasePrueba
    {
        [Fact]
        protected async Task EditarUsuarioFuncional()
        {
            //...Arrange
            var operationResult = new OperationResultRequest();
            string nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            var servicesUsuario = new UsuarioCRUD(context, mapper);
            await AgregarUsuario(nombreDb);
            var objUsuario3 = new TblUsuariosDTO
            {
                Id = 2,
                Apellidos = "Santos Garcia2",
                Nombres = "Gerson Dario",
                NombreDeUsuario = "gdsantos",
                FechaDeNacimiento = Convert.ToDateTime("1997-09-20"),
                TarjetaDeIdentificacion = "00100130517",
                Email = "gdsantos@gmail.com",
                Password = "mari125",
                RolId = 2,
                EstadoId = 1
            };

            //...Action
            operationResult = await servicesUsuario.EditarUsuario(objUsuario3);

            //...Assert
            Assert.True(operationResult.Succcess);
        }

        [Fact]
        protected async Task EditarUsuarioNoFuncional()
        {
            //...Arrange
            var operationResult = new OperationResultRequest();
            string nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            var servicesUsuario = new UsuarioCRUD(context, mapper);
            var objUsuario3 = new TblUsuariosDTO
            {
                Apellidos = "Santos Garcia2",
                Nombres = "Gerson Dario",
                NombreDeUsuario = "gdsantos",
                FechaDeNacimiento = Convert.ToDateTime("1997-09-20"),
                TarjetaDeIdentificacion = "00100130517",
                Email = "gdsantos@gmail.com",
                Password = "mari125",
                RolId = 2,
                EstadoId = 1
            };

            //...Action
            await AgregarUsuario(nombreDb);
            operationResult = await servicesUsuario.EditarUsuario(objUsuario3);

            //...Assert
            Assert.False(operationResult.Succcess);
        }

        private async Task AgregarUsuario(string nombreDb)
        {
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            UsuarioCRUD usuarioCRUD = new UsuarioCRUD(context, mapper);

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
            await usuarioCRUD.AgregarUsuario(objUsuario1);
            await usuarioCRUD.AgregarUsuario(objUsuario2);
        }
    }
}
