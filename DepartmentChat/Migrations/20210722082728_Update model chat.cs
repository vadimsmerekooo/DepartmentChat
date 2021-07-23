using Microsoft.EntityFrameworkCore.Migrations;

namespace DepartmentChat.Migrations
{
    public partial class Updatemodelchat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Group_ChatId",
                table: "Group");

            migrationBuilder.CreateIndex(
                name: "IX_Group_ChatId",
                table: "Group",
                column: "ChatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Group_ChatId",
                table: "Group");

            migrationBuilder.CreateIndex(
                name: "IX_Group_ChatId",
                table: "Group",
                column: "ChatId");
        }
    }
}
