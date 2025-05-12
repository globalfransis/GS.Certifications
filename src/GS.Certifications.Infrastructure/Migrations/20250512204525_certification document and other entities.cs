using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class certificationdocumentandotherentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "TipoId",
                table: "emp_EmpresasPortales",
                type: "smallint",
                nullable: true);

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
                name: "cer_Certificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoEmpresaPortalId = table.Column<short>(type: "smallint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
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
                    table.PrimaryKey("PK_cer_Certificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cer_Certificaciones_com_TipoEmpresasPortales_TipoEmpresaPortalId",
                        column: x => x.TipoEmpresaPortalId,
                        principalTable: "com_TipoEmpresasPortales",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cer_SolicitudCertificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    CertificacionId = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<short>(type: "smallint", nullable: false),
                    CantidadAprobaciones = table.Column<short>(type: "smallint", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
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
                        name: "FK_cer_SolicitudCertificaciones_emp_EmpresasPortales_SocioId",
                        column: x => x.SocioId,
                        principalTable: "emp_EmpresasPortales",
                        principalColumn: "Id",
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
                name: "doc_DocumentosCargados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    DocumentoRequeridoId = table.Column<int>(type: "int", nullable: false),
                    ArchivoURL = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    FechaDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    EstadoIdm = table.Column<short>(type: "smallint", nullable: false),
                    ValidadoPorId = table.Column<long>(type: "bigint", nullable: true),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        name: "FK_doc_DocumentosCargados_doc_DocumentoEstados_EstadoIdm",
                        column: x => x.EstadoIdm,
                        principalTable: "doc_DocumentoEstados",
                        principalColumn: "Idm",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "cer_SolicitudCertificacionEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Pendiente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "En proceso", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Aprobada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rechazada", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "doc_DocumentoEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Pendiente", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)2, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Validado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)3, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Rechazado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null },
                    { (short)4, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Vencido", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_emp_EmpresasPortales_TipoId",
                table: "emp_EmpresasPortales",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones___MigCode",
                table: "cer_Certificaciones",
                column: "__MigCode");

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones___MigId",
                table: "cer_Certificaciones",
                column: "__MigId");

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
                name: "IX_doc_DocumentosCargados_EstadoIdm",
                table: "doc_DocumentosCargados",
                column: "EstadoIdm");

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

            migrationBuilder.AddForeignKey(
                name: "FK_emp_EmpresasPortales_com_TipoEmpresasPortales_TipoId",
                table: "emp_EmpresasPortales",
                column: "TipoId",
                principalTable: "com_TipoEmpresasPortales",
                principalColumn: "Idm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_emp_EmpresasPortales_com_TipoEmpresasPortales_TipoId",
                table: "emp_EmpresasPortales");

            migrationBuilder.DropTable(
                name: "doc_DocumentosCargados");

            migrationBuilder.DropTable(
                name: "cer_SolicitudCertificaciones");

            migrationBuilder.DropTable(
                name: "doc_DocumentoEstados");

            migrationBuilder.DropTable(
                name: "doc_DocumentosRequeridos");

            migrationBuilder.DropTable(
                name: "cer_SolicitudCertificacionEstados");

            migrationBuilder.DropTable(
                name: "cer_Certificaciones");

            migrationBuilder.DropTable(
                name: "doc_TipoDocumentos");

            migrationBuilder.DropTable(
                name: "com_TipoEmpresasPortales");

            migrationBuilder.DropIndex(
                name: "IX_emp_EmpresasPortales_TipoId",
                table: "emp_EmpresasPortales");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "emp_EmpresasPortales");
        }
    }
}
