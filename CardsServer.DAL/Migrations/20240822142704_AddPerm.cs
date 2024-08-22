using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardsServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPerm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvatarEntity_Users_UserId",
                table: "AvatarEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RoleEntity_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_StatusEntity_StatusId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusEntity",
                table: "StatusEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleEntity",
                table: "RoleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvatarEntity",
                table: "AvatarEntity");

            migrationBuilder.RenameTable(
                name: "StatusEntity",
                newName: "Statuses");

            migrationBuilder.RenameTable(
                name: "RoleEntity",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "AvatarEntity",
                newName: "Avatars");

            migrationBuilder.RenameIndex(
                name: "IX_AvatarEntity_UserId",
                table: "Avatars",
                newName: "IX_Avatars_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    PermissionsId = table.Column<int>(type: "integer", nullable: false),
                    RolesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Редактирование всех записей", "EditAllObject" },
                    { 2, "Удаление всех записей", "DeleteAllObject" },
                    { 3, "Получение всех записей", "GetAllObject" },
                    { 4, "Получение своих записей", "GetOwnObject" },
                    { 5, "Создание своих записей", "CreateOwnObject" },
                    { 6, "Редактирование своих записей", "EditOwnObject" },
                    { 7, "Удаление своих записей", "DeleteOwnObject" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_RolesId",
                table: "RolesPermissions",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Statuses_StatusId",
                table: "Users",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Statuses_StatusId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "StatusEntity");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "RoleEntity");

            migrationBuilder.RenameTable(
                name: "Avatars",
                newName: "AvatarEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Avatars_UserId",
                table: "AvatarEntity",
                newName: "IX_AvatarEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusEntity",
                table: "StatusEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleEntity",
                table: "RoleEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvatarEntity",
                table: "AvatarEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvatarEntity_Users_UserId",
                table: "AvatarEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RoleEntity_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "RoleEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_StatusEntity_StatusId",
                table: "Users",
                column: "StatusId",
                principalTable: "StatusEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
