using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS.Certifications.Infrastructure.Migrations
{
    public partial class roleangroupseedig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "sec_Groups",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "DomainFIdm", "GroupOwnerId", "InternalCode", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "SystemUse", "__MigCode", "__MigId" },
                values: new object[] { 1000L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AdminSupplier", 1001L, 1L, "AdminSupplier", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "AdminSupplier", null, true, null, null });

            migrationBuilder.InsertData(
                table: "sec_Roles",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "DomainFIdm", "GroupOwnerId", "InternalCode", "IsDeleted", "Modified", "ModifiedBy", "Name", "Session", "SystemUse", "__MigCode", "__MigId" },
                values: new object[] { 1001L, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Administrador del Portal de Socios", 1001L, 1000L, "Socios_Admin", false, new DateTime(1986, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Process", "Admin", null, true, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "sec_Roles",
                keyColumn: "Id",
                keyValue: 1001L);

            migrationBuilder.DeleteData(
                table: "sec_Groups",
                keyColumn: "Id",
                keyValue: 1000L);
        }
    }
}
