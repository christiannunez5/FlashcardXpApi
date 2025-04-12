using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRecentStudySetPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "RecentStudySets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudySetId",
                table: "Flashcards",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecentStudySets");

            migrationBuilder.AlterColumn<string>(
                name: "StudySetId",
                table: "Flashcards",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
