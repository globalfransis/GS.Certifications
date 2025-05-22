using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class renameoptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100005L,
                column: "TargetPath",
                value: "/Socios/Empresas/Index");

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100006L,
                column: "TargetPath",
                value: "/Socios/Impuestos/Index");

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100007L,
                column: "TargetPath",
                value: "/Socios/Percepciones/Index");

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100009L,
                column: "TargetPath",
                value: "/Socios/OrdenesCompras/Index");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100005L,
                column: "TargetPath",
                value: "/Proveedores/Empresas/Index");

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100006L,
                column: "TargetPath",
                value: "/Proveedores/Impuestos/Index");

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100007L,
                column: "TargetPath",
                value: "/Proveedores/Percepciones/Index");

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100009L,
                column: "TargetPath",
                value: "/Proveedores/OrdenesCompras/Index");
        }
    }
}
