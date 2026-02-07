using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class UseDatabaseGeneratedIdsForMovieCollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "MovieCollection",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");


            migrationBuilder.DropForeignKey(
                name: "MovieCollectionUsers_MovieCollection", // The constraint name from your error
                table: "MovieCollectionUser");

            migrationBuilder.DropForeignKey(
                name: "MovieCollectionMovie_MovieCollection",
                table: "MovieCollectionMovie");

            migrationBuilder.DropPrimaryKey(
                name: "MovieCollection_pk",
                table: "MovieCollection");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieCollection")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieCollection",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "MovieCollection_pk",
                table: "MovieCollection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "MovieCollectionUsers_MovieCollection",
                table: "MovieCollectionUser",
                column: "IdMovieCollection",
                principalTable: "MovieCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "MovieCollectionMovie_MovieCollection",
                table: "MovieCollectionMovie",
                column: "IdMovieCollection",
                principalTable: "MovieCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "MovieCollection",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.DropForeignKey(
                name: "MovieCollectionUsers_MovieCollection",
                table: "MovieCollectionUser");

            migrationBuilder.DropForeignKey(
                name: "MovieCollectionMovie_MovieCollection",
                table: "MovieCollectionMovie");

            migrationBuilder.DropPrimaryKey(
                name: "MovieCollection_pk",
                table: "MovieCollection");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieCollection")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieCollection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "MovieCollection_pk",
                table: "MovieCollection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "MovieCollectionUsers_MovieCollection",
                table: "MovieCollectionUser",
                column: "IdMovieCollection",
                principalTable: "MovieCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "MovieCollectionMovie_MovieCollection",
                table: "MovieCollectionMovie",
                column: "IdMovieCollection",
                principalTable: "MovieCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // Use correct ReferentialAction
        }
    }
}
