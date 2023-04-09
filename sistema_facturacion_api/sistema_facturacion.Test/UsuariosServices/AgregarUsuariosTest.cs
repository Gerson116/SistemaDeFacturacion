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
    public class AgregarUsuariosTest : BasePrueba
    {
        [Fact]
        public async Task AgregarUsuarioTest() 
        {
            //...Arrange
            var nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            var resp1 = new OperationResultRequest();

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

            //...Action
            var usuariosServices = new UsuarioCRUD(context, mapper);
            resp1 = await usuariosServices.AgregarUsuario(objUsuario1);
            await usuariosServices.AgregarUsuario(objUsuario2);

            //...Assert
            Assert.True(resp1.Succcess);
        }

        [Fact]
        public async Task AgregandoUsuarioSinInformacionCompleta()
        {
            //...Arrange
            var nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            var resp1 = new OperationResultRequest();

            var objUsuario1 = new TblUsuariosDTO
            {
                Apellidos = "Mateo Landa",
                Nombres = "Marinelva",
                FechaDeNacimiento = Convert.ToDateTime("1997-09-20"),
                TarjetaDeIdentificacion = "00100130558",
                Password = "mari125",
                RolId = 2,
                EstadoId = 1
            };
            var objUsuario2 = new TblUsuariosDTO
            {
                Apellidos = "Santos Garcia",
                Nombres = "Gerson Dario",
                FechaDeNacimiento = Convert.ToDateTime("1997-09-20"),
                TarjetaDeIdentificacion = "00100130517",
                Password = "mari125",
                RolId = 2,
                EstadoId = 1
            };

            //...Action
            var usuariosServices = new UsuarioCRUD(context, mapper);
            resp1 = await usuariosServices.AgregarUsuario(objUsuario1);
            await usuariosServices.AgregarUsuario(objUsuario2);

            //...Assert
            Assert.False(resp1.Succcess);
        }
    }
}
