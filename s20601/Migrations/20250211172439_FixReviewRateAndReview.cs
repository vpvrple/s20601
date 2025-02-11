using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class FixReviewRateAndReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropPrimaryKey(
                name: "ReviewRate_pk",
                table: "ReviewRate");

            migrationBuilder.AddColumn<int>(
                name: "Review_Id",
                table: "ReviewRate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "ReviewRate_pk",
                table: "ReviewRate",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRate_Review_Id",
                table: "ReviewRate",
                column: "Review_Id");

            migrationBuilder.AddForeignKey(
                name: "ReviewRate_Review",
                table: "ReviewRate",
                column: "Review_Id",
                principalTable: "Review",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ReviewRate_Review",
                table: "ReviewRate");

            migrationBuilder.DropPrimaryKey(
                name: "ReviewRate_pk",
                table: "ReviewRate");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRate_Review_Id",
                table: "ReviewRate");

            migrationBuilder.DropColumn(
                name: "Review_Id",
                table: "ReviewRate");

            migrationBuilder.AddPrimaryKey(
                name: "ReviewRate_pk",
                table: "ReviewRate",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRate_IdUser",
                table: "ReviewRate",
                column: "IdUser",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "ReviewRate_Review",
                table: "ReviewRate",
                column: "Id",
                principalTable: "Review",
                principalColumn: "Id");
        }
    }
}
