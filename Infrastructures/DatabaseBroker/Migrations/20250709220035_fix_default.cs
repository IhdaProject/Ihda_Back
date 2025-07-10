using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class fix_default : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_default",
                schema: "auth",
                table: "structures");

            migrationBuilder.AddColumn<int>(
                name: "type",
                schema: "auth",
                table: "structures",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                schema: "auth",
                table: "structures");

            migrationBuilder.AddColumn<bool>(
                name: "is_default",
                schema: "auth",
                table: "structures",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
