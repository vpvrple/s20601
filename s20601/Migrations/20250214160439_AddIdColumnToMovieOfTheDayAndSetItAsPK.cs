using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class AddIdColumnToMovieOfTheDayAndSetItAsPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "MovieOfTheDay_pk",
                table: "MovieOfTheDay");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieOfTheDay",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "MovieOfTheDay_pk",
                table: "MovieOfTheDay",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieOfTheDay_Movie_Id",
                table: "MovieOfTheDay",
                column: "Movie_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "MovieOfTheDay_pk",
                table: "MovieOfTheDay");

            migrationBuilder.DropIndex(
                name: "IX_MovieOfTheDay_Movie_Id",
                table: "MovieOfTheDay");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieOfTheDay");

            migrationBuilder.AddPrimaryKey(
                name: "MovieOfTheDay_pk",
                table: "MovieOfTheDay",
                column: "Movie_Id");
        }
    }
}
