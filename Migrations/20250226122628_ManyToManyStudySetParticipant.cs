using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashcardXpApi.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyStudySetParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySet_AspNetUsers_CreatedById",
                table: "StudySet");

            migrationBuilder.DropIndex(
                name: "IX_StudySet_CreatedById",
                table: "StudySet");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "StudySet");

            migrationBuilder.CreateTable(
                name: "StudySetParticipant",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudySetId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySetParticipant", x => new { x.StudySetId, x.UserId });
                    table.ForeignKey(
                        name: "FK_StudySetParticipant_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudySetParticipant_StudySet_StudySetId",
                        column: x => x.StudySetId,
                        principalTable: "StudySet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySetParticipant_UserId",
                table: "StudySetParticipant",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudySetParticipant");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "StudySet",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StudySet_CreatedById",
                table: "StudySet",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySet_AspNetUsers_CreatedById",
                table: "StudySet",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
