using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceIsAcceptedWithStatusForMovieUpdateRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "MovieUpdateRequest");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MovieUpdateRequest",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MovieUpdateRequest");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "MovieUpdateRequest",
                type: "bit",
                nullable: true);
        }
    }
}
