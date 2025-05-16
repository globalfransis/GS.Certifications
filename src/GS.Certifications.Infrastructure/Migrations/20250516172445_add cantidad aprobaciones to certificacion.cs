using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class addcantidadaprobacionestocertificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadAprobaciones",
                table: "cer_SolicitudCertificaciones");

            migrationBuilder.AddColumn<short>(
                name: "CantidadAprobaciones",
                table: "cer_Certificaciones",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadAprobaciones",
                table: "cer_Certificaciones");

            migrationBuilder.AddColumn<short>(
                name: "CantidadAprobaciones",
                table: "cer_SolicitudCertificaciones",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
