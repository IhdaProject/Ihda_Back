using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class add_files : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avatar_url",
                schema: "auth",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "passport_photo_url",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "center_managers",
                schema: "quran_courses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    training_center_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_center_managers", x => x.id);
                    table.ForeignKey(
                        name: "FK_center_managers_training_centers_training_center_id",
                        column: x => x.training_center_id,
                        principalSchema: "quran_courses",
                        principalTable: "training_centers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_center_managers_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_center_managers_training_center_id",
                schema: "quran_courses",
                table: "center_managers",
                column: "training_center_id");

            migrationBuilder.CreateIndex(
                name: "IX_center_managers_user_id",
                schema: "quran_courses",
                table: "center_managers",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "center_managers",
                schema: "quran_courses");

            migrationBuilder.DropColumn(
                name: "avatar_url",
                schema: "auth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "passport_photo_url",
                schema: "quran_courses",
                table: "petition_for_quran_course");
        }
    }
}
