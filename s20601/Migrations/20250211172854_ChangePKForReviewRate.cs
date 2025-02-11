using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class ChangePKForReviewRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "ReviewRate",
                type: "int",
                nullable: false
                );

            migrationBuilder.AddForeignKey(
                name: "ReviewRate_IdUser",
                table: "ReviewRate",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id");





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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ReviewRate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "ReviewRate_pk",
                table: "ReviewRate",
                column: "IdUser");
        }
    }
}
