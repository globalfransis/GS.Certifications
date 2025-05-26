using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ale_AlertaTipos",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TablaMaestra = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AceptaDestinatarioVariables = table.Column<bool>(type: "bit", nullable: false),
                    ScriptObtencionVariables = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ale_AlertaTipos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "ale_AlertaTipoUbicaciones",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    AlertaTipoIdm = table.Column<long>(type: "bigint", nullable: false),
                    UbicacionTablaMaestra = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CampoMaestro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UbicacionId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ale_AlertaTipoUbicaciones", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "ale_DestinatarioVariables",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ale_DestinatarioVariables", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "ali_Alicuotas",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    CodigoAFIP = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ali_Alicuotas", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "aud_Sessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    SessionGuid = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserLogin = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByCompanySwitch = table.Column<bool>(type: "bit", nullable: false),
                    EndedByLogout = table.Column<bool>(type: "bit", nullable: false),
                    EndedByTimeOut = table.Column<bool>(type: "bit", nullable: false),
                    EndedByCleanUp = table.Column<bool>(type: "bit", nullable: false),
                    EndedByKill = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aud_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cer_SolicitudCertificacionEstados",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cer_SolicitudCertificacionEstados", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "cny_Organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cny_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "com_CategoriaTipos",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    CodigoArca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_CategoriaTipos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "com_CodigoAutorizacionTipos",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    CodigoArca = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CodigoExterno = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_CodigoAutorizacionTipos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "com_ComprobanteEstados",
                columns: table => new
                {
                    Idm = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_ComprobanteEstados", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "com_ComprobanteTipos",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    CodigoArca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TipoOperacion = table.Column<short>(type: "smallint", nullable: false),
                    DescAbreviada = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NombreComprobante = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Letra = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CodigoExterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_ComprobanteTipos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "com_CondicionVentas",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_CondicionVentas", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "com_EstadosValidacionARCA",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_EstadosValidacionARCA", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "com_ImpuestoTipos",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    General = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_ImpuestoTipos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "com_Origenes",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_Origenes", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "com_TipoEmpresasPortales",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_TipoEmpresasPortales", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "com_UnidadMedidas",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    CodigoAFIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodigoARBA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_UnidadMedidas", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "doc_DocumentoEstados",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doc_DocumentoEstados", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "doc_TipoDocumentos",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequiereVigencia = table.Column<bool>(type: "bit", nullable: false),
                    RequiereValidacion = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doc_TipoDocumentos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "emp_RolTipos",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_RolTipos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "emp_TipoCuentas",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_TipoCuentas", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "gbl_Cultures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gbl_Cultures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gbl_Currencies",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gbl_Currencies", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "gbl_IdentificationTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gbl_IdentificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "geo_Countries",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ISOCode2 = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    ISOCode3 = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_geo_Countries", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "int_InterfazReglaConsecuencias",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_int_InterfazReglaConsecuencias", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "int_TipoOrdenC",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DetalleItems = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_int_TipoOrdenC", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "mlr_ModosLecturas",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mlr_ModosLecturas", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "mpc_ComprobanteEMailExtensiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcesoId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    EmpresaPortalGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComprobanteGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailReceivedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreArchivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mpc_ComprobanteEMailExtensiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ntf_EventoEstados",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_EventoEstados", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "ntf_NotificacionEstados",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_NotificacionEstados", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "ntf_NotificacionEtiquetaTipos",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_NotificacionEtiquetaTipos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "ntf_NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    NotificationLevel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ntf_SubscriptionsKeyValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<long>(type: "bigint", nullable: false),
                    SubscriptionKey = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    SubscriptionValue = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_SubscriptionsKeyValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ntf_TipoNotificaciones",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_TipoNotificaciones", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "orc_OrdenesComprasEstados",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orc_OrdenesComprasEstados", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "par_Parametros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_par_Parametros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "per_PercepcionTipos",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    General = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_per_PercepcionTipos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "prd_EstadoPeriodos",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prd_EstadoPeriodos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "pro_EstadoProcesos",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro_EstadoProcesos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "pro_TipoProcesos",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro_TipoProcesos", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "rep_TaskInvervalsTypes",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rep_TaskInvervalsTypes", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "sec_UserTypes",
                columns: table => new
                {
                    Idm = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_UserTypes", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "sis_Sistemas",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sis_Sistemas", x => x.Idm);
                });

            migrationBuilder.CreateTable(
                name: "ale_AlertaTipoCampoVariable",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    AlertaTipoIdm = table.Column<long>(type: "bigint", nullable: false),
                    TablaMaestra = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CampoMaestro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionColoquial = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Explicacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ObligatorioEnCuerpo = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ale_AlertaTipoCampoVariable", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_ale_AlertaTipoCampoVariable_ale_AlertaTipos_AlertaTipoIdm",
                        column: x => x.AlertaTipoIdm,
                        principalTable: "ale_AlertaTipos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ale_AlertaTipoEstados",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    AlertaTipoIdm = table.Column<long>(type: "bigint", nullable: false),
                    EstadoTablaMaestra = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoCampoMaestro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CampoMaestro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ale_AlertaTipoEstados", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_ale_AlertaTipoEstados_ale_AlertaTipos_AlertaTipoIdm",
                        column: x => x.AlertaTipoIdm,
                        principalTable: "ale_AlertaTipos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ale_AlertaTipoReglaAdicionales",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    AlertaTipoIdm = table.Column<long>(type: "bigint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Explicacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ScriptReglaAdicional = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ale_AlertaTipoReglaAdicionales", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_ale_AlertaTipoReglaAdicionales_ale_AlertaTipos_AlertaTipoIdm",
                        column: x => x.AlertaTipoIdm,
                        principalTable: "ale_AlertaTipos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ale_Alertas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertaTipoIdm = table.Column<long>(type: "bigint", nullable: false),
                    UbicacionIdm = table.Column<long>(type: "bigint", nullable: true),
                    EstadoIdm = table.Column<long>(type: "bigint", nullable: true),
                    Horas = table.Column<int>(type: "int", nullable: false),
                    Para = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    CC = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    CCO = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    Asunto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cuerpo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuerpoRutaHTML = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    DestinatarioVariableIdm = table.Column<short>(type: "smallint", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaHoraDesde = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaHoraHasta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ale_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ale_Alertas_ale_AlertaTipos_AlertaTipoIdm",
                        column: x => x.AlertaTipoIdm,
                        principalTable: "ale_AlertaTipos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ale_Alertas_ale_DestinatarioVariables_DestinatarioVariableIdm",
                        column: x => x.DestinatarioVariableIdm,
                        principalTable: "ale_DestinatarioVariables",
                        principalColumn: "Idm");
                });

            migrationBuilder.CreateTable(
                name: "cny_Company",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cny_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cny_Company_cny_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "cny_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gbl_CulturesOrganization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CultureId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    CultureDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gbl_CulturesOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gbl_CulturesOrganization_cny_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "cny_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_gbl_CulturesOrganization_gbl_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "gbl_Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gbl_IdentificationTax",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gbl_IdentificationTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gbl_IdentificationTax_geo_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "geo_Countries",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "geo_Provinces",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryIdm = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_geo_Provinces", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_geo_Provinces_geo_Countries_CountryIdm",
                        column: x => x.CountryIdm,
                        principalTable: "geo_Countries",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ntf_NotificationTypesOrganizations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_NotificationTypesOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ntf_NotificationTypesOrganizations_cny_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "cny_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ntf_NotificationTypesOrganizations_ntf_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "ntf_NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sys_DomainFs",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserTypeIdm = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_DomainFs", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_sys_DomainFs_sec_UserTypes_UserTypeIdm",
                        column: x => x.UserTypeIdm,
                        principalTable: "sec_UserTypes",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "int_Interfaces",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    SistemaIdm = table.Column<long>(type: "bigint", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CodigoIdentificacionExterna = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescripcionMiddleware = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescripcionExterna = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CodigoInterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_int_Interfaces", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_int_Interfaces_sis_Sistemas_SistemaIdm",
                        column: x => x.SistemaIdm,
                        principalTable: "sis_Sistemas",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ale_AlertaConReglaAdicionales",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertaId = table.Column<long>(type: "bigint", nullable: false),
                    AlertaTipoReglaAdicionalIdm = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ale_AlertaConReglaAdicionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ale_AlertaConReglaAdicionales_ale_Alertas_AlertaId",
                        column: x => x.AlertaId,
                        principalTable: "ale_Alertas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ale_AlertaConReglaAdicionales_ale_AlertaTipoReglaAdicionales_AlertaTipoReglaAdicionalIdm",
                        column: x => x.AlertaTipoReglaAdicionalIdm,
                        principalTable: "ale_AlertaTipoReglaAdicionales",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ale_AlertaEncoladas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertaId = table.Column<long>(type: "bigint", nullable: false),
                    MaestroId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ale_AlertaEncoladas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ale_AlertaEncoladas_ale_Alertas_AlertaId",
                        column: x => x.AlertaId,
                        principalTable: "ale_Alertas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cer_Certificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    TipoEmpresaPortalId = table.Column<short>(type: "smallint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    CantidadAprobaciones = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cer_Certificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cer_Certificaciones_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cer_Certificaciones_com_TipoEmpresasPortales_TipoEmpresaPortalId",
                        column: x => x.TipoEmpresaPortalId,
                        principalTable: "com_TipoEmpresasPortales",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cny_CompanyCurrencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    IsSalesDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cny_CompanyCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cny_CompanyCurrencies_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cny_CompanyCurrencies_cny_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "cny_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cny_CompanyCurrencies_gbl_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "gbl_Currencies",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "com_ConceptosGastosTipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ConceptoContableNombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ConceptoContableValor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_ConceptosGastosTipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_com_ConceptosGastosTipos_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mpc_IntegracionesFacturaPorCorreo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailFolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailsTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoCreateEmpresaPortal = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Actvive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mpc_IntegracionesFacturaPorCorreo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mpc_IntegracionesFacturaPorCorreo_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orc_OrdenesComprasTipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EsRequerida = table.Column<bool>(type: "bit", nullable: false),
                    EsAbierta = table.Column<bool>(type: "bit", nullable: false),
                    EsRecurrente = table.Column<bool>(type: "bit", nullable: false),
                    EsUnica = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orc_OrdenesComprasTipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orc_OrdenesComprasTipos_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "prd_Periodos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Año = table.Column<int>(type: "int", nullable: true),
                    NumeroPeriodo = table.Column<int>(type: "int", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoIdm = table.Column<short>(type: "smallint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prd_Periodos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_prd_Periodos_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_prd_Periodos_prd_EstadoPeriodos_EstadoIdm",
                        column: x => x.EstadoIdm,
                        principalTable: "prd_EstadoPeriodos",
                        principalColumn: "Idm");
                });

            migrationBuilder.CreateTable(
                name: "com_Impuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TipoId = table.Column<short>(type: "smallint", nullable: true),
                    ProvinciaId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    AlicuotaId = table.Column<short>(type: "smallint", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_Impuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_com_Impuestos_ali_Alicuotas_AlicuotaId",
                        column: x => x.AlicuotaId,
                        principalTable: "ali_Alicuotas",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Impuestos_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_com_Impuestos_com_ImpuestoTipos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "com_ImpuestoTipos",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Impuestos_geo_Provinces_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "geo_Provinces",
                        principalColumn: "Idm");
                });

            migrationBuilder.CreateTable(
                name: "geo_Cities",
                columns: table => new
                {
                    Idm = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_geo_Cities", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_geo_Cities_geo_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "geo_Provinces",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "per_Percepciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PercepcionTipoId = table.Column<short>(type: "smallint", nullable: true),
                    ProvinciaId = table.Column<long>(type: "bigint", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_per_Percepciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_per_Percepciones_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_per_Percepciones_geo_Provinces_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "geo_Provinces",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_per_Percepciones_per_PercepcionTipos_PercepcionTipoId",
                        column: x => x.PercepcionTipoId,
                        principalTable: "per_PercepcionTipos",
                        principalColumn: "Idm");
                });

            migrationBuilder.CreateTable(
                name: "ntf_ConfiguracionNotificaciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UltimoEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prioridad = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeSpan>(type: "time", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FrecuenciaHora = table.Column<int>(type: "int", nullable: false),
                    FrecuenciaMinutos = table.Column<int>(type: "int", nullable: false),
                    FrecuenciaSegundos = table.Column<int>(type: "int", nullable: false),
                    DefaultGetNotificationsProcedure = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: true),
                    DomainFIdm = table.Column<long>(type: "bigint", nullable: false),
                    TipoNotificacionIdm = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_ConfiguracionNotificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ntf_ConfiguracionNotificaciones_ntf_TipoNotificaciones_TipoNotificacionIdm",
                        column: x => x.TipoNotificacionIdm,
                        principalTable: "ntf_TipoNotificaciones",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ntf_ConfiguracionNotificaciones_sys_DomainFs_DomainFIdm",
                        column: x => x.DomainFIdm,
                        principalTable: "sys_DomainFs",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pro_Procesos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoIdm = table.Column<long>(type: "bigint", nullable: false),
                    TipoIdm = table.Column<long>(type: "bigint", nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Archivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DominioId = table.Column<long>(type: "bigint", nullable: false),
                    DescripcionError = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro_Procesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pro_Procesos_pro_EstadoProcesos_EstadoIdm",
                        column: x => x.EstadoIdm,
                        principalTable: "pro_EstadoProcesos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pro_Procesos_pro_TipoProcesos_TipoIdm",
                        column: x => x.TipoIdm,
                        principalTable: "pro_TipoProcesos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pro_Procesos_sys_DomainFs_DominioId",
                        column: x => x.DominioId,
                        principalTable: "sys_DomainFs",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_Groups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GroupOwnerId = table.Column<long>(type: "bigint", nullable: true),
                    SystemUse = table.Column<bool>(type: "bit", nullable: false),
                    DomainFIdm = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_Groups_sec_Groups_GroupOwnerId",
                        column: x => x.GroupOwnerId,
                        principalTable: "sec_Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_sec_Groups_sys_DomainFs_DomainFIdm",
                        column: x => x.DomainFIdm,
                        principalTable: "sys_DomainFs",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_Options",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Page = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TargetPath = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ContextKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Transferable = table.Column<bool>(type: "bit", nullable: false),
                    DomainFIdm = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_Options_sec_Options_ParentId",
                        column: x => x.ParentId,
                        principalTable: "sec_Options",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_sec_Options_sys_DomainFs_DomainFIdm",
                        column: x => x.DomainFIdm,
                        principalTable: "sys_DomainFs",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "int_InterfazCampos",
                columns: table => new
                {
                    Idm = table.Column<int>(type: "int", nullable: false),
                    InterfazIdm = table.Column<short>(type: "smallint", nullable: false),
                    NumeroOrdenCampo = table.Column<short>(type: "smallint", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Explicacion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CampoExterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CampoMiddleware = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_int_InterfazCampos", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_int_InterfazCampos_int_Interfaces_InterfazIdm",
                        column: x => x.InterfazIdm,
                        principalTable: "int_Interfaces",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "int_InterfazEstados",
                columns: table => new
                {
                    Idm = table.Column<short>(type: "smallint", nullable: false),
                    InterfazIdm = table.Column<short>(type: "smallint", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CodigoInterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_int_InterfazEstados", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_int_InterfazEstados_int_Interfaces_InterfazIdm",
                        column: x => x.InterfazIdm,
                        principalTable: "int_Interfaces",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "int_InterfazReglas",
                columns: table => new
                {
                    Idm = table.Column<int>(type: "int", nullable: false),
                    InterfazIdm = table.Column<short>(type: "smallint", nullable: false),
                    InterfazReglaConsecuenciaIdm = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_int_InterfazReglas", x => x.Idm);
                    table.ForeignKey(
                        name: "FK_int_InterfazReglas_int_Interfaces_InterfazIdm",
                        column: x => x.InterfazIdm,
                        principalTable: "int_Interfaces",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_int_InterfazReglas_int_InterfazReglaConsecuencias_InterfazReglaConsecuenciaIdm",
                        column: x => x.InterfazReglaConsecuenciaIdm,
                        principalTable: "int_InterfazReglaConsecuencias",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "doc_DocumentosRequeridos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificacionId = table.Column<int>(type: "int", nullable: false),
                    TipoId = table.Column<short>(type: "smallint", nullable: false),
                    Obligatorio = table.Column<bool>(type: "bit", nullable: false),
                    SobreescribeVigencia = table.Column<bool>(type: "bit", nullable: false),
                    VigenciaDiasCustom = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doc_DocumentosRequeridos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_doc_DocumentosRequeridos_cer_Certificaciones_CertificacionId",
                        column: x => x.CertificacionId,
                        principalTable: "cer_Certificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_doc_DocumentosRequeridos_doc_TipoDocumentos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "doc_TipoDocumentos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emp_CompanyExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    PaisId = table.Column<long>(type: "bigint", nullable: false),
                    ProvinciaId = table.Column<long>(type: "bigint", nullable: true),
                    CiudadId = table.Column<long>(type: "bigint", nullable: true),
                    IdentificadorTributario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CiudadDescripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TelefonoPrincipal = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TelefonoAlternativo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    EmailPrincipal = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EmailAlternativo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ContactoAlternativo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TipoResponsableId = table.Column<short>(type: "smallint", nullable: false),
                    NumeroIngresosBrutos = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TipoCuentaId = table.Column<short>(type: "smallint", nullable: true),
                    CuentaBancaria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdMoneda = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_CompanyExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emp_CompanyExtras_cny_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "cny_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emp_CompanyExtras_com_CategoriaTipos_TipoResponsableId",
                        column: x => x.TipoResponsableId,
                        principalTable: "com_CategoriaTipos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_emp_CompanyExtras_emp_TipoCuentas_TipoCuentaId",
                        column: x => x.TipoCuentaId,
                        principalTable: "emp_TipoCuentas",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_emp_CompanyExtras_geo_Cities_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "geo_Cities",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_emp_CompanyExtras_geo_Countries_PaisId",
                        column: x => x.PaisId,
                        principalTable: "geo_Countries",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emp_CompanyExtras_geo_Provinces_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "geo_Provinces",
                        principalColumn: "Idm");
                });

            migrationBuilder.CreateTable(
                name: "emp_EmpresasPortales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CodigoProveedor = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreFantasia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdentificadorTributario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GranContribuyente = table.Column<bool>(type: "bit", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    PaisId = table.Column<long>(type: "bigint", nullable: true),
                    ProvinciaId = table.Column<long>(type: "bigint", nullable: true),
                    CiudadId = table.Column<long>(type: "bigint", nullable: true),
                    CiudadDescripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TelefonoPrincipal = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TelefonoAlternativo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    EmailPrincipal = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EmailAlternativo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ContactoAlternativo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TipoResponsableId = table.Column<short>(type: "smallint", nullable: true),
                    NumeroIngresosBrutos = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TipoCuentaId = table.Column<short>(type: "smallint", nullable: true),
                    CuentaBancaria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaginaWeb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RedesSociales = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DescripcionEmpresa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProductosServiciosOfrecidos = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReferenciasComerciales = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Confirmado = table.Column<bool>(type: "bit", nullable: false),
                    TipoId = table.Column<short>(type: "smallint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_EmpresasPortales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortales_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortales_cny_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "cny_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortales_com_CategoriaTipos_TipoResponsableId",
                        column: x => x.TipoResponsableId,
                        principalTable: "com_CategoriaTipos",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortales_com_TipoEmpresasPortales_TipoId",
                        column: x => x.TipoId,
                        principalTable: "com_TipoEmpresasPortales",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortales_emp_TipoCuentas_TipoCuentaId",
                        column: x => x.TipoCuentaId,
                        principalTable: "emp_TipoCuentas",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortales_geo_Cities_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "geo_Cities",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortales_geo_Countries_PaisId",
                        column: x => x.PaisId,
                        principalTable: "geo_Countries",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortales_geo_Provinces_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "geo_Provinces",
                        principalColumn: "Idm");
                });

            migrationBuilder.CreateTable(
                name: "com_PercepcionCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PercepcionId = table.Column<short>(type: "smallint", nullable: false),
                    PercepcionId1 = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_PercepcionCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_com_PercepcionCompanies_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_com_PercepcionCompanies_per_Percepciones_PercepcionId1",
                        column: x => x.PercepcionId1,
                        principalTable: "per_Percepciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ntf_Notificaciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    ConfiguracionNotificacionId = table.Column<long>(type: "bigint", nullable: true),
                    Destinatarios = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    ConCopia = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    ConCopiaOculta = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    Adjuntos = table.Column<bool>(type: "bit", nullable: false),
                    EstadoIdm = table.Column<long>(type: "bigint", nullable: false),
                    FechaEncolado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EsReenvio = table.Column<bool>(type: "bit", nullable: true),
                    FechaEnviado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Intentos = table.Column<short>(type: "smallint", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, defaultValue: "Notificacion"),
                    Asunto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cuerpo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ntf_Notificaciones_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ntf_Notificaciones_ntf_ConfiguracionNotificaciones_ConfiguracionNotificacionId",
                        column: x => x.ConfiguracionNotificacionId,
                        principalTable: "ntf_ConfiguracionNotificaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ntf_Notificaciones_ntf_NotificacionEstados_EstadoIdm",
                        column: x => x.EstadoIdm,
                        principalTable: "ntf_NotificacionEstados",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "emp_GruposDocsComprasTipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenCompraTipoId = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_GruposDocsComprasTipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emp_GruposDocsComprasTipos_orc_OrdenesComprasTipos_OrdenCompraTipoId",
                        column: x => x.OrdenCompraTipoId,
                        principalTable: "orc_OrdenesComprasTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emp_GruposDocsComprasTipos_sec_Groups_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "sec_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ntf_NotificationTypesOrganizationsGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationTypeOrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_NotificationTypesOrganizationsGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ntf_NotificationTypesOrganizationsGroups_ntf_NotificationTypesOrganizations_NotificationTypeOrganizationId",
                        column: x => x.NotificationTypeOrganizationId,
                        principalTable: "ntf_NotificationTypesOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ntf_NotificationTypesOrganizationsGroups_sec_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "sec_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_GroupsOrganizations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_GroupsOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_GroupsOrganizations_cny_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "cny_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_GroupsOrganizations_sec_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "sec_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GroupOwnerId = table.Column<long>(type: "bigint", nullable: false),
                    SystemUse = table.Column<bool>(type: "bit", nullable: false),
                    DomainFIdm = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_Roles_sec_Groups_GroupOwnerId",
                        column: x => x.GroupOwnerId,
                        principalTable: "sec_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_Roles_sys_DomainFs_DomainFIdm",
                        column: x => x.DomainFIdm,
                        principalTable: "sys_DomainFs",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NormalizedLogin = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    RequiresNewPassword = table.Column<bool>(type: "bit", nullable: false),
                    PasswordRecoveryToken = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ActivationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTypeIdm = table.Column<int>(type: "int", nullable: false),
                    GroupOwnerId = table.Column<long>(type: "bigint", nullable: false),
                    SystemUse = table.Column<bool>(type: "bit", nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    LoginFailedAttempts = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_Users_sec_Groups_GroupOwnerId",
                        column: x => x.GroupOwnerId,
                        principalTable: "sec_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_Users_sec_UserTypes_UserTypeIdm",
                        column: x => x.UserTypeIdm,
                        principalTable: "sec_UserTypes",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rep_ReportTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StoreProcedureName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OptionId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rep_ReportTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rep_ReportTypes_sec_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "sec_Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_Grants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OptionId = table.Column<long>(type: "bigint", nullable: true),
                    Transferable = table.Column<bool>(type: "bit", nullable: false),
                    DomainFIdm = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_Grants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_Grants_sec_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "sec_Options",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_sec_Grants_sys_DomainFs_DomainFIdm",
                        column: x => x.DomainFIdm,
                        principalTable: "sys_DomainFs",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_Pages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContextualOptionId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_Pages_sec_Options_ContextualOptionId",
                        column: x => x.ContextualOptionId,
                        principalTable: "sec_Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "int_InterfazProcesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterfazIdm = table.Column<short>(type: "smallint", nullable: false),
                    EstadoIdm = table.Column<short>(type: "smallint", nullable: false),
                    EstadoModificacionFechaHora = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchivoNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CantidadRegistrosLeidos = table.Column<int>(type: "int", nullable: false),
                    CantidadRegistrosIgnorados = table.Column<int>(type: "int", nullable: false),
                    CantidadRegistrosNoProcesados = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_int_InterfazProcesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_int_InterfazProcesos_int_Interfaces_InterfazIdm",
                        column: x => x.InterfazIdm,
                        principalTable: "int_Interfaces",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_int_InterfazProcesos_int_InterfazEstados_EstadoIdm",
                        column: x => x.EstadoIdm,
                        principalTable: "int_InterfazEstados",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cer_SolicitudCertificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    CertificacionId = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<short>(type: "smallint", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaModificacionEstado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VigenciaDesde = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VigenciaHasta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrigenId = table.Column<short>(type: "smallint", nullable: true),
                    PropietarioActualId = table.Column<short>(type: "smallint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cer_SolicitudCertificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cer_SolicitudCertificaciones_cer_Certificaciones_CertificacionId",
                        column: x => x.CertificacionId,
                        principalTable: "cer_Certificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cer_SolicitudCertificaciones_cer_SolicitudCertificacionEstados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "cer_SolicitudCertificacionEstados",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cer_SolicitudCertificaciones_com_Origenes_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "com_Origenes",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_cer_SolicitudCertificaciones_com_Origenes_PropietarioActualId",
                        column: x => x.PropietarioActualId,
                        principalTable: "com_Origenes",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_cer_SolicitudCertificaciones_emp_EmpresasPortales_SocioId",
                        column: x => x.SocioId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "com_Comprobantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NroIdentificacionFiscalPro = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DomicilioPro = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CategoriaTipoEmisorId = table.Column<short>(type: "smallint", nullable: true),
                    ComprobanteTipoId = table.Column<short>(type: "smallint", nullable: true),
                    CategoriaTipoReceptorId = table.Column<short>(type: "smallint", nullable: true),
                    NroIdentificacionFiscalCompany = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PuntoVenta = table.Column<int>(type: "int", nullable: false),
                    Letra = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaVencimientoCodigoAutorizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRegistracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoCodigoAutorizacionId = table.Column<short>(type: "smallint", nullable: true),
                    CodigoAutorizacion = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MonedaId = table.Column<long>(type: "bigint", nullable: true),
                    ImporteNeto = table.Column<decimal>(type: "money", nullable: false),
                    ImporteTotal = table.Column<decimal>(type: "money", nullable: false),
                    ImporteIVA = table.Column<decimal>(type: "money", nullable: false),
                    ImporteBonificacion = table.Column<decimal>(type: "money", nullable: false),
                    ImportePercepcionIVA = table.Column<decimal>(type: "money", nullable: false),
                    ImportePercepcionIIBB = table.Column<decimal>(type: "money", nullable: false),
                    ImportePercepcionMunicipal = table.Column<decimal>(type: "money", nullable: false),
                    ImporteImpuestosInternos = table.Column<decimal>(type: "money", nullable: false),
                    ImporteOtrosTributosProv = table.Column<decimal>(type: "money", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    CodigoHES = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ComprobanteEstadoId = table.Column<int>(type: "int", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MotivoRechazo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaRechazo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioRechazo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ValidacionQR = table.Column<bool>(type: "bit", nullable: true),
                    QRValor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoValidacionARCAQRId = table.Column<short>(type: "smallint", nullable: true),
                    EstadoValidacionARCAId = table.Column<short>(type: "smallint", nullable: true),
                    ObservacionesARCA = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ObservacionesARCAQR = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PeriodoId = table.Column<int>(type: "int", nullable: true),
                    OrigenId = table.Column<short>(type: "smallint", nullable: true),
                    PropietarioActualId = table.Column<short>(type: "smallint", nullable: true),
                    CondicionVentaId = table.Column<short>(type: "smallint", nullable: true),
                    NroRemito = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NroOrdenCompra = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cotizacion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NombreArchivo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_Comprobantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_CategoriaTipos_CategoriaTipoEmisorId",
                        column: x => x.CategoriaTipoEmisorId,
                        principalTable: "com_CategoriaTipos",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_CategoriaTipos_CategoriaTipoReceptorId",
                        column: x => x.CategoriaTipoReceptorId,
                        principalTable: "com_CategoriaTipos",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_CodigoAutorizacionTipos_TipoCodigoAutorizacionId",
                        column: x => x.TipoCodigoAutorizacionId,
                        principalTable: "com_CodigoAutorizacionTipos",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_ComprobanteEstados_ComprobanteEstadoId",
                        column: x => x.ComprobanteEstadoId,
                        principalTable: "com_ComprobanteEstados",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_ComprobanteTipos_ComprobanteTipoId",
                        column: x => x.ComprobanteTipoId,
                        principalTable: "com_ComprobanteTipos",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_CondicionVentas_CondicionVentaId",
                        column: x => x.CondicionVentaId,
                        principalTable: "com_CondicionVentas",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_EstadosValidacionARCA_EstadoValidacionARCAId",
                        column: x => x.EstadoValidacionARCAId,
                        principalTable: "com_EstadosValidacionARCA",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_EstadosValidacionARCA_EstadoValidacionARCAQRId",
                        column: x => x.EstadoValidacionARCAQRId,
                        principalTable: "com_EstadosValidacionARCA",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_Origenes_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "com_Origenes",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_com_Origenes_PropietarioActualId",
                        column: x => x.PropietarioActualId,
                        principalTable: "com_Origenes",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_emp_EmpresasPortales_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_gbl_Currencies_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "gbl_Currencies",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_com_Comprobantes_prd_Periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "prd_Periodos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "emp_EmpresaCurrency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaPortalId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    MonedaPorDefecto = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_EmpresaCurrency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emp_EmpresaCurrency_emp_EmpresasPortales_EmpresaPortalId",
                        column: x => x.EmpresaPortalId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emp_EmpresaCurrency_gbl_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "gbl_Currencies",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "emp_EmpresaRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaPortalId = table.Column<int>(type: "int", nullable: false),
                    RolTipoId = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_EmpresaRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emp_EmpresaRoles_emp_EmpresasPortales_EmpresaPortalId",
                        column: x => x.EmpresaPortalId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emp_EmpresaRoles_emp_RolTipos_RolTipoId",
                        column: x => x.RolTipoId,
                        principalTable: "emp_RolTipos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emp_EmpresasPortalesDocsComprasTipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaPortalId = table.Column<int>(type: "int", nullable: false),
                    OrdenCompraTipoId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_EmpresasPortalesDocsComprasTipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortalesDocsComprasTipos_emp_EmpresasPortales_EmpresaPortalId",
                        column: x => x.EmpresaPortalId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortalesDocsComprasTipos_orc_OrdenesComprasTipos_OrdenCompraTipoId",
                        column: x => x.OrdenCompraTipoId,
                        principalTable: "orc_OrdenesComprasTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "emp_EmpresasPortalesGastosTipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaPortalId = table.Column<int>(type: "int", nullable: false),
                    ConceptoGastoTipoId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp_EmpresasPortalesGastosTipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortalesGastosTipos_com_ConceptosGastosTipos_ConceptoGastoTipoId",
                        column: x => x.ConceptoGastoTipoId,
                        principalTable: "com_ConceptosGastosTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emp_EmpresasPortalesGastosTipos_emp_EmpresasPortales_EmpresaPortalId",
                        column: x => x.EmpresaPortalId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpresasAlicuotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaPortalId = table.Column<int>(type: "int", nullable: false),
                    AlicuotaIdm = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresasAlicuotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpresasAlicuotas_ali_Alicuotas_AlicuotaIdm",
                        column: x => x.AlicuotaIdm,
                        principalTable: "ali_Alicuotas",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresasAlicuotas_emp_EmpresasPortales_EmpresaPortalId",
                        column: x => x.EmpresaPortalId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpresasModosLecturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaPortalId = table.Column<int>(type: "int", nullable: false),
                    ModoLecturaIdm = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresasModosLecturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpresasModosLecturas_emp_EmpresasPortales_EmpresaPortalId",
                        column: x => x.EmpresaPortalId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpresasModosLecturas_mlr_ModosLecturas_ModoLecturaIdm",
                        column: x => x.ModoLecturaIdm,
                        principalTable: "mlr_ModosLecturas",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mpc_IntegracionFacturaPorCorreoDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntegracionFacturaPorCorreoId = table.Column<int>(type: "int", nullable: false),
                    EmpresaPortalId = table.Column<int>(type: "int", nullable: false),
                    MailsFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Actvive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mpc_IntegracionFacturaPorCorreoDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mpc_IntegracionFacturaPorCorreoDetalles_emp_EmpresasPortales_EmpresaPortalId",
                        column: x => x.EmpresaPortalId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mpc_IntegracionFacturaPorCorreoDetalles_mpc_IntegracionesFacturaPorCorreo_IntegracionFacturaPorCorreoId",
                        column: x => x.IntegracionFacturaPorCorreoId,
                        principalTable: "mpc_IntegracionesFacturaPorCorreo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orc_OrdenesCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroOrden = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpresaPortalId = table.Column<int>(type: "int", nullable: true),
                    OrdenCompraTipoId = table.Column<int>(type: "int", nullable: true),
                    CodigoHES = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodigoQAD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrdenCompraEstadoIdm1 = table.Column<short>(type: "smallint", nullable: true),
                    OrdenCompraEstadoIdm = table.Column<int>(type: "int", nullable: true),
                    Importe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonedaId = table.Column<long>(type: "bigint", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orc_OrdenesCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orc_OrdenesCompras_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orc_OrdenesCompras_emp_EmpresasPortales_EmpresaPortalId",
                        column: x => x.EmpresaPortalId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_orc_OrdenesCompras_gbl_Currencies_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "gbl_Currencies",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_orc_OrdenesCompras_orc_OrdenesComprasEstados_OrdenCompraEstadoIdm1",
                        column: x => x.OrdenCompraEstadoIdm1,
                        principalTable: "orc_OrdenesComprasEstados",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_orc_OrdenesCompras_orc_OrdenesComprasTipos_OrdenCompraTipoId",
                        column: x => x.OrdenCompraTipoId,
                        principalTable: "orc_OrdenesComprasTipos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ntf_Eventos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoIdm = table.Column<short>(type: "smallint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Asunto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventTimestamp = table.Column<long>(type: "bigint", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Intentos = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Razon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificacionId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ntf_Eventos_ntf_EventoEstados_EstadoIdm",
                        column: x => x.EstadoIdm,
                        principalTable: "ntf_EventoEstados",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ntf_Eventos_ntf_Notificaciones_NotificacionId",
                        column: x => x.NotificacionId,
                        principalTable: "ntf_Notificaciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ntf_NotificacionAdjuntos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificacionId = table.Column<long>(type: "bigint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EsRecursoExternoIncrustado = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_NotificacionAdjuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ntf_NotificacionAdjuntos_ntf_Notificaciones_NotificacionId",
                        column: x => x.NotificacionId,
                        principalTable: "ntf_Notificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ntf_NotificacionEtiquetas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificacionEtiquetaTipoIdm = table.Column<short>(type: "smallint", nullable: true),
                    NotificacionId = table.Column<long>(type: "bigint", nullable: false),
                    Etiqueta = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_NotificacionEtiquetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ntf_NotificacionEtiquetas_ntf_Notificaciones_NotificacionId",
                        column: x => x.NotificacionId,
                        principalTable: "ntf_Notificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ntf_NotificacionEtiquetas_ntf_NotificacionEtiquetaTipos_NotificacionEtiquetaTipoIdm",
                        column: x => x.NotificacionEtiquetaTipoIdm,
                        principalTable: "ntf_NotificacionEtiquetaTipos",
                        principalColumn: "Idm");
                });

            migrationBuilder.CreateTable(
                name: "sec_GroupsRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_GroupsRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_GroupsRoles_sec_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "sec_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_GroupsRoles_sec_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "sec_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_RolesOptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    OptionId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_RolesOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_RolesOptions_sec_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "sec_Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_RolesOptions_sec_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "sec_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ntf_Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Dismissed = table.Column<bool>(type: "bit", nullable: false),
                    Readed = table.Column<bool>(type: "bit", nullable: false),
                    NotificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ntf_Notifications_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ntf_Notifications_cny_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "cny_Organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ntf_Notifications_ntf_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "ntf_NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ntf_Notifications_sec_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "sec_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ntf_Subscriptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Criteria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ntf_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ntf_Subscriptions_sec_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "sec_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "sec_CompaniesUsersGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_CompaniesUsersGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_CompaniesUsersGroups_cny_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "cny_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_CompaniesUsersGroups_sec_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "sec_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_CompaniesUsersGroups_sec_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "sec_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_UserActivities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    UltimoAcceso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimoAccesoFallido = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrigenOS = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OrigenUserAgent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_UserActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_UserActivities_sec_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "sec_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_UserExternos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DomainFIdm = table.Column<long>(type: "bigint", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaPortalId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistracion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Habilitado = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_UserExternos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_UserExternos_emp_EmpresasPortales_EmpresaPortalId",
                        column: x => x.EmpresaPortalId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_UserExternos_sec_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "sec_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_UserExternos_sys_DomainFs_DomainFIdm",
                        column: x => x.DomainFIdm,
                        principalTable: "sys_DomainFs",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sys_UserDomainFs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DomainFIdm = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_UserDomainFs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sys_UserDomainFs_sec_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "sec_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sys_UserDomainFs_sys_DomainFs_DomainFIdm",
                        column: x => x.DomainFIdm,
                        principalTable: "sys_DomainFs",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rep_ReportScheduleTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskIntervalTypeIdm = table.Column<long>(type: "bigint", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    DayOfMonth = table.Column<int>(type: "int", nullable: false),
                    ExecutionDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextRunDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rep_ReportScheduleTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rep_ReportScheduleTasks_rep_ReportTypes_ReportTypeId",
                        column: x => x.ReportTypeId,
                        principalTable: "rep_ReportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rep_ReportScheduleTasks_rep_TaskInvervalsTypes_TaskIntervalTypeIdm",
                        column: x => x.TaskIntervalTypeIdm,
                        principalTable: "rep_TaskInvervalsTypes",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rep_ReportTypeColumns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HeaderName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rep_ReportTypeColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rep_ReportTypeColumns_rep_ReportTypes_ReportTypeId",
                        column: x => x.ReportTypeId,
                        principalTable: "rep_ReportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rep_ReportTypeParameters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportTypeId = table.Column<long>(type: "bigint", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PublicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PublicDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TableType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ControlType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ControlValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rep_ReportTypeParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rep_ReportTypeParameters_rep_ReportTypes_ReportTypeId",
                        column: x => x.ReportTypeId,
                        principalTable: "rep_ReportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_RolesGrants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    GrantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_RolesGrants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_RolesGrants_sec_Grants_GrantId",
                        column: x => x.GrantId,
                        principalTable: "sec_Grants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sec_RolesGrants_sec_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "sec_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "int_InterfazProcesoReglaEnforzadas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterfazProcesoId = table.Column<int>(type: "int", nullable: false),
                    RegistroNumero = table.Column<int>(type: "int", nullable: false),
                    InterfazCampoIdm = table.Column<int>(type: "int", nullable: true),
                    InterfazReglaIdm = table.Column<int>(type: "int", nullable: false),
                    CampoValor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CampoValorRelacionado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_int_InterfazProcesoReglaEnforzadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_int_InterfazProcesoReglaEnforzadas_int_InterfazCampos_InterfazCampoIdm",
                        column: x => x.InterfazCampoIdm,
                        principalTable: "int_InterfazCampos",
                        principalColumn: "Idm");
                    table.ForeignKey(
                        name: "FK_int_InterfazProcesoReglaEnforzadas_int_InterfazProcesos_InterfazProcesoId",
                        column: x => x.InterfazProcesoId,
                        principalTable: "int_InterfazProcesos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_int_InterfazProcesoReglaEnforzadas_int_InterfazReglas_InterfazReglaIdm",
                        column: x => x.InterfazReglaIdm,
                        principalTable: "int_InterfazReglas",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "doc_DocumentosCargados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    DocumentoRequeridoId = table.Column<int>(type: "int", nullable: false),
                    ArchivoURL = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: true),
                    FechaDesde = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoId = table.Column<short>(type: "smallint", nullable: false),
                    ValidadoPorId = table.Column<long>(type: "bigint", nullable: true),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doc_DocumentosCargados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_doc_DocumentosCargados_cer_SolicitudCertificaciones_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "cer_SolicitudCertificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_doc_DocumentosCargados_doc_DocumentoEstados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "doc_DocumentoEstados",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_doc_DocumentosCargados_doc_DocumentosRequeridos_DocumentoRequeridoId",
                        column: x => x.DocumentoRequeridoId,
                        principalTable: "doc_DocumentosRequeridos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_doc_DocumentosCargados_sec_Users_ValidadoPorId",
                        column: x => x.ValidadoPorId,
                        principalTable: "sec_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "com_ComprobanteDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComprobanteId = table.Column<int>(type: "int", nullable: false),
                    UnidadMedidaId = table.Column<short>(type: "smallint", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "money", nullable: false),
                    ImporteBonificacion = table.Column<decimal>(type: "money", nullable: false),
                    Subtotal = table.Column<decimal>(type: "money", nullable: false),
                    ImporteIVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Exento = table.Column<bool>(type: "bit", nullable: false),
                    Alicuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrdenCompraId = table.Column<int>(type: "int", nullable: true),
                    Detalle = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_ComprobanteDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_com_ComprobanteDetalles_com_Comprobantes_ComprobanteId",
                        column: x => x.ComprobanteId,
                        principalTable: "com_Comprobantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_com_ComprobanteDetalles_com_UnidadMedidas_UnidadMedidaId",
                        column: x => x.UnidadMedidaId,
                        principalTable: "com_UnidadMedidas",
                        principalColumn: "Idm");
                });

            migrationBuilder.CreateTable(
                name: "com_ImpuestoDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComprobanteId = table.Column<int>(type: "int", nullable: false),
                    ImpuestoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ImporteTotal = table.Column<decimal>(type: "money", nullable: false),
                    Alicuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_ImpuestoDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_com_ImpuestoDetalles_com_Comprobantes_ComprobanteId",
                        column: x => x.ComprobanteId,
                        principalTable: "com_Comprobantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_com_ImpuestoDetalles_com_Impuestos_ImpuestoId",
                        column: x => x.ImpuestoId,
                        principalTable: "com_Impuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "com_PercepcionDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComprobanteId = table.Column<int>(type: "int", nullable: false),
                    PercepcionId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Alicuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImporteTotal = table.Column<decimal>(type: "money", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_PercepcionDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_com_PercepcionDetalles_com_Comprobantes_ComprobanteId",
                        column: x => x.ComprobanteId,
                        principalTable: "com_Comprobantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_com_PercepcionDetalles_per_Percepciones_PercepcionId",
                        column: x => x.PercepcionId,
                        principalTable: "per_Percepciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sec_UsuarioEmpresaPortalRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioEmpresaPortalId = table.Column<long>(type: "bigint", nullable: false),
                    RolTipoId = table.Column<short>(type: "smallint", nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sec_UsuarioEmpresaPortalRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sec_UsuarioEmpresaPortalRoles_emp_RolTipos_RolTipoId",
                        column: x => x.RolTipoId,
                        principalTable: "emp_RolTipos",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sec_UsuarioEmpresaPortalRoles_sec_UserExternos_UsuarioEmpresaPortalId",
                        column: x => x.UsuarioEmpresaPortalId,
                        principalTable: "sec_UserExternos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rep_ReportTypeParametersValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportScheduleTaskId = table.Column<long>(type: "bigint", nullable: false),
                    ReportTypeParameterId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rep_ReportTypeParametersValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rep_ReportTypeParametersValues_rep_ReportScheduleTasks_ReportScheduleTaskId",
                        column: x => x.ReportScheduleTaskId,
                        principalTable: "rep_ReportScheduleTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rep_ReportTypeParametersValues_rep_ReportTypeParameters_ReportTypeParameterId",
                        column: x => x.ReportTypeParameterId,
                        principalTable: "rep_ReportTypeParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "int_InterfazProcesoValidacionAdicionales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterfazProcesoReglaEnforzadaId = table.Column<long>(type: "bigint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    __MigId = table.Column<int>(type: "int", nullable: true),
                    __MigCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_int_InterfazProcesoValidacionAdicionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_int_InterfazProcesoValidacionAdicionales_int_InterfazProcesoReglaEnforzadas_InterfazProcesoReglaEnforzadaId",
                        column: x => x.InterfazProcesoReglaEnforzadaId,
                        principalTable: "int_InterfazProcesoReglaEnforzadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ali_Alicuotas",
                columns: new[] { "Idm", "CodigoAFIP", "Created", "CreatedBy", "IsDeleted", "Modified", "ModifiedBy", "Session", "Valor", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, "0003", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 0.00m, null, null },
                    { (short)2, "0004", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 10.50m, null, null },
                    { (short)3, "0005", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 21.00m, null, null },
                    { (short)4, "0006", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 27.00m, null, null },
                    { (short)5, "0008", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 5.00m, null, null },
                    { (short)6, "0009", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 2.50m, null, null }
                });

            migrationBuilder.InsertData(
                table: "cer_SolicitudCertificacionEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Pendiente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Presentada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Aprobada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rechazada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)5, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Borrador", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "cny_Organization",
                columns: new[] { "Id", "Created", "CreatedBy", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[] { 39L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Globalsis", null, null, null });

            migrationBuilder.InsertData(
                table: "com_CategoriaTipos",
                columns: new[] { "Idm", "CodigoArca", "CodigoExterno", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, "1", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Responsable Inscripto", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, "2", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Responsable no Inscripto", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, "3", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "No Responsable", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, "4", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Sujeto Exento", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)5, "5", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Consumidor Final", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)6, "6", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Responsable Monotributo", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)7, "7", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Sujeto no Categorizado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)8, "8", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Importador del Exterior", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)9, "9", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cliente del Exterior", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)10, "10", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Liberado Ley Nº 19.740", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)11, "11", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Responsable inscripto Agente de percepción", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "com_CodigoAutorizacionTipos",
                columns: new[] { "Idm", "CodigoArca", "CodigoExterno", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, null, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CAE", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, null, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CAEA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, null, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CAI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "com_ComprobanteEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Nombre", "Session", "Valor", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Archivo subido", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Archivo subido", null, "100", null, null },
                    { 2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "En proceso de carga", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "En proceso de carga", null, "200", null, null },
                    { 3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Registro con errores ARCA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Registro con errores ARCA", null, "300", null, null },
                    { 4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Registro confirmado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Registro confirmado", null, "400", null, null },
                    { 5, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Acuse Recibo Cliente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Acuse Recibo Cliente", null, "500", null, null },
                    { 6, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Aprobada Cliente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Aprobada Cliente", null, "600", null, null },
                    { 7, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rechazada Cliente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rechazada Cliente", null, "700", null, null },
                    { 8, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Borrador", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Borrador", null, "800", null, null },
                    { 9, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Autorizado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Autorizado", null, "900", null, null }
                });

            migrationBuilder.InsertData(
                table: "com_ComprobanteTipos",
                columns: new[] { "Idm", "CodigoArca", "CodigoExterno", "Created", "CreatedBy", "DescAbreviada", "Descripcion", "IsDeleted", "Letra", "Modified", "ModifiedBy", "NombreComprobante", "Session", "TipoOperacion", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, "1", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FC-A", "Factura A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)2, "2", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ND-A", "Nota de Débito A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)3, "3", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NC-A", "Nota de Crédito A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)4, "4", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RC-A", "Recibo A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)5, "5", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NV-A", "Nota de venta al contado A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)6, "6", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FC-B", "Factura B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)7, "7", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ND-B", "Nota de Débito B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null }
                });

            migrationBuilder.InsertData(
                table: "com_ComprobanteTipos",
                columns: new[] { "Idm", "CodigoArca", "CodigoExterno", "Created", "CreatedBy", "DescAbreviada", "Descripcion", "IsDeleted", "Letra", "Modified", "ModifiedBy", "NombreComprobante", "Session", "TipoOperacion", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)8, "8", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NC-B", "Nota de Crédito B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)9, "9", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RC-B", "Recibo B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)10, "10", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NV-B", "Nota de venta al contado B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)11, "39", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Comprobante A que cumple con la R.G. N° 3419", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)12, "40", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Comprobante B que cumple con la R.G. N° 3419", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)13, "60", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Cuenta de venta y líquido producto A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)14, "61", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Cuenta de venta y líquido producto B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)15, "63", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LG-A", "Liquidación A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)16, "64", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LG-B", "Liquidación B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)17, "11", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FC-C", "Factura C", false, "C", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)18, "14", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DA-A", "Documento aduanero", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)19, "15", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RC-C", "Recibo C", false, "C", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)20, "16", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Nota de venta al contado C", false, "C", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)21, "19", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FC-E", "Factura E", false, "E", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)22, "20", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ND-E", "Nota de Débito E", false, "E", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)23, "21", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NC-E", "Nota de Crédito E", false, "E", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)24, "22", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Facturas - Permiso Export simplificado - Dto.855/97", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)25, "30", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Comprobantes de compra de bienes usados", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)26, "34", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Comprob A del Anexo I, Apartado A, inc.f), RG 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)27, "35", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Comprob B del Anexo I, Apartado A, inc.f), RG 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)28, "36", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Comprob C del Anexo I, Apartado A, inc.f), RG 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)29, "37", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Notas de Débito o doc equiv que cumplan con la RG 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)30, "38", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Notas de Crédito o doc equiv que cumplan con la RG 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)31, "39", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Otros comprob A que cumplan con la R.G 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)32, "40", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Otros comprob B que cumplan con la R.G 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)33, "41", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Otros comprob C que cumplan con la R.G 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)34, "51", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FC-M", "Facturas M", false, "M", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)35, "52", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ND-M", "Notas de Débito M", false, "M", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)36, "53", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NC-M", "Notas de Crédito M", false, "M", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)37, "54", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RC-M", "Recibos M", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)38, "55", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NV-M", "Notas de Venta al contado M", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)39, "56", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Comprobantes M del Anexo I, Ap A, inc.f), RG 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)40, "57", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Otros comprob M que cumplan con la RG 1415", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)41, "58", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Cuenta de Venta y Líquido producto M", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)42, "59", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LG-M", "Liquidación M", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)43, "62", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LIPC", "Cuenta de Venta y Líquido producto C", false, "C", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)44, "65", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LG-C", "Liquidación C", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)45, "80", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Comprobante diario de cierre (zeta)", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)46, "81", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "TF-A", "Tique-Factura A", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)47, "82", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "TF-B", "Tique-Factura B", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)48, "83", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "T", "Tique", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)49, "84", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Comprobante/Factura de servicios públicos", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null }
                });

            migrationBuilder.InsertData(
                table: "com_ComprobanteTipos",
                columns: new[] { "Idm", "CodigoArca", "CodigoExterno", "Created", "CreatedBy", "DescAbreviada", "Descripcion", "IsDeleted", "Letra", "Modified", "ModifiedBy", "NombreComprobante", "Session", "TipoOperacion", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)50, "85", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Nota de Crédito - servicios públicos", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)51, "86", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Nota de Débito - servicios públicos", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)52, "87", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Otros comp - servicios del exterior", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)53, "89", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Otros comp - doc exceptuados - Nota de Débito", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)54, "90", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Otros comp - doc exceptuados - Nota de Crédito", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)55, "92", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Aj contables que incrementan el débito fiscal", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)56, "93", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Aj contables que disminuyen el débito fiscal", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)57, "94", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Aj contables que incrementan el crédito fiscal", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)58, "95", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Aj contables que disminuyen el crédito fiscal", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)59, "13", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NC-C", "Notas de Crédito C", false, "C", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)60, "12", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ND-C", "Notas de Débito C", false, "C", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)61, "98", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NCCA", "Notas de Crédito sobre CDD A", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)0, null, null },
                    { (short)62, "99", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NCCB", "Notas de Crédito sobre CDD B", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)0, null, null },
                    { (short)63, "199", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RECI", "Recibo", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)0, null, null },
                    { (short)64, "00", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "ACCOR", false, null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)0, null, null },
                    { (short)65, "91", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RMTO", "Remito", false, "R", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)0, null, null },
                    { (short)66, "201", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FCEA", "FCE Factura MiPyMEs A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)67, "202", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NDEA", "FCE Nota de Débito MiPyMEs A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)68, "203", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NCEA", "FCE Nota de Crédito MiPyMEs A", false, "A", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)69, "206", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FCEB", "FCE Factura MiPyMEs B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)70, "207", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NDEB", "FCE Nota de Débito MiPyMEs B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)71, "208", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NCEB", "FCE Nota de Crédito MiPyMEs B", false, "B", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)72, "211", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FCEC", "FCE Factura MiPyMEs C", false, "C", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)73, "212", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NDEC", "FCE Nota de Dédito MiPyMEs C", false, "C", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)74, "213", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NCEC", "FCE Nota de Crédito MiPyMEs C", false, "C", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)75, "501", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DCF+", "Dif Cambio F (+)", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)76, "502", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DCF-", "Dif Cambio F (-)", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)77, "503", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DCR+", "Dif Cambio R (+)", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)1, null, null },
                    { (short)78, "504", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DCR-", "Dif Cambio R (-)", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)-1, null, null },
                    { (short)79, "601", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "EDCC", "Estado de Cuenta", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)0, null, null },
                    { (short)80, "200", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "OP", "Orden de Pago", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)0, null, null },
                    { (short)81, "901", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CERT", "CERTIFICADO", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)0, null, null },
                    { (short)82, "902", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "COTE", "Cotización Etiquetas", false, "X", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, (short)0, null, null }
                });

            migrationBuilder.InsertData(
                table: "com_CondicionVentas",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Contado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta Corriente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tarjeta de Crédito", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tarjeta de Débito", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)5, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Transferencia Bancaria", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)6, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cheque", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)7, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Otro", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "com_EstadosValidacionARCA",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Validada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rechazada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "com_EstadosValidacionARCA",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Error validación", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "No validada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "com_ImpuestoTipos",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "General", "IsDeleted", "Modified", "ModifiedBy", "Session", "Valor", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Impuesto al valor agregado", true, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "IVA", null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Impuestos internos", false, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "INT", null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Impuestos provinciales", false, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "PROV", null, null }
                });

            migrationBuilder.InsertData(
                table: "com_Origenes",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SOCIOS", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BACKOFFICE", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CORREO", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "com_UnidadMedidas",
                columns: new[] { "Idm", "CodigoAFIP", "CodigoARBA", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, "mm", "mm", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Milímetro", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, "cm", "cm", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Centímetro", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, "m", "m", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Metro", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, "mm2", "mm2", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Milímetro cuadrado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)5, "cm2", "cm2", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Centímetro cuadrado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)6, "m2", "m2", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Metro cuadrado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)7, "g", "g", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gramo", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)8, "Kg", "Kg", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Kilogramo", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)9, "%", "%", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Porcentaje", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)10, "seg", "seg", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Segundo", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)11, "min", "min", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Minuto", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)12, "hora", "hora", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Hora", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)13, "Un.", "Un.", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Unidad", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)14, "Millar", "Millar", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Millar", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)15, "Litros", "Litros", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Litro", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)18, "cm3", "cm3", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Centímetro cúbico", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)19, "m3", "m3", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Metro cúbico", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)20, "par", "par", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Par", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)21, "una", "una", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Una", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)22, "tonelada", "tonelada", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tonelada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "doc_DocumentoEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Pendiente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Validado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rechazado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Vencido", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)5, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Presentado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "emp_RolTipos",
                columns: new[] { "Idm", "Codigo", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, "PROV", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Proveedor", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, "CONT", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Contratista", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, "PCLI", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cliente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "emp_TipoCuentas",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Nombre", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta básica para depósitos y retiros diarios.", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta Corriente", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta para ahorrar con disponibilidad inmediata.", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Caja de Ahorro", null, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta para recibir el salario de los empleados.", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta Sueldo", null, null, null },
                    { (short)4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta para invertir en diferentes instrumentos financieros.", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta de Inversión", null, null, null },
                    { (short)5, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta para fines específicos, como el pago de impuestos.", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuenta Especial", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "gbl_Cultures",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "IsDeleted", "Language", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[] { 1L, "es-AR", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, "es", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Español", null, null, null });

            migrationBuilder.InsertData(
                table: "gbl_Cultures",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "IsDeleted", "Language", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 2L, "en-US", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, "en", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Inglés", null, null, null },
                    { 3L, "pt-BR", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, "pt", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Portugués", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "gbl_Currencies",
                columns: new[] { "Idm", "Code", "Created", "CreatedBy", "Description", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "Symbol", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, "ARS", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Peso Argentino", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Peso Argentino", null, "$", null, null },
                    { 2L, "DOL", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Dolar Americano", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Dolar Americano", null, "US$", null, null },
                    { 3L, "EUR", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Euro", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Euro", null, "€", null, null },
                    { 4L, "BRL", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Real Brasilero", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Real Brasilero", null, "R$", null, null },
                    { 5L, "UYU", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Peso Uruguayo", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Peso Uruguayo", null, "$", null, null }
                });

            migrationBuilder.InsertData(
                table: "gbl_IdentificationTypes",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, "DNI", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Documento Argentino", null, null, null },
                    { 2L, "CI", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cédula Uruguaya", null, null, null },
                    { 3L, "PSP", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Pasaporte", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "geo_Countries",
                columns: new[] { "Idm", "Created", "CreatedBy", "ISOCode2", "ISOCode3", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AR", "ARG", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Argentina", null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "UY", "URY", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Uruguay", null, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BR", "BRA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Brasil", null, null, null },
                    { 4L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AF", "AFG", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Afganistán", null, null, null },
                    { 5L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AL", "ALB", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Albania", null, null, null },
                    { 6L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DE", "DEU", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alemania", null, null, null },
                    { 7L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DZ", "DZA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Algeria", null, null, null },
                    { 8L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AD", "AND", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Andorra", null, null, null },
                    { 9L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AO", "AGO", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Angola", null, null, null },
                    { 10L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AI", "AIA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Anguila", null, null, null },
                    { 11L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AQ", "ATA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Antártida", null, null, null },
                    { 12L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AG", "ATG", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Antigua y Barbuda", null, null, null },
                    { 13L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AN", "ANT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Antillas Neerlandesas", null, null, null },
                    { 14L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SA", "SAU", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Arabia Saudita", null, null, null },
                    { 15L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AM", "ARM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Armenia", null, null, null },
                    { 16L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AW", "ABW", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Aruba", null, null, null },
                    { 17L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AU", "AUS", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Australia", null, null, null },
                    { 18L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AT", "AUT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Austria", null, null, null },
                    { 19L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AZ", "AZE", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Azerbayán", null, null, null },
                    { 20L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BE", "BEL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Bélgica", null, null, null },
                    { 21L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BS", "BHS", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Bahamas", null, null, null },
                    { 22L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BH", "BHR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Bahrein", null, null, null },
                    { 23L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BD", "BGD", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Bangladesh", null, null, null },
                    { 24L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BB", "BRB", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Barbados", null, null, null },
                    { 25L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BZ", "BLZ", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Belice", null, null, null },
                    { 26L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BJ", "BEN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Benín", null, null, null },
                    { 27L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BT", "BTN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Bhután", null, null, null },
                    { 28L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BY", "BLR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Bielorrusia", null, null, null },
                    { 29L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MM", "MMR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Birmania", null, null, null },
                    { 30L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BO", "BOL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Bolivia", null, null, null },
                    { 31L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BA", "BIH", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Bosnia y Herzegovina", null, null, null },
                    { 32L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BW", "BWA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Botsuana", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "geo_Countries",
                columns: new[] { "Idm", "Created", "CreatedBy", "ISOCode2", "ISOCode3", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 33L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BN", "BRN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Brunei", null, null, null },
                    { 34L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BG", "BGR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Bulgaria", null, null, null },
                    { 35L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BF", "BFA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Burkina Faso", null, null, null },
                    { 36L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BI", "BDI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Burundi", null, null, null },
                    { 37L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CV", "CPV", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cabo Verde", null, null, null },
                    { 38L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KH", "KHM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Camboya", null, null, null },
                    { 39L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CM", "CMR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Camerún", null, null, null },
                    { 40L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CA", "CAN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Canadá", null, null, null },
                    { 41L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "TD", "TCD", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Chad", null, null, null },
                    { 42L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CL", "CHL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Chile", null, null, null },
                    { 43L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CN", "CHN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "China", null, null, null },
                    { 44L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CY", "CYP", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Chipre", null, null, null },
                    { 45L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "VA", "VAT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Ciudad del Vaticano", null, null, null },
                    { 46L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CO", "COL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Colombia", null, null, null },
                    { 47L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KM", "COM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Comoras", null, null, null },
                    { 48L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CG", "COG", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Congo", null, null, null },
                    { 49L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CD", "COD", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Congo", null, null, null },
                    { 50L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KP", "PRK", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Corea del Norte", null, null, null },
                    { 51L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KR", "KOR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Corea del Sur", null, null, null },
                    { 52L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CI", "CIV", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Costa de Marfil", null, null, null },
                    { 53L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CR", "CRI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Costa Rica", null, null, null },
                    { 54L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "HR", "HRV", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Croacia", null, null, null },
                    { 55L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CU", "CUB", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuba", null, null, null },
                    { 56L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DK", "DNK", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Dinamarca", null, null, null },
                    { 57L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DM", "DMA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Dominica", null, null, null },
                    { 58L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "EC", "ECU", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Ecuador", null, null, null },
                    { 59L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "EG", "EGY", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Egipto", null, null, null },
                    { 60L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SV", "SLV", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "El Salvador", null, null, null },
                    { 61L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AE", "ARE", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Emiratos Árabes Unidos", null, null, null },
                    { 62L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ER", "ERI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Eritrea", null, null, null },
                    { 63L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SK", "SVK", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Eslovaquia", null, null, null },
                    { 64L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SI", "SVN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Eslovenia", null, null, null },
                    { 65L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ES", "ESP", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "España", null, null, null },
                    { 66L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "US", "USA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Estados Unidos de América", null, null, null },
                    { 67L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "EE", "EST", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Estonia", null, null, null },
                    { 68L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ET", "ETH", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Etiopía", null, null, null },
                    { 69L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PH", "PHL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Filipinas", null, null, null },
                    { 70L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FI", "FIN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Finlandia", null, null, null },
                    { 71L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FJ", "FJI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Fiyi", null, null, null },
                    { 72L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FR", "FRA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Francia", null, null, null },
                    { 73L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GA", "GAB", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gabón", null, null, null },
                    { 74L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GM", "GMB", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gambia", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "geo_Countries",
                columns: new[] { "Idm", "Created", "CreatedBy", "ISOCode2", "ISOCode3", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 75L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GE", "GEO", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Georgia", null, null, null },
                    { 76L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GH", "GHA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Ghana", null, null, null },
                    { 77L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GI", "GIB", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gibraltar", null, null, null },
                    { 78L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GD", "GRD", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Granada", null, null, null },
                    { 79L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GR", "GRC", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Grecia", null, null, null },
                    { 80L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GL", "GRL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Groenlandia", null, null, null },
                    { 81L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GP", "GLP", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Guadalupe", null, null, null },
                    { 82L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GU", "GUM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Guam", null, null, null },
                    { 83L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GT", "GTM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Guatemala", null, null, null },
                    { 84L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GF", "GUF", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Guayana Francesa", null, null, null },
                    { 85L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GG", "GGY", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Guernsey", null, null, null },
                    { 86L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GN", "GIN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Guinea", null, null, null },
                    { 87L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GQ", "GNQ", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Guinea Ecuatorial", null, null, null },
                    { 88L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GW", "GNB", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Guinea-Bissau", null, null, null },
                    { 89L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GY", "GUY", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Guyana", null, null, null },
                    { 90L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "HT", "HTI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Haití", null, null, null },
                    { 91L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "HN", "HND", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Honduras", null, null, null },
                    { 92L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "HK", "HKG", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Hong Kong", null, null, null },
                    { 93L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "HU", "HUN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Hungría", null, null, null },
                    { 94L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "IN", "IND", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "India", null, null, null },
                    { 95L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ID", "IDN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Indonesia", null, null, null },
                    { 96L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "IR", "IRN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Irán", null, null, null },
                    { 97L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "IQ", "IRQ", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Irak", null, null, null },
                    { 98L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "IE", "IRL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Irlanda", null, null, null },
                    { 99L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BV", "BVT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Isla Bouvet", null, null, null },
                    { 100L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "IM", "IMN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Isla de Man", null, null, null },
                    { 101L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CX", "CXR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Isla de Navidad", null, null, null },
                    { 102L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NF", "NFK", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Isla Norfolk", null, null, null },
                    { 103L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "IS", "ISL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islandia", null, null, null },
                    { 104L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BM", "BMU", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Bermudas", null, null, null },
                    { 105L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KY", "CYM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Caimán", null, null, null },
                    { 106L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CC", "CCK", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Cocos (Keeling)", null, null, null },
                    { 107L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CK", "COK", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Cook", null, null, null },
                    { 108L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AX", "ALA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas de Åland", null, null, null },
                    { 109L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FO", "FRO", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Feroe", null, null, null },
                    { 110L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GS", "SGS", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Georgias del Sur y Sandwich del Sur", null, null, null },
                    { 111L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "HM", "HMD", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Heard y McDonald", null, null, null },
                    { 112L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MV", "MDV", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Maldivas", null, null, null },
                    { 113L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FK", "FLK", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Malvinas", null, null, null },
                    { 114L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MP", "MNP", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Marianas del Norte", null, null, null },
                    { 115L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MH", "MHL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Marshall", null, null, null },
                    { 116L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PN", "PCN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Pitcairn", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "geo_Countries",
                columns: new[] { "Idm", "Created", "CreatedBy", "ISOCode2", "ISOCode3", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 117L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SB", "SLB", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Salomón", null, null, null },
                    { 118L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "TC", "TCA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Turcas y Caicos", null, null, null },
                    { 119L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "UM", "UMI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Ultramarinas Menores de Estados Unidos", null, null, null },
                    { 120L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "VG", "VGB", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Vírgenes Británicas", null, null, null },
                    { 121L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "VI", "VIR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Islas Vírgenes de los Estados Unidos", null, null, null },
                    { 122L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "IL", "ISR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Israel", null, null, null },
                    { 123L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "IT", "ITA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Italia", null, null, null },
                    { 124L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "JM", "JAM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Jamaica", null, null, null },
                    { 125L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "JP", "JPN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Japón", null, null, null },
                    { 126L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "JE", "JEY", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Jersey", null, null, null },
                    { 127L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "JO", "JOR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Jordania", null, null, null },
                    { 128L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KZ", "KAZ", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Kazajistán", null, null, null },
                    { 129L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KE", "KEN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Kenia", null, null, null },
                    { 130L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KG", "KGZ", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Kirguistán", null, null, null },
                    { 131L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KI", "KIR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Kiribati", null, null, null },
                    { 132L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KW", "KWT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Kuwait", null, null, null },
                    { 133L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LA", "LAO", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Laos", null, null, null },
                    { 134L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LS", "LSO", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Lesoto", null, null, null },
                    { 135L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LV", "LVA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Letonia", null, null, null },
                    { 136L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LB", "LBN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Líbano", null, null, null },
                    { 137L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LR", "LBR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Liberia", null, null, null },
                    { 138L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LY", "LBY", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Libia", null, null, null },
                    { 139L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LI", "LIE", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Liechtenstein", null, null, null },
                    { 140L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LT", "LTU", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Lituania", null, null, null },
                    { 141L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "LU", "LUX", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Luxemburgo", null, null, null },
                    { 142L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MO", "MAC", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Macao", null, null, null },
                    { 143L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MK", "MKD", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Macedonia del Norte", null, null, null },
                    { 144L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MG", "MDG", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Madagascar", null, null, null },
                    { 145L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MY", "MYS", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Malasia", null, null, null },
                    { 146L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MW", "MWI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Malaui", null, null, null },
                    { 147L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ML", "MLI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Mali", null, null, null },
                    { 148L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MT", "MLT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Malta", null, null, null },
                    { 149L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MA", "MAR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Marruecos", null, null, null },
                    { 150L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MQ", "MTQ", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Martinica", null, null, null },
                    { 151L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MU", "MUS", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Mauricio", null, null, null },
                    { 152L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MR", "MRT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Mauritania", null, null, null },
                    { 153L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "YT", "MYT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Mayotte", null, null, null },
                    { 154L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MX", "MEX", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "México", null, null, null },
                    { 155L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "FM", "FSM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Micronesia", null, null, null },
                    { 156L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MD", "MDA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Moldavia", null, null, null },
                    { 157L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MC", "MCO", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Mónaco", null, null, null },
                    { 158L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MN", "MNG", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Mongolia", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "geo_Countries",
                columns: new[] { "Idm", "Created", "CreatedBy", "ISOCode2", "ISOCode3", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 159L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ME", "MNE", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Montenegro", null, null, null },
                    { 160L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MS", "MSR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Montserrat", null, null, null },
                    { 161L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "MZ", "MOZ", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Mozambique", null, null, null },
                    { 162L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NA", "NAM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Namibia", null, null, null },
                    { 163L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NR", "NRU", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Nauru", null, null, null },
                    { 164L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NP", "NPL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Nepal", null, null, null },
                    { 165L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NI", "NIC", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Nicaragua", null, null, null },
                    { 166L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NE", "NER", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Níger", null, null, null },
                    { 167L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NG", "NGA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Nigeria", null, null, null },
                    { 168L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NU", "NIU", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Niue", null, null, null },
                    { 169L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NO", "NOR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Noruega", null, null, null },
                    { 170L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NC", "NCL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Nueva Caledonia", null, null, null },
                    { 171L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NZ", "NZL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Nueva Zelanda", null, null, null },
                    { 172L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "OM", "OMN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Omán", null, null, null },
                    { 173L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "NL", "NLD", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Países Bajos", null, null, null },
                    { 174L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PK", "PAK", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Pakistán", null, null, null },
                    { 175L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PW", "PLW", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Palaos", null, null, null },
                    { 176L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PA", "PAN", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Panamá", null, null, null },
                    { 177L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PG", "PNG", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Papúa Nueva Guinea", null, null, null },
                    { 178L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PY", "PRY", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Paraguay", null, null, null },
                    { 179L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PE", "PER", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Perú", null, null, null },
                    { 180L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PF", "PYF", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Polinesia Francesa", null, null, null },
                    { 181L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PL", "POL", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Polonia", null, null, null },
                    { 182L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PT", "PRT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Portugal", null, null, null },
                    { 183L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PR", "PRI", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Puerto Rico", null, null, null },
                    { 184L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "QA", "QAT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Qatar", null, null, null },
                    { 185L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "GB", "GBR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Reino Unido", null, null, null },
                    { 186L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CF", "CAF", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "República Centroafricana", null, null, null },
                    { 187L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CZ", "CZE", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "República Checa", null, null, null },
                    { 188L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CD", "COD", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "República Democrática del Congo", null, null, null },
                    { 189L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DO", "DOM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "República Dominicana", null, null, null },
                    { 190L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RE", "REU", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Reunión", null, null, null },
                    { 191L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RW", "RWA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Ruanda", null, null, null },
                    { 192L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RO", "ROU", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rumania", null, null, null },
                    { 193L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RU", "RUS", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rusia", null, null, null },
                    { 194L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "EH", "ESH", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Sahara Occidental", null, null, null },
                    { 195L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "WS", "WSM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Samoa", null, null, null },
                    { 196L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AS", "ASM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Samoa Americana", null, null, null },
                    { 197L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BL", "BLM", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "San Bartolomé", null, null, null },
                    { 198L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "KN", "KNA", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "San Cristóbal y Nieves", null, null, null },
                    { 199L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SM", "SMR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "San Marino", null, null, null },
                    { 200L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "San Martín (Francia)", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "geo_Countries",
                columns: new[] { "Idm", "Created", "CreatedBy", "ISOCode2", "ISOCode3", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 201L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "San Pedro y Miquelón", null, null, null },
                    { 202L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "San Vicente y las Granadinas", null, null, null },
                    { 203L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Santa Elena", null, null, null },
                    { 204L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Santa Lucía", null, null, null },
                    { 205L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Santo Tomé y Príncipe", null, null, null },
                    { 206L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Senegal", null, null, null },
                    { 207L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Serbia", null, null, null },
                    { 208L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Seychelles", null, null, null },
                    { 209L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Sierra Leona", null, null, null },
                    { 210L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Singapur", null, null, null },
                    { 211L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Siria", null, null, null },
                    { 212L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Somalia", null, null, null },
                    { 213L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Sri Lanka", null, null, null },
                    { 214L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Sudáfrica", null, null, null },
                    { 215L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Sudán", null, null, null },
                    { 216L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Suecia", null, null, null },
                    { 217L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Suiza", null, null, null },
                    { 218L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Surinám", null, null, null },
                    { 219L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Svalbard y Jan Mayen", null, null, null },
                    { 220L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Swazilandia", null, null, null },
                    { 221L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tadjikistán", null, null, null },
                    { 222L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tailandia", null, null, null },
                    { 223L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Taiwán", null, null, null },
                    { 224L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tanzania", null, null, null },
                    { 225L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Territorio Británico del Océano Índico", null, null, null },
                    { 226L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Territorios Australes y Antárticas Franceses", null, null, null },
                    { 227L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Timor Oriental", null, null, null },
                    { 228L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Togo", null, null, null },
                    { 229L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tokelau", null, null, null },
                    { 230L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tonga", null, null, null },
                    { 231L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Trinidad y Tobago", null, null, null },
                    { 232L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Túnez", null, null, null },
                    { 233L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Turkmenistán", null, null, null },
                    { 234L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Turquía", null, null, null },
                    { 235L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tuvalu", null, null, null },
                    { 236L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Ucrania", null, null, null },
                    { 237L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Uganda", null, null, null },
                    { 238L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Uruguay", null, null, null },
                    { 239L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Uzbekistán", null, null, null },
                    { 240L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Vanuatu", null, null, null },
                    { 241L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Venezuela", null, null, null },
                    { 242L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Vietnam", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "geo_Countries",
                columns: new[] { "Idm", "Created", "CreatedBy", "ISOCode2", "ISOCode3", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 243L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Wallis y Futuna", null, null, null },
                    { 244L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Yemen", null, null, null },
                    { 245L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Yibuti", null, null, null },
                    { 246L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Zambia", null, null, null },
                    { 247L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "", "", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Zimbabue", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "mlr_ModosLecturas",
                columns: new[] { "Idm", "Codigo", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, "QRC", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "QR - Cabecera", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, "OCRD", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "OCR - Detalle", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, "OCRI", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "OCR - Impuestos", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, "OCRC", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "OCR - Cabecera", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)5, "MAN", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Manual (no se usa IA)", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ntf_EventoEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PROCESSED", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DROPPED", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DELIVERED", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "DEFERRED", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)5, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "BOUNCED", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)6, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "OPENED", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)7, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CLICKED", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)8, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "UNSUBSCRIBED", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)9, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "REPORT", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ntf_NotificacionEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Nuevo", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Error general", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Ambiente configurado para no realizar envíos", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 4L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Envío ok", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 5L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Error de SMTP", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 6L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Error de Autenticacion", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 7L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Máximo de reintentos alcanzado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ntf_NotificacionEtiquetaTipos",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Asunto", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cuerpo", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ntf_TipoNotificaciones",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Recuperación de Contraseña", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Activación de Usuario", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Reporte de Errores", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 4L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Vinculación de usuario con empresa portal", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "orc_OrdenesComprasEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Nombre", "Session", "Valor", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Generada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Generada", null, 1, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Aprobada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Aprobada", null, 2, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Anulada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Anulada", null, 3, null, null }
                });

            migrationBuilder.InsertData(
                table: "per_PercepcionTipos",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "General", "IsDeleted", "Modified", "ModifiedBy", "Session", "Valor", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Percepción de IVA", true, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "IVA", null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Percepción de Ingresos Brutoss", false, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "IIBB", null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Percepción Municipal", false, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "MUNICIPAL", null, null }
                });

            migrationBuilder.InsertData(
                table: "prd_EstadoPeriodos",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cerrado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Abierto", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Presentado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "No Vigente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "pro_EstadoProcesos",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "PENDIENTE", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "COMPLETADO", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ERROR", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "rep_TaskInvervalsTypes",
                columns: new[] { "Idm", "Created", "CreatedBy", "Description", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Diario", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Diario", null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Semanal", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Semanal", null, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Mensual", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Mensual", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_UserTypes",
                columns: new[] { "Idm", "Code", "Created", "CreatedBy", "Description", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1, "Standar", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tipo de usuario default.", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { 1001, "Socio", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Usuario de socio", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "cny_Company",
                columns: new[] { "Id", "BusinessName", "Created", "CreatedBy", "IsDeleted", "Modified", "ModifiedBy", "Name", "Number", "OrganizationId", "Session", "TaxId", "__MigCode", "__MigId" },
                values: new object[] { 39L, "Globalsis S.R.L.", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Globalsis S.R.L.", 1, 39L, null, "30707466967", null, null });

            migrationBuilder.InsertData(
                table: "gbl_CulturesOrganization",
                columns: new[] { "Id", "Created", "CreatedBy", "CultureDefault", "CultureId", "IsDeleted", "Modified", "ModifiedBy", "OrganizationId", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", true, 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, 2L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, 3L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "gbl_IdentificationTax",
                columns: new[] { "Id", "CountryId", "Created", "CreatedBy", "Description", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Clave Única de Identificación Tributaria", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "CUIT", null, null, null },
                    { 2L, 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Registro Único Tributario", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "RUT", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "geo_Provinces",
                columns: new[] { "Idm", "CountryIdm", "Created", "CreatedBy", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Buenos Aires", null, null, null },
                    { 2L, 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Río de Janeiro", null, null, null },
                    { 3L, 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Montevideo", null, null, null },
                    { 4L, 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Colonia", null, null, null },
                    { 5L, 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Santa fe", null, null, null },
                    { 6L, 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "San pablo", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "sys_DomainFs",
                columns: new[] { "Idm", "Created", "CreatedBy", "Description", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "UserTypeIdm", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Sistema Back Office", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Sistema Back Office", null, 1, null, null },
                    { 1001L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Usuario de socio", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Socio", null, 1001, null, null }
                });

            migrationBuilder.InsertData(
                table: "cny_CompanyCurrencies",
                columns: new[] { "Id", "CompanyId", "Created", "CreatedBy", "CurrencyId", "IsDeleted", "IsSalesDefault", "Modified", "ModifiedBy", "OrganizationId", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, 39L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, false, true, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null },
                    { 2L, 39L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 2L, false, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null },
                    { 3L, 39L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 3L, false, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null },
                    { 4L, 39L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 4L, false, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null },
                    { 5L, 39L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 5L, false, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "geo_Cities",
                columns: new[] { "Idm", "Created", "CreatedBy", "IsDeleted", "Modified", "ModifiedBy", "Name", "ProvinceId", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "La Plata", 1L, null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Río de Janeiro", 2L, null, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Montevideo", 3L, null, null, null },
                    { 4L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Colonia del Sacramento", 4L, null, null, null },
                    { 5L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Montevideo", 3L, null, null, null },
                    { 6L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rosario", 5L, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ntf_ConfiguracionNotificaciones",
                columns: new[] { "Id", "Activo", "Body", "Created", "CreatedBy", "DefaultGetNotificationsProcedure", "Descripcion", "DomainFIdm", "FrecuenciaHora", "FrecuenciaMinutos", "FrecuenciaSegundos", "HoraFin", "HoraInicio", "IsDeleted", "Modified", "ModifiedBy", "Prioridad", "Session", "Subject", "TipoNotificacionIdm", "UltimoEnvio", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, true, "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n<head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <title>Nueva plantilla de correo electrónico 2020-11-03</title> <!--[if (mso 16)]><style type=\"text/css\">    a {text-decoration: none;}    </style><![endif]--> <!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]-->\r\n    <!--[if gte mso 9]><xml> <o:OfficeDocumentSettings> <o:AllowPNG>\r\n    </o:AllowPNG> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml><![endif]-->\r\n    <style type=\"text/css\">\r\n        #outlook a {\r\n            padding: 0;\r\n        }\r\n\r\n        .ExternalClass {\r\n            width: 100%;\r\n        }\r\n\r\n            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {\r\n                line-height: 100%;\r\n            }\r\n\r\n        .es-button {\r\n            mso-style-priority: 100 !important;\r\n            text-decoration: none !important;\r\n        }\r\n\r\n        a[x-apple-data-detectors] {\r\n            color: inherit !important;\r\n            text-decoration: none !important;\r\n            font-size: inherit !important;\r\n            font-family: inherit !important;\r\n            font-weight: inherit !important;\r\n            line-height: inherit !important;\r\n        }\r\n\r\n        .es-desk-hidden {\r\n            display: none;\r\n            float: left;\r\n            overflow: hidden;\r\n            width: 0;\r\n            max-height: 0;\r\n            line-height: 0;\r\n            mso-hide: all;\r\n        }\r\n\r\n        @media only screen and (max-width:600px) {\r\n            p, ul li, ol li, a {\r\n                font-size: 14px !important;\r\n                line-height: 150% !important\r\n            }\r\n\r\n            h1 {\r\n                font-size: 28px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h2 {\r\n                font-size: 26px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h3 {\r\n                font-size: 20px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h1 a {\r\n                font-size: 28px !important\r\n            }\r\n\r\n            h2 a {\r\n                font-size: 26px !important\r\n            }\r\n\r\n            h3 a {\r\n                font-size: 20px !important\r\n            }\r\n\r\n            .es-menu td a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {\r\n                font-size: 14px !important\r\n            }\r\n\r\n            .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {\r\n                font-size: 11px !important\r\n            }\r\n\r\n            *[class=\"gmail-fix\"] {\r\n                display: none !important\r\n            }\r\n\r\n            .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {\r\n                text-align: right !important\r\n            }\r\n\r\n            .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {\r\n                text-align: left !important\r\n            }\r\n\r\n                .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {\r\n                    display: inline !important\r\n                }\r\n\r\n            .es-button-border {\r\n                display: block !important\r\n            }\r\n\r\n            a.es-button {\r\n                font-size: 14px !important;\r\n                display: block !important;\r\n                border-left-width: 0px !important;\r\n                border-right-width: 0px !important\r\n            }\r\n\r\n            .es-btn-fw {\r\n                border-width: 10px 0px !important;\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right {\r\n                width: 100% !important\r\n            }\r\n\r\n            .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {\r\n                width: 100% !important;\r\n                max-width: 600px !important\r\n            }\r\n\r\n            .es-adapt-td {\r\n                display: block !important;\r\n                width: 100% !important\r\n            }\r\n\r\n            .adapt-img {\r\n                width: 100% !important;\r\n                height: auto !important\r\n            }\r\n\r\n            .es-m-p0 {\r\n                padding: 0px !important\r\n            }\r\n\r\n            .es-m-p0r {\r\n                padding-right: 0px !important\r\n            }\r\n\r\n            .es-m-p0l {\r\n                padding-left: 0px !important\r\n            }\r\n\r\n            .es-m-p0t {\r\n                padding-top: 0px !important\r\n            }\r\n\r\n            .es-m-p0b {\r\n                padding-bottom: 0 !important\r\n            }\r\n\r\n            .es-m-p20b {\r\n                padding-bottom: 20px !important\r\n            }\r\n\r\n            .es-mobile-hidden, .es-hidden {\r\n                display: none !important\r\n            }\r\n\r\n            tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {\r\n                width: auto !important;\r\n                overflow: visible !important;\r\n                float: none !important;\r\n                max-height: inherit !important;\r\n                line-height: inherit !important\r\n            }\r\n\r\n            tr.es-desk-hidden {\r\n                display: table-row !important\r\n            }\r\n\r\n            table.es-desk-hidden {\r\n                display: table !important\r\n            }\r\n\r\n            td.es-desk-menu-hidden {\r\n                display: table-cell !important\r\n            }\r\n\r\n            .es-menu td {\r\n                width: 1% !important\r\n            }\r\n\r\n            table.es-table-not-adapt, .esd-block-html table {\r\n                width: auto !important\r\n            }\r\n\r\n            table.es-social {\r\n                display: inline-block !important\r\n            }\r\n\r\n                table.es-social td {\r\n                    display: inline-block !important\r\n                }\r\n        }\r\n    </style>\r\n</head>\r\n<body style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n    <div class=\"es-wrapper-color\" style=\"background-color:#F6F6F6\">\r\n        <!--[if gte mso 9]><v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\"> <v:fill type=\"tile\" color=\"#f6f6f6\"></v:fill> </v:background><![endif]-->\r\n        <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top\">\r\n            <tr style=\"border-collapse:collapse\">\r\n                <td valign=\"top\" style=\"padding:0;Margin:0\">\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"Margin:0;padding-bottom:10px;padding-top:20px;padding-left:20px;padding-right:20px;background-position:center top\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-position:left top\" role=\"presentation\">\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0;padding-bottom:10px\">\r\n                                                                    <br />\r\n                                                                    <h2>{application}</h2>\r\n                                                                    <h2 style=\"margin-top:120px;line-height:29px;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:24px;font-style:normal;font-weight:bold;color:#040404\">\r\n                                                                        <img class=\"adapt-img\" src=\"{root}/img/GsfLogoFondoNavbar.jpg\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" /><br />{seHaSolicitadoUnCambioDePassword}\r\n                                                                    </h2>\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                            <tr style=\"border-collapse:collapse\"> <td align=\"center\" style=\"padding:0;Margin:0;font-size:0px\"><a target=\"_blank\" href=\"#\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:14px;text-decoration:underline;color:#040404\"><img src=\"https://upload.wikimedia.org/wikipedia/commons/9/97/Avast_Passwords_logo.png\" alt=alt alt=alt style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" width=\"200\" /></a></td> </tr>\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" style=\"Margin:0;padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:15px\">\r\n                                                                    <span class=\"es-button-border\" style=\"border-style:solid;border-color:#38C2F1;background:#37EC86;border-width:0px;display:inline-block;border-radius:25px;width:auto\"><a href=\"{root}security/passwordrecovery?token={token}\" class=\"es-button\" target=\"_blank\" style=\"mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:18px;color:#FFFFFF;border-style:solid;border-color:#37EC86;border-width:10px 30px;display:inline-block;background:#37EC86;border-radius:25px;font-weight:bold;font-style:normal;line-height:22px;width:auto;text-align:center\">{recuperarMiContraseña}</a></span>\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"> <tr style=\"border-collapse:collapse\"> <td style=\"padding:0;Margin:0\"> <div> Si este mail no fué solicitado, escribir a <a href=\"mailto:soporte@globalsis.com.ar\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:14px;text-decoration:underline;color:#040404\">Soporte</a></div><br /></td> </tr> </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table> <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"transparent\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"Margin:0;padding-left:20px;padding-right:20px;padding-top:30px;padding-bottom:30px;background-position:left top\">\r\n                                            <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td valign=\"top\" align=\"center\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" style=\"padding:0;Margin:0;font-size:0px\">\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n</body>\r\n\r\n</html>", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Recuperación de Contraseña", 1L, 0, 0, 45, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1, null, "Recuperación de Contraseña", 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 2L, true, "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n<head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <title>Nueva plantilla de correo electrónico 2020-11-03</title> <!--[if (mso 16)]><style type=\"text/css\">    a {text-decoration: none;}    </style><![endif]--> <!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]-->\r\n    <!--[if gte mso 9]><xml> <o:OfficeDocumentSettings> <o:AllowPNG>\r\n    </o:AllowPNG> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml><![endif]-->\r\n    <style type=\"text/css\">\r\n        #outlook a {\r\n            padding: 0;\r\n        }\r\n\r\n        .ExternalClass {\r\n            width: 100%;\r\n        }\r\n\r\n            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {\r\n                line-height: 100%;\r\n            }\r\n\r\n        .es-button {\r\n            mso-style-priority: 100 !important;\r\n            text-decoration: none !important;\r\n        }\r\n\r\n        a[x-apple-data-detectors] {\r\n            color: inherit !important;\r\n            text-decoration: none !important;\r\n            font-size: inherit !important;\r\n            font-family: inherit !important;\r\n            font-weight: inherit !important;\r\n            line-height: inherit !important;\r\n        }\r\n\r\n        .es-desk-hidden {\r\n            display: none;\r\n            float: left;\r\n            overflow: hidden;\r\n            width: 0;\r\n            max-height: 0;\r\n            line-height: 0;\r\n            mso-hide: all;\r\n        }\r\n\r\n        @media only screen and (max-width:600px) {\r\n            p, ul li, ol li, a {\r\n                font-size: 14px !important;\r\n                line-height: 150% !important\r\n            }\r\n\r\n            h1 {\r\n                font-size: 28px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h2 {\r\n                font-size: 26px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h3 {\r\n                font-size: 20px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h1 a {\r\n                font-size: 28px !important\r\n            }\r\n\r\n            h2 a {\r\n                font-size: 26px !important\r\n            }\r\n\r\n            h3 a {\r\n                font-size: 20px !important\r\n            }\r\n\r\n            .es-menu td a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {\r\n                font-size: 14px !important\r\n            }\r\n\r\n            .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {\r\n                font-size: 11px !important\r\n            }\r\n\r\n            *[class=\"gmail-fix\"] {\r\n                display: none !important\r\n            }\r\n\r\n            .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {\r\n                text-align: right !important\r\n            }\r\n\r\n            .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {\r\n                text-align: left !important\r\n            }\r\n\r\n                .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {\r\n                    display: inline !important\r\n                }\r\n\r\n            .es-button-border {\r\n                display: block !important\r\n            }\r\n\r\n            a.es-button {\r\n                font-size: 14px !important;\r\n                display: block !important;\r\n                border-left-width: 0px !important;\r\n                border-right-width: 0px !important\r\n            }\r\n\r\n            .es-btn-fw {\r\n                border-width: 10px 0px !important;\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right {\r\n                width: 100% !important\r\n            }\r\n\r\n            .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {\r\n                width: 100% !important;\r\n                max-width: 600px !important\r\n            }\r\n\r\n            .es-adapt-td {\r\n                display: block !important;\r\n                width: 100% !important\r\n            }\r\n\r\n            .adapt-img {\r\n                width: 100% !important;\r\n                height: auto !important\r\n            }\r\n\r\n            .es-m-p0 {\r\n                padding: 0px !important\r\n            }\r\n\r\n            .es-m-p0r {\r\n                padding-right: 0px !important\r\n            }\r\n\r\n            .es-m-p0l {\r\n                padding-left: 0px !important\r\n            }\r\n\r\n            .es-m-p0t {\r\n                padding-top: 0px !important\r\n            }\r\n\r\n            .es-m-p0b {\r\n                padding-bottom: 0 !important\r\n            }\r\n\r\n            .es-m-p20b {\r\n                padding-bottom: 20px !important\r\n            }\r\n\r\n            .es-mobile-hidden, .es-hidden {\r\n                display: none !important\r\n            }\r\n\r\n            tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {\r\n                width: auto !important;\r\n                overflow: visible !important;\r\n                float: none !important;\r\n                max-height: inherit !important;\r\n                line-height: inherit !important\r\n            }\r\n\r\n            tr.es-desk-hidden {\r\n                display: table-row !important\r\n            }\r\n\r\n            table.es-desk-hidden {\r\n                display: table !important\r\n            }\r\n\r\n            td.es-desk-menu-hidden {\r\n                display: table-cell !important\r\n            }\r\n\r\n            .es-menu td {\r\n                width: 1% !important\r\n            }\r\n\r\n            table.es-table-not-adapt, .esd-block-html table {\r\n                width: auto !important\r\n            }\r\n\r\n            table.es-social {\r\n                display: inline-block !important\r\n            }\r\n\r\n                table.es-social td {\r\n                    display: inline-block !important\r\n                }\r\n        }\r\n    </style>\r\n</head>\r\n<body style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n    <div class=\"es-wrapper-color\" style=\"background-color:#F6F6F6\">\r\n        <!--[if gte mso 9]><v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\"> <v:fill type=\"tile\" color=\"#f6f6f6\"></v:fill> </v:background><![endif]-->\r\n        <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top\">\r\n            <tr style=\"border-collapse:collapse\">\r\n                <td valign=\"top\" style=\"padding:0;Margin:0\">\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"Margin:0;padding-bottom:10px;padding-top:20px;padding-left:20px;padding-right:20px;background-position:center top\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-position:left top\" role=\"presentation\">\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0;padding-bottom:10px\">\r\n                                                                    <br />\r\n                                                                    <h2>{application}</h2>\r\n                                                                    <h2 style=\"margin-top:120px;line-height:29px;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:24px;font-style:normal;font-weight:bold;color:#040404\">\r\n                                                                        <img class=\"adapt-img\" src=\"{root}/img/GsfLogoFondoNavbar.jpg\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" /><br />{seHaSolicitadoActivacionDeCuenta}\r\n                                                                    </h2>\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                            <tr style=\"border-collapse:collapse\"> <td align=\"center\" style=\"padding:0;Margin:0;font-size:0px\"><a target=\"_blank\" href=\"#\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:14px;text-decoration:underline;color:#040404\"><img src=\"https://upload.wikimedia.org/wikipedia/commons/9/97/Avast_Passwords_logo.png\" alt=alt alt=alt style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" width=\"200\" /></a></td> </tr>\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" style=\"Margin:0;padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:15px\">\r\n                                                                    <span class=\"es-button-border\" style=\"border-style:solid;border-color:#38C2F1;background:#37EC86;border-width:0px;display:inline-block;border-radius:25px;width:auto\"><a href=\"{root}security/useractivation?token={token}\" class=\"es-button\" target=\"_blank\" style=\"mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:18px;color:#FFFFFF;border-style:solid;border-color:#37EC86;border-width:10px 30px;display:inline-block;background:#37EC86;border-radius:25px;font-weight:bold;font-style:normal;line-height:22px;width:auto;text-align:center\">{activarMiCuenta}</a></span>\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"> <tr style=\"border-collapse:collapse\"> <td style=\"padding:0;Margin:0\"> <div> Si este mail no fué solicitado, escribir a <a href=\"mailto:soporte@globalsis.com.ar\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:14px;text-decoration:underline;color:#040404\">Soporte</a></div><br /></td> </tr> </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table> <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"transparent\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"Margin:0;padding-left:20px;padding-right:20px;padding-top:30px;padding-bottom:30px;background-position:left top\">\r\n                                            <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td valign=\"top\" align=\"center\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" style=\"padding:0;Margin:0;font-size:0px\">\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n</body>\r\n\r\n</html>\r\n", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Activación de Usuario", 1L, 0, 0, 45, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1, null, "Activación de Usuario", 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 3L, true, "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n<head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <title>Error Log</title>\r\n    <!--[if (mso 16)]><style type=\"text/css\">    a {text-decoration: none;}    </style><![endif]--> <!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]-->\r\n    <!--[if gte mso 9]><xml> <o:OfficeDocumentSettings> <o:AllowPNG>\r\n    </o:AllowPNG> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml><![endif]-->\r\n    <style type=\"text/css\">\r\n        #outlook a {\r\n            padding: 0;\r\n        }\r\n\r\n        .ExternalClass {\r\n            width: 100%;\r\n        }\r\n\r\n            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {\r\n                line-height: 100%;\r\n            }\r\n\r\n        .es-button {\r\n            mso-style-priority: 100 !important;\r\n            text-decoration: none !important;\r\n        }\r\n\r\n        a[x-apple-data-detectors] {\r\n            color: inherit !important;\r\n            text-decoration: none !important;\r\n            font-size: inherit !important;\r\n            font-family: inherit !important;\r\n            font-weight: inherit !important;\r\n            line-height: inherit !important;\r\n        }\r\n\r\n        .es-desk-hidden {\r\n            display: none;\r\n            float: left;\r\n            overflow: hidden;\r\n            width: 0;\r\n            max-height: 0;\r\n            line-height: 0;\r\n            mso-hide: all;\r\n        }\r\n\r\n        @media only screen and (max-width:600px) {\r\n            p, ul li, ol li, a {\r\n                font-size: 14px !important;\r\n                line-height: 150% !important\r\n            }\r\n\r\n            h1 {\r\n                font-size: 28px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h2 {\r\n                font-size: 26px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h3 {\r\n                font-size: 20px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h1 a {\r\n                font-size: 28px !important\r\n            }\r\n\r\n            h2 a {\r\n                font-size: 26px !important\r\n            }\r\n\r\n            h3 a {\r\n                font-size: 20px !important\r\n            }\r\n\r\n            .es-menu td a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {\r\n                font-size: 14px !important\r\n            }\r\n\r\n            .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {\r\n                font-size: 11px !important\r\n            }\r\n\r\n            *[class=\"gmail-fix\"] {\r\n                display: none !important\r\n            }\r\n\r\n            .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {\r\n                text-align: right !important\r\n            }\r\n\r\n            .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {\r\n                text-align: left !important\r\n            }\r\n\r\n                .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {\r\n                    display: inline !important\r\n                }\r\n\r\n            .es-button-border {\r\n                display: block !important\r\n            }\r\n\r\n            a.es-button {\r\n                font-size: 14px !important;\r\n                display: block !important;\r\n                border-left-width: 0px !important;\r\n                border-right-width: 0px !important\r\n            }\r\n\r\n            .es-btn-fw {\r\n                border-width: 10px 0px !important;\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right {\r\n                width: 100% !important\r\n            }\r\n\r\n            .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {\r\n                width: 100% !important;\r\n                max-width: 600px !important\r\n            }\r\n\r\n            .es-adapt-td {\r\n                display: block !important;\r\n                width: 100% !important\r\n            }\r\n\r\n            .adapt-img {\r\n                width: 100% !important;\r\n                height: auto !important\r\n            }\r\n\r\n            .es-m-p0 {\r\n                padding: 0px !important\r\n            }\r\n\r\n            .es-m-p0r {\r\n                padding-right: 0px !important\r\n            }\r\n\r\n            .es-m-p0l {\r\n                padding-left: 0px !important\r\n            }\r\n\r\n            .es-m-p0t {\r\n                padding-top: 0px !important\r\n            }\r\n\r\n            .es-m-p0b {\r\n                padding-bottom: 0 !important\r\n            }\r\n\r\n            .es-m-p20b {\r\n                padding-bottom: 20px !important\r\n            }\r\n\r\n            .es-mobile-hidden, .es-hidden {\r\n                display: none !important\r\n            }\r\n\r\n            tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {\r\n                width: auto !important;\r\n                overflow: visible !important;\r\n                float: none !important;\r\n                max-height: inherit !important;\r\n                line-height: inherit !important\r\n            }\r\n\r\n            tr.es-desk-hidden {\r\n                display: table-row !important\r\n            }\r\n\r\n            table.es-desk-hidden {\r\n                display: table !important\r\n            }\r\n\r\n            td.es-desk-menu-hidden {\r\n                display: table-cell !important\r\n            }\r\n\r\n            .es-menu td {\r\n                width: 1% !important\r\n            }\r\n\r\n            table.es-table-not-adapt, .esd-block-html table {\r\n                width: auto !important\r\n            }\r\n\r\n            table.es-social {\r\n                display: inline-block !important\r\n            }\r\n\r\n                table.es-social td {\r\n                    display: inline-block !important\r\n                }\r\n        }\r\n    </style>\r\n</head>\r\n<body style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n    <div class=\"es-wrapper-color\" style=\"background-color:#F6F6F6\">\r\n        <!--[if gte mso 9]><v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\"> <v:fill type=\"tile\" color=\"#f6f6f6\"></v:fill> </v:background><![endif]-->\r\n        <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top\">\r\n            <tr style=\"border-collapse:collapse\">\r\n                <td valign=\"top\" style=\"padding:0;Margin:0\">\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"Margin:0;padding-bottom:10px;padding-top:20px;padding-left:20px;padding-right:20px;background-position:center top\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-position:left top\" role=\"presentation\">\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0;padding-bottom:10px\">\r\n                                                                    <br />\r\n                                                                    <h2>{System}</h2>\r\n                                                                    <h2 style=\"margin-top:120px;line-height:29px;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:24px;font-style:normal;font-weight:bold;color:#040404\">\r\n                                                                        <img class=\"adapt-img\" src=\"{root}/img/GsfLogoFondoNavbar.jpg\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" /><br />\r\n                                                                    </h2>\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                            <hr />\r\n                                            <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position: top\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td valign=\"top\" style=\"padding:0;Margin:0\">\r\n                                                        <p>\r\n                                                            <b>  Usuario: </b>{UserName}\r\n                                                        </p>\r\n                                                        <p>\r\n                                                            <b>Descripción de la acción realizada: </b>{UserDescription}\r\n                                                        </p>\r\n                                                        <p>\r\n                                                            <label for=\"logFilter\"><b>Seq Filter</b></label>\r\n                                                            <input type=\"text\" id=\"logFilter\" class=\"fobrrm-control\" readonly=readonly value=\"{LogFilter}\" />\r\n                                                        </p>\r\n                                                        <hr />\r\n                                                        <p>\r\n                                                            <b>  Path: </b>{Path}\r\n                                                        </p>\r\n                                                        <p>\r\n                                                            <b>  Exception Type: </b>{Type}\r\n                                                        </p>\r\n                                                        <p>\r\n                                                            <b>  Message: </b>{Message}\r\n                                                        </p>\r\n                                                        <p>\r\n                                                            <b>  StackTrace: </b>{StackTrace}\r\n                                                        </p>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n</body>\r\n\r\n</html>\r\n", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Reporte de Errores", 1L, 0, 0, 45, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1, null, "Reporte de Errores", 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 4L, true, "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n<head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <title>Nueva plantilla de correo electrónico 2020-11-03</title> <!--[if (mso 16)]><style type=\"text/css\">    a {text-decoration: none;}    </style><![endif]--> <!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]-->\r\n    <!--[if gte mso 9]><xml> <o:OfficeDocumentSettings> <o:AllowPNG>\r\n    </o:AllowPNG> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml><![endif]-->\r\n    <style type=\"text/css\">\r\n        #outlook a {\r\n            padding: 0;\r\n        }\r\n\r\n        .ExternalClass {\r\n            width: 100%;\r\n        }\r\n\r\n            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {\r\n                line-height: 100%;\r\n            }\r\n\r\n        .es-button {\r\n            mso-style-priority: 100 !important;\r\n            text-decoration: none !important;\r\n        }\r\n\r\n        a[x-apple-data-detectors] {\r\n            color: inherit !important;\r\n            text-decoration: none !important;\r\n            font-size: inherit !important;\r\n            font-family: inherit !important;\r\n            font-weight: inherit !important;\r\n            line-height: inherit !important;\r\n        }\r\n\r\n        .es-desk-hidden {\r\n            display: none;\r\n            float: left;\r\n            overflow: hidden;\r\n            width: 0;\r\n            max-height: 0;\r\n            line-height: 0;\r\n            mso-hide: all;\r\n        }\r\n\r\n        @media only screen and (max-width:600px) {\r\n            p, ul li, ol li, a {\r\n                font-size: 14px !important;\r\n                line-height: 150% !important\r\n            }\r\n\r\n            h1 {\r\n                font-size: 28px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h2 {\r\n                font-size: 26px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h3 {\r\n                font-size: 20px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h1 a {\r\n                font-size: 28px !important\r\n            }\r\n\r\n            h2 a {\r\n                font-size: 26px !important\r\n            }\r\n\r\n            h3 a {\r\n                font-size: 20px !important\r\n            }\r\n\r\n            .es-menu td a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {\r\n                font-size: 14px !important\r\n            }\r\n\r\n            .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {\r\n                font-size: 11px !important\r\n            }\r\n\r\n            *[class=\"gmail-fix\"] {\r\n                display: none !important\r\n            }\r\n\r\n            .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {\r\n                text-align: right !important\r\n            }\r\n\r\n            .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {\r\n                text-align: left !important\r\n            }\r\n\r\n                .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {\r\n                    display: inline !important\r\n                }\r\n\r\n            .es-button-border {\r\n                display: block !important\r\n            }\r\n\r\n            a.es-button {\r\n                font-size: 14px !important;\r\n                display: block !important;\r\n                border-left-width: 0px !important;\r\n                border-right-width: 0px !important\r\n            }\r\n\r\n            .es-btn-fw {\r\n                border-width: 10px 0px !important;\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right {\r\n                width: 100% !important\r\n            }\r\n\r\n            .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {\r\n                width: 100% !important;\r\n                max-width: 600px !important\r\n            }\r\n\r\n            .es-adapt-td {\r\n                display: block !important;\r\n                width: 100% !important\r\n            }\r\n\r\n            .adapt-img {\r\n                width: 100% !important;\r\n                height: auto !important\r\n            }\r\n\r\n            .es-m-p0 {\r\n                padding: 0px !important\r\n            }\r\n\r\n            .es-m-p0r {\r\n                padding-right: 0px !important\r\n            }\r\n\r\n            .es-m-p0l {\r\n                padding-left: 0px !important\r\n            }\r\n\r\n            .es-m-p0t {\r\n                padding-top: 0px !important\r\n            }\r\n\r\n            .es-m-p0b {\r\n                padding-bottom: 0 !important\r\n            }\r\n\r\n            .es-m-p20b {\r\n                padding-bottom: 20px !important\r\n            }\r\n\r\n            .es-mobile-hidden, .es-hidden {\r\n                display: none !important\r\n            }\r\n\r\n            tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {\r\n                width: auto !important;\r\n                overflow: visible !important;\r\n                float: none !important;\r\n                max-height: inherit !important;\r\n                line-height: inherit !important\r\n            }\r\n\r\n            tr.es-desk-hidden {\r\n                display: table-row !important\r\n            }\r\n\r\n            table.es-desk-hidden {\r\n                display: table !important\r\n            }\r\n\r\n            td.es-desk-menu-hidden {\r\n                display: table-cell !important\r\n            }\r\n\r\n            .es-menu td {\r\n                width: 1% !important\r\n            }\r\n\r\n            table.es-table-not-adapt, .esd-block-html table {\r\n                width: auto !important\r\n            }\r\n\r\n            table.es-social {\r\n                display: inline-block !important\r\n            }\r\n\r\n                table.es-social td {\r\n                    display: inline-block !important\r\n                }\r\n        }\r\n    </style>\r\n</head>\r\n<body style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n    <div class=\"es-wrapper-color\" style=\"background-color:#F6F6F6\">\r\n        <!--[if gte mso 9]><v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\"> <v:fill type=\"tile\" color=\"#f6f6f6\"></v:fill> </v:background><![endif]-->\r\n        <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top\">\r\n            <tr style=\"border-collapse:collapse\">\r\n                <td valign=\"top\" style=\"padding:0;Margin:0\">\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"Margin:0;padding-bottom:10px;padding-top:20px;padding-left:20px;padding-right:20px;background-position:center top\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-position:left top\" role=\"presentation\">\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0;padding-bottom:10px\">\r\n                                                                    <br />\r\n                                                                    <h2>{application}</h2>\r\n                                                                    <h2 style=\"margin-top:120px;line-height:29px;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:24px;font-style:normal;font-weight:bold;color:#040404\">\r\n                                                                        <img class=\"adapt-img\" src=\"{root}/img/GsfLogoFondoNavbar.jpg\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" /><br />{seHaSolicitadoActivacionDeCuenta}\r\n                                                                    </h2>\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                            <tr style=\"border-collapse:collapse\"> <td align=\"center\" style=\"padding:0;Margin:0;font-size:0px\"><a target=\"_blank\" href=\"#\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:14px;text-decoration:underline;color:#040404\"><img src=\"https://upload.wikimedia.org/wikipedia/commons/9/97/Avast_Passwords_logo.png\" alt=alt alt=alt style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" width=\"200\" /></a></td> </tr>\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" style=\"Margin:0;padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:15px\">\r\n                                                                    <span class=\"es-button-border\" style=\"border-style:solid;border-color:#38C2F1;background:#37EC86;border-width:0px;display:inline-block;border-radius:25px;width:auto\"><a href=\"demo.globalsis.com.ar/Socios/security/useractivation?token={token}\" class=\"es-button\" target=\"_blank\" style=\"mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:18px;color:#FFFFFF;border-style:solid;border-color:#37EC86;border-width:10px 30px;display:inline-block;background:#37EC86;border-radius:25px;font-weight:bold;font-style:normal;line-height:22px;width:auto;text-align:center\">{activarMiCuenta}</a></span>\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"> <tr style=\"border-collapse:collapse\"> <td style=\"padding:0;Margin:0\"> <div> Si este mail no fué solicitado, escribir a <a href=\"mailto:soporte@globalsis.com.ar\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:14px;text-decoration:underline;color:#040404\">Soporte</a></div><br /></td> </tr> </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table> <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"transparent\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"Margin:0;padding-left:20px;padding-right:20px;padding-top:30px;padding-bottom:30px;background-position:left top\">\r\n                                            <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td valign=\"top\" align=\"center\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" style=\"padding:0;Margin:0;font-size:0px\">\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n</body>\r\n\r\n</html>", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Activación de usuario", 1001L, 0, 0, 45, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1, null, "Portal Socios | Activación de usuario", 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 5L, true, "<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Vinculacion de Usuario con Empresa</title>\r\n</head>\r\n<body>\r\n    Se Vinculo tu usuario con la empresa {empresaPortalBody} \r\n</body>\r\n</html>\r\n", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Vinculación de usuario", 1L, 0, 0, 45, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1, null, "Portal Certificaciones | Se vinculó tu usuario con el socio {empresaPortalSubject}", 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 6L, true, "<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Vinculacion de Usuario con Empresa</title>\r\n</head>\r\n<body>\r\n    Se Vinculo tu usuario con la empresa {empresaPortalBody}\r\n</body>\r\n</html>", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Vinculación de usuario", 1001L, 0, 0, 45, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 2, null, "Portal Socios | Se vinculó tu usuario con el socio {empresaPortalSubject}", 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 7L, true, "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n<head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <title>Nueva plantilla de correo electrónico 2020-11-03</title> <!--[if (mso 16)]><style type=\"text/css\">    a {text-decoration: none;}    </style><![endif]--> <!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]-->\r\n    <!--[if gte mso 9]><xml> <o:OfficeDocumentSettings> <o:AllowPNG>\r\n    </o:AllowPNG> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml><![endif]-->\r\n    <style type=\"text/css\">\r\n        #outlook a {\r\n            padding: 0;\r\n        }\r\n\r\n        .ExternalClass {\r\n            width: 100%;\r\n        }\r\n\r\n            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {\r\n                line-height: 100%;\r\n            }\r\n\r\n        .es-button {\r\n            mso-style-priority: 100 !important;\r\n            text-decoration: none !important;\r\n        }\r\n\r\n        a[x-apple-data-detectors] {\r\n            color: inherit !important;\r\n            text-decoration: none !important;\r\n            font-size: inherit !important;\r\n            font-family: inherit !important;\r\n            font-weight: inherit !important;\r\n            line-height: inherit !important;\r\n        }\r\n\r\n        .es-desk-hidden {\r\n            display: none;\r\n            float: left;\r\n            overflow: hidden;\r\n            width: 0;\r\n            max-height: 0;\r\n            line-height: 0;\r\n            mso-hide: all;\r\n        }\r\n\r\n        @media only screen and (max-width:600px) {\r\n            p, ul li, ol li, a {\r\n                font-size: 14px !important;\r\n                line-height: 150% !important\r\n            }\r\n\r\n            h1 {\r\n                font-size: 28px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h2 {\r\n                font-size: 26px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h3 {\r\n                font-size: 20px !important;\r\n                text-align: center;\r\n                line-height: 120% !important\r\n            }\r\n\r\n            h1 a {\r\n                font-size: 28px !important\r\n            }\r\n\r\n            h2 a {\r\n                font-size: 26px !important\r\n            }\r\n\r\n            h3 a {\r\n                font-size: 20px !important\r\n            }\r\n\r\n            .es-menu td a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {\r\n                font-size: 12px !important\r\n            }\r\n\r\n            .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {\r\n                font-size: 14px !important\r\n            }\r\n\r\n            .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {\r\n                font-size: 11px !important\r\n            }\r\n\r\n            *[class=\"gmail-fix\"] {\r\n                display: none !important\r\n            }\r\n\r\n            .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {\r\n                text-align: right !important\r\n            }\r\n\r\n            .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {\r\n                text-align: left !important\r\n            }\r\n\r\n                .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {\r\n                    display: inline !important\r\n                }\r\n\r\n            .es-button-border {\r\n                display: block !important\r\n            }\r\n\r\n            a.es-button {\r\n                font-size: 14px !important;\r\n                display: block !important;\r\n                border-left-width: 0px !important;\r\n                border-right-width: 0px !important\r\n            }\r\n\r\n            .es-btn-fw {\r\n                border-width: 10px 0px !important;\r\n                text-align: center !important\r\n            }\r\n\r\n            .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right {\r\n                width: 100% !important\r\n            }\r\n\r\n            .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {\r\n                width: 100% !important;\r\n                max-width: 600px !important\r\n            }\r\n\r\n            .es-adapt-td {\r\n                display: block !important;\r\n                width: 100% !important\r\n            }\r\n\r\n            .adapt-img {\r\n                width: 100% !important;\r\n                height: auto !important\r\n            }\r\n\r\n            .es-m-p0 {\r\n                padding: 0px !important\r\n            }\r\n\r\n            .es-m-p0r {\r\n                padding-right: 0px !important\r\n            }\r\n\r\n            .es-m-p0l {\r\n                padding-left: 0px !important\r\n            }\r\n\r\n            .es-m-p0t {\r\n                padding-top: 0px !important\r\n            }\r\n\r\n            .es-m-p0b {\r\n                padding-bottom: 0 !important\r\n            }\r\n\r\n            .es-m-p20b {\r\n                padding-bottom: 20px !important\r\n            }\r\n\r\n            .es-mobile-hidden, .es-hidden {\r\n                display: none !important\r\n            }\r\n\r\n            tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {\r\n                width: auto !important;\r\n                overflow: visible !important;\r\n                float: none !important;\r\n                max-height: inherit !important;\r\n                line-height: inherit !important\r\n            }\r\n\r\n            tr.es-desk-hidden {\r\n                display: table-row !important\r\n            }\r\n\r\n            table.es-desk-hidden {\r\n                display: table !important\r\n            }\r\n\r\n            td.es-desk-menu-hidden {\r\n                display: table-cell !important\r\n            }\r\n\r\n            .es-menu td {\r\n                width: 1% !important\r\n            }\r\n\r\n            table.es-table-not-adapt, .esd-block-html table {\r\n                width: auto !important\r\n            }\r\n\r\n            table.es-social {\r\n                display: inline-block !important\r\n            }\r\n\r\n                table.es-social td {\r\n                    display: inline-block !important\r\n                }\r\n        }\r\n    </style>\r\n</head>\r\n<body style=\"width:100%;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n    <div class=\"es-wrapper-color\" style=\"background-color:#F6F6F6\">\r\n        <!--[if gte mso 9]><v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\"> <v:fill type=\"tile\" color=\"#f6f6f6\"></v:fill> </v:background><![endif]-->\r\n        <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top\">\r\n            <tr style=\"border-collapse:collapse\">\r\n                <td valign=\"top\" style=\"padding:0;Margin:0\">\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"Margin:0;padding-bottom:10px;padding-top:20px;padding-left:20px;padding-right:20px;background-position:center top\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-position:left top\" role=\"presentation\">\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0;padding-bottom:10px\">\r\n                                                                    <br />\r\n                                                                    <h2>{application}</h2>\r\n                                                                    <h2 style=\"margin-top:120px;line-height:29px;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:24px;font-style:normal;font-weight:bold;color:#040404\">\r\n                                                                        <img class=\"adapt-img\" src=\"{root}/img/GsfLogoFondoNavbar.jpg\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" /><br />{seHaSolicitadoUnCambioDePassword}\r\n                                                                    </h2>\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                            <tr style=\"border-collapse:collapse\"> <td align=\"center\" style=\"padding:0;Margin:0;font-size:0px\"><a target=\"_blank\" href=\"#\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:14px;text-decoration:underline;color:#040404\"><img src=\"https://upload.wikimedia.org/wikipedia/commons/9/97/Avast_Passwords_logo.png\" alt=alt alt=alt style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" width=\"200\" /></a></td> </tr>\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" style=\"Margin:0;padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:15px\">\r\n                                                                    <span class=\"es-button-border\" style=\"border-style:solid;border-color:#38C2F1;background:#37EC86;border-width:0px;display:inline-block;border-radius:25px;width:auto\"><a href=\"demo.globalsis.com.ar/Socios/security/passwordrecovery?token={token}\" class=\"es-button\" target=\"_blank\" style=\"mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:18px;color:#FFFFFF;border-style:solid;border-color:#37EC86;border-width:10px 30px;display:inline-block;background:#37EC86;border-radius:25px;font-weight:bold;font-style:normal;line-height:22px;width:auto;text-align:center\">{recuperarMiContraseña}</a></span>\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"> <tr style=\"border-collapse:collapse\"> <td style=\"padding:0;Margin:0\"> <div> Si este mail no fué solicitado, escribir a <a href=\"mailto:soporte@globalsis.com.ar\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-size:14px;text-decoration:underline;color:#040404\">Soporte</a></div><br /></td> </tr> </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table> <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n                        <tr style=\"border-collapse:collapse\">\r\n                            <td align=\"center\" style=\"padding:0;Margin:0\">\r\n                                <table bgcolor=\"transparent\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px\">\r\n                                    <tr style=\"border-collapse:collapse\">\r\n                                        <td align=\"left\" style=\"Margin:0;padding-left:20px;padding-right:20px;padding-top:30px;padding-bottom:30px;background-position:left top\">\r\n                                            <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                <tr style=\"border-collapse:collapse\">\r\n                                                    <td valign=\"top\" align=\"center\" style=\"padding:0;Margin:0;width:560px\">\r\n                                                        <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                                                            <tr style=\"border-collapse:collapse\">\r\n                                                                <td align=\"center\" style=\"padding:0;Margin:0;font-size:0px\">\r\n                                                                </td>\r\n                                                            </tr>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n</body>\r\n\r\n</html>", new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, "Recuperación de Contraseña", 1001L, 0, 0, 45, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1, null, "Portal Socios | Recuperación de Contraseña", 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_Groups",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "DomainFIdm", "GroupOwnerId", "InternalCode", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "SystemUse", "__MigCode", "__MigId" },
                values: new object[] { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Grupo de Administradores del Sistema", 1L, 1L, "SysAdmin", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Grupo SysAdmin", null, true, null, null });

            migrationBuilder.InsertData(
                table: "sec_Options",
                columns: new[] { "Id", "Code", "ContextKey", "Created", "CreatedBy", "Description", "DomainFIdm", "Icon", "IsDeleted", "Modified", "ModifiedBy", "Name", "OrderNo", "Page", "ParentId", "Session", "TargetPath", "Transferable", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 10L, "PRM", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modulo de Parametrización", 1L, "fas fas fa-folder", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Parametrización", 10, null, null, null, "", true, null, null },
                    { 20L, "SEG", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modulo de Seguridad", 1L, "fas fas fa-lock", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Seguridad", 100, null, null, null, "", false, null, null },
                    { 25L, "CON", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modulo de configuración de Usuario", 1L, "fas fas fa-cog", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Configuración de Usuario", 100000, null, null, null, "", true, null, null },
                    { 27L, "NTF", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Notificaciones", 1L, "fas fas fa-bell", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Notificaciones", 500, null, null, null, "", true, null, null },
                    { 29L, "CFG", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Configuración", 1L, "fas fa-sliders-h", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Configuración", 500, null, null, null, "", true, null, null },
                    { 32L, "INT", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Módulo de Interfaces", 1L, "fas fa-file", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Interfaces", 500, null, null, null, "", true, null, null },
                    { 100001L, "SOC", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Módulo de Socios", 1001L, "fas fa-users", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Socios", 50, null, null, null, "", true, null, null },
                    { 100003L, "PRO", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Módulo de Socios", 1L, "fas fa-users", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Socios", 50, null, null, null, "", true, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_Grants",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "DomainFIdm", "IsDeleted", "Modified", "ModifiedBy", "Name", "OptionId", "Session", "Transferable", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 27L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Parámetros", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "parametros.create", 29L, null, true, null, null },
                    { 28L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Parámetros", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "parametros.update", 29L, null, true, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_Groups",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "DomainFIdm", "GroupOwnerId", "InternalCode", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "SystemUse", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Grupo de Módulo de Seguridad", 1L, 1L, "MainSeguridad", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Grupo Seguridad", null, false, null, null },
                    { 1000L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Grupo de usuarios del portal de Socios", 1001L, 1L, "Socios", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Socios", null, true, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_GroupsOrganizations",
                columns: new[] { "Id", "Created", "CreatedBy", "GroupId", "IsDeleted", "Modified", "ModifiedBy", "OrganizationId", "Session", "__MigCode", "__MigId" },
                values: new object[] { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null });

            migrationBuilder.InsertData(
                table: "sec_Options",
                columns: new[] { "Id", "Code", "ContextKey", "Created", "CreatedBy", "Description", "DomainFIdm", "Icon", "IsDeleted", "Modified", "ModifiedBy", "Name", "OrderNo", "Page", "ParentId", "Session", "TargetPath", "Transferable", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 11L, "PRM-REG", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Administración de Idiomas", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Regionales", 20, null, 10L, null, "/configuration/cultures/index", true, null, null },
                    { 12L, "PRM-NOT", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modulo de Notificaciones", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Notificaciones", 1, null, 10L, null, "/Configuration/NotificationTypes/Index", false, null, null },
                    { 21L, "SEG-USR", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modulo de usuarios", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Usuarios", 10, null, 20L, null, "/security/users/index", false, null, null },
                    { 22L, "SEG-GRP", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modulo de grupos", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Grupos", 20, null, 20L, null, "/security/groups/index", false, null, null },
                    { 23L, "SEG-ROL", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modulo de Roles", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Roles", 30, null, 20L, null, "/security/roles/index", false, null, null },
                    { 24L, "PRM-CNY", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Empresas", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Empresas", 5, null, 10L, null, "/Companies/Index", false, null, null },
                    { 26L, "CON-PSS", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modulo de cambio de contraseña", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Cambiar contraseña", 10, null, 25L, null, "/security/PasswordChange", true, null, null },
                    { 28L, "NTF-MGT", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gestión", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Consulta", 10, null, 27L, null, "/notifications/index", true, null, null },
                    { 30L, "CFG-PAR", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gestión de Parámetros", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Parámetros", 10, null, 29L, null, "/configuracion/parametros/index", true, null, null },
                    { 31L, "ALE-GES", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gestión", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gestión", 10, null, 27L, null, "/alertas/index", true, null, null },
                    { 33L, "INT-PRO", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Resultados de Procesos de importación y exportación", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Resultados de Procesos", 10, null, 32L, null, "/Interfaces/ResultadosProcesos/Index", true, null, null },
                    { 34L, "INT-DEF", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Definición de Interfaces", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Definiciones", 10, null, 32L, null, "/Interfaces/Definiciones/Index", true, null, null },
                    { 35L, "INT-REG", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Definición de Reglas de Interfaces", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Reglas - Definiciones", 10, null, 32L, null, "/Interfaces/ReglasDefiniciones/Index", true, null, null },
                    { 36L, "SEG-ACT", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modulo de Actividad de Usuarios", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Actividad Usuarios", 15, null, 20L, null, "/security/Usuarios/Actividades/index", false, null, null },
                    { 100002L, "SOC-CER", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gestión de Solicitudes de Certificación", 1001L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Certificaciones", 1, null, 100001L, null, "Socios/Certificaciones/Index", true, null, null },
                    { 100004L, "SOC-CER", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Gestión de Solicitudes de Certificación", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Certificaciones", 1, null, 100003L, null, "Socios/Certificaciones/Index", true, null, null },
                    { 100005L, "PRO-ADM", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Administracion", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Administracion", 50, null, 100003L, null, "/Socios/Empresas/Index", true, null, null },
                    { 100006L, "PRO-IMP", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Impuestos", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Impuestos", 50, null, 10L, null, "/Socios/Impuestos/Index", true, null, null },
                    { 100007L, "PRO-PER", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Percepciones", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Percepciones", 50, null, 10L, null, "/Socios/Percepciones/Index", true, null, null },
                    { 100008L, "PRO-PRD", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Periodos", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Periodos", 50, null, 10L, null, "/Configuration/Periodos/Index", true, null, null },
                    { 100009L, "PRO-ODC", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Documentos de Compras", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Documentos de Compras", 50, null, 100003L, null, "/Socios/OrdenesCompras/Index", true, null, null },
                    { 100010L, "PRO-TOC", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tipos de Docs de Compra", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Tipos de Docs de Compra", 50, null, 10L, null, "/Configuration/TiposOrdenesCompras/Index", true, null, null },
                    { 100011L, "PRO-CGT", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Conceptos de Gastos", 1L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Conceptos de Gastos", 50, null, 10L, null, "/Configuration/ConceptosGastosTipos/Index", true, null, null },
                    { 100012L, "SOC-RFQ", null, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Muestra de Requisitos de FQS", 1001L, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Requisitos FQS", 2, null, 100001L, null, "Socios/RequisitosFQS/Index", true, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_Roles",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "DomainFIdm", "GroupOwnerId", "InternalCode", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "SystemUse", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rol de Administradores del Sistema", 1L, 1L, "SysAdmin", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SysAdmin", null, true, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rol de Módulo de Seguridad", 1L, 1L, "MainSeguridad", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Seguridad", null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_Users",
                columns: new[] { "Id", "Activated", "ActivationToken", "Blocked", "Created", "CreatedBy", "Email", "FirstName", "GroupOwnerId", "Hash", "IdNumber", "IsDeleted", "LastName", "Login", "LoginFailedAttempts", "Modified", "ModifiedBy", "NormalizedEmail", "NormalizedLogin", "PasswordRecoveryToken", "PhoneNumber", "RequiresNewPassword", "Salt", "Session", "SystemUse", "UserTypeIdm", "__MigCode", "__MigId" },
                values: new object[] { 1L, true, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "soporte@globalsis.com.ar", "Administrador", 1L, "pJPDrqVBpiPht6eOxNon3+ZhFd+uZFgW", "99000000", false, "Del Sistema", "admin", (short)0, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SOPORTE@GLOBALSIS.COM.AR", "ADMIN", null, null, false, "OesHsxvxkMeHoad0soJwKln5aKREznXa", null, true, 1, null, null });

            migrationBuilder.InsertData(
                table: "sec_CompaniesUsersGroups",
                columns: new[] { "Id", "CompanyId", "Created", "CreatedBy", "GroupId", "IsDeleted", "Modified", "ModifiedBy", "Session", "UserId", "__MigCode", "__MigId" },
                values: new object[] { 1L, 39L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 1L, null, null });

            migrationBuilder.InsertData(
                table: "sec_Grants",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "DomainFIdm", "IsDeleted", "Modified", "ModifiedBy", "Name", "OptionId", "Session", "Transferable", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.create", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.create", 21L, null, false, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.update", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.update", 21L, null, false, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.delete", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.delete", 21L, null, false, null, null },
                    { 4L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.delegate", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.delegate", 21L, null, false, null, null },
                    { 5L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.groupOwnerIdOverrule", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "users.groupOwnerIdOverrule", 21L, null, false, null, null },
                    { 6L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "companies.create", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "companies.create", 24L, null, false, null, null },
                    { 7L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "companies.update", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "companies.update", 24L, null, false, null, null },
                    { 8L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "companies.delete", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "companies.delete", 24L, null, false, null, null },
                    { 9L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.create", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.create", 22L, null, false, null, null },
                    { 10L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.update", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.update", 22L, null, false, null, null },
                    { 11L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.delete", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.delete", 22L, null, false, null, null },
                    { 12L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.delegate", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.delegate", 22L, null, false, null, null },
                    { 13L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.groupOwnerIdOverrule", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "groups.groupOwnerIdOverrule", 22L, null, false, null, null },
                    { 14L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.create", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.create", 23L, null, false, null, null },
                    { 15L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.update", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.update", 23L, null, false, null, null },
                    { 16L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.delete", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.delete", 23L, null, false, null, null },
                    { 17L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.delegate", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.delegate", 23L, null, false, null, null },
                    { 18L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.groupOwnerIdOverrule", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "roles.groupOwnerIdOverrule", 23L, null, false, null, null },
                    { 19L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "cultures.create", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "cultures.create", 11L, null, true, null, null },
                    { 20L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "cultures.update", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "cultures.update", 11L, null, true, null, null },
                    { 21L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "cultures.delete", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "cultures.delete", 11L, null, true, null, null },
                    { 22L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "notifications.create", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "notifications.create", 12L, null, true, null, null },
                    { 23L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "notifications.update", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "notifications.update", 12L, null, true, null, null },
                    { 24L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "notifications.delete", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "notifications.delete", 12L, null, true, null, null },
                    { 25L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "companies.create", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "config.changepassword", 26L, null, true, null, null },
                    { 26L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ntfManagement.create", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ntfManagement.create", 28L, null, true, null, null },
                    { 100001L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Solicitud de Certificación", 1001L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "solicitudcertificacion.create", 100002L, null, true, null, null },
                    { 100002L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Solicitud de Certificación", 1001L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "solicitudcertificacion.delete", 100002L, null, true, null, null },
                    { 100003L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Solicitud de Certificación", 1001L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "solicitudcertificacion.update", 100002L, null, true, null, null },
                    { 100004L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Solicitud de Certificación", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "solicitudcertificacion.create", 100004L, null, true, null, null },
                    { 100005L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Comprobantes", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "solicitudcertificacion.update", 100004L, null, true, null, null },
                    { 100006L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Solicitud de Certificacion", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "solicitudcertificacion.delete", 100004L, null, true, null, null },
                    { 100007L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Empresas Portales", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "empresasportales.create", 100005L, null, true, null, null },
                    { 100008L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Empresas Portales", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "empresasportales.update", 100005L, null, true, null, null },
                    { 100009L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Empresas Portales", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "empresasportales.delete", 100005L, null, true, null, null },
                    { 100010L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Usuarios de Empresas Portales", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "usuariosempresas.create", 100005L, null, true, null, null },
                    { 100011L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Usuarios de Empresas Portales", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "usuariosempresas.update", 100005L, null, true, null, null },
                    { 100012L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Usuarios de Empresas Portales", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "usuariosempresas.delete", 100005L, null, true, null, null },
                    { 100013L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Impuestos", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "impuestos.create", 100006L, null, true, null, null },
                    { 100014L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Impuestos", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "impuestos.update", 100006L, null, true, null, null },
                    { 100015L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Impuestos", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "impuestos.delete", 100006L, null, true, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_Grants",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "DomainFIdm", "IsDeleted", "Modified", "ModifiedBy", "Name", "OptionId", "Session", "Transferable", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 100016L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Percepciones", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Percepciones.create", 100007L, null, true, null, null },
                    { 100017L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Percepciones", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Percepciones.update", 100007L, null, true, null, null },
                    { 100018L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Percepciones", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Percepciones.delete", 100007L, null, true, null, null },
                    { 100019L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Periodos", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "periodos.create", 100008L, null, true, null, null },
                    { 100020L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Periodos", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "periodos.update", 100008L, null, true, null, null },
                    { 100021L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Periodos", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "periodos.delete", 100008L, null, true, null, null },
                    { 100022L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Documentos de Compras", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ordenescompras.create", 100009L, null, true, null, null },
                    { 100023L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Documentos de Compras", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ordenescompras.update", 100009L, null, true, null, null },
                    { 100024L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Documentos de Compras", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "ordenescompras.delete", 100009L, null, true, null, null },
                    { 100025L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Tipos de Documentos de Compras", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "tiposordenescompras.create", 100010L, null, true, null, null },
                    { 100026L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Tipos de Documentos de Compras", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "tiposordenescompras.update", 100010L, null, true, null, null },
                    { 100027L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Tipos de Documentos de Compras", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "tiposordenescompras.delete", 100010L, null, true, null, null },
                    { 100028L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Alta de Tipos de Conceptos de Gastos", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "conceptosgastostipos.create", 100011L, null, true, null, null },
                    { 100029L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Modificación de Tipos de Conceptos de Gastos", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "conceptosgastostipos.update", 100011L, null, true, null, null },
                    { 100030L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Baja de Tipos de Conceptos de Gastos", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "conceptosgastostipos.delete", 100011L, null, true, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_GroupsOrganizations",
                columns: new[] { "Id", "Created", "CreatedBy", "GroupId", "IsDeleted", "Modified", "ModifiedBy", "OrganizationId", "Session", "__MigCode", "__MigId" },
                values: new object[] { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 2L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 39L, null, null, null });

            migrationBuilder.InsertData(
                table: "sec_GroupsRoles",
                columns: new[] { "Id", "Created", "CreatedBy", "GroupId", "IsDeleted", "Modified", "ModifiedBy", "RoleId", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 2L, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_Roles",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "DomainFIdm", "GroupOwnerId", "InternalCode", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "SystemUse", "__MigCode", "__MigId" },
                values: new object[] { 1001L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Administrador del Portal de Socios", 1001L, 1000L, "Socios_Admin", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Admin", null, true, null, null });

            migrationBuilder.InsertData(
                table: "sec_RolesOptions",
                columns: new[] { "Id", "Created", "CreatedBy", "IsDeleted", "Modified", "ModifiedBy", "OptionId", "RoleId", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 10L, 1L, null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 11L, 1L, null, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 12L, 1L, null, null, null },
                    { 4L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 20L, 1L, null, null, null },
                    { 5L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 21L, 1L, null, null, null },
                    { 6L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 22L, 1L, null, null, null },
                    { 7L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 23L, 1L, null, null, null },
                    { 8L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 24L, 1L, null, null, null },
                    { 9L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 25L, 1L, null, null, null },
                    { 10L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 26L, 1L, null, null, null },
                    { 11L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 27L, 1L, null, null, null },
                    { 12L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 28L, 1L, null, null, null },
                    { 13L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 29L, 1L, null, null, null },
                    { 14L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 30L, 1L, null, null, null },
                    { 15L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 31L, 1L, null, null, null },
                    { 16L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 32L, 1L, null, null, null },
                    { 17L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 33L, 1L, null, null, null },
                    { 18L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 34L, 1L, null, null, null },
                    { 19L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 35L, 1L, null, null, null },
                    { 20L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 36L, 1L, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "sec_Users",
                columns: new[] { "Id", "Activated", "ActivationToken", "Blocked", "Created", "CreatedBy", "Email", "FirstName", "GroupOwnerId", "Hash", "IdNumber", "IsDeleted", "LastName", "Login", "LoginFailedAttempts", "Modified", "ModifiedBy", "NormalizedEmail", "NormalizedLogin", "PasswordRecoveryToken", "PhoneNumber", "RequiresNewPassword", "Salt", "Session", "SystemUse", "UserTypeIdm", "__MigCode", "__MigId" },
                values: new object[] { 2L, true, null, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "soporte@globalsis.com.ar", "Usuario", 2L, "hlgFFdBU8cbw+1+Wdd35wDkv8VaAsoM6", "99000000", false, "Seguridad", "seguridad", (short)0, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "SOPORTE@GLOBALSIS.COM.AR", "SEGURIDAD", null, null, false, "Mg57oGVIZZoT1DaNlgpgc2jByxN1c+7f", null, false, 1, null, null });

            migrationBuilder.InsertData(
                table: "sys_UserDomainFs",
                columns: new[] { "Id", "Created", "CreatedBy", "DomainFIdm", "IsDeleted", "Modified", "ModifiedBy", "Session", "UserId", "__MigCode", "__MigId" },
                values: new object[] { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 1L, null, null });

            migrationBuilder.InsertData(
                table: "sec_CompaniesUsersGroups",
                columns: new[] { "Id", "CompanyId", "Created", "CreatedBy", "GroupId", "IsDeleted", "Modified", "ModifiedBy", "Session", "UserId", "__MigCode", "__MigId" },
                values: new object[] { 2L, 39L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 2L, null, null });

            migrationBuilder.InsertData(
                table: "sec_RolesGrants",
                columns: new[] { "Id", "Created", "CreatedBy", "GrantId", "IsDeleted", "Modified", "ModifiedBy", "RoleId", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 2L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 3L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 3L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 4L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 4L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 5L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 5L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 6L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 6L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 7L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 7L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 8L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 8L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 9L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 9L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 10L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 10L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 11L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 11L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 12L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 12L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 13L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 13L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 14L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 14L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 15L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 15L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 16L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 16L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 17L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 17L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 18L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 18L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 19L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 19L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 20L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 20L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 21L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 21L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 22L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 22L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 23L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 23L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 24L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 24L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null },
                    { 25L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 25L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "sys_UserDomainFs",
                columns: new[] { "Id", "Created", "CreatedBy", "DomainFIdm", "IsDeleted", "Modified", "ModifiedBy", "Session", "UserId", "__MigCode", "__MigId" },
                values: new object[] { 2L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", 1L, false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, 2L, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaConReglaAdicionales___MigCode",
                table: "ale_AlertaConReglaAdicionales",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaConReglaAdicionales___MigId",
                table: "ale_AlertaConReglaAdicionales",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaConReglaAdicionales_AlertaId",
                table: "ale_AlertaConReglaAdicionales",
                column: "AlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaConReglaAdicionales_AlertaTipoReglaAdicionalIdm",
                table: "ale_AlertaConReglaAdicionales",
                column: "AlertaTipoReglaAdicionalIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaConReglaAdicionales_Created_Modified",
                table: "ale_AlertaConReglaAdicionales",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaConReglaAdicionales_CreatedBy_ModifiedBy",
                table: "ale_AlertaConReglaAdicionales",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaConReglaAdicionales_Session",
                table: "ale_AlertaConReglaAdicionales",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaEncoladas___MigCode",
                table: "ale_AlertaEncoladas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaEncoladas___MigId",
                table: "ale_AlertaEncoladas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaEncoladas_AlertaId",
                table: "ale_AlertaEncoladas",
                column: "AlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaEncoladas_Created_Modified",
                table: "ale_AlertaEncoladas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaEncoladas_CreatedBy_ModifiedBy",
                table: "ale_AlertaEncoladas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaEncoladas_Session",
                table: "ale_AlertaEncoladas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ale_Alertas___MigCode",
                table: "ale_Alertas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ale_Alertas___MigId",
                table: "ale_Alertas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_Alertas_AlertaTipoIdm",
                table: "ale_Alertas",
                column: "AlertaTipoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ale_Alertas_Created_Modified",
                table: "ale_Alertas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_Alertas_CreatedBy_ModifiedBy",
                table: "ale_Alertas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_Alertas_DestinatarioVariableIdm",
                table: "ale_Alertas",
                column: "DestinatarioVariableIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ale_Alertas_Session",
                table: "ale_Alertas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoCampoVariable___MigCode",
                table: "ale_AlertaTipoCampoVariable",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoCampoVariable___MigId",
                table: "ale_AlertaTipoCampoVariable",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoCampoVariable_AlertaTipoIdm",
                table: "ale_AlertaTipoCampoVariable",
                column: "AlertaTipoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoCampoVariable_Created_Modified",
                table: "ale_AlertaTipoCampoVariable",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoCampoVariable_CreatedBy_ModifiedBy",
                table: "ale_AlertaTipoCampoVariable",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoCampoVariable_Session",
                table: "ale_AlertaTipoCampoVariable",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoEstados___MigCode",
                table: "ale_AlertaTipoEstados",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoEstados___MigId",
                table: "ale_AlertaTipoEstados",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoEstados_AlertaTipoIdm",
                table: "ale_AlertaTipoEstados",
                column: "AlertaTipoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoEstados_Created_Modified",
                table: "ale_AlertaTipoEstados",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoEstados_CreatedBy_ModifiedBy",
                table: "ale_AlertaTipoEstados",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoEstados_Session",
                table: "ale_AlertaTipoEstados",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoReglaAdicionales___MigCode",
                table: "ale_AlertaTipoReglaAdicionales",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoReglaAdicionales___MigId",
                table: "ale_AlertaTipoReglaAdicionales",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoReglaAdicionales_AlertaTipoIdm",
                table: "ale_AlertaTipoReglaAdicionales",
                column: "AlertaTipoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoReglaAdicionales_Created_Modified",
                table: "ale_AlertaTipoReglaAdicionales",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoReglaAdicionales_CreatedBy_ModifiedBy",
                table: "ale_AlertaTipoReglaAdicionales",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoReglaAdicionales_Session",
                table: "ale_AlertaTipoReglaAdicionales",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipos___MigCode",
                table: "ale_AlertaTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipos___MigId",
                table: "ale_AlertaTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipos_Created_Modified",
                table: "ale_AlertaTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipos_CreatedBy_ModifiedBy",
                table: "ale_AlertaTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipos_Session",
                table: "ale_AlertaTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoUbicaciones___MigCode",
                table: "ale_AlertaTipoUbicaciones",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoUbicaciones___MigId",
                table: "ale_AlertaTipoUbicaciones",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoUbicaciones_Created_Modified",
                table: "ale_AlertaTipoUbicaciones",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoUbicaciones_CreatedBy_ModifiedBy",
                table: "ale_AlertaTipoUbicaciones",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_AlertaTipoUbicaciones_Session",
                table: "ale_AlertaTipoUbicaciones",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ale_DestinatarioVariables___MigCode",
                table: "ale_DestinatarioVariables",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ale_DestinatarioVariables___MigId",
                table: "ale_DestinatarioVariables",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ale_DestinatarioVariables_Created_Modified",
                table: "ale_DestinatarioVariables",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_DestinatarioVariables_CreatedBy_ModifiedBy",
                table: "ale_DestinatarioVariables",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ale_DestinatarioVariables_Session",
                table: "ale_DestinatarioVariables",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ali_Alicuotas___MigCode",
                table: "ali_Alicuotas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ali_Alicuotas___MigId",
                table: "ali_Alicuotas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ali_Alicuotas_CodigoAFIP",
                table: "ali_Alicuotas",
                column: "CodigoAFIP",
                unique: true,
                filter: "[CodigoAFIP] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ali_Alicuotas_Created_Modified",
                table: "ali_Alicuotas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ali_Alicuotas_CreatedBy_ModifiedBy",
                table: "ali_Alicuotas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ali_Alicuotas_Session",
                table: "ali_Alicuotas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_aud_Sessions___MigCode",
                table: "aud_Sessions",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_aud_Sessions___MigId",
                table: "aud_Sessions",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_aud_Sessions_Created_Modified",
                table: "aud_Sessions",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_aud_Sessions_CreatedBy_ModifiedBy",
                table: "aud_Sessions",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_aud_Sessions_Session",
                table: "aud_Sessions",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_aud_Sessions_SessionGuid",
                table: "aud_Sessions",
                column: "SessionGuid");

            migrationBuilder.CreateIndex(
                name: "IX_aud_Sessions_UserLogin",
                table: "aud_Sessions",
                column: "UserLogin");

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones___MigCode",
                table: "cer_Certificaciones",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones___MigId",
                table: "cer_Certificaciones",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones_CompanyId",
                table: "cer_Certificaciones",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones_Created_Modified",
                table: "cer_Certificaciones",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones_CreatedBy_ModifiedBy",
                table: "cer_Certificaciones",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones_Session",
                table: "cer_Certificaciones",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones_TipoEmpresaPortalId",
                table: "cer_Certificaciones",
                column: "TipoEmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones___MigCode",
                table: "cer_SolicitudCertificaciones",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones___MigId",
                table: "cer_SolicitudCertificaciones",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_CertificacionId",
                table: "cer_SolicitudCertificaciones",
                column: "CertificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_Created_Modified",
                table: "cer_SolicitudCertificaciones",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_CreatedBy_ModifiedBy",
                table: "cer_SolicitudCertificaciones",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_EstadoId",
                table: "cer_SolicitudCertificaciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_OrigenId",
                table: "cer_SolicitudCertificaciones",
                column: "OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_PropietarioActualId",
                table: "cer_SolicitudCertificaciones",
                column: "PropietarioActualId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_Session",
                table: "cer_SolicitudCertificaciones",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_SocioId",
                table: "cer_SolicitudCertificaciones",
                column: "SocioId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificacionEstados___MigCode",
                table: "cer_SolicitudCertificacionEstados",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificacionEstados___MigId",
                table: "cer_SolicitudCertificacionEstados",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificacionEstados_Created_Modified",
                table: "cer_SolicitudCertificacionEstados",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificacionEstados_CreatedBy_ModifiedBy",
                table: "cer_SolicitudCertificacionEstados",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificacionEstados_Session",
                table: "cer_SolicitudCertificacionEstados",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_cny_Company___MigCode",
                table: "cny_Company",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_cny_Company___MigId",
                table: "cny_Company",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_cny_Company_Created_Modified",
                table: "cny_Company",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_cny_Company_CreatedBy_ModifiedBy",
                table: "cny_Company",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_cny_Company_OrganizationId",
                table: "cny_Company",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_cny_Company_Session",
                table: "cny_Company",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_cny_CompanyCurrencies___MigCode",
                table: "cny_CompanyCurrencies",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_cny_CompanyCurrencies___MigId",
                table: "cny_CompanyCurrencies",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_cny_CompanyCurrencies_CompanyId",
                table: "cny_CompanyCurrencies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_cny_CompanyCurrencies_Created_Modified",
                table: "cny_CompanyCurrencies",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_cny_CompanyCurrencies_CreatedBy_ModifiedBy",
                table: "cny_CompanyCurrencies",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_cny_CompanyCurrencies_CurrencyId",
                table: "cny_CompanyCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_cny_CompanyCurrencies_OrganizationId",
                table: "cny_CompanyCurrencies",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_cny_CompanyCurrencies_Session",
                table: "cny_CompanyCurrencies",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_cny_Organization___MigCode",
                table: "cny_Organization",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_cny_Organization___MigId",
                table: "cny_Organization",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_cny_Organization_Created_Modified",
                table: "cny_Organization",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_cny_Organization_CreatedBy_ModifiedBy",
                table: "cny_Organization",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_cny_Organization_Session",
                table: "cny_Organization",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_CategoriaTipos___MigCode",
                table: "com_CategoriaTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_CategoriaTipos___MigId",
                table: "com_CategoriaTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_CategoriaTipos_Created_Modified",
                table: "com_CategoriaTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_CategoriaTipos_CreatedBy_ModifiedBy",
                table: "com_CategoriaTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_CategoriaTipos_Session",
                table: "com_CategoriaTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_CodigoAutorizacionTipos___MigCode",
                table: "com_CodigoAutorizacionTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_CodigoAutorizacionTipos___MigId",
                table: "com_CodigoAutorizacionTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_CodigoAutorizacionTipos_Created_Modified",
                table: "com_CodigoAutorizacionTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_CodigoAutorizacionTipos_CreatedBy_ModifiedBy",
                table: "com_CodigoAutorizacionTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_CodigoAutorizacionTipos_Session",
                table: "com_CodigoAutorizacionTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteDetalles___MigCode",
                table: "com_ComprobanteDetalles",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteDetalles___MigId",
                table: "com_ComprobanteDetalles",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteDetalles_ComprobanteId",
                table: "com_ComprobanteDetalles",
                column: "ComprobanteId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteDetalles_Created_Modified",
                table: "com_ComprobanteDetalles",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteDetalles_CreatedBy_ModifiedBy",
                table: "com_ComprobanteDetalles",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteDetalles_Session",
                table: "com_ComprobanteDetalles",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteDetalles_UnidadMedidaId",
                table: "com_ComprobanteDetalles",
                column: "UnidadMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteEstados___MigCode",
                table: "com_ComprobanteEstados",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteEstados___MigId",
                table: "com_ComprobanteEstados",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteEstados_Created_Modified",
                table: "com_ComprobanteEstados",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteEstados_CreatedBy_ModifiedBy",
                table: "com_ComprobanteEstados",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteEstados_Session",
                table: "com_ComprobanteEstados",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes___MigCode",
                table: "com_Comprobantes",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes___MigId",
                table: "com_Comprobantes",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_CategoriaTipoEmisorId",
                table: "com_Comprobantes",
                column: "CategoriaTipoEmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_CategoriaTipoReceptorId",
                table: "com_Comprobantes",
                column: "CategoriaTipoReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_CompanyId",
                table: "com_Comprobantes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_ComprobanteEstadoId",
                table: "com_Comprobantes",
                column: "ComprobanteEstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_ComprobanteTipoId",
                table: "com_Comprobantes",
                column: "ComprobanteTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_CondicionVentaId",
                table: "com_Comprobantes",
                column: "CondicionVentaId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_Created_Modified",
                table: "com_Comprobantes",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_CreatedBy_ModifiedBy",
                table: "com_Comprobantes",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_EmpresaId",
                table: "com_Comprobantes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_EstadoValidacionARCAId",
                table: "com_Comprobantes",
                column: "EstadoValidacionARCAId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_EstadoValidacionARCAQRId",
                table: "com_Comprobantes",
                column: "EstadoValidacionARCAQRId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_MonedaId",
                table: "com_Comprobantes",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_OrigenId",
                table: "com_Comprobantes",
                column: "OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_PeriodoId",
                table: "com_Comprobantes",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_PropietarioActualId",
                table: "com_Comprobantes",
                column: "PropietarioActualId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_Session",
                table: "com_Comprobantes",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_Comprobantes_TipoCodigoAutorizacionId",
                table: "com_Comprobantes",
                column: "TipoCodigoAutorizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteTipos___MigCode",
                table: "com_ComprobanteTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteTipos___MigId",
                table: "com_ComprobanteTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteTipos_Created_Modified",
                table: "com_ComprobanteTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteTipos_CreatedBy_ModifiedBy",
                table: "com_ComprobanteTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ComprobanteTipos_Session",
                table: "com_ComprobanteTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_ConceptosGastosTipos___MigCode",
                table: "com_ConceptosGastosTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_ConceptosGastosTipos___MigId",
                table: "com_ConceptosGastosTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ConceptosGastosTipos_CompanyId",
                table: "com_ConceptosGastosTipos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ConceptosGastosTipos_Created_Modified",
                table: "com_ConceptosGastosTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ConceptosGastosTipos_CreatedBy_ModifiedBy",
                table: "com_ConceptosGastosTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ConceptosGastosTipos_Session",
                table: "com_ConceptosGastosTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_CondicionVentas___MigCode",
                table: "com_CondicionVentas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_CondicionVentas___MigId",
                table: "com_CondicionVentas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_CondicionVentas_Created_Modified",
                table: "com_CondicionVentas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_CondicionVentas_CreatedBy_ModifiedBy",
                table: "com_CondicionVentas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_CondicionVentas_Session",
                table: "com_CondicionVentas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_EstadosValidacionARCA___MigCode",
                table: "com_EstadosValidacionARCA",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_EstadosValidacionARCA___MigId",
                table: "com_EstadosValidacionARCA",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_EstadosValidacionARCA_Created_Modified",
                table: "com_EstadosValidacionARCA",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_EstadosValidacionARCA_CreatedBy_ModifiedBy",
                table: "com_EstadosValidacionARCA",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_EstadosValidacionARCA_Session",
                table: "com_EstadosValidacionARCA",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoDetalles___MigCode",
                table: "com_ImpuestoDetalles",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoDetalles___MigId",
                table: "com_ImpuestoDetalles",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoDetalles_ComprobanteId",
                table: "com_ImpuestoDetalles",
                column: "ComprobanteId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoDetalles_Created_Modified",
                table: "com_ImpuestoDetalles",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoDetalles_CreatedBy_ModifiedBy",
                table: "com_ImpuestoDetalles",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoDetalles_ImpuestoId",
                table: "com_ImpuestoDetalles",
                column: "ImpuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoDetalles_Session",
                table: "com_ImpuestoDetalles",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_Impuestos___MigCode",
                table: "com_Impuestos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_Impuestos___MigId",
                table: "com_Impuestos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Impuestos_AlicuotaId",
                table: "com_Impuestos",
                column: "AlicuotaId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Impuestos_CompanyId",
                table: "com_Impuestos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Impuestos_Created_Modified",
                table: "com_Impuestos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_Impuestos_CreatedBy_ModifiedBy",
                table: "com_Impuestos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_Impuestos_ProvinciaId",
                table: "com_Impuestos",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Impuestos_Session",
                table: "com_Impuestos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_Impuestos_TipoId",
                table: "com_Impuestos",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoTipos___MigCode",
                table: "com_ImpuestoTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoTipos___MigId",
                table: "com_ImpuestoTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoTipos_Created_Modified",
                table: "com_ImpuestoTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoTipos_CreatedBy_ModifiedBy",
                table: "com_ImpuestoTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_ImpuestoTipos_Session",
                table: "com_ImpuestoTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_Origenes___MigCode",
                table: "com_Origenes",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_Origenes___MigId",
                table: "com_Origenes",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_Origenes_Created_Modified",
                table: "com_Origenes",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_Origenes_CreatedBy_ModifiedBy",
                table: "com_Origenes",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_Origenes_Session",
                table: "com_Origenes",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionCompanies___MigCode",
                table: "com_PercepcionCompanies",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionCompanies___MigId",
                table: "com_PercepcionCompanies",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionCompanies_CompanyId",
                table: "com_PercepcionCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionCompanies_Created_Modified",
                table: "com_PercepcionCompanies",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionCompanies_CreatedBy_ModifiedBy",
                table: "com_PercepcionCompanies",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionCompanies_PercepcionId1",
                table: "com_PercepcionCompanies",
                column: "PercepcionId1");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionCompanies_Session",
                table: "com_PercepcionCompanies",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionDetalles___MigCode",
                table: "com_PercepcionDetalles",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionDetalles___MigId",
                table: "com_PercepcionDetalles",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionDetalles_ComprobanteId",
                table: "com_PercepcionDetalles",
                column: "ComprobanteId");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionDetalles_Created_Modified",
                table: "com_PercepcionDetalles",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionDetalles_CreatedBy_ModifiedBy",
                table: "com_PercepcionDetalles",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionDetalles_PercepcionId",
                table: "com_PercepcionDetalles",
                column: "PercepcionId");

            migrationBuilder.CreateIndex(
                name: "IX_com_PercepcionDetalles_Session",
                table: "com_PercepcionDetalles",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_TipoEmpresasPortales___MigCode",
                table: "com_TipoEmpresasPortales",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_TipoEmpresasPortales___MigId",
                table: "com_TipoEmpresasPortales",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_TipoEmpresasPortales_Created_Modified",
                table: "com_TipoEmpresasPortales",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_TipoEmpresasPortales_CreatedBy_ModifiedBy",
                table: "com_TipoEmpresasPortales",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_TipoEmpresasPortales_Session",
                table: "com_TipoEmpresasPortales",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_com_UnidadMedidas___MigCode",
                table: "com_UnidadMedidas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_com_UnidadMedidas___MigId",
                table: "com_UnidadMedidas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_com_UnidadMedidas_Created_Modified",
                table: "com_UnidadMedidas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_com_UnidadMedidas_CreatedBy_ModifiedBy",
                table: "com_UnidadMedidas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_com_UnidadMedidas_Session",
                table: "com_UnidadMedidas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentoEstados___MigCode",
                table: "doc_DocumentoEstados",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentoEstados___MigId",
                table: "doc_DocumentoEstados",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentoEstados_Created_Modified",
                table: "doc_DocumentoEstados",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentoEstados_CreatedBy_ModifiedBy",
                table: "doc_DocumentoEstados",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentoEstados_Session",
                table: "doc_DocumentoEstados",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosCargados___MigCode",
                table: "doc_DocumentosCargados",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosCargados___MigId",
                table: "doc_DocumentosCargados",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosCargados_Created_Modified",
                table: "doc_DocumentosCargados",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosCargados_CreatedBy_ModifiedBy",
                table: "doc_DocumentosCargados",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosCargados_DocumentoRequeridoId",
                table: "doc_DocumentosCargados",
                column: "DocumentoRequeridoId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosCargados_EstadoId",
                table: "doc_DocumentosCargados",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosCargados_Session",
                table: "doc_DocumentosCargados",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosCargados_SolicitudId",
                table: "doc_DocumentosCargados",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosCargados_ValidadoPorId",
                table: "doc_DocumentosCargados",
                column: "ValidadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosRequeridos___MigCode",
                table: "doc_DocumentosRequeridos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosRequeridos___MigId",
                table: "doc_DocumentosRequeridos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosRequeridos_CertificacionId",
                table: "doc_DocumentosRequeridos",
                column: "CertificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosRequeridos_Created_Modified",
                table: "doc_DocumentosRequeridos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosRequeridos_CreatedBy_ModifiedBy",
                table: "doc_DocumentosRequeridos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosRequeridos_Session",
                table: "doc_DocumentosRequeridos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_doc_DocumentosRequeridos_TipoId",
                table: "doc_DocumentosRequeridos",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_TipoDocumentos___MigCode",
                table: "doc_TipoDocumentos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_doc_TipoDocumentos___MigId",
                table: "doc_TipoDocumentos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_doc_TipoDocumentos_Created_Modified",
                table: "doc_TipoDocumentos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_doc_TipoDocumentos_CreatedBy_ModifiedBy",
                table: "doc_TipoDocumentos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_doc_TipoDocumentos_Session",
                table: "doc_TipoDocumentos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras___MigCode",
                table: "emp_CompanyExtras",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras___MigId",
                table: "emp_CompanyExtras",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras_CiudadId",
                table: "emp_CompanyExtras",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras_Created_Modified",
                table: "emp_CompanyExtras",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras_CreatedBy_ModifiedBy",
                table: "emp_CompanyExtras",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras_OrganizationId",
                table: "emp_CompanyExtras",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras_PaisId",
                table: "emp_CompanyExtras",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras_ProvinciaId",
                table: "emp_CompanyExtras",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras_Session",
                table: "emp_CompanyExtras",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras_TipoCuentaId",
                table: "emp_CompanyExtras",
                column: "TipoCuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_CompanyExtras_TipoResponsableId",
                table: "emp_CompanyExtras",
                column: "TipoResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaCurrency___MigCode",
                table: "emp_EmpresaCurrency",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaCurrency___MigId",
                table: "emp_EmpresaCurrency",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaCurrency_Created_Modified",
                table: "emp_EmpresaCurrency",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaCurrency_CreatedBy_ModifiedBy",
                table: "emp_EmpresaCurrency",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaCurrency_CurrencyId",
                table: "emp_EmpresaCurrency",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaCurrency_EmpresaPortalId",
                table: "emp_EmpresaCurrency",
                column: "EmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaCurrency_Session",
                table: "emp_EmpresaCurrency",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaRoles___MigCode",
                table: "emp_EmpresaRoles",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaRoles___MigId",
                table: "emp_EmpresaRoles",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaRoles_Created_Modified",
                table: "emp_EmpresaRoles",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaRoles_CreatedBy_ModifiedBy",
                table: "emp_EmpresaRoles",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaRoles_EmpresaPortalId",
                table: "emp_EmpresaRoles",
                column: "EmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaRoles_RolTipoId",
                table: "emp_EmpresaRoles",
                column: "RolTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresaRoles_Session",
                table: "emp_EmpresaRoles",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales___MigCode",
                table: "emp_EmpresasPortales",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales___MigId",
                table: "emp_EmpresasPortales",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_CiudadId",
                table: "emp_EmpresasPortales",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_CompanyId",
                table: "emp_EmpresasPortales",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_Created_Modified",
                table: "emp_EmpresasPortales",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_CreatedBy_ModifiedBy",
                table: "emp_EmpresasPortales",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_OrganizationId",
                table: "emp_EmpresasPortales",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_PaisId",
                table: "emp_EmpresasPortales",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_ProvinciaId",
                table: "emp_EmpresasPortales",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_Session",
                table: "emp_EmpresasPortales",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_TipoCuentaId",
                table: "emp_EmpresasPortales",
                column: "TipoCuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_TipoId",
                table: "emp_EmpresasPortales",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_TipoResponsableId",
                table: "emp_EmpresasPortales",
                column: "TipoResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesDocsComprasTipos___MigCode",
                table: "emp_EmpresasPortalesDocsComprasTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesDocsComprasTipos___MigId",
                table: "emp_EmpresasPortalesDocsComprasTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesDocsComprasTipos_Created_Modified",
                table: "emp_EmpresasPortalesDocsComprasTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesDocsComprasTipos_CreatedBy_ModifiedBy",
                table: "emp_EmpresasPortalesDocsComprasTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesDocsComprasTipos_EmpresaPortalId",
                table: "emp_EmpresasPortalesDocsComprasTipos",
                column: "EmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesDocsComprasTipos_OrdenCompraTipoId",
                table: "emp_EmpresasPortalesDocsComprasTipos",
                column: "OrdenCompraTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesDocsComprasTipos_Session",
                table: "emp_EmpresasPortalesDocsComprasTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesGastosTipos___MigCode",
                table: "emp_EmpresasPortalesGastosTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesGastosTipos___MigId",
                table: "emp_EmpresasPortalesGastosTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesGastosTipos_ConceptoGastoTipoId",
                table: "emp_EmpresasPortalesGastosTipos",
                column: "ConceptoGastoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesGastosTipos_Created_Modified",
                table: "emp_EmpresasPortalesGastosTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesGastosTipos_CreatedBy_ModifiedBy",
                table: "emp_EmpresasPortalesGastosTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesGastosTipos_EmpresaPortalId",
                table: "emp_EmpresasPortalesGastosTipos",
                column: "EmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortalesGastosTipos_Session",
                table: "emp_EmpresasPortalesGastosTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_emp_GruposDocsComprasTipos___MigCode",
                table: "emp_GruposDocsComprasTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_emp_GruposDocsComprasTipos___MigId",
                table: "emp_GruposDocsComprasTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_GruposDocsComprasTipos_Created_Modified",
                table: "emp_GruposDocsComprasTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_GruposDocsComprasTipos_CreatedBy_ModifiedBy",
                table: "emp_GruposDocsComprasTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_GruposDocsComprasTipos_GrupoId",
                table: "emp_GruposDocsComprasTipos",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_GruposDocsComprasTipos_OrdenCompraTipoId",
                table: "emp_GruposDocsComprasTipos",
                column: "OrdenCompraTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_GruposDocsComprasTipos_Session",
                table: "emp_GruposDocsComprasTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_emp_RolTipos___MigCode",
                table: "emp_RolTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_emp_RolTipos___MigId",
                table: "emp_RolTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_RolTipos_Created_Modified",
                table: "emp_RolTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_RolTipos_CreatedBy_ModifiedBy",
                table: "emp_RolTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_RolTipos_Session",
                table: "emp_RolTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_emp_TipoCuentas___MigCode",
                table: "emp_TipoCuentas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_emp_TipoCuentas___MigId",
                table: "emp_TipoCuentas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_emp_TipoCuentas_Created_Modified",
                table: "emp_TipoCuentas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_TipoCuentas_CreatedBy_ModifiedBy",
                table: "emp_TipoCuentas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_emp_TipoCuentas_Session",
                table: "emp_TipoCuentas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasAlicuotas_AlicuotaIdm",
                table: "EmpresasAlicuotas",
                column: "AlicuotaIdm");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasAlicuotas_EmpresaPortalId",
                table: "EmpresasAlicuotas",
                column: "EmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasModosLecturas_EmpresaPortalId",
                table: "EmpresasModosLecturas",
                column: "EmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasModosLecturas_ModoLecturaIdm",
                table: "EmpresasModosLecturas",
                column: "ModoLecturaIdm");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Cultures___MigCode",
                table: "gbl_Cultures",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Cultures___MigId",
                table: "gbl_Cultures",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Cultures_Created_Modified",
                table: "gbl_Cultures",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Cultures_CreatedBy_ModifiedBy",
                table: "gbl_Cultures",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Cultures_Session",
                table: "gbl_Cultures",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_CulturesOrganization___MigCode",
                table: "gbl_CulturesOrganization",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_CulturesOrganization___MigId",
                table: "gbl_CulturesOrganization",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_CulturesOrganization_Created_Modified",
                table: "gbl_CulturesOrganization",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_CulturesOrganization_CreatedBy_ModifiedBy",
                table: "gbl_CulturesOrganization",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_CulturesOrganization_CultureId",
                table: "gbl_CulturesOrganization",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_CulturesOrganization_OrganizationId",
                table: "gbl_CulturesOrganization",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_CulturesOrganization_Session",
                table: "gbl_CulturesOrganization",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Currencies___MigCode",
                table: "gbl_Currencies",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Currencies___MigId",
                table: "gbl_Currencies",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Currencies_Created_Modified",
                table: "gbl_Currencies",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Currencies_CreatedBy_ModifiedBy",
                table: "gbl_Currencies",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_Currencies_Session",
                table: "gbl_Currencies",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTax___MigCode",
                table: "gbl_IdentificationTax",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTax___MigId",
                table: "gbl_IdentificationTax",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTax_CountryId",
                table: "gbl_IdentificationTax",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTax_Created_Modified",
                table: "gbl_IdentificationTax",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTax_CreatedBy_ModifiedBy",
                table: "gbl_IdentificationTax",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTax_Session",
                table: "gbl_IdentificationTax",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTypes___MigCode",
                table: "gbl_IdentificationTypes",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTypes___MigId",
                table: "gbl_IdentificationTypes",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTypes_Created_Modified",
                table: "gbl_IdentificationTypes",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTypes_CreatedBy_ModifiedBy",
                table: "gbl_IdentificationTypes",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_gbl_IdentificationTypes_Session",
                table: "gbl_IdentificationTypes",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Cities___MigCode",
                table: "geo_Cities",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Cities___MigId",
                table: "geo_Cities",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Cities_Created_Modified",
                table: "geo_Cities",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_geo_Cities_CreatedBy_ModifiedBy",
                table: "geo_Cities",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_geo_Cities_ProvinceId",
                table: "geo_Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Cities_Session",
                table: "geo_Cities",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Countries___MigCode",
                table: "geo_Countries",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Countries___MigId",
                table: "geo_Countries",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Countries_Created_Modified",
                table: "geo_Countries",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_geo_Countries_CreatedBy_ModifiedBy",
                table: "geo_Countries",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_geo_Countries_Session",
                table: "geo_Countries",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Provinces___MigCode",
                table: "geo_Provinces",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Provinces___MigId",
                table: "geo_Provinces",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Provinces_CountryIdm",
                table: "geo_Provinces",
                column: "CountryIdm");

            migrationBuilder.CreateIndex(
                name: "IX_geo_Provinces_Created_Modified",
                table: "geo_Provinces",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_geo_Provinces_CreatedBy_ModifiedBy",
                table: "geo_Provinces",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_geo_Provinces_Session",
                table: "geo_Provinces",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_int_Interfaces___MigCode",
                table: "int_Interfaces",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_int_Interfaces___MigId",
                table: "int_Interfaces",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_int_Interfaces_Created_Modified",
                table: "int_Interfaces",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_int_Interfaces_CreatedBy_ModifiedBy",
                table: "int_Interfaces",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_int_Interfaces_Session",
                table: "int_Interfaces",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_int_Interfaces_SistemaIdm",
                table: "int_Interfaces",
                column: "SistemaIdm");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazCampos___MigCode",
                table: "int_InterfazCampos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazCampos___MigId",
                table: "int_InterfazCampos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazCampos_Created_Modified",
                table: "int_InterfazCampos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazCampos_CreatedBy_ModifiedBy",
                table: "int_InterfazCampos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazCampos_InterfazIdm",
                table: "int_InterfazCampos",
                column: "InterfazIdm");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazCampos_Session",
                table: "int_InterfazCampos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazEstados___MigCode",
                table: "int_InterfazEstados",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazEstados___MigId",
                table: "int_InterfazEstados",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazEstados_Created_Modified",
                table: "int_InterfazEstados",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazEstados_CreatedBy_ModifiedBy",
                table: "int_InterfazEstados",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazEstados_InterfazIdm",
                table: "int_InterfazEstados",
                column: "InterfazIdm");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazEstados_Session",
                table: "int_InterfazEstados",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoReglaEnforzadas___MigCode",
                table: "int_InterfazProcesoReglaEnforzadas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoReglaEnforzadas___MigId",
                table: "int_InterfazProcesoReglaEnforzadas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoReglaEnforzadas_Created_Modified",
                table: "int_InterfazProcesoReglaEnforzadas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoReglaEnforzadas_CreatedBy_ModifiedBy",
                table: "int_InterfazProcesoReglaEnforzadas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoReglaEnforzadas_InterfazCampoIdm",
                table: "int_InterfazProcesoReglaEnforzadas",
                column: "InterfazCampoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoReglaEnforzadas_InterfazProcesoId",
                table: "int_InterfazProcesoReglaEnforzadas",
                column: "InterfazProcesoId");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoReglaEnforzadas_InterfazReglaIdm",
                table: "int_InterfazProcesoReglaEnforzadas",
                column: "InterfazReglaIdm");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoReglaEnforzadas_Session",
                table: "int_InterfazProcesoReglaEnforzadas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesos___MigCode",
                table: "int_InterfazProcesos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesos___MigId",
                table: "int_InterfazProcesos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesos_Created_Modified",
                table: "int_InterfazProcesos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesos_CreatedBy_ModifiedBy",
                table: "int_InterfazProcesos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesos_EstadoIdm",
                table: "int_InterfazProcesos",
                column: "EstadoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesos_InterfazIdm",
                table: "int_InterfazProcesos",
                column: "InterfazIdm");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesos_Session",
                table: "int_InterfazProcesos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoValidacionAdicionales___MigCode",
                table: "int_InterfazProcesoValidacionAdicionales",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoValidacionAdicionales___MigId",
                table: "int_InterfazProcesoValidacionAdicionales",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoValidacionAdicionales_Created_Modified",
                table: "int_InterfazProcesoValidacionAdicionales",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoValidacionAdicionales_CreatedBy_ModifiedBy",
                table: "int_InterfazProcesoValidacionAdicionales",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoValidacionAdicionales_InterfazProcesoReglaEnforzadaId",
                table: "int_InterfazProcesoValidacionAdicionales",
                column: "InterfazProcesoReglaEnforzadaId");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazProcesoValidacionAdicionales_Session",
                table: "int_InterfazProcesoValidacionAdicionales",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglaConsecuencias___MigCode",
                table: "int_InterfazReglaConsecuencias",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglaConsecuencias___MigId",
                table: "int_InterfazReglaConsecuencias",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglaConsecuencias_Created_Modified",
                table: "int_InterfazReglaConsecuencias",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglaConsecuencias_CreatedBy_ModifiedBy",
                table: "int_InterfazReglaConsecuencias",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglaConsecuencias_Session",
                table: "int_InterfazReglaConsecuencias",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglas___MigCode",
                table: "int_InterfazReglas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglas___MigId",
                table: "int_InterfazReglas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglas_Created_Modified",
                table: "int_InterfazReglas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglas_CreatedBy_ModifiedBy",
                table: "int_InterfazReglas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglas_InterfazIdm",
                table: "int_InterfazReglas",
                column: "InterfazIdm");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglas_InterfazReglaConsecuenciaIdm",
                table: "int_InterfazReglas",
                column: "InterfazReglaConsecuenciaIdm");

            migrationBuilder.CreateIndex(
                name: "IX_int_InterfazReglas_Session",
                table: "int_InterfazReglas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_int_TipoOrdenC___MigCode",
                table: "int_TipoOrdenC",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_int_TipoOrdenC___MigId",
                table: "int_TipoOrdenC",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_int_TipoOrdenC_Created_Modified",
                table: "int_TipoOrdenC",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_int_TipoOrdenC_CreatedBy_ModifiedBy",
                table: "int_TipoOrdenC",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_int_TipoOrdenC_Session",
                table: "int_TipoOrdenC",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_mlr_ModosLecturas___MigCode",
                table: "mlr_ModosLecturas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_mlr_ModosLecturas___MigId",
                table: "mlr_ModosLecturas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_mlr_ModosLecturas_Created_Modified",
                table: "mlr_ModosLecturas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_mlr_ModosLecturas_CreatedBy_ModifiedBy",
                table: "mlr_ModosLecturas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_mlr_ModosLecturas_Session",
                table: "mlr_ModosLecturas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_ComprobanteEMailExtensiones___MigCode",
                table: "mpc_ComprobanteEMailExtensiones",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_ComprobanteEMailExtensiones___MigId",
                table: "mpc_ComprobanteEMailExtensiones",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_ComprobanteEMailExtensiones_Created_Modified",
                table: "mpc_ComprobanteEMailExtensiones",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_mpc_ComprobanteEMailExtensiones_CreatedBy_ModifiedBy",
                table: "mpc_ComprobanteEMailExtensiones",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_mpc_ComprobanteEMailExtensiones_Session",
                table: "mpc_ComprobanteEMailExtensiones",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionesFacturaPorCorreo___MigCode",
                table: "mpc_IntegracionesFacturaPorCorreo",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionesFacturaPorCorreo___MigId",
                table: "mpc_IntegracionesFacturaPorCorreo",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionesFacturaPorCorreo_CompanyId",
                table: "mpc_IntegracionesFacturaPorCorreo",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionesFacturaPorCorreo_Created_Modified",
                table: "mpc_IntegracionesFacturaPorCorreo",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionesFacturaPorCorreo_CreatedBy_ModifiedBy",
                table: "mpc_IntegracionesFacturaPorCorreo",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionesFacturaPorCorreo_Session",
                table: "mpc_IntegracionesFacturaPorCorreo",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionFacturaPorCorreoDetalles___MigCode",
                table: "mpc_IntegracionFacturaPorCorreoDetalles",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionFacturaPorCorreoDetalles___MigId",
                table: "mpc_IntegracionFacturaPorCorreoDetalles",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionFacturaPorCorreoDetalles_Created_Modified",
                table: "mpc_IntegracionFacturaPorCorreoDetalles",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionFacturaPorCorreoDetalles_CreatedBy_ModifiedBy",
                table: "mpc_IntegracionFacturaPorCorreoDetalles",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionFacturaPorCorreoDetalles_EmpresaPortalId",
                table: "mpc_IntegracionFacturaPorCorreoDetalles",
                column: "EmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionFacturaPorCorreoDetalles_IntegracionFacturaPorCorreoId",
                table: "mpc_IntegracionFacturaPorCorreoDetalles",
                column: "IntegracionFacturaPorCorreoId");

            migrationBuilder.CreateIndex(
                name: "IX_mpc_IntegracionFacturaPorCorreoDetalles_Session",
                table: "mpc_IntegracionFacturaPorCorreoDetalles",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_ConfiguracionNotificaciones___MigCode",
                table: "ntf_ConfiguracionNotificaciones",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_ConfiguracionNotificaciones___MigId",
                table: "ntf_ConfiguracionNotificaciones",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_ConfiguracionNotificaciones_Created_Modified",
                table: "ntf_ConfiguracionNotificaciones",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_ConfiguracionNotificaciones_CreatedBy_ModifiedBy",
                table: "ntf_ConfiguracionNotificaciones",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_ConfiguracionNotificaciones_DomainFIdm",
                table: "ntf_ConfiguracionNotificaciones",
                column: "DomainFIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_ConfiguracionNotificaciones_Session",
                table: "ntf_ConfiguracionNotificaciones",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_ConfiguracionNotificaciones_TipoNotificacionIdm",
                table: "ntf_ConfiguracionNotificaciones",
                column: "TipoNotificacionIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_EventoEstados___MigCode",
                table: "ntf_EventoEstados",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_EventoEstados___MigId",
                table: "ntf_EventoEstados",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_EventoEstados_Created_Modified",
                table: "ntf_EventoEstados",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_EventoEstados_CreatedBy_ModifiedBy",
                table: "ntf_EventoEstados",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_EventoEstados_Session",
                table: "ntf_EventoEstados",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Eventos___MigCode",
                table: "ntf_Eventos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Eventos___MigId",
                table: "ntf_Eventos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Eventos_Created_Modified",
                table: "ntf_Eventos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Eventos_CreatedBy_ModifiedBy",
                table: "ntf_Eventos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Eventos_EstadoIdm",
                table: "ntf_Eventos",
                column: "EstadoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Eventos_NotificacionId",
                table: "ntf_Eventos",
                column: "NotificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Eventos_Session",
                table: "ntf_Eventos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionAdjuntos___MigCode",
                table: "ntf_NotificacionAdjuntos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionAdjuntos___MigId",
                table: "ntf_NotificacionAdjuntos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionAdjuntos_Created_Modified",
                table: "ntf_NotificacionAdjuntos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionAdjuntos_CreatedBy_ModifiedBy",
                table: "ntf_NotificacionAdjuntos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionAdjuntos_NotificacionId",
                table: "ntf_NotificacionAdjuntos",
                column: "NotificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionAdjuntos_Session",
                table: "ntf_NotificacionAdjuntos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notificaciones___MigCode",
                table: "ntf_Notificaciones",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notificaciones___MigId",
                table: "ntf_Notificaciones",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notificaciones_CompanyId",
                table: "ntf_Notificaciones",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notificaciones_ConfiguracionNotificacionId",
                table: "ntf_Notificaciones",
                column: "ConfiguracionNotificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notificaciones_Created_Modified",
                table: "ntf_Notificaciones",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notificaciones_CreatedBy_ModifiedBy",
                table: "ntf_Notificaciones",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notificaciones_EstadoIdm",
                table: "ntf_Notificaciones",
                column: "EstadoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notificaciones_Session",
                table: "ntf_Notificaciones",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEstados___MigCode",
                table: "ntf_NotificacionEstados",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEstados___MigId",
                table: "ntf_NotificacionEstados",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEstados_Created_Modified",
                table: "ntf_NotificacionEstados",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEstados_CreatedBy_ModifiedBy",
                table: "ntf_NotificacionEstados",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEstados_Session",
                table: "ntf_NotificacionEstados",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetas___MigCode",
                table: "ntf_NotificacionEtiquetas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetas___MigId",
                table: "ntf_NotificacionEtiquetas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetas_Created_Modified",
                table: "ntf_NotificacionEtiquetas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetas_CreatedBy_ModifiedBy",
                table: "ntf_NotificacionEtiquetas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetas_NotificacionEtiquetaTipoIdm",
                table: "ntf_NotificacionEtiquetas",
                column: "NotificacionEtiquetaTipoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetas_NotificacionId",
                table: "ntf_NotificacionEtiquetas",
                column: "NotificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetas_Session",
                table: "ntf_NotificacionEtiquetas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetaTipos___MigCode",
                table: "ntf_NotificacionEtiquetaTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetaTipos___MigId",
                table: "ntf_NotificacionEtiquetaTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetaTipos_Created_Modified",
                table: "ntf_NotificacionEtiquetaTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetaTipos_CreatedBy_ModifiedBy",
                table: "ntf_NotificacionEtiquetaTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificacionEtiquetaTipos_Session",
                table: "ntf_NotificacionEtiquetaTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notifications___MigCode",
                table: "ntf_Notifications",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notifications___MigId",
                table: "ntf_Notifications",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notifications_CompanyId",
                table: "ntf_Notifications",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notifications_Created_Modified",
                table: "ntf_Notifications",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notifications_CreatedBy_ModifiedBy",
                table: "ntf_Notifications",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notifications_NotificationTypeId",
                table: "ntf_Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notifications_OrganizationId",
                table: "ntf_Notifications",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notifications_Session",
                table: "ntf_Notifications",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Notifications_UserId",
                table: "ntf_Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypes___MigCode",
                table: "ntf_NotificationTypes",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypes___MigId",
                table: "ntf_NotificationTypes",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypes_Created_Modified",
                table: "ntf_NotificationTypes",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypes_CreatedBy_ModifiedBy",
                table: "ntf_NotificationTypes",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypes_Session",
                table: "ntf_NotificationTypes",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizations___MigCode",
                table: "ntf_NotificationTypesOrganizations",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizations___MigId",
                table: "ntf_NotificationTypesOrganizations",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizations_Created_Modified",
                table: "ntf_NotificationTypesOrganizations",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizations_CreatedBy_ModifiedBy",
                table: "ntf_NotificationTypesOrganizations",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizations_NotificationTypeId",
                table: "ntf_NotificationTypesOrganizations",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizations_OrganizationId",
                table: "ntf_NotificationTypesOrganizations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizations_Session",
                table: "ntf_NotificationTypesOrganizations",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizationsGroups___MigCode",
                table: "ntf_NotificationTypesOrganizationsGroups",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizationsGroups___MigId",
                table: "ntf_NotificationTypesOrganizationsGroups",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizationsGroups_Created_Modified",
                table: "ntf_NotificationTypesOrganizationsGroups",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizationsGroups_CreatedBy_ModifiedBy",
                table: "ntf_NotificationTypesOrganizationsGroups",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizationsGroups_GroupId",
                table: "ntf_NotificationTypesOrganizationsGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizationsGroups_NotificationTypeOrganizationId",
                table: "ntf_NotificationTypesOrganizationsGroups",
                column: "NotificationTypeOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_NotificationTypesOrganizationsGroups_Session",
                table: "ntf_NotificationTypesOrganizationsGroups",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Subscriptions___MigCode",
                table: "ntf_Subscriptions",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Subscriptions___MigId",
                table: "ntf_Subscriptions",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Subscriptions_Created_Modified",
                table: "ntf_Subscriptions",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Subscriptions_CreatedBy_ModifiedBy",
                table: "ntf_Subscriptions",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Subscriptions_Session",
                table: "ntf_Subscriptions",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_Subscriptions_UserId",
                table: "ntf_Subscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_SubscriptionsKeyValues___MigCode",
                table: "ntf_SubscriptionsKeyValues",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_SubscriptionsKeyValues___MigId",
                table: "ntf_SubscriptionsKeyValues",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_SubscriptionsKeyValues_Created_Modified",
                table: "ntf_SubscriptionsKeyValues",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_SubscriptionsKeyValues_CreatedBy_ModifiedBy",
                table: "ntf_SubscriptionsKeyValues",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_SubscriptionsKeyValues_Session",
                table: "ntf_SubscriptionsKeyValues",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_TipoNotificaciones___MigCode",
                table: "ntf_TipoNotificaciones",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_TipoNotificaciones___MigId",
                table: "ntf_TipoNotificaciones",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_ntf_TipoNotificaciones_Created_Modified",
                table: "ntf_TipoNotificaciones",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_TipoNotificaciones_CreatedBy_ModifiedBy",
                table: "ntf_TipoNotificaciones",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_ntf_TipoNotificaciones_Session",
                table: "ntf_TipoNotificaciones",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras___MigCode",
                table: "orc_OrdenesCompras",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras___MigId",
                table: "orc_OrdenesCompras",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras_CompanyId",
                table: "orc_OrdenesCompras",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras_Created_Modified",
                table: "orc_OrdenesCompras",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras_CreatedBy_ModifiedBy",
                table: "orc_OrdenesCompras",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras_EmpresaPortalId",
                table: "orc_OrdenesCompras",
                column: "EmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras_MonedaId",
                table: "orc_OrdenesCompras",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras_OrdenCompraEstadoIdm1",
                table: "orc_OrdenesCompras",
                column: "OrdenCompraEstadoIdm1");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras_OrdenCompraTipoId",
                table: "orc_OrdenesCompras",
                column: "OrdenCompraTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesCompras_Session",
                table: "orc_OrdenesCompras",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasEstados___MigCode",
                table: "orc_OrdenesComprasEstados",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasEstados___MigId",
                table: "orc_OrdenesComprasEstados",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasEstados_Created_Modified",
                table: "orc_OrdenesComprasEstados",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasEstados_CreatedBy_ModifiedBy",
                table: "orc_OrdenesComprasEstados",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasEstados_Session",
                table: "orc_OrdenesComprasEstados",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasTipos___MigCode",
                table: "orc_OrdenesComprasTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasTipos___MigId",
                table: "orc_OrdenesComprasTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasTipos_CompanyId",
                table: "orc_OrdenesComprasTipos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasTipos_Created_Modified",
                table: "orc_OrdenesComprasTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasTipos_CreatedBy_ModifiedBy",
                table: "orc_OrdenesComprasTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_orc_OrdenesComprasTipos_Session",
                table: "orc_OrdenesComprasTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_par_Parametros___MigCode",
                table: "par_Parametros",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_par_Parametros___MigId",
                table: "par_Parametros",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_par_Parametros_Created_Modified",
                table: "par_Parametros",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_par_Parametros_CreatedBy_ModifiedBy",
                table: "par_Parametros",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_par_Parametros_Session",
                table: "par_Parametros",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_per_Percepciones___MigCode",
                table: "per_Percepciones",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_per_Percepciones___MigId",
                table: "per_Percepciones",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_per_Percepciones_CompanyId",
                table: "per_Percepciones",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_per_Percepciones_Created_Modified",
                table: "per_Percepciones",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_per_Percepciones_CreatedBy_ModifiedBy",
                table: "per_Percepciones",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_per_Percepciones_PercepcionTipoId",
                table: "per_Percepciones",
                column: "PercepcionTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_per_Percepciones_ProvinciaId",
                table: "per_Percepciones",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_per_Percepciones_Session",
                table: "per_Percepciones",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_per_PercepcionTipos___MigCode",
                table: "per_PercepcionTipos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_per_PercepcionTipos___MigId",
                table: "per_PercepcionTipos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_per_PercepcionTipos_Created_Modified",
                table: "per_PercepcionTipos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_per_PercepcionTipos_CreatedBy_ModifiedBy",
                table: "per_PercepcionTipos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_per_PercepcionTipos_Session",
                table: "per_PercepcionTipos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_prd_EstadoPeriodos___MigCode",
                table: "prd_EstadoPeriodos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_prd_EstadoPeriodos___MigId",
                table: "prd_EstadoPeriodos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_prd_EstadoPeriodos_Created_Modified",
                table: "prd_EstadoPeriodos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_prd_EstadoPeriodos_CreatedBy_ModifiedBy",
                table: "prd_EstadoPeriodos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_prd_EstadoPeriodos_Session",
                table: "prd_EstadoPeriodos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_prd_Periodos___MigCode",
                table: "prd_Periodos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_prd_Periodos___MigId",
                table: "prd_Periodos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_prd_Periodos_CompanyId",
                table: "prd_Periodos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_prd_Periodos_Created_Modified",
                table: "prd_Periodos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_prd_Periodos_CreatedBy_ModifiedBy",
                table: "prd_Periodos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_prd_Periodos_EstadoIdm",
                table: "prd_Periodos",
                column: "EstadoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_prd_Periodos_Session",
                table: "prd_Periodos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_pro_EstadoProcesos___MigCode",
                table: "pro_EstadoProcesos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_pro_EstadoProcesos___MigId",
                table: "pro_EstadoProcesos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_pro_EstadoProcesos_Created_Modified",
                table: "pro_EstadoProcesos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_pro_EstadoProcesos_CreatedBy_ModifiedBy",
                table: "pro_EstadoProcesos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_pro_EstadoProcesos_Session",
                table: "pro_EstadoProcesos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_pro_Procesos___MigCode",
                table: "pro_Procesos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_pro_Procesos___MigId",
                table: "pro_Procesos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_pro_Procesos_Created_Modified",
                table: "pro_Procesos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_pro_Procesos_CreatedBy_ModifiedBy",
                table: "pro_Procesos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_pro_Procesos_DominioId",
                table: "pro_Procesos",
                column: "DominioId");

            migrationBuilder.CreateIndex(
                name: "IX_pro_Procesos_EstadoIdm",
                table: "pro_Procesos",
                column: "EstadoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_pro_Procesos_Session",
                table: "pro_Procesos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_pro_Procesos_TipoIdm",
                table: "pro_Procesos",
                column: "TipoIdm");

            migrationBuilder.CreateIndex(
                name: "IX_pro_TipoProcesos___MigCode",
                table: "pro_TipoProcesos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_pro_TipoProcesos___MigId",
                table: "pro_TipoProcesos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_pro_TipoProcesos_Created_Modified",
                table: "pro_TipoProcesos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_pro_TipoProcesos_CreatedBy_ModifiedBy",
                table: "pro_TipoProcesos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_pro_TipoProcesos_Session",
                table: "pro_TipoProcesos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportScheduleTasks___MigCode",
                table: "rep_ReportScheduleTasks",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportScheduleTasks___MigId",
                table: "rep_ReportScheduleTasks",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportScheduleTasks_Created_Modified",
                table: "rep_ReportScheduleTasks",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportScheduleTasks_CreatedBy_ModifiedBy",
                table: "rep_ReportScheduleTasks",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportScheduleTasks_ReportTypeId",
                table: "rep_ReportScheduleTasks",
                column: "ReportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportScheduleTasks_Session",
                table: "rep_ReportScheduleTasks",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportScheduleTasks_TaskIntervalTypeIdm",
                table: "rep_ReportScheduleTasks",
                column: "TaskIntervalTypeIdm");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeColumns___MigCode",
                table: "rep_ReportTypeColumns",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeColumns___MigId",
                table: "rep_ReportTypeColumns",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeColumns_Created_Modified",
                table: "rep_ReportTypeColumns",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeColumns_CreatedBy_ModifiedBy",
                table: "rep_ReportTypeColumns",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeColumns_ReportTypeId",
                table: "rep_ReportTypeColumns",
                column: "ReportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeColumns_Session",
                table: "rep_ReportTypeColumns",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParameters___MigCode",
                table: "rep_ReportTypeParameters",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParameters___MigId",
                table: "rep_ReportTypeParameters",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParameters_Created_Modified",
                table: "rep_ReportTypeParameters",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParameters_CreatedBy_ModifiedBy",
                table: "rep_ReportTypeParameters",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParameters_ReportTypeId",
                table: "rep_ReportTypeParameters",
                column: "ReportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParameters_Session",
                table: "rep_ReportTypeParameters",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParametersValues___MigCode",
                table: "rep_ReportTypeParametersValues",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParametersValues___MigId",
                table: "rep_ReportTypeParametersValues",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParametersValues_Created_Modified",
                table: "rep_ReportTypeParametersValues",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParametersValues_CreatedBy_ModifiedBy",
                table: "rep_ReportTypeParametersValues",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParametersValues_ReportScheduleTaskId",
                table: "rep_ReportTypeParametersValues",
                column: "ReportScheduleTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParametersValues_ReportTypeParameterId",
                table: "rep_ReportTypeParametersValues",
                column: "ReportTypeParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypeParametersValues_Session",
                table: "rep_ReportTypeParametersValues",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypes___MigCode",
                table: "rep_ReportTypes",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypes___MigId",
                table: "rep_ReportTypes",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypes_Created_Modified",
                table: "rep_ReportTypes",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypes_CreatedBy_ModifiedBy",
                table: "rep_ReportTypes",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypes_OptionId",
                table: "rep_ReportTypes",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_ReportTypes_Session",
                table: "rep_ReportTypes",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_rep_TaskInvervalsTypes___MigCode",
                table: "rep_TaskInvervalsTypes",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_rep_TaskInvervalsTypes___MigId",
                table: "rep_TaskInvervalsTypes",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_rep_TaskInvervalsTypes_Created_Modified",
                table: "rep_TaskInvervalsTypes",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_TaskInvervalsTypes_CreatedBy_ModifiedBy",
                table: "rep_TaskInvervalsTypes",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_rep_TaskInvervalsTypes_Session",
                table: "rep_TaskInvervalsTypes",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_CompaniesUsersGroups___MigCode",
                table: "sec_CompaniesUsersGroups",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_CompaniesUsersGroups___MigId",
                table: "sec_CompaniesUsersGroups",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_CompaniesUsersGroups_CompanyId",
                table: "sec_CompaniesUsersGroups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_CompaniesUsersGroups_Created_Modified",
                table: "sec_CompaniesUsersGroups",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_CompaniesUsersGroups_CreatedBy_ModifiedBy",
                table: "sec_CompaniesUsersGroups",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_CompaniesUsersGroups_GroupId",
                table: "sec_CompaniesUsersGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_CompaniesUsersGroups_Session",
                table: "sec_CompaniesUsersGroups",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_CompaniesUsersGroups_UserId",
                table: "sec_CompaniesUsersGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Grants___MigCode",
                table: "sec_Grants",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Grants___MigId",
                table: "sec_Grants",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Grants_Created_Modified",
                table: "sec_Grants",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Grants_CreatedBy_ModifiedBy",
                table: "sec_Grants",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Grants_DomainFIdm",
                table: "sec_Grants",
                column: "DomainFIdm");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Grants_OptionId",
                table: "sec_Grants",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Grants_Session",
                table: "sec_Grants",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Groups___MigCode",
                table: "sec_Groups",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Groups___MigId",
                table: "sec_Groups",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Groups_Created_Modified",
                table: "sec_Groups",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Groups_CreatedBy_ModifiedBy",
                table: "sec_Groups",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Groups_DomainFIdm",
                table: "sec_Groups",
                column: "DomainFIdm");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Groups_GroupOwnerId",
                table: "sec_Groups",
                column: "GroupOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Groups_Session",
                table: "sec_Groups",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsOrganizations___MigCode",
                table: "sec_GroupsOrganizations",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsOrganizations___MigId",
                table: "sec_GroupsOrganizations",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsOrganizations_Created_Modified",
                table: "sec_GroupsOrganizations",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsOrganizations_CreatedBy_ModifiedBy",
                table: "sec_GroupsOrganizations",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsOrganizations_GroupId",
                table: "sec_GroupsOrganizations",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsOrganizations_OrganizationId",
                table: "sec_GroupsOrganizations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsOrganizations_Session",
                table: "sec_GroupsOrganizations",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsRoles___MigCode",
                table: "sec_GroupsRoles",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsRoles___MigId",
                table: "sec_GroupsRoles",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsRoles_Created_Modified",
                table: "sec_GroupsRoles",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsRoles_CreatedBy_ModifiedBy",
                table: "sec_GroupsRoles",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsRoles_GroupId",
                table: "sec_GroupsRoles",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsRoles_RoleId",
                table: "sec_GroupsRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_GroupsRoles_Session",
                table: "sec_GroupsRoles",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Options___MigCode",
                table: "sec_Options",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Options___MigId",
                table: "sec_Options",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Options_Created_Modified",
                table: "sec_Options",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Options_CreatedBy_ModifiedBy",
                table: "sec_Options",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Options_DomainFIdm",
                table: "sec_Options",
                column: "DomainFIdm");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Options_ParentId",
                table: "sec_Options",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Options_Session",
                table: "sec_Options",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Pages___MigCode",
                table: "sec_Pages",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Pages___MigId",
                table: "sec_Pages",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Pages_ContextualOptionId",
                table: "sec_Pages",
                column: "ContextualOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Pages_Created_Modified",
                table: "sec_Pages",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Pages_CreatedBy_ModifiedBy",
                table: "sec_Pages",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Pages_Session",
                table: "sec_Pages",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Roles___MigCode",
                table: "sec_Roles",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Roles___MigId",
                table: "sec_Roles",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Roles_Created_Modified",
                table: "sec_Roles",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Roles_CreatedBy_ModifiedBy",
                table: "sec_Roles",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Roles_DomainFIdm",
                table: "sec_Roles",
                column: "DomainFIdm");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Roles_GroupOwnerId",
                table: "sec_Roles",
                column: "GroupOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Roles_Session",
                table: "sec_Roles",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesGrants___MigCode",
                table: "sec_RolesGrants",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesGrants___MigId",
                table: "sec_RolesGrants",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesGrants_Created_Modified",
                table: "sec_RolesGrants",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesGrants_CreatedBy_ModifiedBy",
                table: "sec_RolesGrants",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesGrants_GrantId",
                table: "sec_RolesGrants",
                column: "GrantId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesGrants_RoleId",
                table: "sec_RolesGrants",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesGrants_Session",
                table: "sec_RolesGrants",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesOptions___MigCode",
                table: "sec_RolesOptions",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesOptions___MigId",
                table: "sec_RolesOptions",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesOptions_Created_Modified",
                table: "sec_RolesOptions",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesOptions_CreatedBy_ModifiedBy",
                table: "sec_RolesOptions",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesOptions_OptionId",
                table: "sec_RolesOptions",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesOptions_RoleId",
                table: "sec_RolesOptions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_RolesOptions_Session",
                table: "sec_RolesOptions",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserActivities___MigCode",
                table: "sec_UserActivities",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserActivities___MigId",
                table: "sec_UserActivities",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserActivities_Created_Modified",
                table: "sec_UserActivities",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserActivities_CreatedBy_ModifiedBy",
                table: "sec_UserActivities",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserActivities_Session",
                table: "sec_UserActivities",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserActivities_UserId",
                table: "sec_UserActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserExternos___MigCode",
                table: "sec_UserExternos",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserExternos___MigId",
                table: "sec_UserExternos",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserExternos_Created_Modified",
                table: "sec_UserExternos",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserExternos_CreatedBy_ModifiedBy",
                table: "sec_UserExternos",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserExternos_DomainFIdm",
                table: "sec_UserExternos",
                column: "DomainFIdm");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserExternos_EmpresaPortalId",
                table: "sec_UserExternos",
                column: "EmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserExternos_Session",
                table: "sec_UserExternos",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserExternos_UserId",
                table: "sec_UserExternos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Users___MigCode",
                table: "sec_Users",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Users___MigId",
                table: "sec_Users",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Users_Created_Modified",
                table: "sec_Users",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Users_CreatedBy_ModifiedBy",
                table: "sec_Users",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_Users_GroupOwnerId",
                table: "sec_Users",
                column: "GroupOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Users_Session",
                table: "sec_Users",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_Users_UserTypeIdm",
                table: "sec_Users",
                column: "UserTypeIdm");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserTypes___MigCode",
                table: "sec_UserTypes",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserTypes___MigId",
                table: "sec_UserTypes",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserTypes_Created_Modified",
                table: "sec_UserTypes",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserTypes_CreatedBy_ModifiedBy",
                table: "sec_UserTypes",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_UserTypes_Session",
                table: "sec_UserTypes",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UsuarioEmpresaPortalRoles___MigCode",
                table: "sec_UsuarioEmpresaPortalRoles",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UsuarioEmpresaPortalRoles___MigId",
                table: "sec_UsuarioEmpresaPortalRoles",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UsuarioEmpresaPortalRoles_Created_Modified",
                table: "sec_UsuarioEmpresaPortalRoles",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_UsuarioEmpresaPortalRoles_CreatedBy_ModifiedBy",
                table: "sec_UsuarioEmpresaPortalRoles",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sec_UsuarioEmpresaPortalRoles_RolTipoId",
                table: "sec_UsuarioEmpresaPortalRoles",
                column: "RolTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UsuarioEmpresaPortalRoles_Session",
                table: "sec_UsuarioEmpresaPortalRoles",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sec_UsuarioEmpresaPortalRoles_UsuarioEmpresaPortalId",
                table: "sec_UsuarioEmpresaPortalRoles",
                column: "UsuarioEmpresaPortalId");

            migrationBuilder.CreateIndex(
                name: "IX_sis_Sistemas___MigCode",
                table: "sis_Sistemas",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sis_Sistemas___MigId",
                table: "sis_Sistemas",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sis_Sistemas_Created_Modified",
                table: "sis_Sistemas",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sis_Sistemas_CreatedBy_ModifiedBy",
                table: "sis_Sistemas",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sis_Sistemas_Session",
                table: "sis_Sistemas",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sys_DomainFs___MigCode",
                table: "sys_DomainFs",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sys_DomainFs___MigId",
                table: "sys_DomainFs",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sys_DomainFs_Created_Modified",
                table: "sys_DomainFs",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_DomainFs_CreatedBy_ModifiedBy",
                table: "sys_DomainFs",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_DomainFs_Session",
                table: "sys_DomainFs",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sys_DomainFs_UserTypeIdm",
                table: "sys_DomainFs",
                column: "UserTypeIdm");

            migrationBuilder.CreateIndex(
                name: "IX_sys_UserDomainFs___MigCode",
                table: "sys_UserDomainFs",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_sys_UserDomainFs___MigId",
                table: "sys_UserDomainFs",
                column: "__MigId");

            migrationBuilder.CreateIndex(
                name: "IX_sys_UserDomainFs_Created_Modified",
                table: "sys_UserDomainFs",
                columns: new[] { "Created", "Modified" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_UserDomainFs_CreatedBy_ModifiedBy",
                table: "sys_UserDomainFs",
                columns: new[] { "CreatedBy", "ModifiedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_UserDomainFs_DomainFIdm",
                table: "sys_UserDomainFs",
                column: "DomainFIdm");

            migrationBuilder.CreateIndex(
                name: "IX_sys_UserDomainFs_Session",
                table: "sys_UserDomainFs",
                column: "Session");

            migrationBuilder.CreateIndex(
                name: "IX_sys_UserDomainFs_UserId",
                table: "sys_UserDomainFs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ale_AlertaConReglaAdicionales");

            migrationBuilder.DropTable(
                name: "ale_AlertaEncoladas");

            migrationBuilder.DropTable(
                name: "ale_AlertaTipoCampoVariable");

            migrationBuilder.DropTable(
                name: "ale_AlertaTipoEstados");

            migrationBuilder.DropTable(
                name: "ale_AlertaTipoUbicaciones");

            migrationBuilder.DropTable(
                name: "aud_Sessions");

            migrationBuilder.DropTable(
                name: "cny_CompanyCurrencies");

            migrationBuilder.DropTable(
                name: "com_ComprobanteDetalles");

            migrationBuilder.DropTable(
                name: "com_ImpuestoDetalles");

            migrationBuilder.DropTable(
                name: "com_PercepcionCompanies");

            migrationBuilder.DropTable(
                name: "com_PercepcionDetalles");

            migrationBuilder.DropTable(
                name: "doc_DocumentosCargados");

            migrationBuilder.DropTable(
                name: "emp_CompanyExtras");

            migrationBuilder.DropTable(
                name: "emp_EmpresaCurrency");

            migrationBuilder.DropTable(
                name: "emp_EmpresaRoles");

            migrationBuilder.DropTable(
                name: "emp_EmpresasPortalesDocsComprasTipos");

            migrationBuilder.DropTable(
                name: "emp_EmpresasPortalesGastosTipos");

            migrationBuilder.DropTable(
                name: "emp_GruposDocsComprasTipos");

            migrationBuilder.DropTable(
                name: "EmpresasAlicuotas");

            migrationBuilder.DropTable(
                name: "EmpresasModosLecturas");

            migrationBuilder.DropTable(
                name: "gbl_CulturesOrganization");

            migrationBuilder.DropTable(
                name: "gbl_IdentificationTax");

            migrationBuilder.DropTable(
                name: "gbl_IdentificationTypes");

            migrationBuilder.DropTable(
                name: "int_InterfazProcesoValidacionAdicionales");

            migrationBuilder.DropTable(
                name: "int_TipoOrdenC");

            migrationBuilder.DropTable(
                name: "mpc_ComprobanteEMailExtensiones");

            migrationBuilder.DropTable(
                name: "mpc_IntegracionFacturaPorCorreoDetalles");

            migrationBuilder.DropTable(
                name: "ntf_Eventos");

            migrationBuilder.DropTable(
                name: "ntf_NotificacionAdjuntos");

            migrationBuilder.DropTable(
                name: "ntf_NotificacionEtiquetas");

            migrationBuilder.DropTable(
                name: "ntf_Notifications");

            migrationBuilder.DropTable(
                name: "ntf_NotificationTypesOrganizationsGroups");

            migrationBuilder.DropTable(
                name: "ntf_Subscriptions");

            migrationBuilder.DropTable(
                name: "ntf_SubscriptionsKeyValues");

            migrationBuilder.DropTable(
                name: "orc_OrdenesCompras");

            migrationBuilder.DropTable(
                name: "par_Parametros");

            migrationBuilder.DropTable(
                name: "pro_Procesos");

            migrationBuilder.DropTable(
                name: "rep_ReportTypeColumns");

            migrationBuilder.DropTable(
                name: "rep_ReportTypeParametersValues");

            migrationBuilder.DropTable(
                name: "sec_CompaniesUsersGroups");

            migrationBuilder.DropTable(
                name: "sec_GroupsOrganizations");

            migrationBuilder.DropTable(
                name: "sec_GroupsRoles");

            migrationBuilder.DropTable(
                name: "sec_Pages");

            migrationBuilder.DropTable(
                name: "sec_RolesGrants");

            migrationBuilder.DropTable(
                name: "sec_RolesOptions");

            migrationBuilder.DropTable(
                name: "sec_UserActivities");

            migrationBuilder.DropTable(
                name: "sec_UsuarioEmpresaPortalRoles");

            migrationBuilder.DropTable(
                name: "sys_UserDomainFs");

            migrationBuilder.DropTable(
                name: "ale_AlertaTipoReglaAdicionales");

            migrationBuilder.DropTable(
                name: "ale_Alertas");

            migrationBuilder.DropTable(
                name: "com_UnidadMedidas");

            migrationBuilder.DropTable(
                name: "com_Impuestos");

            migrationBuilder.DropTable(
                name: "com_Comprobantes");

            migrationBuilder.DropTable(
                name: "per_Percepciones");

            migrationBuilder.DropTable(
                name: "cer_SolicitudCertificaciones");

            migrationBuilder.DropTable(
                name: "doc_DocumentoEstados");

            migrationBuilder.DropTable(
                name: "doc_DocumentosRequeridos");

            migrationBuilder.DropTable(
                name: "com_ConceptosGastosTipos");

            migrationBuilder.DropTable(
                name: "mlr_ModosLecturas");

            migrationBuilder.DropTable(
                name: "gbl_Cultures");

            migrationBuilder.DropTable(
                name: "int_InterfazProcesoReglaEnforzadas");

            migrationBuilder.DropTable(
                name: "mpc_IntegracionesFacturaPorCorreo");

            migrationBuilder.DropTable(
                name: "ntf_EventoEstados");

            migrationBuilder.DropTable(
                name: "ntf_Notificaciones");

            migrationBuilder.DropTable(
                name: "ntf_NotificacionEtiquetaTipos");

            migrationBuilder.DropTable(
                name: "ntf_NotificationTypesOrganizations");

            migrationBuilder.DropTable(
                name: "orc_OrdenesComprasEstados");

            migrationBuilder.DropTable(
                name: "orc_OrdenesComprasTipos");

            migrationBuilder.DropTable(
                name: "pro_EstadoProcesos");

            migrationBuilder.DropTable(
                name: "pro_TipoProcesos");

            migrationBuilder.DropTable(
                name: "rep_ReportScheduleTasks");

            migrationBuilder.DropTable(
                name: "rep_ReportTypeParameters");

            migrationBuilder.DropTable(
                name: "sec_Grants");

            migrationBuilder.DropTable(
                name: "sec_Roles");

            migrationBuilder.DropTable(
                name: "emp_RolTipos");

            migrationBuilder.DropTable(
                name: "sec_UserExternos");

            migrationBuilder.DropTable(
                name: "ale_AlertaTipos");

            migrationBuilder.DropTable(
                name: "ale_DestinatarioVariables");

            migrationBuilder.DropTable(
                name: "ali_Alicuotas");

            migrationBuilder.DropTable(
                name: "com_ImpuestoTipos");

            migrationBuilder.DropTable(
                name: "com_CodigoAutorizacionTipos");

            migrationBuilder.DropTable(
                name: "com_ComprobanteEstados");

            migrationBuilder.DropTable(
                name: "com_ComprobanteTipos");

            migrationBuilder.DropTable(
                name: "com_CondicionVentas");

            migrationBuilder.DropTable(
                name: "com_EstadosValidacionARCA");

            migrationBuilder.DropTable(
                name: "gbl_Currencies");

            migrationBuilder.DropTable(
                name: "prd_Periodos");

            migrationBuilder.DropTable(
                name: "per_PercepcionTipos");

            migrationBuilder.DropTable(
                name: "cer_SolicitudCertificacionEstados");

            migrationBuilder.DropTable(
                name: "com_Origenes");

            migrationBuilder.DropTable(
                name: "cer_Certificaciones");

            migrationBuilder.DropTable(
                name: "doc_TipoDocumentos");

            migrationBuilder.DropTable(
                name: "int_InterfazCampos");

            migrationBuilder.DropTable(
                name: "int_InterfazProcesos");

            migrationBuilder.DropTable(
                name: "int_InterfazReglas");

            migrationBuilder.DropTable(
                name: "ntf_ConfiguracionNotificaciones");

            migrationBuilder.DropTable(
                name: "ntf_NotificacionEstados");

            migrationBuilder.DropTable(
                name: "ntf_NotificationTypes");

            migrationBuilder.DropTable(
                name: "rep_TaskInvervalsTypes");

            migrationBuilder.DropTable(
                name: "rep_ReportTypes");

            migrationBuilder.DropTable(
                name: "emp_EmpresasPortales");

            migrationBuilder.DropTable(
                name: "sec_Users");

            migrationBuilder.DropTable(
                name: "prd_EstadoPeriodos");

            migrationBuilder.DropTable(
                name: "int_InterfazEstados");

            migrationBuilder.DropTable(
                name: "int_InterfazReglaConsecuencias");

            migrationBuilder.DropTable(
                name: "ntf_TipoNotificaciones");

            migrationBuilder.DropTable(
                name: "sec_Options");

            migrationBuilder.DropTable(
                name: "cny_Company");

            migrationBuilder.DropTable(
                name: "com_CategoriaTipos");

            migrationBuilder.DropTable(
                name: "com_TipoEmpresasPortales");

            migrationBuilder.DropTable(
                name: "emp_TipoCuentas");

            migrationBuilder.DropTable(
                name: "geo_Cities");

            migrationBuilder.DropTable(
                name: "sec_Groups");

            migrationBuilder.DropTable(
                name: "int_Interfaces");

            migrationBuilder.DropTable(
                name: "cny_Organization");

            migrationBuilder.DropTable(
                name: "geo_Provinces");

            migrationBuilder.DropTable(
                name: "sys_DomainFs");

            migrationBuilder.DropTable(
                name: "sis_Sistemas");

            migrationBuilder.DropTable(
                name: "geo_Countries");

            migrationBuilder.DropTable(
                name: "sec_UserTypes");
        }
    }
}
