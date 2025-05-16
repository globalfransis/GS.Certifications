using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class addobservacionesDocumentoCargado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "doc_DocumentosCargados",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "doc_DocumentosCargados");
        }
    }
}
