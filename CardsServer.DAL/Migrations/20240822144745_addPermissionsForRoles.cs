using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addPermissionsForRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Создание записей", "CreateObject" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Создание своих записей", "CreateOwnObject" });
        }
    }
}
