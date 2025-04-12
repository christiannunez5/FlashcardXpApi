using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipToStudySet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySetProgresses_Flashcards_FlashcardId",
                table: "StudySetProgresses");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetProgresses_Flashcards_FlashcardId",
                table: "StudySetProgresses",
                column: "FlashcardId",
                principalTable: "Flashcards",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySetProgresses_Flashcards_FlashcardId",
                table: "StudySetProgresses");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetProgresses_Flashcards_FlashcardId",
                table: "StudySetProgresses",
                column: "FlashcardId",
                principalTable: "Flashcards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
