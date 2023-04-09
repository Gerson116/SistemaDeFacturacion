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
    public class ListarMarcasTest : BasePrueba
    {
        [Fact]
        public async Task ListarMarcasFuncional()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            int pagina = 1;
            int cantidadDeElementos = 10;
            OperationResultRequest result = new OperationResultRequest();

            //...Action
            result = await AgregarMarca(nombreDb, pagina, cantidadDeElementos);

            //...Assert
            Assert.True(result.Succcess);
        }

        [Fact]
        public async Task ListarMarcasNoFuncional()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            int pagina = 100;
            int cantidadDeElementos = 10;
            OperationResultRequest result = new OperationResultRequest();

            //...Action
            result = await AgregarMarca(nombreDb, pagina, cantidadDeElementos);

            //...Assert
            Assert.False(result.Succcess);
        }

        private async Task<OperationResultRequest> AgregarMarca(string nombreDb, int pagina, int cantidadDeElementos)
        {
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            SMarcaServices services = new SMarcaServices(context, mapper);
            List<TblMarcaDTO> listaDeMarca = new List<TblMarcaDTO>
            {
                new TblMarcaDTO { Nombre = "Coca Cola", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblMarcaDTO { Nombre = "Kola Real", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblMarcaDTO { Nombre = "Red Rock", EstadoId = 1, FechaDeCreacion = DateTime.Now }
            };
            await services.PostNuevaMarca(listaDeMarca);
            return await services.GetAllMarcas(pagina, cantidadDeElementos);
        }
    }
}
