using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "SocialActivityLog");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.RenameColumn(
                name: "avatar",
                table: "AspNetUsers",
                newName: "Avatar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "AspNetUsers",
                newName: "avatar");

            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ActivityType_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCommentNavigationId = table.Column<int>(type: "int", nullable: true),
                    IdUserNavigationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdComment = table.Column<int>(type: "int", nullable: true),
                    IdPost = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_IdUserNavigationId",
                        column: x => x.IdUserNavigationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Comments_IdCommentNavigationId",
                        column: x => x.IdCommentNavigationId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocialActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdActivityType = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SocialActivityLog_pk", x => x.Id);
                    table.ForeignKey(
                        name: "ActivityLog_ActivityType",
                        column: x => x.IdActivityType,
                        principalTable: "ActivityType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "ActivityLog_User",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdCommentNavigationId",
                table: "Comments",
                column: "IdCommentNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdUserNavigationId",
                table: "Comments",
                column: "IdUserNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialActivityLog_IdActivityType",
                table: "SocialActivityLog",
                column: "IdActivityType");

            migrationBuilder.CreateIndex(
                name: "IX_SocialActivityLog_IdUser",
                table: "SocialActivityLog",
                column: "IdUser");
        }
    }
}
