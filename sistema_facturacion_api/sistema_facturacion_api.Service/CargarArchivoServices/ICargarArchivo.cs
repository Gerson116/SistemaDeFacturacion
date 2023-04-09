using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.CargarArchivoServices
{
    public interface ICargarArchivo
    {
        Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType);
        Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType);
        Task<bool> BorrarArchivo(string ruta, string contenedor);
    }
}
