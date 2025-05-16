using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class addnuevoestadodocumento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "doc_DocumentoEstados",
                columns: new[] { "Idm", "Created", "CreatedBy", "Descripcion", "IsDeleted", "Modified", "ModifiedBy", "Session", "__MigCode", "__MigId" },
                values: new object[] { (short)5, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Presentado", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "doc_DocumentoEstados",
                keyColumn: "Idm",
                keyValue: (short)5);
        }
    }
}
