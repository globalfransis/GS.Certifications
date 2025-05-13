using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class updateoptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sec_Groups",
                keyColumn: "Id",
                keyValue: 1000L,
                columns: new[] { "Description", "InternalCode", "Name" },
                values: new object[] { "AdminSocios", "AdminSocios", "AdminSocios" });

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100002L,
                column: "TargetPath",
                value: "Socios/Certificaciones/Index");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sec_Groups",
                keyColumn: "Id",
                keyValue: 1000L,
                columns: new[] { "Description", "InternalCode", "Name" },
                values: new object[] { "AdminSupplier", "AdminSupplier", "AdminSupplier" });

            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100002L,
                column: "TargetPath",
                value: "Proveedores/Comprobantes/Index");
        }
    }
}
