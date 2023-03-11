using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OstreCWEB.Repository.Migrations
{
    public partial class Add_Id_in_ParagraphItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParagraphItems",
                table: "ParagraphItems");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ParagraphItems",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParagraphItems",
                table: "ParagraphItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ParagraphItems_ItemId",
                table: "ParagraphItems",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParagraphItems",
                table: "ParagraphItems");

            migrationBuilder.DropIndex(
                name: "IX_ParagraphItems_ItemId",
                table: "ParagraphItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ParagraphItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParagraphItems",
                table: "ParagraphItems",
                columns: new[] { "ItemId", "ParagraphId" });
        }
    }
}
