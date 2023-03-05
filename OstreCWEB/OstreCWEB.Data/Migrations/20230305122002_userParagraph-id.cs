using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OstreCWEB.Repository.Migrations
{
    public partial class userParagraphid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserParagraphId",
                table: "UserParagraphs",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserParagraphs",
                newName: "UserParagraphId");
        }
    }
}
