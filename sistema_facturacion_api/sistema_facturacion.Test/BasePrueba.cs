using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.UsuarioService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion.Test
{
    public class BasePrueba
    {
        protected FacturacionDbContext ConstruirContext(string nombreDb) 
        {
            //... Este es mi dbcontext
            var opciones = new DbContextOptionsBuilder<FacturacionDbContext>()
                .UseInMemoryDatabase(nombreDb).Options;
            var dbContext = new FacturacionDbContext(opciones);
            return dbContext;
        }

        protected IMapper ConfigurarAutoMapper()
        {
            //...
            var conf = new MapperConfiguration(options => 
            {
                options.CreateMap<TblUsuarios, TblUsuariosDTO>().ReverseMap();
                options.CreateMap<TblMarca, TblMarcaDTO>().ReverseMap();
                options.CreateMap<TblProducto, TblProductosDTO>().ReverseMap();
            });
            return conf.CreateMapper();
        }
    }
}
