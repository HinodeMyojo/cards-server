using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addFingerPrintColumnToRefreshEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FingerPrint",
                table: "RefreshTokenEntities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FingerPrint",
                table: "RefreshTokenEntities");
        }
    }
}
