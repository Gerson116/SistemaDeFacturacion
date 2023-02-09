using Microsoft.EntityFrameworkCore;
using sistema_facturacion_api.Context;
using sistema_facturacion_api.Data;
using sistema_facturacion_api.Data.DTOs;
using sistema_facturacion_api.Service.MarcaServices;
using sistema_facturacion_api.Service.ProductosServices;
using sistema_facturacion_api.Service.UsuarioService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(conf =>
{
    conf.CreateMap<TblUsuarios, TblUsuariosDTO>().ReverseMap();
    conf.CreateMap<TblProducto, TblProductosDTO>().ReverseMap();
    conf.CreateMap<TblMarca, TblMarcaDTO>().ReverseMap();
});

builder.Services.AddTransient<IUsuarioCRUD, UsuarioCRUD>();
builder.Services.AddTransient<IProductosServices, SProductosServices>();
builder.Services.AddTransient<IMarcaServices, SMarcaServices>();
builder.Services.AddTransient<IMarcaServices, SMarcaServices>();

string connectionString = "DbFactura";
builder.Services.AddDbContext<FacturacionDbContext>(conf =>
{
    conf.UseSqlServer(builder.Configuration.GetConnectionString(connectionString));
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

app.MapControllers();

app.Run();
