using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class models_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "firstname",
                schema: "auth",
                table: "users");

            migrationBuilder.EnsureSchema(
                name: "reference_book");

            migrationBuilder.RenameColumn(
                name: "middlename",
                schema: "auth",
                table: "users",
                newName: "pinfl");

            migrationBuilder.RenameColumn(
                name: "lastname",
                schema: "auth",
                table: "users",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "access_token",
                schema: "auth",
                table: "tokens",
                newName: "access_token_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "birth_date",
                schema: "auth",
                table: "users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "gender",
                schema: "auth",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "salt",
                schema: "auth",
                table: "user_sign_methods",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "calculating_centuries",
                schema: "reference_book",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calculating_centuries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "reference_book",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prayer_time_styles",
                schema: "reference_book",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prayer_time_styles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                schema: "reference_book",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regions", x => x.id);
                    table.ForeignKey(
                        name: "FK_regions_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "reference_book",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                schema: "reference_book",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    region_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.id);
                    table.ForeignKey(
                        name: "FK_districts_regions_region_id",
                        column: x => x.region_id,
                        principalSchema: "reference_book",
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_calculating_centuries_code",
                schema: "reference_book",
                table: "calculating_centuries",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_countries_code",
                schema: "reference_book",
                table: "countries",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_districts_code",
                schema: "reference_book",
                table: "districts",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_districts_region_id",
                schema: "reference_book",
                table: "districts",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_prayer_time_styles_code",
                schema: "reference_book",
                table: "prayer_time_styles",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_regions_code",
                schema: "reference_book",
                table: "regions",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_regions_country_id",
                schema: "reference_book",
                table: "regions",
                column: "country_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calculating_centuries",
                schema: "reference_book");

            migrationBuilder.DropTable(
                name: "districts",
                schema: "reference_book");

            migrationBuilder.DropTable(
                name: "prayer_time_styles",
                schema: "reference_book");

            migrationBuilder.DropTable(
                name: "regions",
                schema: "reference_book");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "reference_book");

            migrationBuilder.DropColumn(
                name: "birth_date",
                schema: "auth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "gender",
                schema: "auth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "salt",
                schema: "auth",
                table: "user_sign_methods");

            migrationBuilder.RenameColumn(
                name: "pinfl",
                schema: "auth",
                table: "users",
                newName: "middlename");

            migrationBuilder.RenameColumn(
                name: "full_name",
                schema: "auth",
                table: "users",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "access_token_id",
                schema: "auth",
                table: "tokens",
                newName: "access_token");

            migrationBuilder.AddColumn<string>(
                name: "firstname",
                schema: "auth",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
