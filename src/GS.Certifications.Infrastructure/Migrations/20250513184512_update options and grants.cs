using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class updateoptionsandgrants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100001L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Alta de Solicitud de Certificación", "solicitudcertificacion.create" });

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100002L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Baja de Solicitud de Certificación", "solicitudcertificacion.delete" });

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100003L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Modificación de Solicitud de Certificación", "solicitudcertificacion.update" });

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100004L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Alta de Solicitud de Certificación", "solicitudcertificacion.create" });

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100005L,
                column: "Name",
                value: "solicitudcertificacion.update");

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100006L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Baja de Solicitud de Certificacion", "solicitudcertificacion.delete" });

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100001L,
                columns: new[] { "Code", "Description", "Name" },
                values: new object[] { "SOC", "Módulo de Socios", "Socios" });

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100002L,
                columns: new[] { "Code", "Description", "Name" },
                values: new object[] { "SOC-CER", "Gestión de Solicitudes de Certificación", "Certificaciones" });

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100003L,
                column: "Description",
                value: "Módulo de Socios");

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100004L,
                columns: new[] { "Code", "Description", "Name", "TargetPath" },
                values: new object[] { "SOC-CER", "Gestión de Solicitudes de Certificación", "Certificaciones", "Socios/Certificaciones/Index" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100001L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Alta de Comprobantes", "comprobantes.create" });

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100002L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Baja de Comprobantes", "comprobantes.delete" });

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100003L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Modificación de Comprobantes", "comprobantes.update" });

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100004L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Alta de Comprobantes", "comprobantes.create" });

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100005L,
                column: "Name",
                value: "comprobantes.update");

            migrationBuilder.UpdateData(
                table: "sec_Grants",
                keyColumn: "Id",
                keyValue: 100006L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Baja de Comprobantes", "comprobantes.delete" });

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100001L,
                columns: new[] { "Code", "Description", "Name" },
                values: new object[] { "PRO", "Modulo de Proveedores", "Proveedores" });

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100002L,
                columns: new[] { "Code", "Description", "Name" },
                values: new object[] { "PRO-COM", "Carga de Comprobantes", "Comprobantes" });

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100003L,
                column: "Description",
                value: "Modulo de Socios");

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100004L,
                columns: new[] { "Code", "Description", "Name", "TargetPath" },
                values: new object[] { "PRO-COM", "Carga de Comprobantes", "Comprobantes", "Proveedores/Comprobantes/Index" });
        }
    }
}
