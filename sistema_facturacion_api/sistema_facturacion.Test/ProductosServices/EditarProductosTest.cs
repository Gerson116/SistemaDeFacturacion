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
    public class EditarProductosTest : BasePrueba
    {
        [Fact]
        public async Task EditarProductosFuncional()
        {
            //...Arrage
            string nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            SProductosServices services = new SProductosServices(dbContext: context, mapper: mapper);
            OperationResultRequest result = new OperationResultRequest();
            TblProductosDTO producto = new TblProductosDTO 
            {
                Id = 1,
                Nombre = "Agua Dasani",
                EstadoId = 1
            };

            //...Action
            await AgregarProducto(nombreDb, producto);
            result = await services.PutEditarProducto(producto);

            //...Assert
            Assert.True(result.Succcess);
        }
        [Fact]
        public async Task EditarProductosNoFuncional()
        {
            //...Arrage
            string nombreDb = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();
            SProductosServices services = new SProductosServices(dbContext: context, mapper: mapper);
            OperationResultRequest result = new OperationResultRequest();
            TblProductosDTO producto = new TblProductosDTO
            {
                Nombre = "Agua Dasani",
                EstadoId = 1
            };

            //...Action
            await AgregarProducto(nombreDb, producto);
            result = await services.PutEditarProducto(producto);

            //...Assert
            Assert.False(result.Succcess);
        }
        private async Task AgregarProducto(string nombreDb, TblProductosDTO producto)
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
        }
    }
}
