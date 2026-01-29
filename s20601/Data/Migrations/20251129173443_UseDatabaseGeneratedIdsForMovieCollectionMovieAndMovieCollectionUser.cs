using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class UseDatabaseGeneratedIdsForMovieCollectionMovieAndMovieCollectionUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "MovieCollectionUser",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");
            //migrationBuilder.DropForeignKey(
            //   name: "MovieCollectionUsers_MovieCollection", // The constraint name from your error
            //   table: "MovieCollectionUser");

            //migrationBuilder.DropForeignKey(
            //    name: "MovieCollectionMovie_MovieCollection",
            //    table: "MovieCollectionMovie");

            //migrationBuilder.DropPrimaryKey(
            //    name: "MovieCollectionMovie_pk",
            //    table: "MovieCollectionMovie");

            //migrationBuilder.DropPrimaryKey(
            //    name: "MovieCollectionUser_pk",
            //    table: "MovieCollectionUser");

            //    migrationBuilder.DropIndex(
            //name: "IX_MovieCollectionUser_IdMovieCollection",
            //table: "MovieCollectionUser");

            //    migrationBuilder.DropIndex(
            //name: "MovieCollectionUser_ak_1",
            //table: "MovieCollectionUser");

            //    migrationBuilder.DropColumn(
            //        name: "IdMovieCollection",
            //        table: "MovieCollectionMovie")
            //        .Annotation("SqlServer:Identity", "1, 1");

            //    migrationBuilder.DropColumn(
            //        name: "IdMovieCollection",
            //        table: "MovieCollectionUser")
            //        .Annotation("SqlServer:Identity", "1, 1");

            //    migrationBuilder.AddColumn<int>(
            //        name: "IdMovieCollection",
            //        table: "MovieCollectionMovie",
            //        type: "int",
            //        nullable: false,
            //        defaultValue: 0)
            //        .Annotation("SqlServer:Identity", "1, 1");

            //    migrationBuilder.AddColumn<int>(
            //        name: "Id",
            //        table: "MovieCollectionUser",
            //        type: "int",
            //        nullable: false,
            //        defaultValue: 0)
            //        .Annotation("SqlServer:Identity", "1, 1");

            //    migrationBuilder.AddPrimaryKey(
            //        name: "MovieCollectionMovie_pk",
            //        table: "MovieCollectionMovie",
            //        column: "IdMovieCollection");

            //    migrationBuilder.AddPrimaryKey(
            //        name: "MovieCollectionUser_pk",
            //        table: "MovieCollectionUser",
            //        column: "Id");

            //    migrationBuilder.AddForeignKey(
            //        name: "MovieCollectionMovie_MovieCollection", // Matches the SQL constraint name
            //        table: "MovieCollectionMovie",
            //        column: "IdMovieCollection",
            //        principalTable: "MovieCollection",
            //        principalColumn: "Id",
            //        onDelete: ReferentialAction.Cascade); // Matches the ON DELETE CASCADE in SQL

            //    migrationBuilder.AddForeignKey(
            //        name: "MovieCollectionMovie_Movie", // Matches the SQL constraint name
            //        table: "MovieCollectionMovie",
            //        column: "Movie_Id",
            //        principalTable: "Movie",
            //        principalColumn: "Id",
            //        onDelete: ReferentialAction.Restrict); // The default if no ON DELETE is specified (like in your SQL)

            //    migrationBuilder.AddForeignKey(
            //        name: "MovieCollectionUsers_MovieCollection", // Matches the SQL constraint name
            //        table: "MovieCollectionUser",
            //        column: "IdMovieCollection",
            //        principalTable: "MovieCollection",
            //        principalColumn: "Id",
            //        onDelete: ReferentialAction.Cascade); // Matches the ON DELETE CASCADE in SQL

            //    migrationBuilder.AddForeignKey(
            //        name: "MovieCollectionUsers_User", // Matches the SQL constraint name
            //        table: "MovieCollectionUser",
            //        column: "IdUser",
            //        principalTable: "AspNetUsers",
            //        principalColumn: "Id",
            //        onDelete: ReferentialAction.Restrict); // Default if not specified (no ON DELETE in SQL)

            //    migrationBuilder.CreateIndex(
            //name: "IX_MovieCollectionUser_IdMovieCollection",
            //table: "MovieCollectionUser",
            //column: "IdMovieCollection");

            //    migrationBuilder.CreateIndex(
            //name: "MovieCollectionUser_ak_1",
            //table: "MovieCollectionUser",
            //columns: new[] { "IdUser", "IdMovieCollection" }, // ADJUST COLUMNS
            //unique: true);

            // *** 1. DROP ALL DEPENDENCIES (FKs, Indexes) BEFORE touching PKs or Columns ***

            // Drop ALL Foreign Keys (MUST be first)
            migrationBuilder.Sql(@"
    IF OBJECT_ID(N'[MovieCollectionUsers_MovieCollection]', N'F') IS NOT NULL
        ALTER TABLE [MovieCollectionUser] DROP CONSTRAINT [MovieCollectionUsers_MovieCollection];
");

            migrationBuilder.Sql(@"
    IF OBJECT_ID(N'[MovieCollectionMovie_MovieCollection]', N'F') IS NOT NULL
        ALTER TABLE [MovieCollectionMovie] DROP CONSTRAINT [MovieCollectionMovie_MovieCollection];
");

            // Drop ALL Indexes/Alternate Keys (Reported as remaining dependencies)
            migrationBuilder.DropIndex(
                name: "IX_MovieCollectionUser_IdMovieCollection",
                table: "MovieCollectionUser");

            migrationBuilder.DropIndex(
                name: "MovieCollectionUser_ak_1",
                table: "MovieCollectionUser");

            // *** 2. DROP PRIMARY KEYS ***
            // Dropping PKs is required before dropping columns that belong to the key
            migrationBuilder.Sql(@"
    IF OBJECT_ID(N'[MovieCollectionMovie_pk]', N'PK') IS NOT NULL
        ALTER TABLE [MovieCollectionMovie] DROP CONSTRAINT [MovieCollectionMovie_pk];
");

            migrationBuilder.Sql(@"
    IF OBJECT_ID(N'[MovieCollectionUser_pk]', N'PK') IS NOT NULL
        ALTER TABLE [MovieCollectionUser] DROP CONSTRAINT [MovieCollectionUser_pk];
");

            // *** 3. DROP COLUMNS (Original columns are removed) ***

            // Drop IdMovieCollection (from join table 1)
            migrationBuilder.DropColumn(
                name: "IdMovieCollection",
                table: "MovieCollectionMovie");

            // Drop IdMovieCollection (from join table 2)
            // NOTE: If this column was being used for a different purpose, you need to adjust its dropping/re-adding
            migrationBuilder.DropColumn(
                name: "IdMovieCollection",
                table: "MovieCollectionUser");

            // Drop Id from MovieCollectionUser (if you are converting it to a composite key without an ID column)
            // Based on your original code, this was NOT being dropped, but based on the previous error,
            // you likely need to drop it if you're re-adding it with IDENTITY
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieCollectionUser");

            // *** 4. ADD COLUMNS (New definitions are applied) ***

            // Re-add IdMovieCollection to MovieCollectionMovie (as non-IDENTITY, assuming it's a FK)
            migrationBuilder.AddColumn<int>(
                name: "IdMovieCollection",
                table: "MovieCollectionMovie",
                type: "int",
                nullable: false,
                defaultValue: 0); // NO Identity Annotation

            // Re-add IdMovieCollection to MovieCollectionUser (as non-IDENTITY, assuming it's a FK)
            migrationBuilder.AddColumn<int>(
                name: "IdMovieCollection",
                table: "MovieCollectionUser",
                type: "int",
                nullable: false,
                defaultValue: 0); // NO Identity Annotation

            // Re-add Id to MovieCollectionUser as IDENTITY (assuming this was the goal for this table's key)
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieCollectionUser",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // *** 5. RE-ADD PRIMARY KEYS ***

            // Re-add PK for MovieCollectionMovie (Assuming composite key)
            migrationBuilder.AddPrimaryKey(
                name: "MovieCollectionMovie_pk",
                table: "MovieCollectionMovie",
                columns: new[] { "IdMovieCollection", "Movie_Id" }); // Corrected to be composite key

            // Re-add PK for MovieCollectionUser (Assuming single Identity key)
            migrationBuilder.AddPrimaryKey(
                name: "MovieCollectionUser_pk",
                table: "MovieCollectionUser",
                column: "Id");

            // *** 6. RE-ADD INDEXES ***

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollectionUser_IdMovieCollection",
                table: "MovieCollectionUser",
                column: "IdMovieCollection");

            migrationBuilder.CreateIndex(
                name: "MovieCollectionUser_ak_1",
                table: "MovieCollectionUser",
                columns: new[] { "IdUser", "IdMovieCollection" },
                unique: true);

            // *** 7. RE-ADD FOREIGN KEYS (MUST be last) ***

            migrationBuilder.AddForeignKey(
                name: "MovieCollectionMovie_MovieCollection",
                table: "MovieCollectionMovie",
                column: "IdMovieCollection",
                principalTable: "MovieCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "MovieCollectionMovie_Movie",
                table: "MovieCollectionMovie",
                column: "Movie_Id",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "MovieCollectionUsers_MovieCollection",
                table: "MovieCollectionUser",
                column: "IdMovieCollection",
                principalTable: "MovieCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "MovieCollectionUsers_User",
                table: "MovieCollectionUser",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "MovieCollectionUser",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
