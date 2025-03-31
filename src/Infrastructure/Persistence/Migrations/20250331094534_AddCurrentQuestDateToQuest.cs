using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashcardXpApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentQuestDateToQuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "CurrentQuestDate",
                table: "UserQuests",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentQuestDate",
                table: "UserQuests");
        }
    }
}
