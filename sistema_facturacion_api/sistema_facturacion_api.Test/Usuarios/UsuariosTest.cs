using sistema_facturacion_api.Service.UsuarioService;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace sistema_facturacion_api.Test.Usuarios
{
    public class UsuariosTest
    {
        private IUsuarioCRUD _usuario;

        public UsuariosTest(IUsuarioCRUD usuario)
        {
            _usuario = usuario;
        }
        [Fact]
        public async void ListarLosUsuariosTest()
        {
            //...Arrage
            int pagina, cantidadDeRegistros;
            pagina = 1;
            cantidadDeRegistros = 10;
            //...Action
            OperationResultRequest operationResultRequest = new OperationResultRequest();
            operationResultRequest = await _usuario.ListadoDeUsuarios(pagina, cantidadDeRegistros);
            bool resp = operationResultRequest.Succcess;
            //...Assert
            Assert.True(resp);
        }
    }
}
