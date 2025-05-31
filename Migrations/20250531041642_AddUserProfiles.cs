using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(250)", nullable: false),
                    Title = table.Column<string>(type: "varchar(250)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "varchar(500)", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "varchar(250)", nullable: true),
                    StateOrProvince = table.Column<string>(type: "varchar(250)", nullable: true),
                    Country = table.Column<string>(type: "varchar(250)", nullable: true),
                    FollowerCount = table.Column<int>(type: "int", nullable: false),
                    FolloweeCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_Username",
                table: "UserProfiles",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
