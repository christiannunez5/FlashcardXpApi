using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteCascadeOnCompletedFlashcardsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedFlashcards_AspNetUsers_UserId",
                table: "CompletedFlashcards");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedFlashcards_Flashcards_FlashcardId",
                table: "CompletedFlashcards");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedFlashcards_AspNetUsers_UserId",
                table: "CompletedFlashcards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedFlashcards_Flashcards_FlashcardId",
                table: "CompletedFlashcards",
                column: "FlashcardId",
                principalTable: "Flashcards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedFlashcards_AspNetUsers_UserId",
                table: "CompletedFlashcards");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedFlashcards_Flashcards_FlashcardId",
                table: "CompletedFlashcards");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedFlashcards_AspNetUsers_UserId",
                table: "CompletedFlashcards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedFlashcards_Flashcards_FlashcardId",
                table: "CompletedFlashcards",
                column: "FlashcardId",
                principalTable: "Flashcards",
                principalColumn: "Id");
        }
    }
}
