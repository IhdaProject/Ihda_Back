using System;
using System.Collections.Generic;
using Entity.Models.QuranCourses;
using Entity.Models.ReferenceBook;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class AddFileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cloud");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "quran_courses",
                table: "training_centers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "created_by",
                schema: "quran_courses",
                table: "training_centers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                schema: "quran_courses",
                table: "training_centers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "updated_by",
                schema: "quran_courses",
                table: "training_centers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "created_by",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "updated_by",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "quran_courses",
                table: "courses",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "created_by",
                schema: "quran_courses",
                table: "courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                schema: "quran_courses",
                table: "courses",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "updated_by",
                schema: "quran_courses",
                table: "courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<WorkingHour>(
                name: "working_hours",
                schema: "quran_courses",
                table: "course_forms",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(WorkingHour),
                oldType: "jsonb");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "quran_courses",
                table: "course_forms",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "created_by",
                schema: "quran_courses",
                table: "course_forms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                schema: "quran_courses",
                table: "course_forms",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "updated_by",
                schema: "quran_courses",
                table: "course_forms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "quran_courses",
                table: "course_form_teachers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "created_by",
                schema: "quran_courses",
                table: "course_form_teachers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                schema: "quran_courses",
                table: "course_form_teachers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "updated_by",
                schema: "quran_courses",
                table: "course_form_teachers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "files",
                schema: "cloud",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objname = table.Column<string>(type: "text", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_schemas",
                schema: "reference_book",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    fields = table.Column<List<FieldSchema>>(type: "jsonb", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_schemas", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_type_schemas_name",
                schema: "reference_book",
                table: "type_schemas",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "files",
                schema: "cloud");

            migrationBuilder.DropTable(
                name: "type_schemas",
                schema: "reference_book");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "quran_courses",
                table: "training_centers");

            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "quran_courses",
                table: "training_centers");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "quran_courses",
                table: "training_centers");

            migrationBuilder.DropColumn(
                name: "updated_by",
                schema: "quran_courses",
                table: "training_centers");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "quran_courses",
                table: "petition_for_quran_course");

            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "quran_courses",
                table: "petition_for_quran_course");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "quran_courses",
                table: "petition_for_quran_course");

            migrationBuilder.DropColumn(
                name: "updated_by",
                schema: "quran_courses",
                table: "petition_for_quran_course");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "quran_courses",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "quran_courses",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "quran_courses",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "updated_by",
                schema: "quran_courses",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "quran_courses",
                table: "course_forms");

            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "quran_courses",
                table: "course_forms");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "quran_courses",
                table: "course_forms");

            migrationBuilder.DropColumn(
                name: "updated_by",
                schema: "quran_courses",
                table: "course_forms");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "quran_courses",
                table: "course_form_teachers");

            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "quran_courses",
                table: "course_form_teachers");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "quran_courses",
                table: "course_form_teachers");

            migrationBuilder.DropColumn(
                name: "updated_by",
                schema: "quran_courses",
                table: "course_form_teachers");

            migrationBuilder.AlterColumn<WorkingHour>(
                name: "working_hours",
                schema: "quran_courses",
                table: "course_forms",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(WorkingHour),
                oldType: "jsonb",
                oldNullable: true);
        }
    }
}
