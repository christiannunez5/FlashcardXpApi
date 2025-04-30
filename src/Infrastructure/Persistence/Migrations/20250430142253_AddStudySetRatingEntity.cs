using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStudySetRatingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards");

            migrationBuilder.CreateTable(
                name: "StudySetRatings",
                columns: table => new
                {
                    RatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudySetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySetRatings", x => new { x.StudySetId, x.RatedById });
                    table.ForeignKey(
                        name: "FK_StudySetRatings_AspNetUsers_RatedById",
                        column: x => x.RatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudySetRatings_StudySets_StudySetId",
                        column: x => x.StudySetId,
                        principalTable: "StudySets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySetRatings_RatedById",
                table: "StudySetRatings",
                column: "RatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards");

            migrationBuilder.DropTable(
                name: "StudySetRatings");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id");
        }
    }
}
