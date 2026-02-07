using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class RemovePostGroupsAndGroupMembershipEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Comment_Comment",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "Comment_Post",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "Comment_User",
                table: "Comment");

            migrationBuilder.DropTable(
                name: "GroupMembership");

            migrationBuilder.DropTable(
                name: "GroupOwnership");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropPrimaryKey(
                name: "Comment_pk",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_IdComment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_IdPost",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_IdUser",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Comments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(2)",
                oldPrecision: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdUser",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(2)",
                oldPrecision: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "IdCommentNavigationId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdUserNavigationId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdCommentNavigationId",
                table: "Comments",
                column: "IdCommentNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdUserNavigationId",
                table: "Comments",
                column: "IdUserNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_IdUserNavigationId",
                table: "Comments",
                column: "IdUserNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_IdCommentNavigationId",
                table: "Comments",
                column: "IdCommentNavigationId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_IdUserNavigationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_IdCommentNavigationId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_IdCommentNavigationId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_IdUserNavigationId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IdCommentNavigationId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IdUserNavigationId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Comment",
                type: "datetime2(2)",
                precision: 2,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdUser",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Comment",
                type: "datetime2(2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comment",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "Comment_pk",
                table: "Comment",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(100)", maxLength: 100, nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Group_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembership",
                columns: table => new
                {
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdGroup = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GroupMembership_pk", x => new { x.IdUser, x.IdGroup });
                    table.ForeignKey(
                        name: "GroupMembership_Group",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "GroupMembership_User",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupOwnership",
                columns: table => new
                {
                    IdOwner = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdGroup = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GroupOwnership_pk", x => new { x.IdOwner, x.IdGroup });
                    table.ForeignKey(
                        name: "GroupOwnership_Group",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "GroupOwnership_User",
                        column: x => x.IdOwner,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGroup = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "varchar(5000)", unicode: false, maxLength: 5000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false),
                    LastModifiedAt = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Post_pk", x => x.Id);
                    table.ForeignKey(
                        name: "Post_Group",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Post_User",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IdComment",
                table: "Comment",
                column: "IdComment");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IdPost",
                table: "Comment",
                column: "IdPost");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IdUser",
                table: "Comment",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembership_IdGroup",
                table: "GroupMembership",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOwnership_IdGroup",
                table: "GroupOwnership",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IdGroup",
                table: "Post",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IdUser",
                table: "Post",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "Comment_Comment",
                table: "Comment",
                column: "IdComment",
                principalTable: "Comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Comment_Post",
                table: "Comment",
                column: "IdPost",
                principalTable: "Post",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Comment_User",
                table: "Comment",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
