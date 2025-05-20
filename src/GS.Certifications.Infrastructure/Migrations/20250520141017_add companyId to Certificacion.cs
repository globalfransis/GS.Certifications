using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class addcompanyIdtoCertificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "cer_Certificaciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_cer_Certificaciones_CompanyId",
                table: "cer_Certificaciones",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_cer_Certificaciones_cny_Company_CompanyId",
                table: "cer_Certificaciones",
                column: "CompanyId",
                principalTable: "cny_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cer_Certificaciones_cny_Company_CompanyId",
                table: "cer_Certificaciones");

            migrationBuilder.DropIndex(
                name: "IX_cer_Certificaciones_CompanyId",
                table: "cer_Certificaciones");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "cer_Certificaciones");
        }
    }
}
