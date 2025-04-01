using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashcardXpApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteBehaviorRecentStuySet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecentStudySets_StudySet_StudySetId",
                table: "RecentStudySets");

            migrationBuilder.AddForeignKey(
                name: "FK_RecentStudySets_StudySet_StudySetId",
                table: "RecentStudySets",
                column: "StudySetId",
                principalTable: "StudySet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecentStudySets_StudySet_StudySetId",
                table: "RecentStudySets");

            migrationBuilder.AddForeignKey(
                name: "FK_RecentStudySets_StudySet_StudySetId",
                table: "RecentStudySets",
                column: "StudySetId",
                principalTable: "StudySet",
                principalColumn: "Id");
        }
    }
}
