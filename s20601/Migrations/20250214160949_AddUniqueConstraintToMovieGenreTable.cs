using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToMovieGenreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieGenre_Movie_Id",
                table: "MovieGenre");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_Movie_Id_Genre_Id",
                table: "MovieGenre",
                columns: new[] { "Movie_Id", "Genre_Id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieGenre_Movie_Id_Genre_Id",
                table: "MovieGenre");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_Movie_Id",
                table: "MovieGenre",
                column: "Movie_Id");
        }
    }
}
