using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehaviorOnRecentStudySet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecentStudySets_StudySets_StudySetId",
                table: "RecentStudySets");

            migrationBuilder.AddForeignKey(
                name: "FK_RecentStudySets_StudySets_StudySetId",
                table: "RecentStudySets",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecentStudySets_StudySets_StudySetId",
                table: "RecentStudySets");

            migrationBuilder.AddForeignKey(
                name: "FK_RecentStudySets_StudySets_StudySetId",
                table: "RecentStudySets",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
