using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s20601.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStatusTableAndAdjustTypesInMessageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Message_Status",
                table: "Message");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Message_IdStatus",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "DeliverTime",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "IdStatus",
                table: "Message",
                newName: "MessageStatus");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Message");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Message",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageStatus",
                table: "Message",
                newName: "IdStatus");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Message");

            migrationBuilder.AddColumn<int>(
                name: "Created",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliverTime",
                table: "Message",
                type: "datetime2(2)",
                precision: 2,
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Message_IdStatus",
                table: "Message",
                column: "IdStatus");

            migrationBuilder.AddForeignKey(
                name: "Message_Status",
                table: "Message",
                column: "IdStatus",
                principalTable: "Status",
                principalColumn: "Id");
        }
    }
}
