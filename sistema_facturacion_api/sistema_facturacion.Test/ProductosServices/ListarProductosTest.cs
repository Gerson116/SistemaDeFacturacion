using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.MarcaServices;
using sistema_facturacion_api.Service.ProductosServices;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion.Test.ProductosServices
{
    public class ListarProductosTest : BasePrueba
    {
        [Fact]
        public async Task ListarProductoFuncional() 
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            int pagina = 1;
            int cantidad = 10;
            OperationResultRequest result = new OperationResultRequest();

            //...Action
            result = await AgregarProducto(nombreDb, pagina, cantidad);

            //...Assert
            Assert.True(result.Succcess);
        }
        [Fact]
        public async Task ListarProductoNoFuncional()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            int pagina = 100;
            int cantidad = 10;
            OperationResultRequest result = new OperationResultRequest();

            //...Action
            result = await AgregarProducto(nombreDb, pagina, cantidad);

            //...Assert
            Assert.False(result.Succcess);
        }
        private async Task<OperationResultRequest> AgregarProducto(string nombreDb, int pagina, int cantidadDeElementos)
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
            return await services.GetAllProductos(pagina, cantidadDeElementos);
        }
    }
}
