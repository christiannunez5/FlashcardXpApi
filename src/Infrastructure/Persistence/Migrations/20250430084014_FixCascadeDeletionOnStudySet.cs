using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDeletionOnStudySet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudySets_StudySets_StudySetId",
                table: "GroupStudySets");

            migrationBuilder.DropForeignKey(
                name: "FK_RecentStudySets_AspNetUsers_UserId",
                table: "RecentStudySets");

            migrationBuilder.DropForeignKey(
                name: "FK_RecentStudySets_StudySets_StudySetId",
                table: "RecentStudySets");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UpdatedAt",
                table: "StudySets",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "getDate()");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "StudySets",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "getDate()");

            migrationBuilder.CreateTable(
                name: "StudySetParticipants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudySetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySetParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudySetParticipants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudySetParticipants_StudySets_StudySetId",
                        column: x => x.StudySetId,
                        principalTable: "StudySets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySetParticipants_StudySetId",
                table: "StudySetParticipants",
                column: "StudySetId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySetParticipants_UserId",
                table: "StudySetParticipants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudySets_StudySets_StudySetId",
                table: "GroupStudySets",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecentStudySets_AspNetUsers_UserId",
                table: "RecentStudySets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecentStudySets_StudySets_StudySetId",
                table: "RecentStudySets",
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

            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudySets_StudySets_StudySetId",
                table: "GroupStudySets");

            migrationBuilder.DropForeignKey(
                name: "FK_RecentStudySets_AspNetUsers_UserId",
                table: "RecentStudySets");

            migrationBuilder.DropForeignKey(
                name: "FK_RecentStudySets_StudySets_StudySetId",
                table: "RecentStudySets");

            migrationBuilder.DropTable(
                name: "StudySetParticipants");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UpdatedAt",
                table: "StudySets",
                type: "date",
                nullable: false,
                defaultValueSql: "getDate()",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "StudySets",
                type: "date",
                nullable: false,
                defaultValueSql: "getDate()",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_StudySets_StudySetId",
                table: "Flashcards",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudySets_StudySets_StudySetId",
                table: "GroupStudySets",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecentStudySets_AspNetUsers_UserId",
                table: "RecentStudySets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecentStudySets_StudySets_StudySetId",
                table: "RecentStudySets",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
