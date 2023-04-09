using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Service.CargarArchivoServices
{
    public class SCargarArchivo : ICargarArchivo
    {
        private readonly string _connectionString;

        public SCargarArchivo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ContenedorImagenEmpresa");
        }
        public async Task<bool> BorrarArchivo(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return false;
            }
            var archivo = Path.GetFileName(ruta);
            BlobClient blob = new BlobClient(connectionString: _connectionString, blobContainerName: contenedor, blobName: archivo);
            await blob.DeleteAsync();
            return true;
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            //... Este método busca guardar un archivo en Azure.
            var nombreDelContenedor = new BlobContainerClient(connectionString: _connectionString, blobContainerName: contenedor);
            await nombreDelContenedor.CreateIfNotExistsAsync(); //... Crea la carpeta en caso de existir.
            nombreDelContenedor.SetAccessPolicyAsync(PublicAccessType.Blob);

            var archivoNombre = $"{extension}"; //... Creamos un nombre.
            var blob = nombreDelContenedor.GetBlobClient(archivoNombre);

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeater = new BlobHttpHeaders();
            blobHttpHeater.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHeater;

            await blob.UploadAsync(new BinaryData(contenido), blobUploadOptions);
            return blob.Uri.ToString();
        }
    }
}
