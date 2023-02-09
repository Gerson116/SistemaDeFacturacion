using Microsoft.EntityFrameworkCore;
using sistema_facturacion_api.Data;

namespace sistema_facturacion_api.Context
{
    public class FacturacionDbContext : DbContext
    {
        public FacturacionDbContext(DbContextOptions<FacturacionDbContext> options) : base(options)
        {
        }
        public DbSet<TblActividadEconomica> ActividadEconomica { get; set; }
        public DbSet<TblDetalleDeFacturas> DetalleDeFactura { get; set; }
        public DbSet<TblEmpresas> Empresa { get; set; }
        public DbSet<TblFacturas> Factura { get; set; }
        public DbSet<TblFormaDePago> FormaDePago { get; set; }
        public DbSet<TblImpuestoAlValorAgregado> ImpuestoAlValorAgregado { get; set; }
        public DbSet<TblLineasDeFacturas> LineaDeFactura { get; set; }
        public DbSet<TblModulo> Modulo { get; set; }
        public DbSet<TblPermiso> Permiso { get; set; }
        public DbSet<TblUsuarios> Usuario { get; set; }
        public DbSet<TblRol> Rol { get; set; }
        public DbSet<TblEstado> Estado { get; set; }
        public DbSet<TblProducto> Producto { get; set; }
        public DbSet<TblMarca> Marca { get; set; }
    }
}
