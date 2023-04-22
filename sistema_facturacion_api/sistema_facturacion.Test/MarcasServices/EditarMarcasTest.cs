using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.MarcaServices;
using sistema_facturacion_api.Service.UsuarioService;
using sistema_facturacion_api.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace sistema_facturacion.Test.MarcasServices
{
    public class EditarMarcasTest : BasePrueba
    {
        [Fact]
        public async Task EditarMarcaFuncional()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            TblMarcaDTO marcaDTO = new TblMarcaDTO 
            {
                Id = 1,
                Nombre = "Cola Coca",
                EstadoId = 2
            };

            //...Action
            var resp = await AgregarMarca(nombreDb, marcaDTO);

            //...Assert
            Assert.True(resp.Succcess);
        }

        [Fact]
        public async Task EditarMarcaNoFuncional()
        {
            //...Arrange
            string nombreDb = Guid.NewGuid().ToString();
            TblMarcaDTO marcaDTO = new TblMarcaDTO
            {
                Nombre = "Cola Coca",
                EstadoId = 2
            };

            //...Action
            var resp = await AgregarMarca(nombreDb, marcaDTO);

            //...Assert
            Assert.False(resp.Succcess);
        }

        private async Task<OperationResultRequest> AgregarMarca(string nombreDb, TblMarcaDTO marcaDTO)
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
            return await services.PutEditarMarca(marcaDTO);
        }
    }
}
