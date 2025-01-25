using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultDateInDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<Guid>(
                name: "DateCreated",
                table: "Jits",
                nullable: false,
                defaultValueSql: "GETDATE()");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
