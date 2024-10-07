using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editImageEntity1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsModuleImage",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ElementId",
                table: "Images",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElementId",
                table: "Images");

            migrationBuilder.AddColumn<bool>(
                name: "IsModuleImage",
                table: "Images",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
