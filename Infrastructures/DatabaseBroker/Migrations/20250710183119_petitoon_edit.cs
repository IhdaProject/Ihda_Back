using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class petitoon_edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "passport_expire_date",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "for_gender",
                schema: "quran_courses",
                table: "course_forms",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "max_years_old",
                schema: "quran_courses",
                table: "course_forms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "min_years_old",
                schema: "quran_courses",
                table: "course_forms",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                schema: "quran_courses",
                table: "petition_for_quran_course");

            migrationBuilder.DropColumn(
                name: "passport_expire_date",
                schema: "quran_courses",
                table: "petition_for_quran_course");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "quran_courses",
                table: "petition_for_quran_course");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "quran_courses",
                table: "petition_for_quran_course");

            migrationBuilder.DropColumn(
                name: "max_years_old",
                schema: "quran_courses",
                table: "course_forms");

            migrationBuilder.DropColumn(
                name: "min_years_old",
                schema: "quran_courses",
                table: "course_forms");

            migrationBuilder.AlterColumn<int>(
                name: "for_gender",
                schema: "quran_courses",
                table: "course_forms",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
