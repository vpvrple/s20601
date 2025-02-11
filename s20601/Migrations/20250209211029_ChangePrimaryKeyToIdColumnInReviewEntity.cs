using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrimaryKeyToIdColumnInReviewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Review_IdAuthor",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_Movie_Id",
                table: "Review");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ReviewRate_Review",
                table: "ReviewRate");

            migrationBuilder.DropPrimaryKey(
                name: "Review_pk",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_IdAuthor",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Review");

            migrationBuilder.AddPrimaryKey(
                name: "Review_pk",
                table: "Review",
                column: "IdAuthor");

            migrationBuilder.AddForeignKey(
                name: "ReviewRate_Review",
                table: "ReviewRate",
                column: "Review_IdAuthor",
                principalTable: "Review",
                principalColumn: "IdAuthor");
        }
    }
}
