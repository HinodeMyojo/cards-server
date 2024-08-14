using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardsServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_RoleEntity_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_StatusEntity_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvatarEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvatarEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvatarEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AvatarEntity",
                columns: new[] { "Id", "Avatar", "UserId" },
                values: new object[] { 1, "/9j/4AAQSkZJRgABAQEBLAEsAAD/4QBWRXhpZgAATU0AKgAAAAgABAEaAAUAAAABAAAAPgEbAAUAAAABAAAARgEoAAMAAAABAAIAAAITAAMAAAABAAEAAAAAAAAAAAEsAAAAAQAAASwAAAAB/+0ALFBob3Rvc2hvcCAzLjAAOEJJTQQEAAAAAAAPHAFaAAMbJUccAQAAAgAEAP/hDW5odHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0n77u/JyBpZD0nVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkJz8+Cjx4OnhtcG1ldGEgeG1sbnM6eD0nYWRvYmU6bnM6bWV0YS8nIHg6eG1wdGs9J0ltYWdlOjpFeGlmVG9vbCAxMS44OCc+CjxyZGY6UkRGIHhtbG5zOnJkZj0naHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyc+CgogPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9JycKICB4bWxuczp0aWZmPSdodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyc+CiAgPHRpZmY6UmVzb2x1dGlvblVuaXQ+MjwvdGlmZjpSZXNvbHV0aW9uVW5pdD4KICA8dGlmZjpYUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpYUmVzb2x1dGlvbj4KICA8dGlmZjpZUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpZUmVzb2x1dGlvbj4KIDwvcmRmOkRlc2NyaXB0aW9uPgoKIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PScnCiAgeG1sbnM6eG1wPSdodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvJz4KICA8eG1wOkNyZWF0b3JUb29sPkFkb2JlIFN0b2NrIFBsYXRmb3JtPC94bXA6Q3JlYXRvclRvb2w+CiA8L3JkZjpEZXNjcmlwdGlvbj4KCiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0nJwogIHhtbG5zOnhtcE1NPSdodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vJz4KICA8eG1wTU06RG9jdW1lbnRJRD54bXAuaWlkOjAzMTFmOWRhLThiZTQtNGIxOC04MmRlLTg0Y2MwYzc1ZTM3YjwveG1wTU06RG9jdW1lbnRJRD4KICA8eG1wTU06SW5zdGFuY2VJRD5hZG9iZTpkb2NpZDpzdG9jazoxYWQyMTk4Yi1hMDIzLTRmNGMtYWMzOS0zZTc3ZjhlNzY3ODM8L3htcE1NOkluc3RhbmNlSUQ+CiAgPHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD5hZG9iZTpkb2NpZDpzdG9jazo1NzA3MTA2NjA8L3htcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD4KIDwvcmRmOkRlc2NyaXB0aW9uPgo8L3JkZjpSREY+CjwveDp4bXBtZXRhPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSd3Jz8+/9sAQwAF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", 0 });

            migrationBuilder.InsertData(
                table: "RoleEntity",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Пользователь" },
                    { 2, "Администратор" },
                    { 3, "Модератор" }
                });

            migrationBuilder.InsertData(
                table: "StatusEntity",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Действует" },
                    { 2, "Заблокирован" },
                    { 3, "Удален" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvatarEntity_UserId",
                table: "AvatarEntity",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvatarEntity");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RoleEntity");

            migrationBuilder.DropTable(
                name: "StatusEntity");
        }
    }
}
