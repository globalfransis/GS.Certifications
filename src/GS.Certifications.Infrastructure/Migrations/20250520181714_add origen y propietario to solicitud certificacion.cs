using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class addorigenypropietariotosolicitudcertificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "OrigenId",
                table: "cer_SolicitudCertificaciones",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PropietarioActualId",
                table: "cer_SolicitudCertificaciones",
                type: "smallint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_OrigenId",
                table: "cer_SolicitudCertificaciones",
                column: "OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_cer_SolicitudCertificaciones_PropietarioActualId",
                table: "cer_SolicitudCertificaciones",
                column: "PropietarioActualId");

            migrationBuilder.AddForeignKey(
                name: "FK_cer_SolicitudCertificaciones_com_Origenes_OrigenId",
                table: "cer_SolicitudCertificaciones",
                column: "OrigenId",
                principalTable: "com_Origenes",
                principalColumn: "Idm");

            migrationBuilder.AddForeignKey(
                name: "FK_cer_SolicitudCertificaciones_com_Origenes_PropietarioActualId",
                table: "cer_SolicitudCertificaciones",
                column: "PropietarioActualId",
                principalTable: "com_Origenes",
                principalColumn: "Idm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cer_SolicitudCertificaciones_com_Origenes_OrigenId",
                table: "cer_SolicitudCertificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_cer_SolicitudCertificaciones_com_Origenes_PropietarioActualId",
                table: "cer_SolicitudCertificaciones");

            migrationBuilder.DropIndex(
                name: "IX_cer_SolicitudCertificaciones_OrigenId",
                table: "cer_SolicitudCertificaciones");

            migrationBuilder.DropIndex(
                name: "IX_cer_SolicitudCertificaciones_PropietarioActualId",
                table: "cer_SolicitudCertificaciones");

            migrationBuilder.DropColumn(
                name: "OrigenId",
                table: "cer_SolicitudCertificaciones");

            migrationBuilder.DropColumn(
                name: "PropietarioActualId",
                table: "cer_SolicitudCertificaciones");
        }
    }
}
