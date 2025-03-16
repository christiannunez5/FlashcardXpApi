using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashcardXpApi.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeStudySet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySetParticipant_StudySet_StudySetId",
                table: "StudySetParticipant");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetParticipant_StudySet_StudySetId",
                table: "StudySetParticipant",
                column: "StudySetId",
                principalTable: "StudySet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySetParticipant_StudySet_StudySetId",
                table: "StudySetParticipant");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetParticipant_StudySet_StudySetId",
                table: "StudySetParticipant",
                column: "StudySetId",
                principalTable: "StudySet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
