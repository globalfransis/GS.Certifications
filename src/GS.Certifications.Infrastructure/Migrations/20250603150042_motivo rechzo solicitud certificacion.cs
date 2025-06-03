using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class motivorechzosolicitudcertificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotivoRechazo",
                table: "cer_SolicitudCertificaciones",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.InsertData(
                table: "cer_SolicitudCertificacionEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[] { (short)6, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Revisión", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cer_SolicitudCertificacionEstados",
                keyColumn: "Idm",
                keyValue: (short)6);

            migrationBuilder.DropColumn(
                name: "MotivoRechazo",
                table: "cer_SolicitudCertificaciones");
        }
    }
}
