using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class addfechasolicitudyultimamodifestadoenSolicitudCertificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSolicitud",
                table: "cer_SolicitudCertificaciones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaModificacionEstado",
                table: "cer_SolicitudCertificaciones",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaSolicitud",
                table: "cer_SolicitudCertificaciones");

            migrationBuilder.DropColumn(
                name: "UltimaModificacionEstado",
                table: "cer_SolicitudCertificaciones");
        }
    }
}
