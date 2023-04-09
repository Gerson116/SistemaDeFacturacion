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
    public class ListarUsuariosTest : BasePrueba
    {
        [Fact]
        public async Task ListadoDeUsuariosExistentes()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            int pagina, cantidadDeElementos;
            pagina = 1;
            cantidadDeElementos = 1;
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            var resp = new OperationResultRequest();

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

            var servicesUsuario = new UsuarioCRUD(context, mapper);

            //...Action
            await servicesUsuario.AgregarUsuario(objUsuario1);
            await servicesUsuario.AgregarUsuario(objUsuario2);
            resp = await servicesUsuario.ListadoDeUsuarios(pagina, cantidadDeElementos);
            Console.WriteLine(resp.Data);

            //...Assert
            Assert.True(resp.Succcess);
        }
    }
}
