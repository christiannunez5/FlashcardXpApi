using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStudySetTagsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySetRecords_AspNetUsers_StudiedById",
                table: "StudySetRecords");
            
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudySetTags",
                columns: table => new
                {
                    StudySetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySetTags", x => new { x.StudySetId, x.TagId });
                    table.ForeignKey(
                        name: "FK_StudySetTags_StudySets_StudySetId",
                        column: x => x.StudySetId,
                        principalTable: "StudySets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudySetTags_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySetTags_TagId",
                table: "StudySetTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetRecords_AspNetUsers_StudiedById",
                table: "StudySetRecords",
                column: "StudiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySetRecords_AspNetUsers_StudiedById",
                table: "StudySetRecords");

            migrationBuilder.DropTable(
                name: "StudySetTags");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySetRecords_AspNetUsers_StudiedById",
                table: "StudySetRecords",
                column: "StudiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
