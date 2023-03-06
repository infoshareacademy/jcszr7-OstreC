using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OstreCWEB.Repository.Migrations
{
    public partial class RenamedIdFieldsForDomainModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Statuses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlayableRaceId",
                table: "PlayableCharacterRaces",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlayableClassId",
                table: "PlayableCharacterClasses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AbilityId",
                table: "CharacterActions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "Character",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Statuses",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PlayableCharacterRaces",
                newName: "PlayableRaceId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PlayableCharacterClasses",
                newName: "PlayableClassId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CharacterActions",
                newName: "AbilityId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Character",
                newName: "CharacterId");
        }
    }
}
