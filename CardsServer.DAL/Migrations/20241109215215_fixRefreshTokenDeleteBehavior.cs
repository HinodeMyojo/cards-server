using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixRefreshTokenDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokenEntities_Users_UserId",
                table: "RefreshTokenEntities");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokenEntities_Users_UserId",
                table: "RefreshTokenEntities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokenEntities_Users_UserId",
                table: "RefreshTokenEntities");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokenEntities_Users_UserId",
                table: "RefreshTokenEntities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
