using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editUsersAndModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Modules_ModuleEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ModuleEntityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModuleEntityId",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "Modules",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "ModuleEntityUserEntity",
                columns: table => new
                {
                    ModulesId = table.Column<int>(type: "integer", nullable: false),
                    UsedUsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleEntityUserEntity", x => new { x.ModulesId, x.UsedUsersId });
                    table.ForeignKey(
                        name: "FK_ModuleEntityUserEntity_Modules_ModulesId",
                        column: x => x.ModulesId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleEntityUserEntity_Users_UsedUsersId",
                        column: x => x.UsedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleEntityUserEntity_UsedUsersId",
                table: "ModuleEntityUserEntity",
                column: "UsedUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleEntityUserEntity");

            migrationBuilder.AddColumn<int>(
                name: "ModuleEntityId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "Modules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModuleEntityId",
                table: "Users",
                column: "ModuleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Modules_ModuleEntityId",
                table: "Users",
                column: "ModuleEntityId",
                principalTable: "Modules",
                principalColumn: "Id");
        }
    }
}
