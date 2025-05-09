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
                keyValue: 100003L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Modulo de Socios", "Socios" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sec_Options",
                keyColumn: "Id",
                keyValue: 100003L,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Modulo de Proveedores", "Proveedores" });
        }
    }
}
