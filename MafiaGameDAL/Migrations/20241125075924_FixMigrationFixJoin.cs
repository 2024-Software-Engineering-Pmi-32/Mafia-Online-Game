using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MafiaGameDAL.Migrations
{
    /// <inheritdoc />
    public partial class FixMigrationFixJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_CreatorId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Sessions_SessionId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_CreatorId",
                table: "Sessions",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Sessions_SessionId",
                table: "Users",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_CreatorId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Sessions_SessionId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_CreatorId",
                table: "Sessions",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Sessions_SessionId",
                table: "Users",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }
    }
}
