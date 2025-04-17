using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRecentStudySetCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecentStudySets",
                table: "RecentStudySets");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "RecentStudySets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecentStudySets",
                table: "RecentStudySets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RecentStudySets_UserId",
                table: "RecentStudySets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecentStudySets",
                table: "RecentStudySets");

            migrationBuilder.DropIndex(
                name: "IX_RecentStudySets_UserId",
                table: "RecentStudySets");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "RecentStudySets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecentStudySets",
                table: "RecentStudySets",
                columns: new[] { "UserId", "StudySetId" });
        }
    }
}
