using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.MarcaServices;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion.Test.MarcasServices
{
    public class AgregarMarcasTest : BasePrueba
    {
        [Fact]
        public async Task AgregarMarcasFuncional()
        {
            //...Arrange
            var nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            var nuevaMarca = new List<TblMarcaDTO>() 
            {
                new TblMarcaDTO() { Nombre = "Coca Cola", EstadoId = 1 },
                new TblMarcaDTO() { Nombre = "Pepsi", EstadoId = 1 },
                new TblMarcaDTO() { Nombre = "Kola Real", EstadoId = 1 },
                new TblMarcaDTO() { Nombre = "7Up", EstadoId = 1 }
            };
            var respuesta = new OperationResultRequest();

            //...Action
            var services = new SMarcaServices(context, mapper);
            respuesta = await services.PostNuevaMarca(nuevaMarca);

            //...Assert
            Assert.True(respuesta.Succcess);
        }
        [Fact]
        public async Task AgregarMarcasNoFuncional()
        {
            //...Arrange
            var nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            var nuevaMarca = new List<TblMarcaDTO>()
            {
                new TblMarcaDTO() { EstadoId = 1 },
                new TblMarcaDTO() { Nombre = "Pepsi", EstadoId = 1 },
                new TblMarcaDTO() { Nombre = "Kola Real" },
                new TblMarcaDTO() { Nombre = "7Up" }
            };
            var respuesta = new OperationResultRequest();

            //...Action
            var services = new SMarcaServices(context, mapper);
            respuesta = await services.PostNuevaMarca(nuevaMarca);

            //...Assert
            Assert.False(respuesta.Succcess);
        }

    }
}
