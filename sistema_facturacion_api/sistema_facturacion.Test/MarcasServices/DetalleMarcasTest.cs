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
    public class DetalleMarcasTest : BasePrueba
    {
        [Fact]
        public async Task DetalleMarcasFuncional()
        {
            //...Arrange
            var respuesta = new OperationResultRequest();
            int marcaId = 4;

            //...Action
            respuesta = await AgregarMarcaYConsultarElDetalleDeLaMarca(marcaId);

            //...Assert
            Assert.True(respuesta.Succcess);
        }
        [Fact]
        public async Task DetalleMarcasNoFuncional()
        {
            //...Arrange
            var respuesta = new OperationResultRequest();
            int marcaId = 10;

            //...Action
            respuesta = await AgregarMarcaYConsultarElDetalleDeLaMarca(marcaId);

            //...Assert
            Assert.False(respuesta.Succcess);
        }
        private async Task<OperationResultRequest> AgregarMarcaYConsultarElDetalleDeLaMarca(int marcaId)
        {
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
            var services = new SMarcaServices(context, mapper);
            await services.PostNuevaMarca(nuevaMarca);

            respuesta = await services.GetMarcaPorId(marcaId);
            return respuesta;
        }
    }
}
