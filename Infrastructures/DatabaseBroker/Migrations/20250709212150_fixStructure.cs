using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class fixStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_structures_structure_id",
                schema: "auth",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_structure_id",
                schema: "auth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "structure_id",
                schema: "auth",
                table: "users");

            migrationBuilder.AddColumn<DateTime>(
                name: "expire_token",
                schema: "auth",
                table: "tokens",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "user_structures",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    structure_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_structures", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_structures_structures_structure_id",
                        column: x => x.structure_id,
                        principalSchema: "auth",
                        principalTable: "structures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_structures_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_structures_structure_id",
                schema: "auth",
                table: "user_structures",
                column: "structure_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_structures_user_id",
                schema: "auth",
                table: "user_structures",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_structures",
                schema: "auth");

            migrationBuilder.DropColumn(
                name: "expire_token",
                schema: "auth",
                table: "tokens");

            migrationBuilder.AddColumn<long>(
                name: "structure_id",
                schema: "auth",
                table: "users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_structure_id",
                schema: "auth",
                table: "users",
                column: "structure_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_structures_structure_id",
                schema: "auth",
                table: "users",
                column: "structure_id",
                principalSchema: "auth",
                principalTable: "structures",
                principalColumn: "id");
        }
    }
}
