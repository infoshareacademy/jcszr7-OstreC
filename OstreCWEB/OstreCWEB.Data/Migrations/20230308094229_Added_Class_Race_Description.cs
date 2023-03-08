using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OstreCWEB.Repository.Migrations
{
    public partial class Added_Class_Race_Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RaceDescription",
                table: "PlayableCharacterRaces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClassDescription",
                table: "PlayableCharacterClasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RaceDescription",
                table: "PlayableCharacterRaces");

            migrationBuilder.DropColumn(
                name: "ClassDescription",
                table: "PlayableCharacterClasses");
        }
    }
}
