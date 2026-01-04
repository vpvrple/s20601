using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class MovieUpdateRequestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MovieUpdateRequest",
                type: "varchar(2500)",
                unicode: false,
                maxLength: 2500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2500)",
                oldUnicode: false,
                oldMaxLength: 2500);

            migrationBuilder.AddColumn<int>(
                name: "NewEndYear",
                table: "MovieUpdateRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewOriginalTitle",
                table: "MovieUpdateRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NewRuntimeMinutes",
                table: "MovieUpdateRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NewStartYear",
                table: "MovieUpdateRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewTitle",
                table: "MovieUpdateRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewTitleType",
                table: "MovieUpdateRequest",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewEndYear",
                table: "MovieUpdateRequest");

            migrationBuilder.DropColumn(
                name: "NewOriginalTitle",
                table: "MovieUpdateRequest");

            migrationBuilder.DropColumn(
                name: "NewRuntimeMinutes",
                table: "MovieUpdateRequest");

            migrationBuilder.DropColumn(
                name: "NewStartYear",
                table: "MovieUpdateRequest");

            migrationBuilder.DropColumn(
                name: "NewTitle",
                table: "MovieUpdateRequest");

            migrationBuilder.DropColumn(
                name: "NewTitleType",
                table: "MovieUpdateRequest");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MovieUpdateRequest",
                type: "varchar(2500)",
                unicode: false,
                maxLength: 2500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(2500)",
                oldUnicode: false,
                oldMaxLength: 2500,
                oldNullable: true);
        }
    }
}
