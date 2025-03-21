using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserFollowModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.CreateTable(
                name: "UserFollows",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfFollow = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserFollowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFolloweeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollows_Users_UserFolloweeId",
                        column: x => x.UserFolloweeId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserFollows_Users_UserFollowerId",
                        column: x => x.UserFollowerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_UserFolloweeId",
                table: "UserFollows",
                column: "UserFolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_UserFollowerId",
                table: "UserFollows",
                column: "UserFollowerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFollows");

            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolloweeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FollowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfFollow = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relationships_Users_FolloweeId",
                        column: x => x.FolloweeId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Relationships_Users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_FolloweeId",
                table: "Relationships",
                column: "FolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_FollowerId",
                table: "Relationships",
                column: "FollowerId");
        }
    }
}
