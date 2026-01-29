using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieUpdateRequestCrewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieUpdateRequestCrew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieUpdateRequestId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    BirthYear = table.Column<int>(type: "int", nullable: false),
                    DeathYear = table.Column<int>(type: "int", nullable: true),
                    Job = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CharacterName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CrewId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieUpdateRequestCrew_pk", x => x.Id);
                    table.ForeignKey(
                        name: "MovieUpdateRequestCrew_MovieUpdateRequest",
                        column: x => x.MovieUpdateRequestId,
                        principalTable: "MovieUpdateRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieUpdateRequestCrew_MovieUpdateRequestId",
                table: "MovieUpdateRequestCrew",
                column: "MovieUpdateRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieUpdateRequestCrew");
        }
    }
}
