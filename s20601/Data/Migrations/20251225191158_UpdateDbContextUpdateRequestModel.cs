using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContextUpdateRequestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "MovieUpdateRequestGenre",
                columns: table => new
                {
                    MovieUpdateRequestId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieUpdateRequestGenre_pk", x => new { x.MovieUpdateRequestId, x.GenreId });
                    table.ForeignKey(
                        name: "MovieUpdateRequestGenre_Genre",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MovieUpdateRequestGenre_MovieUpdateRequest",
                        column: x => x.MovieUpdateRequestId,
                        principalTable: "MovieUpdateRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieUpdateRequestGenre_GenreId",
                table: "MovieUpdateRequestGenre",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieUpdateRequestGenre");

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
    }
}
