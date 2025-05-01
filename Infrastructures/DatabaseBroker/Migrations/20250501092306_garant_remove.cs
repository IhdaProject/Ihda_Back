using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class garant_remove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "granted_by_id",
                schema: "auth",
                table: "structure_permissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "granted_by_id",
                schema: "auth",
                table: "structure_permissions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
