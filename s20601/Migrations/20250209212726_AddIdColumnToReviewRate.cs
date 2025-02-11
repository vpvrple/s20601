using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class AddIdColumnToReviewRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "ReviewRate_pk",
                table: "ReviewRate");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReviewRate",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "ReviewRate_pk",
                table: "ReviewRate",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRate_IdUser",
                table: "ReviewRate",
                column: "IdUser",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "ReviewRate_pk",
                table: "ReviewRate");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRate_IdUser",
                table: "ReviewRate");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReviewRate");

            migrationBuilder.AddPrimaryKey(
                name: "ReviewRate_pk",
                table: "ReviewRate",
                column: "IdUser");
        }
    }
}
