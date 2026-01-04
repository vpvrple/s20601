using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMovieUpdateRequestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieUpdateRequestId",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_MovieUpdateRequestId",
                table: "Genre",
                column: "MovieUpdateRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_MovieUpdateRequest_MovieUpdateRequestId",
                table: "Genre",
                column: "MovieUpdateRequestId",
                principalTable: "MovieUpdateRequest",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_MovieUpdateRequest_MovieUpdateRequestId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_MovieUpdateRequestId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "MovieUpdateRequestId",
                table: "Genre");
        }
    }
}
