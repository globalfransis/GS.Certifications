using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class addvigenciaenSolicitudCertificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "VigenciaDesde",
                table: "cer_SolicitudCertificaciones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VigenciaHasta",
                table: "cer_SolicitudCertificaciones",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VigenciaDesde",
                table: "cer_SolicitudCertificaciones");

            migrationBuilder.DropColumn(
                name: "VigenciaHasta",
                table: "cer_SolicitudCertificaciones");
        }
    }
}
