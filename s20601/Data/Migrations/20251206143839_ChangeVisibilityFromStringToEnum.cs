using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class ChangeVisibilityFromStringToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPublic",
                table: "MovieCollection");

            migrationBuilder.AddColumn<int>(
                name: "Visibility",
                table: "MovieCollection",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "MovieCollection");

            migrationBuilder.AddColumn<bool>(
                name: "isPublic",
                table: "MovieCollection",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
