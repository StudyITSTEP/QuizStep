using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizStep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRefreshTokenOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RefreshTokens_RefreshTokenId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RefreshTokenId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<int>(
                name: "RefreshTokenId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RefreshTokenId",
                table: "AspNetUsers",
                column: "RefreshTokenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RefreshTokens_RefreshTokenId",
                table: "AspNetUsers",
                column: "RefreshTokenId",
                principalTable: "RefreshTokens",
                principalColumn: "Id");
        }
    }
}
