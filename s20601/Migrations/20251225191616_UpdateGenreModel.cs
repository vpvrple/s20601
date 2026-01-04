using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGenreModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}