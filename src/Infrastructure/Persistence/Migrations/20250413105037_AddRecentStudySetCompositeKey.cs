using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRecentStudySetCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecentStudySets",
                table: "RecentStudySets");

            migrationBuilder.DropIndex(
                name: "IX_RecentStudySets_UserId",
                table: "RecentStudySets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecentStudySets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecentStudySets",
                table: "RecentStudySets",
                columns: new[] { "UserId", "StudySetId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecentStudySets",
                table: "RecentStudySets");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "RecentStudySets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecentStudySets",
                table: "RecentStudySets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RecentStudySets_UserId",
                table: "RecentStudySets",
                column: "UserId");
        }
    }
}
