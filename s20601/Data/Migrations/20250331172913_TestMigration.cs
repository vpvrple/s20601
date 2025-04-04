using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "User");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2(2)",
                precision: 2,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "User",
                type: "datetime2(2)",
                precision: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileDescription",
                table: "User",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReputationPoints",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "User_pk",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ActivityType_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    BirthYear = table.Column<int>(type: "int", nullable: false),
                    DeathYear = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Crew_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Genre_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Group_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", unicode: false, maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    OriginalTitle = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    StartYear = table.Column<int>(type: "int", nullable: false),
                    EndYear = table.Column<int>(type: "int", nullable: true),
                    RuntimeMinutes = table.Column<int>(type: "int", nullable: false),
                    TitleType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Movie_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieCollection_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Status_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRelationship",
                columns: table => new
                {
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdRelatedUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserRelationship_pk", x => new { x.IdUser, x.IdRelatedUser });
                    table.ForeignKey(
                        name: "UserRelationship_User1",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "UserRelationship_User2",
                        column: x => x.IdRelatedUser,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocialActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdActivityType = table.Column<int>(type: "int", nullable: false),
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
                        principalTable: "User",
                        principalColumn: "Id");
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
                        principalTable: "User",
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
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "varchar(5000)", unicode: false, maxLength: 5000, nullable: false),
                    IdGroup = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false),
                    LastModifiedAt = table.Column<int>(type: "int", nullable: true)
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
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieCrew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdCrew = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CharacterName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Movie_Id = table.Column<int>(type: "int", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieCrew_pk", x => x.Id);
                    table.ForeignKey(
                        name: "MovieCrew_Crew",
                        column: x => x.IdCrew,
                        principalTable: "Crew",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MovieCrew_Movie",
                        column: x => x.Movie_Id,
                        principalTable: "Movie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Movie_Id = table.Column<int>(type: "int", unicode: false, maxLength: 10, nullable: false),
                    Genre_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieGenre_pk", x => x.Id);
                    table.ForeignKey(
                        name: "MovieGenre_Genre",
                        column: x => x.Genre_Id,
                        principalTable: "Genre",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MovieGenre_Movie",
                        column: x => x.Movie_Id,
                        principalTable: "Movie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieOfTheDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie_Id = table.Column<int>(type: "int", unicode: false, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieOfTheDay_pk", x => x.Id);
                    table.ForeignKey(
                        name: "MovieOfTheDay_Movie",
                        column: x => x.Movie_Id,
                        principalTable: "Movie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<byte>(type: "tinyint", nullable: false),
                    RatedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false),
                    Movie_Id = table.Column<int>(type: "int", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieRate_pk", x => x.Id);
                    table.ForeignKey(
                        name: "MovieRate_Movie",
                        column: x => x.Movie_Id,
                        principalTable: "Movie",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MovieRate_User",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieUpdateRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "varchar(2500)", unicode: false, maxLength: 2500, nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false),
                    Movie_Id = table.Column<int>(type: "int", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieUpdateRequest_pk", x => x.Id);
                    table.ForeignKey(
                        name: "MovieUpdateRequest_Movie",
                        column: x => x.Movie_Id,
                        principalTable: "Movie",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MovieUpdateRequest_User",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAuthor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "varchar(2500)", unicode: false, maxLength: 2500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: true),
                    Movie_Id = table.Column<int>(type: "int", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Review_pk", x => x.Id);
                    table.ForeignKey(
                        name: "Review_Movie",
                        column: x => x.Movie_Id,
                        principalTable: "Movie",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Review_User",
                        column: x => x.IdAuthor,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieCollectionMovie",
                columns: table => new
                {
                    IdMovieCollection = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false),
                    Movie_Id = table.Column<int>(type: "int", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieCollectionMovie_pk", x => x.IdMovieCollection);
                    table.ForeignKey(
                        name: "MovieCollectionMovie_Movie",
                        column: x => x.Movie_Id,
                        principalTable: "Movie",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MovieCollectionMovie_MovieCollection",
                        column: x => x.IdMovieCollection,
                        principalTable: "MovieCollection",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieCollectionUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdMovieCollection = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MovieCollectionUser_pk", x => x.Id);
                    table.ForeignKey(
                        name: "MovieCollectionUsers_MovieCollection",
                        column: x => x.IdMovieCollection,
                        principalTable: "MovieCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "MovieCollectionUsers_User",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdSender = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdRecipient = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "varchar(2500)", unicode: false, maxLength: 2500, nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    DeliverTime = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Message_pk", x => x.Id);
                    table.ForeignKey(
                        name: "Message_Recipient",
                        column: x => x.IdRecipient,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Message_Sender",
                        column: x => x.IdSender,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Message_Status",
                        column: x => x.IdStatus,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdPost = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdComment = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: true),
                    Content = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Comment_pk", x => x.Id);
                    table.ForeignKey(
                        name: "Comment_Comment",
                        column: x => x.IdComment,
                        principalTable: "Comment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Comment_Post",
                        column: x => x.IdPost,
                        principalTable: "Post",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Comment_User",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReviewRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Review_Id = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    RatedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ReviewRate_pk", x => x.Id);
                    table.ForeignKey(
                        name: "ReviewRate_Review",
                        column: x => x.Review_Id,
                        principalTable: "Review",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "ReviewRate_User",
                        column: x => x.IdUser,
                        principalTable: "User",
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
                name: "IX_Message_IdRecipient",
                table: "Message",
                column: "IdRecipient");

            migrationBuilder.CreateIndex(
                name: "IX_Message_IdSender",
                table: "Message",
                column: "IdSender");

            migrationBuilder.CreateIndex(
                name: "IX_Message_IdStatus",
                table: "Message",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollectionMovie_Movie_Id",
                table: "MovieCollectionMovie",
                column: "Movie_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollectionUser_IdMovieCollection",
                table: "MovieCollectionUser",
                column: "IdMovieCollection");

            migrationBuilder.CreateIndex(
                name: "MovieCollectionUser_ak_1",
                table: "MovieCollectionUser",
                columns: new[] { "IdUser", "IdMovieCollection" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieCrew_IdCrew",
                table: "MovieCrew",
                column: "IdCrew");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCrew_Movie_Id",
                table: "MovieCrew",
                column: "Movie_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_Genre_Id",
                table: "MovieGenre",
                column: "Genre_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_Movie_Id_Genre_Id",
                table: "MovieGenre",
                columns: new[] { "Movie_Id", "Genre_Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieOfTheDay_Movie_Id",
                table: "MovieOfTheDay",
                column: "Movie_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieRate_Movie_Id",
                table: "MovieRate",
                column: "Movie_Id");

            migrationBuilder.CreateIndex(
                name: "UniqueRating",
                table: "MovieRate",
                columns: new[] { "IdUser", "Movie_Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieUpdateRequest_IdUser",
                table: "MovieUpdateRequest",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_MovieUpdateRequest_Movie_Id",
                table: "MovieUpdateRequest",
                column: "Movie_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IdGroup",
                table: "Post",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IdUser",
                table: "Post",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Review_IdAuthor",
                table: "Review",
                column: "IdAuthor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_Movie_Id",
                table: "Review",
                column: "Movie_Id");

            migrationBuilder.CreateIndex(
                name: "UniqueReview",
                table: "Review",
                columns: new[] { "IdAuthor", "Movie_Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRate_IdUser",
                table: "ReviewRate",
                column: "IdUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRate_Review_Id",
                table: "ReviewRate",
                column: "Review_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SocialActivityLog_IdActivityType",
                table: "SocialActivityLog",
                column: "IdActivityType");

            migrationBuilder.CreateIndex(
                name: "IX_SocialActivityLog_IdUser",
                table: "SocialActivityLog",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelationship_IdRelatedUser",
                table: "UserRelationship",
                column: "IdRelatedUser");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_User_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_User_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_User_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_User_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_User_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_User_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_User_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_User_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "GroupMembership");

            migrationBuilder.DropTable(
                name: "GroupOwnership");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "MovieCollectionMovie");

            migrationBuilder.DropTable(
                name: "MovieCollectionUser");

            migrationBuilder.DropTable(
                name: "MovieCrew");

            migrationBuilder.DropTable(
                name: "MovieGenre");

            migrationBuilder.DropTable(
                name: "MovieOfTheDay");

            migrationBuilder.DropTable(
                name: "MovieRate");

            migrationBuilder.DropTable(
                name: "MovieUpdateRequest");

            migrationBuilder.DropTable(
                name: "ReviewRate");

            migrationBuilder.DropTable(
                name: "SocialActivityLog");

            migrationBuilder.DropTable(
                name: "UserRelationship");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "MovieCollection");

            migrationBuilder.DropTable(
                name: "Crew");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "User_pk",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProfileDescription",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ReputationPoints",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
