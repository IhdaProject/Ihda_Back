using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class foreginKeyToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tokens_user_id",
                schema: "auth",
                table: "tokens",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tokens_users_user_id",
                schema: "auth",
                table: "tokens",
                column: "user_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tokens_users_user_id",
                schema: "auth",
                table: "tokens");

            migrationBuilder.DropIndex(
                name: "IX_tokens_user_id",
                schema: "auth",
                table: "tokens");
        }
    }
}
