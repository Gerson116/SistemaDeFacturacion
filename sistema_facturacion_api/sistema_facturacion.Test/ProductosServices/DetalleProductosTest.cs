using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.ProductosServices;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion.Test.ProductosServices
{
    public class DetalleProductosTest: BasePrueba
    {
        [Fact]
        public async Task BuscarDetalleDelProductoFuncional()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            OperationResultRequest result = new OperationResultRequest();
            int productoId = 2;

            //...Action
            result = await AgregarProducto(nombreDb, productoId);

            //...Assert
            Assert.True(result.Succcess);
        }

        [Fact]
        public async Task BuscarDetalleDelProductoNoFuncional()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            OperationResultRequest result = new OperationResultRequest();
            int productoId = 2000;

            //...Action
            result = await AgregarProducto(nombreDb, productoId);

            //...Assert
            Assert.False(result.Succcess);
        }

        private async Task<OperationResultRequest> AgregarProducto(string nombreDb, int productoId)
        {
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            SProductosServices services = new SProductosServices(dbContext: context, mapper: mapper);
            List<TblProductosDTO> listaDeMarca = new List<TblProductosDTO>
            {
                new TblProductosDTO { Nombre = "Agua Dasany", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Refresco de Cola", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Refresco Rojo", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Refresco Uva", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Arroz", EstadoId = 1, FechaDeCreacion = DateTime.Now }
            };
            await services.PostNuevoProducto(listaDeMarca);
            return await services.GetProductosPorId(productoId);
        }
    }
}
