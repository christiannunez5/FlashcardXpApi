using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashcardXpApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByToStudySet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySetParticipant_AspNetUsers_UserId",
                table: "StudySetParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_StudySetParticipant_StudySet_StudySetId",
                table: "StudySetParticipant");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "StudySet",
                type: "nvarchar(450)",
                nullable: true);

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
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetParticipant_AspNetUsers_UserId",
                table: "StudySetParticipant",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetParticipant_StudySet_StudySetId",
                table: "StudySetParticipant",
                column: "StudySetId",
                principalTable: "StudySet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySet_AspNetUsers_CreatedById",
                table: "StudySet");

            migrationBuilder.DropForeignKey(
                name: "FK_StudySetParticipant_AspNetUsers_UserId",
                table: "StudySetParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_StudySetParticipant_StudySet_StudySetId",
                table: "StudySetParticipant");

            migrationBuilder.DropIndex(
                name: "IX_StudySet_CreatedById",
                table: "StudySet");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "StudySet");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetParticipant_AspNetUsers_UserId",
                table: "StudySetParticipant",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetParticipant_StudySet_StudySetId",
                table: "StudySetParticipant",
                column: "StudySetId",
                principalTable: "StudySet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
