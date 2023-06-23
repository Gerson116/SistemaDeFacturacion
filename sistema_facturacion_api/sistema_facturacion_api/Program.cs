using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.CargarArchivoServices;
using sistema_facturacion_api.Service.EmpresaServices;
using sistema_facturacion_api.Service.FacturaServices;
using sistema_facturacion_api.Service.FormaDePagoServices;
using sistema_facturacion_api.Service.IVAServices;
using sistema_facturacion_api.Service.ManejoDeSesionServices;
using sistema_facturacion_api.Service.MarcaServices;
using sistema_facturacion_api.Service.ModuloServices;
using sistema_facturacion_api.Service.PermisosServices;
using sistema_facturacion_api.Service.ProductosServices;
using sistema_facturacion_api.Service.UsuarioService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(conf =>
{
    conf.CreateMap<TblUsuarios, TblUsuariosDTO>().ReverseMap();
    conf.CreateMap<TblUsuarios, UsuarioSesionDTO>().ReverseMap();
    conf.CreateMap<TblProducto, TblProductosDTO>().ReverseMap();
    conf.CreateMap<TblMarca, TblMarcaDTO>().ReverseMap();
    conf.CreateMap<TblEmpresas, TblEmpresasDTO>().ReverseMap();
    conf.CreateMap<TblEmpresas, TblEmpresaDatosEditablesDesdeElFrontDTO>().ReverseMap();
    conf.CreateMap<TblFormaDePago, TblFormaDePagoDTO>().ReverseMap();
    conf.CreateMap<TblImpuestoAlValorAgregado, IVADTO>().ReverseMap();
    conf.CreateMap<TblModulo, TblModuloDTO>().ReverseMap();
    conf.CreateMap<TblFacturas, TblFacturasDTO>().ReverseMap();
    conf.CreateMap<TblDetalleDeFacturas, TblDetalleDeFacturasDTO>()
    .ForMember(
        dest => dest.NombreProducto,
        apt => apt.MapFrom(src => src.Producto.Nombre))
    .ReverseMap();

    conf.CreateMap<TblFacturas, NuevaFacturaDTO>().ReverseMap();
    conf.CreateMap<TblDetalleDeFacturas, NuevaFacturaDTO>().ReverseMap();
    conf.CreateMap<TblPermiso, TblPermisoDTO>().ReverseMap();
});

builder.Services.AddTransient<IUsuarioCRUD, UsuarioCRUD>();
builder.Services.AddTransient<IProductosServices, SProductosServices>();
builder.Services.AddTransient<IMarcaServices, SMarcaServices>();
builder.Services.AddTransient<IManejoDeSesion, ManejoDeSesion>();
builder.Services.AddTransient<IEmpresaServices, SEmpresaServices>();
builder.Services.AddTransient<ICargarArchivo, SCargarArchivo>();
builder.Services.AddTransient<IFormaDePago, SFormaDePago>();
builder.Services.AddTransient<IIVA, SIVA>();
builder.Services.AddTransient<IModuloServices, SModuloServices>();
builder.Services.AddTransient<IFacturaServices, SFacturaServices>();
builder.Services.AddTransient<IPermisosServices, SPermisosServices>();

string connectionStringDesarrollo = System.Environment.GetEnvironmentVariable("DbFacturaDesarrollo");
string jwtkey = System.Environment.GetEnvironmentVariable("jwtkey");
string _myPolice = "_myPolice";

builder.Services.AddDbContext<FacturacionDbContext>(conf =>
{
    //conf.UseSqlServer(builder.Configuration.GetConnectionString(connectionStringDesarrollo));
    conf.UseSqlServer(connectionStringDesarrollo);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(_myPolice, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtkey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("policy", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(_myPolice);

app.MapControllers();

app.Run();
