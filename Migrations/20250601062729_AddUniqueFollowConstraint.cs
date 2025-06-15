using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueFollowConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollows_UserFollowerId",
                table: "UserFollows");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_UserFollowerId_UserFolloweeId",
                table: "UserFollows",
                columns: new[] { "UserFollowerId", "UserFolloweeId" },
                unique: true,
                filter: "[UserFolloweeId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollows_UserFollowerId_UserFolloweeId",
                table: "UserFollows");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_UserFollowerId",
                table: "UserFollows",
                column: "UserFollowerId");
        }
    }
}
