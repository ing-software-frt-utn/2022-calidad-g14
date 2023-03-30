using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlDeCalidad.AccesoADatos.Migrations
{
    public partial class Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Defectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puesto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineaDeTrabajo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaDeTrabajo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Denominacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LimiteInferiorReproceso = table.Column<int>(type: "int", nullable: false),
                    LimiteSuperiorReproceso = table.Column<int>(type: "int", nullable: false),
                    LimiteInferiorObservado = table.Column<int>(type: "int", nullable: false),
                    LimiteSuperiorObservado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semaforos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    LimiteInferior = table.Column<int>(type: "int", nullable: false),
                    LimiteSuperior = table.Column<int>(type: "int", nullable: false),
                    Contador = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semaforos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraInicio = table.Column<int>(type: "int", nullable: false),
                    HoraFin = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesDeProduccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    SupervisorDeLineaId = table.Column<int>(type: "int", nullable: false),
                    LineaId = table.Column<int>(type: "int", nullable: false),
                    SemaforoObservadoId = table.Column<int>(type: "int", nullable: true),
                    SemaforoReprocesoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesDeProduccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_Empleados_SupervisorDeLineaId",
                        column: x => x.SupervisorDeLineaId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_LineaDeTrabajo_LineaId",
                        column: x => x.LineaId,
                        principalTable: "LineaDeTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_Semaforos_SemaforoObservadoId",
                        column: x => x.SemaforoObservadoId,
                        principalTable: "Semaforos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_Semaforos_SemaforoReprocesoId",
                        column: x => x.SemaforoReprocesoId,
                        principalTable: "Semaforos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Jornadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SupervisorDeCalidadId = table.Column<int>(type: "int", nullable: false),
                    TotalHermanado = table.Column<int>(type: "int", nullable: false),
                    TotalSegunda = table.Column<int>(type: "int", nullable: false),
                    OrdenDeProduccionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jornadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jornadas_Empleados_SupervisorDeCalidadId",
                        column: x => x.SupervisorDeCalidadId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jornadas_OrdenesDeProduccion_OrdenDeProduccionId",
                        column: x => x.OrdenDeProduccionId,
                        principalTable: "OrdenesDeProduccion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jornadas_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParesDePrimera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraPlanilla = table.Column<short>(type: "smallint", nullable: false),
                    HoraReal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<short>(type: "smallint", nullable: false),
                    JornadaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParesDePrimera", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParesDePrimera_Jornadas_JornadaId",
                        column: x => x.JornadaId,
                        principalTable: "Jornadas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegistrosDeDefecto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraPlanilla = table.Column<short>(type: "smallint", nullable: false),
                    HoraReal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pie = table.Column<int>(type: "int", nullable: false),
                    DefectoId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<short>(type: "smallint", nullable: false),
                    JornadaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosDeDefecto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosDeDefecto_Defectos_DefectoId",
                        column: x => x.DefectoId,
                        principalTable: "Defectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrosDeDefecto_Jornadas_JornadaId",
                        column: x => x.JornadaId,
                        principalTable: "Jornadas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jornadas_OrdenDeProduccionId",
                table: "Jornadas",
                column: "OrdenDeProduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Jornadas_SupervisorDeCalidadId",
                table: "Jornadas",
                column: "SupervisorDeCalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Jornadas_TurnoId",
                table: "Jornadas",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_ColorId",
                table: "OrdenesDeProduccion",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_LineaId",
                table: "OrdenesDeProduccion",
                column: "LineaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_ModeloId",
                table: "OrdenesDeProduccion",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_SemaforoObservadoId",
                table: "OrdenesDeProduccion",
                column: "SemaforoObservadoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_SemaforoReprocesoId",
                table: "OrdenesDeProduccion",
                column: "SemaforoReprocesoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_SupervisorDeLineaId",
                table: "OrdenesDeProduccion",
                column: "SupervisorDeLineaId");

            migrationBuilder.CreateIndex(
                name: "IX_ParesDePrimera_JornadaId",
                table: "ParesDePrimera",
                column: "JornadaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosDeDefecto_DefectoId",
                table: "RegistrosDeDefecto",
                column: "DefectoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosDeDefecto_JornadaId",
                table: "RegistrosDeDefecto",
                column: "JornadaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParesDePrimera");

            migrationBuilder.DropTable(
                name: "RegistrosDeDefecto");

            migrationBuilder.DropTable(
                name: "Defectos");

            migrationBuilder.DropTable(
                name: "Jornadas");

            migrationBuilder.DropTable(
                name: "OrdenesDeProduccion");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Colores");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "LineaDeTrabajo");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Semaforos");
        }
    }
}
