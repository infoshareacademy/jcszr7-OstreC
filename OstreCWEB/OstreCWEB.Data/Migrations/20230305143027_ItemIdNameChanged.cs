using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OstreCWEB.Repository.Migrations
{
    public partial class ItemIdNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Items",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Items",
                newName: "ItemId");
        }
    }
}
