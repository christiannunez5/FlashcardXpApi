using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOtherEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedFlashcard_AspNetUsers_UserId",
                table: "CompletedFlashcard");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedFlashcard_Flashcards_FlashcardId",
                table: "CompletedFlashcard");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExperience_AspNetUsers_UserId",
                table: "UserExperience");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuest_AspNetUsers_UserId",
                table: "UserQuest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuest_Quest_QuestId",
                table: "UserQuest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuest",
                table: "UserQuest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExperience",
                table: "UserExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quest",
                table: "Quest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedFlashcard",
                table: "CompletedFlashcard");

            migrationBuilder.RenameTable(
                name: "UserQuest",
                newName: "UserQuests");

            migrationBuilder.RenameTable(
                name: "UserExperience",
                newName: "UserExperiences");

            migrationBuilder.RenameTable(
                name: "Quest",
                newName: "Quests");

            migrationBuilder.RenameTable(
                name: "CompletedFlashcard",
                newName: "CompletedFlashcards");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuest_UserId",
                table: "UserQuests",
                newName: "IX_UserQuests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuest_QuestId",
                table: "UserQuests",
                newName: "IX_UserQuests_QuestId");

            migrationBuilder.RenameIndex(
                name: "IX_UserExperience_UserId",
                table: "UserExperiences",
                newName: "IX_UserExperiences_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedFlashcard_UserId",
                table: "CompletedFlashcards",
                newName: "IX_CompletedFlashcards_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuests",
                table: "UserQuests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExperiences",
                table: "UserExperiences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quests",
                table: "Quests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedFlashcards",
                table: "CompletedFlashcards",
                columns: new[] { "FlashcardId", "UserId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserExperiences_AspNetUsers_UserId",
                table: "UserExperiences",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuests_AspNetUsers_UserId",
                table: "UserQuests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuests_Quests_QuestId",
                table: "UserQuests",
                column: "QuestId",
                principalTable: "Quests",
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

            migrationBuilder.DropForeignKey(
                name: "FK_UserExperiences_AspNetUsers_UserId",
                table: "UserExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuests_AspNetUsers_UserId",
                table: "UserQuests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuests_Quests_QuestId",
                table: "UserQuests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuests",
                table: "UserQuests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExperiences",
                table: "UserExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quests",
                table: "Quests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedFlashcards",
                table: "CompletedFlashcards");

            migrationBuilder.RenameTable(
                name: "UserQuests",
                newName: "UserQuest");

            migrationBuilder.RenameTable(
                name: "UserExperiences",
                newName: "UserExperience");

            migrationBuilder.RenameTable(
                name: "Quests",
                newName: "Quest");

            migrationBuilder.RenameTable(
                name: "CompletedFlashcards",
                newName: "CompletedFlashcard");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuests_UserId",
                table: "UserQuest",
                newName: "IX_UserQuest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuests_QuestId",
                table: "UserQuest",
                newName: "IX_UserQuest_QuestId");

            migrationBuilder.RenameIndex(
                name: "IX_UserExperiences_UserId",
                table: "UserExperience",
                newName: "IX_UserExperience_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedFlashcards_UserId",
                table: "CompletedFlashcard",
                newName: "IX_CompletedFlashcard_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuest",
                table: "UserQuest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExperience",
                table: "UserExperience",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quest",
                table: "Quest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedFlashcard",
                table: "CompletedFlashcard",
                columns: new[] { "FlashcardId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedFlashcard_AspNetUsers_UserId",
                table: "CompletedFlashcard",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedFlashcard_Flashcards_FlashcardId",
                table: "CompletedFlashcard",
                column: "FlashcardId",
                principalTable: "Flashcards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExperience_AspNetUsers_UserId",
                table: "UserExperience",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuest_AspNetUsers_UserId",
                table: "UserQuest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuest_Quest_QuestId",
                table: "UserQuest",
                column: "QuestId",
                principalTable: "Quest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
