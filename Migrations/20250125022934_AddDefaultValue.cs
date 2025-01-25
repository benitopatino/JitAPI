using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Jits",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Users",
                nullable: false,
                defaultValueSql: "NEWID()");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
