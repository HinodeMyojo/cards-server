using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addElements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementEntity_ImageEntity_ImageId",
                table: "ElementEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementEntity_Modules_ModuleEntityId",
                table: "ElementEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElementEntity",
                table: "ElementEntity");

            migrationBuilder.RenameTable(
                name: "ElementEntity",
                newName: "Elements");

            migrationBuilder.RenameIndex(
                name: "IX_ElementEntity_ModuleEntityId",
                table: "Elements",
                newName: "IX_Elements_ModuleEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ElementEntity_ImageId",
                table: "Elements",
                newName: "IX_Elements_ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Elements",
                table: "Elements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_ImageEntity_ImageId",
                table: "Elements",
                column: "ImageId",
                principalTable: "ImageEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Modules_ModuleEntityId",
                table: "Elements",
                column: "ModuleEntityId",
                principalTable: "Modules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_ImageEntity_ImageId",
                table: "Elements");

            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Modules_ModuleEntityId",
                table: "Elements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Elements",
                table: "Elements");

            migrationBuilder.RenameTable(
                name: "Elements",
                newName: "ElementEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Elements_ModuleEntityId",
                table: "ElementEntity",
                newName: "IX_ElementEntity_ModuleEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Elements_ImageId",
                table: "ElementEntity",
                newName: "IX_ElementEntity_ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElementEntity",
                table: "ElementEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementEntity_ImageEntity_ImageId",
                table: "ElementEntity",
                column: "ImageId",
                principalTable: "ImageEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementEntity_Modules_ModuleEntityId",
                table: "ElementEntity",
                column: "ModuleEntityId",
                principalTable: "Modules",
                principalColumn: "Id");
        }
    }
}
