using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ChatDetailForeignKeyAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChatDetail_EntitySourceDocumentId",
                table: "ChatDetail",
                column: "EntitySourceDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatDetail_EntitySourceDocumentDetails_EntitySourceDocumentId",
                table: "ChatDetail",
                column: "EntitySourceDocumentId",
                principalTable: "EntitySourceDocumentDetails",
                principalColumn: "EntitySourceDocumentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatDetail_EntitySourceDocumentDetails_EntitySourceDocumentId",
                table: "ChatDetail");

            migrationBuilder.DropIndex(
                name: "IX_ChatDetail_EntitySourceDocumentId",
                table: "ChatDetail");
        }
    }
}
