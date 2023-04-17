﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sistema_facturacion_api.Context;

#nullable disable

namespace sistemafacturacionapi.Contexts.Migrations
{
    [DbContext(typeof(FacturacionDbContext))]
    [Migration("20230415122123_MigracionNumeroNueve")]
    partial class MigracionNumeroNueve
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("sistema_facturacion_api.Data.TblActividadEconomica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CodigoDeLaActividad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreDeLaActividad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblActividadEconomica");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblDetalleDeFacturas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadProducto")
                        .HasColumnType("int");

                    b.Property<int>("FacturaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaDeRegistro")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PrecioUnidad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("TblDetalleDeFacturas");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblEmpresas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TblUsuariosId")
                        .HasColumnType("int");

                    b.Property<string>("rutaImagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TblUsuariosId");

                    b.ToTable("TblEmpresas");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblEstado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblEstado");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblFacturas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<int>("EstadoFacturaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormaDePagoId")
                        .HasColumnType("int");

                    b.Property<decimal>("IVA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LineaDePago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPagado")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TblFacturas");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblFormaDePago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblFormaDePago");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblImpuestoAlValorAgregado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<decimal>("IVA")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("TblImpuestoAlValorAgregado");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblLineasDeFacturas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("LineaDeFactura")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblLineasDeFacturas");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblMarca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblMarca");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblModulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ruta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblModulo");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblPermiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("C")
                        .HasColumnType("bit");

                    b.Property<bool>("D")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModuloId")
                        .HasColumnType("int");

                    b.Property<bool>("R")
                        .HasColumnType("bit");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<bool>("U")
                        .HasColumnType("bit");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("UsuariosId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModuloId");

                    b.HasIndex("RolId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("TblPermiso");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblProductos");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblRol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblRol");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblUsuarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaDeNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreDeUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pasaporte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<string>("TarjetaDeIdentificacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("TblUsuarios");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblDetalleDeFacturas", b =>
                {
                    b.HasOne("sistema_facturacion_api.Data.TblProducto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblEmpresas", b =>
                {
                    b.HasOne("sistema_facturacion_api.Data.TblUsuarios", null)
                        .WithMany("Empresas")
                        .HasForeignKey("TblUsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblPermiso", b =>
                {
                    b.HasOne("sistema_facturacion_api.Data.TblModulo", "Modulo")
                        .WithMany()
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sistema_facturacion_api.Data.TblRol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sistema_facturacion_api.Data.TblUsuarios", "Usuarios")
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modulo");

                    b.Navigation("Rol");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblUsuarios", b =>
                {
                    b.HasOne("sistema_facturacion_api.Data.TblEstado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("sistema_facturacion_api.Data.TblUsuarios", b =>
                {
                    b.Navigation("Empresas");
                });
#pragma warning restore 612, 618
        }
    }
}
