using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OstreCWEB.Repository.Migrations
{
    public partial class UpdateUserPara : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RelatedCharacterClass",
                table: "UserParagraphs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelatedCharacterName",
                table: "UserParagraphs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelatedCharacterRace",
                table: "UserParagraphs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelatedStoryName",
                table: "UserParagraphs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedCharacterClass",
                table: "UserParagraphs");

            migrationBuilder.DropColumn(
                name: "RelatedCharacterName",
                table: "UserParagraphs");

            migrationBuilder.DropColumn(
                name: "RelatedCharacterRace",
                table: "UserParagraphs");

            migrationBuilder.DropColumn(
                name: "RelatedStoryName",
                table: "UserParagraphs");
        }
    }
}
