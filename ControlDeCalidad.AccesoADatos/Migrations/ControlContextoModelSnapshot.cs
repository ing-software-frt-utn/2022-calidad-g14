﻿// <auto-generated />
using System;
using ControlDeCalidad.AccesoADatos.Contextos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControlDeCalidad.AccesoADatos.Migrations
{
    [DbContext(typeof(ControlContexto))]
    partial class ControlContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colores", (string)null);
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.Defecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Defectos", (string)null);
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Puesto")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Empleados", (string)null);
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.Jornada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrdenDeProduccionId")
                        .HasColumnType("int");

                    b.Property<int>("SupervisorDeCalidadId")
                        .HasColumnType("int");

                    b.Property<int>("TotalHermanado")
                        .HasColumnType("int");

                    b.Property<int>("TotalSegunda")
                        .HasColumnType("int");

                    b.Property<int>("TurnoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrdenDeProduccionId");

                    b.HasIndex("SupervisorDeCalidadId");

                    b.HasIndex("TurnoId");

                    b.ToTable("Jornadas", (string)null);
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.LineaDeTrabajo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LineaDeTrabajo");
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.Modelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Denominacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LimiteInferiorObservado")
                        .HasColumnType("int");

                    b.Property<int>("LimiteInferiorReproceso")
                        .HasColumnType("int");

                    b.Property<int>("LimiteSuperiorObservado")
                        .HasColumnType("int");

                    b.Property<int>("LimiteSuperiorReproceso")
                        .HasColumnType("int");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Modelos", (string)null);
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.OrdenDeProduccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFinalizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("LineaId")
                        .HasColumnType("int");

                    b.Property<int>("ModeloId")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SemaforoObservadoId")
                        .HasColumnType("int");

                    b.Property<int?>("SemaforoReprocesoId")
                        .HasColumnType("int");

                    b.Property<int>("SupervisorDeLineaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("LineaId");

                    b.HasIndex("ModeloId");

                    b.HasIndex("SemaforoObservadoId");

                    b.HasIndex("SemaforoReprocesoId");

                    b.HasIndex("SupervisorDeLineaId");

                    b.ToTable("OrdenesDeProduccion", (string)null);
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.ParDePrimera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<short>("HoraPlanilla")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("HoraReal")
                        .HasColumnType("datetime2");

                    b.Property<int?>("JornadaId")
                        .HasColumnType("int");

                    b.Property<short>("Valor")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("JornadaId");

                    b.ToTable("ParesDePrimera", (string)null);
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.RegistroDeDefecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DefectoId")
                        .HasColumnType("int");

                    b.Property<short>("HoraPlanilla")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("HoraReal")
                        .HasColumnType("datetime2");

                    b.Property<int?>("JornadaId")
                        .HasColumnType("int");

                    b.Property<int>("Pie")
                        .HasColumnType("int");

                    b.Property<short>("Valor")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("DefectoId");

                    b.HasIndex("JornadaId");

                    b.ToTable("RegistrosDeDefecto", (string)null);
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.Semaforo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<int>("Contador")
                        .HasColumnType("int");

                    b.Property<int>("LimiteInferior")
                        .HasColumnType("int");

                    b.Property<int>("LimiteSuperior")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Semaforos", (string)null);
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HoraFin")
                        .HasColumnType("int");

                    b.Property<int>("HoraInicio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Turno");
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.Jornada", b =>
                {
                    b.HasOne("ControlDeCalidad.Dominio.Entidades.OrdenDeProduccion", null)
                        .WithMany("Jornadas")
                        .HasForeignKey("OrdenDeProduccionId");

                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Empleado", "SupervisorDeCalidad")
                        .WithMany()
                        .HasForeignKey("SupervisorDeCalidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SupervisorDeCalidad");

                    b.Navigation("Turno");
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.OrdenDeProduccion", b =>
                {
                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControlDeCalidad.Dominio.Entidades.LineaDeTrabajo", "Linea")
                        .WithMany()
                        .HasForeignKey("LineaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Modelo", "Modelo")
                        .WithMany()
                        .HasForeignKey("ModeloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Semaforo", "SemaforoObservado")
                        .WithMany()
                        .HasForeignKey("SemaforoObservadoId");

                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Semaforo", "SemaforoReproceso")
                        .WithMany()
                        .HasForeignKey("SemaforoReprocesoId");

                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Empleado", "SupervisorDeLinea")
                        .WithMany()
                        .HasForeignKey("SupervisorDeLineaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Linea");

                    b.Navigation("Modelo");

                    b.Navigation("SemaforoObservado");

                    b.Navigation("SemaforoReproceso");

                    b.Navigation("SupervisorDeLinea");
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.ParDePrimera", b =>
                {
                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Jornada", null)
                        .WithMany("ParesDePrimera")
                        .HasForeignKey("JornadaId");
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.RegistroDeDefecto", b =>
                {
                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Defecto", "Defecto")
                        .WithMany()
                        .HasForeignKey("DefectoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControlDeCalidad.Dominio.Entidades.Jornada", null)
                        .WithMany("RegistrosDeDefecto")
                        .HasForeignKey("JornadaId");

                    b.Navigation("Defecto");
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.Jornada", b =>
                {
                    b.Navigation("ParesDePrimera");

                    b.Navigation("RegistrosDeDefecto");
                });

            modelBuilder.Entity("ControlDeCalidad.Dominio.Entidades.OrdenDeProduccion", b =>
                {
                    b.Navigation("Jornadas");
                });
#pragma warning restore 612, 618
        }
    }
}