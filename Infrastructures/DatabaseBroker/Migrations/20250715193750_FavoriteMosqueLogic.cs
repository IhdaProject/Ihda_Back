using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteMosqueLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_users_teacher_id",
                schema: "quran_courses",
                table: "courses");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "quran_courses",
                table: "courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "favorite_mosques",
                schema: "prayerful",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    mosque_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorite_mosques", x => x.id);
                    table.ForeignKey(
                        name: "FK_favorite_mosques_mosques_mosque_id",
                        column: x => x.mosque_id,
                        principalSchema: "prayerful",
                        principalTable: "mosques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_favorite_mosques_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MosqueAdmin",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_form_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MosqueAdmin", x => x.id);
                    table.ForeignKey(
                        name: "FK_MosqueAdmin_mosques_course_form_id",
                        column: x => x.course_form_id,
                        principalSchema: "prayerful",
                        principalTable: "mosques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MosqueAdmin_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_favorite_mosques_mosque_id",
                schema: "prayerful",
                table: "favorite_mosques",
                column: "mosque_id");

            migrationBuilder.CreateIndex(
                name: "IX_favorite_mosques_user_id",
                schema: "prayerful",
                table: "favorite_mosques",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_MosqueAdmin_course_form_id",
                table: "MosqueAdmin",
                column: "course_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_MosqueAdmin_user_id",
                table: "MosqueAdmin",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_course_form_teachers_teacher_id",
                schema: "quran_courses",
                table: "courses",
                column: "teacher_id",
                principalSchema: "quran_courses",
                principalTable: "course_form_teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_course_form_teachers_teacher_id",
                schema: "quran_courses",
                table: "courses");

            migrationBuilder.DropTable(
                name: "favorite_mosques",
                schema: "prayerful");

            migrationBuilder.DropTable(
                name: "MosqueAdmin");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "quran_courses",
                table: "courses");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_users_teacher_id",
                schema: "quran_courses",
                table: "courses",
                column: "teacher_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
