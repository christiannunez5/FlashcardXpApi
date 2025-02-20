using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashcardXpApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameFlashcardSetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcard_FlashcardSet_FlashcardSetId",
                table: "Flashcard");

            migrationBuilder.DropTable(
                name: "FlashcardSet");

            migrationBuilder.RenameColumn(
                name: "FlashcardSetId",
                table: "Flashcard",
                newName: "StudySetId");

            migrationBuilder.RenameIndex(
                name: "IX_Flashcard_FlashcardSetId",
                table: "Flashcard",
                newName: "IX_Flashcard_StudySetId");

            migrationBuilder.CreateTable(
                name: "StudySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "getDate()"),
                    CreatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudySet_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySet_CreatedById",
                table: "StudySet",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcard_StudySet_StudySetId",
                table: "Flashcard",
                column: "StudySetId",
                principalTable: "StudySet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcard_StudySet_StudySetId",
                table: "Flashcard");

            migrationBuilder.DropTable(
                name: "StudySet");

            migrationBuilder.RenameColumn(
                name: "StudySetId",
                table: "Flashcard",
                newName: "FlashcardSetId");

            migrationBuilder.RenameIndex(
                name: "IX_Flashcard_StudySetId",
                table: "Flashcard",
                newName: "IX_Flashcard_FlashcardSetId");

            migrationBuilder.CreateTable(
                name: "FlashcardSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "getDate()"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashcardSet_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardSet_CreatedById",
                table: "FlashcardSet",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcard_FlashcardSet_FlashcardSetId",
                table: "Flashcard",
                column: "FlashcardSetId",
                principalTable: "FlashcardSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
