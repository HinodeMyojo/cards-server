using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addConfForProfileAndUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProfileEntities_UserId",
                table: "ProfileEntities");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileEntities_UserId",
                table: "ProfileEntities",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProfileEntities_UserId",
                table: "ProfileEntities");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileEntities_UserId",
                table: "ProfileEntities",
                column: "UserId");
        }
    }
}
