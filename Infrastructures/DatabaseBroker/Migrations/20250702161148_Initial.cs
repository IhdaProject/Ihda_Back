using System;
using System.Collections.Generic;
using Entity.Models.QuranCourses;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reference_book");

            migrationBuilder.EnsureSchema(
                name: "quran_courses");

            migrationBuilder.EnsureSchema(
                name: "prayerful");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.EnsureSchema(
                name: "asset");

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
                name: "mosques",
                schema: "prayerful",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    photo_urls = table.Column<List<string>>(type: "text[]", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mosques", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "auth",
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
                    table.PrimaryKey("PK_permissions", x => x.id);
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
                name: "static_files",
                schema: "asset",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    path = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    file_extension = table.Column<string>(type: "text", nullable: true),
                    old_name = table.Column<string>(type: "text", nullable: true),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_static_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "structures",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_structures", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tokens",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    access_token_id = table.Column<string>(type: "text", nullable: false),
                    refresh_token = table.Column<string>(type: "text", nullable: false),
                    expire_refresh_token = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens", x => x.id);
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
                name: "mosque_prayer_times",
                schema: "prayerful",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adham_fajr = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    adham_dhuhr = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    adham_asr = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    adham_maghrib = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    adham_isha = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    iqamah_fajr = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    iqamah_dhuhr = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    iqamah_asr = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    iqamah_maghrib = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    iqamah_isha = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    mosque_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mosque_prayer_times", x => x.id);
                    table.ForeignKey(
                        name: "FK_mosque_prayer_times_mosques_mosque_id",
                        column: x => x.mosque_id,
                        principalSchema: "prayerful",
                        principalTable: "mosques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "structure_permissions",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    structure_id = table.Column<long>(type: "bigint", nullable: false),
                    permission_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_structure_permissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_structure_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalSchema: "auth",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_structure_permissions_structures_structure_id",
                        column: x => x.structure_id,
                        principalSchema: "auth",
                        principalTable: "structures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    pinfl = table.Column<string>(type: "text", nullable: true),
                    structure_id = table.Column<long>(type: "bigint", nullable: true),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_structures_structure_id",
                        column: x => x.structure_id,
                        principalSchema: "auth",
                        principalTable: "structures",
                        principalColumn: "id");
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

            migrationBuilder.CreateTable(
                name: "user_sign_methods",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    username = table.Column<string>(type: "text", nullable: true),
                    salt = table.Column<string>(type: "text", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_sign_methods", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_sign_methods_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "training_centers",
                schema: "quran_courses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    landmark = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    photos_link = table.Column<string[]>(type: "text[]", nullable: false),
                    working_hours = table.Column<WorkingHour>(type: "jsonb", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    district_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_training_centers", x => x.id);
                    table.ForeignKey(
                        name: "FK_training_centers_districts_district_id",
                        column: x => x.district_id,
                        principalSchema: "reference_book",
                        principalTable: "districts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_forms",
                schema: "quran_courses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    admission_quota = table.Column<int>(type: "integer", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    for_gender = table.Column<int>(type: "integer", nullable: false),
                    working_hours = table.Column<WorkingHour>(type: "jsonb", nullable: false),
                    training_center_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_forms", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_forms_training_centers_training_center_id",
                        column: x => x.training_center_id,
                        principalSchema: "quran_courses",
                        principalTable: "training_centers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_form_teachers",
                schema: "quran_courses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_form_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_form_teachers", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_form_teachers_course_forms_course_form_id",
                        column: x => x.course_form_id,
                        principalSchema: "quran_courses",
                        principalTable: "course_forms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_form_teachers_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                schema: "quran_courses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    course_form_id = table.Column<long>(type: "bigint", nullable: false),
                    teacher_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_courses_course_forms_course_form_id",
                        column: x => x.course_form_id,
                        principalSchema: "quran_courses",
                        principalTable: "course_forms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_courses_users_teacher_id",
                        column: x => x.teacher_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "petition_for_quran_course",
                schema: "quran_courses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_form_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    pinfl = table.Column<string>(type: "text", nullable: false),
                    passport = table.Column<string>(type: "text", nullable: false),
                    birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_petition_for_quran_course", x => x.id);
                    table.ForeignKey(
                        name: "FK_petition_for_quran_course_course_forms_course_form_id",
                        column: x => x.course_form_id,
                        principalSchema: "quran_courses",
                        principalTable: "course_forms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_petition_for_quran_course_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id");
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
                name: "IX_course_form_teachers_course_form_id",
                schema: "quran_courses",
                table: "course_form_teachers",
                column: "course_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_form_teachers_user_id",
                schema: "quran_courses",
                table: "course_form_teachers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_forms_training_center_id",
                schema: "quran_courses",
                table: "course_forms",
                column: "training_center_id");

            migrationBuilder.CreateIndex(
                name: "IX_courses_course_form_id",
                schema: "quran_courses",
                table: "courses",
                column: "course_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_courses_teacher_id",
                schema: "quran_courses",
                table: "courses",
                column: "teacher_id");

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
                name: "IX_mosque_prayer_times_mosque_id",
                schema: "prayerful",
                table: "mosque_prayer_times",
                column: "mosque_id");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_code",
                schema: "auth",
                table: "permissions",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_petition_for_quran_course_course_form_id",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                column: "course_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_petition_for_quran_course_user_id",
                schema: "quran_courses",
                table: "petition_for_quran_course",
                column: "user_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_structure_permissions_permission_id",
                schema: "auth",
                table: "structure_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_structure_permissions_structure_id",
                schema: "auth",
                table: "structure_permissions",
                column: "structure_id");

            migrationBuilder.CreateIndex(
                name: "IX_training_centers_district_id",
                schema: "quran_courses",
                table: "training_centers",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_sign_methods_user_id",
                schema: "auth",
                table: "user_sign_methods",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_structure_id",
                schema: "auth",
                table: "users",
                column: "structure_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calculating_centuries",
                schema: "reference_book");

            migrationBuilder.DropTable(
                name: "course_form_teachers",
                schema: "quran_courses");

            migrationBuilder.DropTable(
                name: "courses",
                schema: "quran_courses");

            migrationBuilder.DropTable(
                name: "mosque_prayer_times",
                schema: "prayerful");

            migrationBuilder.DropTable(
                name: "petition_for_quran_course",
                schema: "quran_courses");

            migrationBuilder.DropTable(
                name: "prayer_time_styles",
                schema: "reference_book");

            migrationBuilder.DropTable(
                name: "static_files",
                schema: "asset");

            migrationBuilder.DropTable(
                name: "structure_permissions",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "tokens",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "user_sign_methods",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "mosques",
                schema: "prayerful");

            migrationBuilder.DropTable(
                name: "course_forms",
                schema: "quran_courses");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "users",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "training_centers",
                schema: "quran_courses");

            migrationBuilder.DropTable(
                name: "structures",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "districts",
                schema: "reference_book");

            migrationBuilder.DropTable(
                name: "regions",
                schema: "reference_book");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "reference_book");
        }
    }
}
