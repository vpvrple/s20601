using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class AdjustModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop Foreign Keys
            migrationBuilder.DropForeignKey(name: "ActivityLog_ActivityType", table: "SocialActivityLog");
            migrationBuilder.DropForeignKey(name: "MovieCrew_Crew", table: "MovieCrew");
            migrationBuilder.DropForeignKey(name: "MovieGenre_Genre", table: "MovieGenre");
            migrationBuilder.DropForeignKey(name: "Comment_Post", table: "Comment");
            migrationBuilder.DropForeignKey(name: "Comment_Comment", table: "Comment");
            migrationBuilder.DropForeignKey(name: "GroupMembership_Group", table: "GroupMembership");
            migrationBuilder.DropForeignKey(name: "Post_Group", table: "Post");
            migrationBuilder.DropForeignKey(name: "GroupOwnership_Group", table: "GroupOwnership");

            // 2. Drop Primary Keys and Columns
            // Message
            migrationBuilder.DropPrimaryKey(name: "Message_pk", table: "Message");
            migrationBuilder.DropColumn(name: "Id", table: "Message");

            // SocialActivityLog
            migrationBuilder.DropPrimaryKey(name: "SocialActivityLog_pk", table: "SocialActivityLog");
            migrationBuilder.DropColumn(name: "Id", table: "SocialActivityLog");

            // MovieUpdateRequest
            migrationBuilder.DropPrimaryKey(name: "MovieUpdateRequest_pk", table: "MovieUpdateRequest");
            migrationBuilder.DropColumn(name: "Id", table: "MovieUpdateRequest");

            // MovieGenre
            migrationBuilder.DropPrimaryKey(name: "MovieGenre_pk", table: "MovieGenre");
            migrationBuilder.DropColumn(name: "Id", table: "MovieGenre");

            // MovieCrew
            migrationBuilder.DropPrimaryKey(name: "MovieCrew_pk", table: "MovieCrew");
            migrationBuilder.DropColumn(name: "Id", table: "MovieCrew");

            // ActivityType
            migrationBuilder.DropPrimaryKey(name: "ActivityType_pk", table: "ActivityType");
            migrationBuilder.DropColumn(name: "Id", table: "ActivityType");

            // Crew
            migrationBuilder.DropPrimaryKey(name: "Crew_pk", table: "Crew");
            migrationBuilder.DropColumn(name: "Id", table: "Crew");

            // Genre
            migrationBuilder.DropPrimaryKey(name: "Genre_pk", table: "Genre");
            migrationBuilder.DropColumn(name: "Id", table: "Genre");

            // Post
            migrationBuilder.DropPrimaryKey(name: "Post_pk", table: "Post");
            migrationBuilder.DropColumn(name: "Id", table: "Post");

            // Comment
            migrationBuilder.DropPrimaryKey(name: "Comment_pk", table: "Comment");
            migrationBuilder.DropColumn(name: "Id", table: "Comment");

            // Group
            migrationBuilder.DropPrimaryKey(name: "Group_pk", table: "Group");
            migrationBuilder.DropColumn(name: "Id", table: "Group");

            // 3. Add Columns with Identity
            migrationBuilder.AddColumn<int>(name: "Id", table: "Message", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "SocialActivityLog", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "MovieUpdateRequest", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "MovieGenre", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "MovieCrew", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "ActivityType", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "Crew", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "Genre", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "Post", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "Comment", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(name: "Id", table: "Group", type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // 4. Add Primary Keys
            migrationBuilder.AddPrimaryKey(name: "Message_pk", table: "Message", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "SocialActivityLog_pk", table: "SocialActivityLog", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "MovieUpdateRequest_pk", table: "MovieUpdateRequest", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "MovieGenre_pk", table: "MovieGenre", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "MovieCrew_pk", table: "MovieCrew", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "ActivityType_pk", table: "ActivityType", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Crew_pk", table: "Crew", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Genre_pk", table: "Genre", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Post_pk", table: "Post", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Comment_pk", table: "Comment", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Group_pk", table: "Group", column: "Id");

            // 5. Add Foreign Keys
            migrationBuilder.AddForeignKey(
                name: "ActivityLog_ActivityType",
                table: "SocialActivityLog",
                column: "IdActivityType",
                principalTable: "ActivityType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MovieCrew_Crew",
                table: "MovieCrew",
                column: "IdCrew",
                principalTable: "Crew",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MovieGenre_Genre",
                table: "MovieGenre",
                column: "Genre_Id",
                principalTable: "Genre",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Comment_Post",
                table: "Comment",
                column: "IdPost",
                principalTable: "Post",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Comment_Comment",
                table: "Comment",
                column: "IdComment",
                principalTable: "Comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "GroupMembership_Group",
                table: "GroupMembership",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Post_Group",
                table: "Post",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "GroupOwnership_Group",
                table: "GroupOwnership",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse of Up

            // 1. Drop Foreign Keys
            migrationBuilder.DropForeignKey(name: "ActivityLog_ActivityType", table: "SocialActivityLog");
            migrationBuilder.DropForeignKey(name: "MovieCrew_Crew", table: "MovieCrew");
            migrationBuilder.DropForeignKey(name: "MovieGenre_Genre", table: "MovieGenre");
            migrationBuilder.DropForeignKey(name: "Comment_Post", table: "Comment");
            migrationBuilder.DropForeignKey(name: "Comment_Comment", table: "Comment");
            migrationBuilder.DropForeignKey(name: "GroupMembership_Group", table: "GroupMembership");
            migrationBuilder.DropForeignKey(name: "Post_Group", table: "Post");
            migrationBuilder.DropForeignKey(name: "GroupOwnership_Group", table: "GroupOwnership");

            // 2. Drop Primary Keys and Columns
            migrationBuilder.DropPrimaryKey(name: "Message_pk", table: "Message");
            migrationBuilder.DropColumn(name: "Id", table: "Message");

            migrationBuilder.DropPrimaryKey(name: "SocialActivityLog_pk", table: "SocialActivityLog");
            migrationBuilder.DropColumn(name: "Id", table: "SocialActivityLog");

            migrationBuilder.DropPrimaryKey(name: "MovieUpdateRequest_pk", table: "MovieUpdateRequest");
            migrationBuilder.DropColumn(name: "Id", table: "MovieUpdateRequest");

            migrationBuilder.DropPrimaryKey(name: "MovieGenre_pk", table: "MovieGenre");
            migrationBuilder.DropColumn(name: "Id", table: "MovieGenre");

            migrationBuilder.DropPrimaryKey(name: "MovieCrew_pk", table: "MovieCrew");
            migrationBuilder.DropColumn(name: "Id", table: "MovieCrew");

            migrationBuilder.DropPrimaryKey(name: "ActivityType_pk", table: "ActivityType");
            migrationBuilder.DropColumn(name: "Id", table: "ActivityType");

            migrationBuilder.DropPrimaryKey(name: "Crew_pk", table: "Crew");
            migrationBuilder.DropColumn(name: "Id", table: "Crew");

            migrationBuilder.DropPrimaryKey(name: "Genre_pk", table: "Genre");
            migrationBuilder.DropColumn(name: "Id", table: "Genre");

            migrationBuilder.DropPrimaryKey(name: "Post_pk", table: "Post");
            migrationBuilder.DropColumn(name: "Id", table: "Post");

            migrationBuilder.DropPrimaryKey(name: "Comment_pk", table: "Comment");
            migrationBuilder.DropColumn(name: "Id", table: "Comment");

            migrationBuilder.DropPrimaryKey(name: "Group_pk", table: "Group");
            migrationBuilder.DropColumn(name: "Id", table: "Group");

            // 3. Add Columns without Identity
            migrationBuilder.AddColumn<int>(name: "Id", table: "Message", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "SocialActivityLog", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "MovieUpdateRequest", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "MovieGenre", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "MovieCrew", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "ActivityType", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "Crew", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "Genre", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "Post", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "Comment", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>(name: "Id", table: "Group", type: "int", nullable: false);

            // 4. Add Primary Keys
            migrationBuilder.AddPrimaryKey(name: "Message_pk", table: "Message", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "SocialActivityLog_pk", table: "SocialActivityLog", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "MovieUpdateRequest_pk", table: "MovieUpdateRequest", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "MovieGenre_pk", table: "MovieGenre", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "MovieCrew_pk", table: "MovieCrew", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "ActivityType_pk", table: "ActivityType", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Crew_pk", table: "Crew", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Genre_pk", table: "Genre", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Post_pk", table: "Post", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Comment_pk", table: "Comment", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "Group_pk", table: "Group", column: "Id");

            // 5. Add Foreign Keys
            migrationBuilder.AddForeignKey(
                name: "ActivityLog_ActivityType",
                table: "SocialActivityLog",
                column: "IdActivityType",
                principalTable: "ActivityType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MovieCrew_Crew",
                table: "MovieCrew",
                column: "IdCrew",
                principalTable: "Crew",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "MovieGenre_Genre",
                table: "MovieGenre",
                column: "Genre_Id",
                principalTable: "Genre",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Comment_Post",
                table: "Comment",
                column: "IdPost",
                principalTable: "Post",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Comment_Comment",
                table: "Comment",
                column: "IdComment",
                principalTable: "Comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "GroupMembership_Group",
                table: "GroupMembership",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Post_Group",
                table: "Post",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "GroupOwnership_Group",
                table: "GroupOwnership",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "Id");
        }
    }
}
