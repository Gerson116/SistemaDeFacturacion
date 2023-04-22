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
    public class AgregarProductosTest : BasePrueba
    {
        [Fact]
        public async Task AgregarProductoFuncional()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            OperationResultRequest result = new OperationResultRequest();
            SProductosServices services = new SProductosServices(dbContext: context, mapper: mapper);
            List<TblProductosDTO> listaDeMarca = new List<TblProductosDTO>
            {
                new TblProductosDTO { Nombre = "Agua Dasany", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Refresco de Cola", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Refresco Rojo", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Refresco Uva", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Arroz", EstadoId = 1, FechaDeCreacion = DateTime.Now }
            };

            //...Action
            result = await services.PostNuevoProducto(listaDeMarca);

            //...Assert
            Assert.True(result.Succcess);
        }

        [Fact]
        public async Task AgregarProductoNoFuncional()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            OperationResultRequest result = new OperationResultRequest();
            SProductosServices services = new SProductosServices(dbContext: context, mapper: mapper);
            List<TblProductosDTO> listaDeMarca = new List<TblProductosDTO>
            {
                new TblProductosDTO { Nombre = "Agua Dasany", FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Refresco de Cola", FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Refresco Uva", EstadoId = 1, FechaDeCreacion = DateTime.Now },
                new TblProductosDTO { Nombre = "Arroz", EstadoId = 1, FechaDeCreacion = DateTime.Now }
            };

            //...Action
            result = await services.PostNuevoProducto(listaDeMarca);

            //...Assert
            Assert.False(result.Succcess);
        }
    }
}
