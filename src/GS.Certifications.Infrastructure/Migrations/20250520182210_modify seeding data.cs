using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class modifyseedingdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "com_Origenes",
                keyColumn: "Idm",
                keyValue: (short)1,
                column: "Descripcion",
                value: "SOCIOS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "com_Origenes",
                keyColumn: "Idm",
                keyValue: (short)1,
                column: "Descripcion",
                value: "PROVEEDOR");
        }
    }
}
